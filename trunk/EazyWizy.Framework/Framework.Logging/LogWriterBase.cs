using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using EazyWizy.Framework.Caching;
using System.Data.SqlClient;
using EazyWizy.Framework.Configuration;

namespace EazyWizy.Framework.Logging
{
    /// <summary>
    /// Base implementation for all log writers
    /// </summary>
    public abstract class LogWriterBase : ILogWriter, IDisposable
    {
        public const int LogLevelTimeoutMins = 1;

        protected string _applicationName;
        protected LogWriterConfiguration _logWriterConfiguration;
        protected System.Timers.Timer _refreshTimer;

        protected static readonly object _lockObj = new object();
        protected static volatile bool _isGlobalConfigured = false;

        public string Name { get { return _applicationName; } }

        public abstract bool IsDebugEnabled { get; }
        public abstract bool IsErrorEnabled { get; }
        public abstract bool IsFatalEnabled { get; }
        public abstract bool IsInfoEnabled { get; }
        public abstract bool IsTraceEnabled { get; }
        public abstract bool IsWarnEnabled { get; }

        public LogWriterBase(IConfigManager configManager, string applicationName)
        {
            _logWriterConfiguration = new LogWriterConfiguration(configManager);
            _applicationName = applicationName;

            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            _refreshTimer = new System.Timers.Timer(LogLevelTimeoutMins * 60 * 1000);
            _refreshTimer.AutoReset = false;
            _refreshTimer.Elapsed += RefreshTimerElapsed;
        }

        public void WriteXml(string title, string xml)
        {
            WriteLogBase(new LogRecord(LogSeverityType.Debug, title, xml), "X");
        }

        public void WriteException(Exception ex)
        {
            WriteException(ex, null, null);
        }

        public void WriteException(Exception ex, string title)
        {
            WriteException(ex, title, null);
        }

        public void WriteException(Exception ex, string title, string referenceId)
        {
            const int MaxTitleLength = 512;

            if (ex == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(title))
            {
                title = ex.Message.Substring(0, Math.Min(ex.Message.Length, MaxTitleLength));
            }
            else if (title.Length > MaxTitleLength)
            {
                title = title.Substring(0, Math.Min(title.Length, MaxTitleLength));
            }

            string message = string.Empty;

            if (ex is SqlException)
            {
                message = GetSqlErrors(ex as SqlException);
            }

            WriteLogBase(new LogRecord(LogSeverityType.Error, title, message) { ReferenceId = referenceId, Exception = ex }, "T");
        }

        public void WriteLog(LogRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }

            WriteLogBase(record, "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message)
        {
            WriteLogBase(new LogRecord(level, title, message), "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message, string referenceId)
        {
            WriteLogBase(new LogRecord(level, title, message) { ReferenceId = referenceId }, "T");
        }

        public void WriteLog(LogSeverityType level, string title, LogMessageGenerator messageFunc)
        {
            if (messageFunc == null)
            {
                throw new ArgumentNullException("messageFunc");
            }

            if (IsSeverityEnabledForLogging(level))
            {
                string message = messageFunc();
                WriteLogBase(new LogRecord(level, title, message), "T");
            }
        }

        public void WriteLog(LogSeverityType level, string title, LogMessageGenerator messageFunc, Exception exception)
        {
            if (messageFunc == null)
            {
                throw new ArgumentNullException("messageFunc");
            }

            if (IsSeverityEnabledForLogging(level))
            {
                string message = messageFunc();

                if (exception is SqlException)
                {
                    string sqlErrorsMessage = GetSqlErrors(exception as SqlException);
                    message += Environment.NewLine + sqlErrorsMessage;
                }

                WriteLogBase(new LogRecord(level, title, message) { Exception = exception }, "T");
            }
        }

        public void WriteLog(LogSeverityType level, string title, string message, string referenceId, params object[] args)
        {
            WriteLogBase(new LogRecord(level, title, message) { ReferenceId = referenceId }, "T", args);
        }

        public void WriteLog(LogSeverityType level, string title, string message, Exception exception)
        {
            if (exception is SqlException)
            {
                string sqlErrorsMessage = GetSqlErrors(exception as SqlException);
                message += Environment.NewLine + sqlErrorsMessage;
            }

            WriteLogBase(new LogRecord(level, title, message) { Exception = exception }, "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message, string referenceId, Exception exception)
        {
            if (exception is SqlException)
            {
                string sqlErrorsMessage = GetSqlErrors(exception as SqlException);
                message += Environment.NewLine + sqlErrorsMessage;
            }

            WriteLogBase(new LogRecord(level, title, message) { ReferenceId = referenceId, Exception = exception }, "T");
        }

        public void WriteSecurityActionLog(string actionName)
        {
            WriteSecurityActionLogInternal(actionName, null, null, null, GetUserIPAddress(), GetServerIPAddress());
        }

        public void WriteSecurityActionLog(string actionName, string description)
        {
            WriteSecurityActionLogInternal(actionName, description, null, null, GetUserIPAddress(), GetServerIPAddress());
        }

        public void WriteSecurityActionLog(string actionName, string description, string userId)
        {
            WriteSecurityActionLogInternal(actionName, description, userId, userId, GetUserIPAddress(), GetServerIPAddress());
        }

        public void WriteSecurityActionLog(string actionName, string description, string userId, string targetAccount)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserName();
                targetAccount = userId;
            }

            WriteSecurityActionLogInternal(actionName, description, userId, targetAccount, GetUserIPAddress(), GetServerIPAddress());
        }

        public void Configure(string applicationName)
        {
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginConfiguration, "Starting configuration... App Name: {0}", applicationName);

            if (!_isGlobalConfigured)
            {
                lock (_lockObj)
                {
                    if (!_isGlobalConfigured)
                    {
                        TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginGlobalConfiguration, "Starting global configuration...");
                        ConfigureLogWriterGlobal();
                        TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndGlobalConfiguration, "Ended global configuration.");

                        _isGlobalConfigured = true;
                    }
                }
            }

            lock (_lockObj)
            {
                TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginInstanceConfiguration, "Starting instance configuration... App Name: {0}", applicationName);
                ConfigureLogWriterInstance(applicationName);
                TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndInstanceConfiguration, "Ended instance configuration. App Name: {0}", applicationName);

                _refreshTimer.Start();
            }

            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndConfiguration, "Ended configuration. App Name: {0}", applicationName);
        }

        public void RefreshActiveLogLevel()
        {
            lock (_lockObj)
            {
                TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginRefreshActiveLogLevel, "App Name: {0}", _applicationName);
                ReloadActiveLogLevel();
                TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndRefreshActiveLogLevel, "App Name: {0}", _applicationName);
            }
        }

        public void Flush()
        {
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginFlush, "App Name: {0}", _applicationName);
            FlushInternal(false);
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndFlush, "App Name: {0}", _applicationName);
        }

        internal abstract void FlushInternal(bool shuttingDown);
        protected abstract bool IsSeverityEnabledForLogging(LogSeverityType severity);
        protected abstract void ConfigureLogWriterGlobal();
        protected abstract void ConfigureLogWriterInstance(string applicationName);
        protected abstract void WriteLogInternal(LogRecord record, string messageType, params object[] args);
        protected abstract void WriteSecurityActionLogInternal(
            string actionName, string description, string userId, string targetAccount, string userIpAddress, string serverIpAddress);
        protected abstract string ReloadActiveLogLevel();

        private void WriteLogBase(LogRecord record, string messageType, params object[] args)
        {
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginWriteLog, "Starting log write request... App Name: {0}", _applicationName);

            string logEnabled = null;
            if (TraceManager.ShouldTrace(TraceEventType.Verbose))
            {
                logEnabled = IsSeverityEnabledForLogging(record.Severity) ? "Yes" : "No";
            }
            
            TraceManager.TraceEvent(
                TraceEventType.Verbose, TraceEvent.WriteLog, "Proposed log: App Name: {0}; WillBeLogged: {1}; LogRecord = {2}", _applicationName, logEnabled, record.ToString());
            WriteLogInternal(record, messageType, args);

            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndWriteLog, "Ended log write request. App Name: {0}", _applicationName);
        }

        protected static string GetUserName()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null)
            {
                return System.Web.HttpContext.Current.User.Identity.Name;
            }
            else if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }

            return "Unknown";
        }

        private void RefreshTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TraceManager.TraceEvent(
                TraceEventType.Verbose, TraceEvent.BeginLogLevelRefreshTimerElapsed,
                "Timer elapsed - refreshing log level for application '{0}'...", _applicationName);

            string newLogLevel = null;

            lock (_lockObj)
            {
                newLogLevel = ReloadActiveLogLevel();
                _refreshTimer.Start();
            }

            TraceManager.TraceEvent(
                TraceEventType.Verbose, TraceEvent.EndLogLevelRefreshTimerElapsed,
                "Timer elapsed - refreshed log level for application '{0}' to '{1}'.", _applicationName, newLogLevel);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static string GetUserIPAddress()
        {
            if (System.Web.HttpContext.Current != null)
            {
                try
                {
                    string result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(result) || result.Equals("unknown", StringComparison.InvariantCulture))
                    {
                        return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    else
                    {
                        return result.Split(',')[0].Trim();
                    }
                }
                catch
                {
                    // Don't die due to an error here, just continue
                }
            }

            return "Unknown";
        }

        private static string GetServerIPAddress()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                foreach (System.Net.IPAddress ipAddress in host.AddressList)
                {
                    if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ipAddress.ToString();
                    }
                }
            }
            catch (ArgumentException)
            {
                // Don't die due to an error here, just continue
            }
            catch (System.Net.Sockets.SocketException)
            {
                // Don't die due to an error here, just continue
            }

            return "Unknown";
        }

        private string GetSqlErrors(SqlException sqlException)
        {
            string msg = string.Empty;

            foreach (SqlError err in sqlException.Errors)
            {
                msg += err.ToString() + ";" + Environment.NewLine;
            }

            return msg;
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.BeginFlush, "App Name: {0}", _applicationName);
            FlushInternal(true);
            TraceManager.TraceEvent(TraceEventType.Verbose, TraceEvent.EndFlush, "App Name: {0}", _applicationName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_refreshTimer != null)
                {
                    _refreshTimer.Elapsed -= RefreshTimerElapsed;
                    _refreshTimer.Dispose();
                    _refreshTimer = null;
                }
            }
        }
    }
}
