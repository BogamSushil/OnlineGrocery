using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using EazyWizy.Framework.Caching;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using EazyWizy.Framework.Configuration;
using System.IO;

namespace EazyWizy.Framework.Logging.NLog
{
    /// <summary>
    /// LogWriter implementation built upon NLog
    /// </summary>
    public sealed class NLogLogWriter : LogWriterBase
    {
        // NLog returns its log level ordinal value as the hash code
        private readonly int MinLogLevel = LogLevel.Trace.GetHashCode();
        private readonly int MaxLogLevel = LogLevel.Fatal.GetHashCode();

        private Logger _logger;
        private Logger _securityActionLogger;

        public override bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public override bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public override bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        public override bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public override bool IsTraceEnabled
        {
            get { return _logger.IsTraceEnabled; }
        }

        public override bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public NLogLogWriter(IConfigManager configManager)
            : this(configManager, LogWriterConfiguration.GetDefaultApplicationName())
        {
        }

        public NLogLogWriter(IConfigManager configManager, string applicationName)
            : base(configManager, applicationName)
        {
            Configure(applicationName);

            _logger = LogManager.GetLogger(applicationName);
            _securityActionLogger = LogManager.GetLogger("SecurityAction");
        }

        protected override bool IsSeverityEnabledForLogging(LogSeverityType severity)
        {
            return _logger.IsEnabled(LogLevel.FromOrdinal((int)severity));
        }

        protected override void ConfigureLogWriterGlobal()
        {
            if (LogManager.Configuration == null)
            {
                LogManager.Configuration = new LoggingConfiguration();
            }

            ConfigureDefaultTarget();
            Target securityActionTarget = ConfigureSecurityActionTarget();

            LogManager.Configuration.LoggingRules.Clear();

            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.Message, "Adding security action logging rule...");
            TraceAllCurrentLogRulePatterns();

            LoggingRule securityActionRule = new LoggingRule("SecurityAction", LogLevel.Info, securityActionTarget);
            LogManager.Configuration.LoggingRules.Add(securityActionRule);

            LogManager.ReconfigExistingLoggers();

            TraceAllCurrentLogRulePatterns();
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.Message, "Added security action logging rule.");
        }

        protected override void ConfigureLogWriterInstance(string applicationName)
        {
            LoggingRule existingRuleForApplication = null;

            try
            {
                existingRuleForApplication = LogManager.Configuration.LoggingRules.SingleOrDefault(x => x.NameMatches(applicationName));
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Multiple entries found in the LoggingRules collection for the same LogWriter '" + applicationName + "'.", e);
            }

            if (existingRuleForApplication != null)
            {
                // If a default, unnamed log writer is created through the FrameworkContainer and the app.config
                // specifies an app name, then the "unnamed" default log writer will use the name from the
                // app.config. If you then create a named log writer using the same name specified in the app.config,
                // the FrameworkContainer creates a second log writer, when they really should be one and the same.
                // That happens because the first one was registered in the container with no name, while the second
                // was registered with a name. To work around that situation, we skip configuration here if we
                // find that a target already exists with the same name.
                return;
            }

            Target defaultTarget = LogManager.Configuration.FindTargetByName("FrameworkLoggingDefault");

            LogLevel activeLogLevel = LogLevel.Error;

            try
            {
                activeLogLevel = LogLevel.FromString(_logWriterConfiguration.GetActiveLogLevel(applicationName));
            }
            catch (Exception)
            {
                activeLogLevel = LogLevel.Error;
            }

            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.Message, "Adding new logging rule for App Name: {0}...", applicationName);
            TraceAllCurrentLogRulePatterns();

            LogManager.Configuration.LoggingRules.Add(new LoggingRule(applicationName, activeLogLevel, defaultTarget));
            LogManager.ReconfigExistingLoggers();

            TraceAllCurrentLogRulePatterns();
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.Message, "Added new logging rule for App Name: {0}.", applicationName);
        }

        protected override void WriteLogInternal(LogRecord record, string messageType, params object[] args)
        {
            try
            {
                LogEventInfo logEvent = LogEventInfo.Create(LogLevel.FromOrdinal((int)record.Severity), _logger.Name, record.Message);

                logEvent.Exception = record.Exception;
                logEvent.Properties.Add("EventDateTime", DateTime.UtcNow);
                logEvent.Properties.Add("AppDomainName", AppDomain.CurrentDomain.FriendlyName);
                logEvent.Properties.Add("LevelID", record.Severity.GetHashCode());
                logEvent.Properties.Add("ApplicationName", _logger.Name);
                logEvent.Properties.Add("Title", record.Title);
                logEvent.Properties.Add("Category", record.Category);
                logEvent.Properties.Add("ReferenceID", record.ReferenceId);
                logEvent.Properties.Add("Identity", GetUserName());
                logEvent.Properties.Add("MessageType", messageType);
                logEvent.Properties.Add("AlertCondition", record.AlertCondition);
                logEvent.Properties.Add("AdditionalInfo1", record.AdditionalInfo1);
                logEvent.Properties.Add("AdditionalInfo2", record.AdditionalInfo2);

                if (args != null && args.Length > 0)
                {
                    logEvent.Parameters = args;
                }

                InternalLogger.Debug("*** Error level is enabled: " + _logger.IsErrorEnabled);
                InternalLogger.Debug("*** Logging: " + (LogLevel.FromOrdinal((int)record.Severity).Name + " - " + record.Title));
                _logger.Log(logEvent);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Caught an exception in WriteLogInternal: " + ex.Message, "EazyWizy.Framework.Logging.NLog.NLogLogWriter");
                InternalLogger.Error("*** Caught an exception in WriteLogInternal " + ex.ToString());
            }
        }

        protected override void WriteSecurityActionLogInternal(string actionName, string description, string userId, string targetAccount, string userIpAddress, string serverIpAddress)
        {
            LogEventInfo logEvent = LogEventInfo.Create(LogLevel.Info, "SecurityAction", description);

            logEvent.Properties.Add("EventDateTime", DateTime.UtcNow);
            logEvent.Properties.Add("ApplicationName", _logger.Name);
            logEvent.Properties.Add("Action", actionName);
            logEvent.Properties.Add("TargetAccount", targetAccount);
            logEvent.Properties.Add("ActionBy", userId);
            logEvent.Properties.Add("ActionByIPAddress", userIpAddress);
            logEvent.Properties.Add("ServerIPAddress", serverIpAddress);

            _securityActionLogger.Log(logEvent);
        }

        protected override string ReloadActiveLogLevel()
        {
            LogLevel activeLogLevel = LogLevel.Error;

            try
            {
                activeLogLevel = LogLevel.FromString(_logWriterConfiguration.GetActiveLogLevel(_logger.Name));
            }
            catch (Exception)
            {
                activeLogLevel = LogLevel.Error;
            }

            TraceManager.TraceEvent(
                TraceEventType.Verbose, TraceEvent.Message,
                "LogManager reports that logging is currently {0} and configured with {1} logging rules.", LogManager.IsLoggingEnabled() ? "enabled" : "disabled", LogManager.Configuration.LoggingRules.Count);

            TraceAllCurrentLogRulePatterns();

            LoggingRule rule = LogManager.Configuration.LoggingRules.SingleOrDefault(x => x.NameMatches(_logger.Name));

            if (rule == null)
            {
                TraceManager.TraceEvent(
                    TraceEventType.Verbose, TraceEvent.Message,
                    "ERROR - No existing logging rule '{0}' found while refreshing log level for application '{1}'...", _logger.Name, _applicationName);
                Debug.Assert(false, "Could not find an existing LoggingRule for '" + _logger.Name + "'");
                return null;
            }

            SetLogLevelForRule(rule, activeLogLevel);

            LogManager.ReconfigExistingLoggers();

            return activeLogLevel.Name;
        }

        internal override void FlushInternal(bool shuttingDown)
        {
            if (!shuttingDown)
            {
                LogManager.Flush(60000);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void ConfigureDefaultTarget()
        {
            const int RetryCount = 2;
            const int RetryIntervalMs = 500;

            // Create database target
            DatabaseTarget eventLogDatabaseTarget = ConfigureDatabaseTarget();

            RetryingTargetWrapper retryTarget = new RetryingTargetWrapper(eventLogDatabaseTarget, RetryCount, RetryIntervalMs);

            // Create file target
            FileTarget fileTarget = ConfigureFileTarget();

            // Create Event Log target
            EventLogTarget applicationEventLogTarget = ConfigureEventLogTarget(base._applicationName);

            // Create fallback group target - database target first, then Event Log target
            FallbackGroupTarget fallbackTarget = new FallbackGroupTarget(retryTarget, fileTarget, applicationEventLogTarget);

            // With ReturnToFirstOnSuccess true, when the primary target fails and the secondary
            // succeeds, NLog will switch back to the primary for the next write, which is what we
            // want. With the default of false, if the primary fails once, NLog stays on the secondary
            // for the life of the AppDomain -- i.e. only Event Log and no more database.
            fallbackTarget.ReturnToFirstOnSuccess = true;

            AsyncTargetWrapper asyncFallbackTarget = new AsyncTargetWrapper(fallbackTarget);
            asyncFallbackTarget.BatchSize = 200;
            asyncFallbackTarget.QueueLimit = 5000;

            LogManager.Configuration.AddTarget("FrameworkLoggingDefault", asyncFallbackTarget);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private Target ConfigureSecurityActionTarget()
        {
            // Create database target
            DatabaseTarget eventLogDatabaseTarget = ConfigureSecurityActionDatabaseTarget();

            RetryingTargetWrapper retryTarget = new RetryingTargetWrapper(eventLogDatabaseTarget, 5, 1000);

            LogManager.Configuration.AddTarget("FrameworkLoggingSecurityAction", retryTarget);

            return retryTarget;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static EventLogTarget ConfigureEventLogTarget(string applicationName)
        {
            return new EventLogTarget()
            {
                Name = "ApplicationEventLogTarget",
                Log = "Application",
                Source = applicationName,
                Layout = "${literal:text=Date/Time -} ${longdate}${newline}${literal:text=Process -} ${processname}${newline}${literal:text=Windows Identity -} ${windows-identity}${newline}${literal:text=Identity -} ${identity}${newline}${literal:text=Severity -} ${level:uppercase=true}${newline}${literal:text=Logger -} ${logger}${newline}${literal:text=Message -} ${message}${newline}${literal:text=Stack Trace -}${newline}${stacktrace}"
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private DatabaseTarget ConfigureDatabaseTarget()
        {
            // Create database target
            DatabaseTarget eventLogDatabaseTarget = new DatabaseTarget();
            try
            {
                eventLogDatabaseTarget.Name = "EventLogDatabase";

                DatabaseParameterInfo param;

                string connectionString = _logWriterConfiguration.GetEventLogConnectionString();

                SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(connectionString);

                eventLogDatabaseTarget.DBProvider = "mssql";
                eventLogDatabaseTarget.DBHost = csBuilder.DataSource;
                if (!csBuilder.IntegratedSecurity)
                {
                    eventLogDatabaseTarget.DBUserName = csBuilder.UserID;
                    eventLogDatabaseTarget.DBPassword = csBuilder.Password;
                }
                eventLogDatabaseTarget.DBDatabase = csBuilder.InitialCatalog;

                eventLogDatabaseTarget.CommandText =
                    "exec [InsertLog] @SeverityID, @Title, @Timestamp, @MachineName, @AppDomainName, @ProcessName, @ApplicationName, @CategoryName, @ProcessAccountName, @IdentityName, @Message, @ReferenceID, @MessageType, @AdditionalInfo1, @AdditionalInfo2, @AlertCondition";

                param = new DatabaseParameterInfo();
                param.Name = "@SeverityID";
                param.Layout = "${event-context:item=LevelID}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@Title";
                param.Layout = "${event-context:item=Title}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@Timestamp";
                param.Layout = "${event-context:item=EventDateTime}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@MachineName";
                param.Layout = "${machinename}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@AppDomainName";
                param.Layout = "${event-context:item=AppDomainName}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@ProcessName";
                param.Layout = "${processname}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@ApplicationName";
                param.Layout = "${event-context:item=ApplicationName}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@CategoryName";
                param.Layout = "${event-context:item=Category}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@ProcessAccountName";
                param.Layout = "${windows-identity}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@IdentityName";
                param.Layout = "${event-context:item=Identity}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@Message";
                param.Layout = "${message:withException=true}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@ReferenceID";
                param.Layout = "${event-context:item=ReferenceID}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@MessageType";
                param.Layout = "${event-context:item=MessageType}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@AdditionalInfo1";
                param.Layout = "${event-context:item=AdditionalInfo1}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@AdditionalInfo2";
                param.Layout = "${event-context:item=AdditionalInfo2}";
                eventLogDatabaseTarget.Parameters.Add(param);

                param = new DatabaseParameterInfo();
                param.Name = "@AlertCondition";
                param.Layout = "${event-context:item=AlertCondition}";
                eventLogDatabaseTarget.Parameters.Add(param);
            }
            catch (Exception ex)
            {
                InternalLogger.Error("*** " + ex.ToString());
                throw;
            }

            return eventLogDatabaseTarget;
        }

        private FileTarget ConfigureFileTarget()
        {
            FileTarget fileTarget = new FileTarget();

            fileTarget.Name = "FallbackLogFileTarget";
            fileTarget.CreateDirs = true;
            fileTarget.ConcurrentWrites = true;
            fileTarget.AutoFlush = true;
            fileTarget.FileName =
                Path.Combine(
                    _logWriterConfiguration.GetFallbackLogFolderPath(),
                    base._applicationName,
                    base._applicationName + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            return fileTarget;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private DatabaseTarget ConfigureSecurityActionDatabaseTarget()
        {
            // Create database target
            DatabaseTarget eventLogDatabaseTarget = new DatabaseTarget();
            eventLogDatabaseTarget.Name = "SecurityActionEventLogDatabase";

            DatabaseParameterInfo param;

            string connectionString = _logWriterConfiguration.GetEventLogConnectionString();

            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(connectionString);

            eventLogDatabaseTarget.DBProvider = "mssql";
            eventLogDatabaseTarget.DBHost = csBuilder.DataSource;
            if (!csBuilder.IntegratedSecurity)
            {
                eventLogDatabaseTarget.DBUserName = csBuilder.UserID;
                eventLogDatabaseTarget.DBPassword = csBuilder.Password;
            }
            eventLogDatabaseTarget.DBDatabase = csBuilder.InitialCatalog;

            eventLogDatabaseTarget.CommandText = "exec [LogSecurityEvent] @action, @targetAccount, @actionBy, @actionByIPAddress, @serverIPAddress, @serverName, @application, @description";

            param = new DatabaseParameterInfo();
            param.Name = "@action";
            param.Layout = "${event-context:item=Action}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@targetAccount";
            param.Layout = "${event-context:item=TargetAccount}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@actionBy";
            param.Layout = "${event-context:item=ActionBy}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@actionByIPAddress";
            param.Layout = "${event-context:item=ActionByIPAddress}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@serverIPAddress";
            param.Layout = "${event-context:item=ServerIPAddress}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@serverName";
            param.Layout = "${machinename}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@application";
            param.Layout = "${event-context:item=ApplicationName}";
            eventLogDatabaseTarget.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@description";
            param.Layout = "${message}";
            eventLogDatabaseTarget.Parameters.Add(param);

            return eventLogDatabaseTarget;
        }

        private void SetLogLevelForRule(LoggingRule rule, LogLevel activeLogLevel)
        {
            // Reset all log levels to off
            for (int i = MinLogLevel; i <= MaxLogLevel; i++)
            {
                rule.DisableLoggingForLevel(LogLevel.FromOrdinal(i));
            }

            // Enable the active log level and all levels numerically higher
            for (int i = activeLogLevel.GetHashCode(); i <= MaxLogLevel; i++)
            {
                rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
            }

            InternalLogger.Debug("*** Setting active log level for " + _logger.Name + " to " + activeLogLevel.Name);
        }

        private static void TraceAllCurrentLogRulePatterns()
        {
            if (TraceManager.ShouldTrace(TraceEventType.Verbose))
            {
                string rulePatternNames = "";
                foreach (LoggingRule lr in LogManager.Configuration.LoggingRules)
                {
                    rulePatternNames += lr.LoggerNamePattern + ", ";
                }

                TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.Message, "Logging rule pattern names are: {0}", rulePatternNames);
            }
        }
    }
}
