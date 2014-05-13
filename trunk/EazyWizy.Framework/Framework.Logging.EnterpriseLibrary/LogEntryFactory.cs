using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation;
using System.Diagnostics;

namespace EazyWizy.Framework.Logging.EnterpriseLibrary
{
    /// <summary>
    /// Creates pre-configured instances of the LogEntry class that contain additional information that is logged
    /// to the EventLog database.
    /// </summary>
    [Serializable]
    public class LogEntryFactory
    {
        private static LogEntryFactory _instance = new LogEntryFactory();

        /// <summary>
        /// Returns an instance of the factory object
        /// </summary>
        public static LogEntryFactory Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Creates a preconfigured instance of a LogEntry.
        /// </summary>
        /// <param name="severity">Severity of the log entry.</param>
        /// <param name="message">Error message</param>
        /// <returns>Configured LogEntry instance</returns>
        public LogEntry CreateInstance(TraceEventType severity, string message)
        {
            return CreateInstance(severity, message, null);
        }

        /// <summary>
        /// Creates a preconfigured instance of a LogEntry.
        /// </summary>
        /// <param name="severity">Severity of the log entry.</param>
        /// <param name="message">Error message</param>
        /// <param name="title">Title of the log entry</param>
        /// <returns>Configured LogEntry instance</returns>
        public LogEntry CreateInstance(TraceEventType severity, string message, string title)
        {
            return CreateInstance(severity, message, title, true);
        }

        /// <summary>
        /// Creates a preconfigured instance of a LogEntry.
        /// </summary>
        /// <param name="severity">Severity of the log entry.</param>
        /// <param name="message">Error message</param>
        /// <param name="title">Title of the log entry</param>
        /// <param name="includeSecurityDetail">if set to <c>true</c> security information is included in the log.</param>
        /// <returns>Configured LogEntry instance</returns>
        public LogEntry CreateInstance(
            TraceEventType severity, string message, string title, bool includeSecurityDetail)
        {
            LogEntry le = new LogEntry();

            if (string.IsNullOrEmpty(title))
            {
                le.Title = "Untitled";
            }
            else
            {
                le.Title = title;
            }

            le.Message = message;
            le.Severity = severity;

            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            if (includeSecurityDetail)
            {
                ManagedSecurityContextInformationProvider informationHelper = new ManagedSecurityContextInformationProvider();
                informationHelper.PopulateDictionary(dictionary);

                UnmanagedSecurityContextInformationProvider unmanagedHelper = new UnmanagedSecurityContextInformationProvider();
                unmanagedHelper.PopulateDictionary(dictionary);
            }

            le.ExtendedProperties = dictionary;

            return le;
        }
    }
}
