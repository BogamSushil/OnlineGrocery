using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using EazyWizy.Framework.Configuration;

namespace EazyWizy.Framework.Logging.EnterpriseLibrary
{
	/// <summary>
	/// A <see cref="System.Diagnostics.TraceListener"/> that writes to a database.
	/// </summary>
	[ConfigurationElementType(typeof(CustomTraceListenerData))]
	public class DatabaseTraceListener : CustomTraceListener
	{
        private SqlDatabase database;
        //need to check internal methods for LogWriterConfiguration
        private LogWriterConfiguration _logWriterConfiguration = new LogWriterConfiguration(ConfigManager.Current);
        private string applicationName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTraceListener"/> class.
        /// </summary>
        public DatabaseTraceListener()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseTraceListener"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public DatabaseTraceListener(Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase db) : base()
        {
            this.database = db;
        }

		/// <summary>
		/// The Write method 
		/// </summary>
		/// <param name="message">The message to log</param>
		public override void Write(string message)
		{
            VerifyState();
            
            ExecuteWriteLogStoredProcedure(
                TraceEventType.Information, "Trace", DateTime.UtcNow, Environment.MachineName,
				AppDomain.CurrentDomain.FriendlyName, null, message, this.applicationName, null, null, string.Empty);
		}

		/// <summary>
		/// The WriteLine method.
		/// </summary>
		/// <param name="message">The message to log</param>
		public override void WriteLine(string message)
		{
			Write(message);
		}

		/// <summary>
		/// Delivers the trace data to the underlying database.
		/// </summary>
		/// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
		/// <param name="source">The name of the trace source that delivered the trace data.</param>
		/// <param name="eventType">The type of event.</param>
		/// <param name="id">The id of the event.</param>
		/// <param name="data">The data to trace.</param>
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    LogEntry logEntry = data as LogEntry;
                    if (ValidateParameters(logEntry))
                    {
                        ExecuteStoredProcedure(logEntry);
                    }
                }
                else if (data is string)
                {
                    Write(data as string);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
		}

		/// <summary>
		/// Validates that enough information exists to attempt executing the stored procedures
		/// </summary>
		/// <param name="logEntry">The LogEntry to validate.</param>
		/// <returns>A boolean indicating whether the parameters for the LogEntry configuration are valid.</returns>
		private bool ValidateParameters(LogEntry logEntry)
		{
			return (logEntry != null && !string.IsNullOrEmpty(logEntry.Message));
		}

		/// <summary>
		/// Executes the stored procedures
		/// </summary>
		/// <param name="logEntry">The LogEntry to store in the database</param>
		private void ExecuteStoredProcedure(LogEntry logEntry)
		{
            VerifyState();
            ExecuteWriteLogStoredProcedure(logEntry);
		}

		/// <summary>
		/// Executes the WriteLog stored procedure
		/// </summary>
		/// <param name="severity">The severity for this LogEntry.</param>
		/// <param name="title">The title for this LogEntry.</param>
		/// <param name="timeStamp">The timestamp for this LogEntry.</param>
		/// <param name="machineName">The machine name for this LogEntry.</param>
		/// <param name="appDomainName">The appDomainName for this LogEntry.</param>
		/// <param name="processName">The processName for this LogEntry.</param>
		/// <param name="message">The message for this LogEntry.</param>
        /// <param name="applicationName">The application name</param>
        /// <param name="processAccountName">Account name under which the process was executing</param>
        /// <param name="identityName">Account name under with which a user was authenticated</param>
        /// <param name="categoryName">The category for this LogEntry.</param>
        /// <returns>An integer for the LogEntry Id</returns>
		private void ExecuteWriteLogStoredProcedure(
            TraceEventType severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processName,
            string message, string applicationName, string processAccountName, string identityName, string categoryName)
		{
            LogData ld = new LogData(this.database);

            ld.InsertLog(
                severity,
                title,
                timeStamp,
                machineName,
                appDomainName,
                processName,
                message,
                applicationName,
                processAccountName,
                identityName,
                categoryName);
		}

		/// <summary>
		/// Executes the WriteLog stored procedure
		/// </summary>
		/// <param name="logEntry">The LogEntry to store in the database.</param>
		/// <returns>An integer for the LogEntry Id</returns>
		private void ExecuteWriteLogStoredProcedure(LogEntry logEntry)
		{
            string processAccountName = null;

            if (logEntry.ExtendedProperties.ContainsKey("ProcessAccountName"))
            {
                processAccountName = logEntry.ExtendedProperties["ProcessAccountName"].ToString();
            }

            string identityName = null;

            if (logEntry.ExtendedProperties.ContainsKey("IdentityName"))
            {
                identityName = logEntry.ExtendedProperties["IdentityName"].ToString();
            }

            string categoryName = null;

            if (logEntry.Categories != null && logEntry.Categories.Count > 0)
            {
                categoryName = logEntry.CategoriesStrings[0];
            }

            if (string.IsNullOrWhiteSpace(logEntry.MachineName))
            {
                logEntry.MachineName = Environment.MachineName;
            }

            this.ExecuteWriteLogStoredProcedure(
                logEntry.Severity, logEntry.Title, logEntry.TimeStamp,
                logEntry.MachineName, logEntry.AppDomainName, logEntry.ProcessName, 
                logEntry.Message, this.applicationName, processAccountName, identityName, categoryName);
		}

        private void VerifyState()
        {
            if (this.database == null)
            {
                this.database = new SqlDatabase(_logWriterConfiguration.GetEventLogConnectionString());
            }

            if (this.applicationName == null)
            {
                if (!base.Attributes.ContainsKey("ApplicationName"))
                {
                    throw new ConfigurationErrorsException("The Attributes collection must contain an ApplicationName entry.");
                }

                this.applicationName = base.Attributes["ApplicationName"];
            }
        }
    }
}
