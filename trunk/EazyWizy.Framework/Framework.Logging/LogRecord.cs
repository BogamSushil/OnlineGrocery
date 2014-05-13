using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EazyWizy.Framework.Logging
{
    /// <summary>
    /// LogRecord represents a single log entry to be written to the log store.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("EazyWizy.Framework.Logging.LogRecord.1.6")]
    [Serializable]
    public class LogRecord : ILogRecord
    {
        private const string DefaultCategory = null;

        /// <summary>
        /// Severity of this log record
        /// </summary>
        public LogSeverityType Severity { get; private set; }

        /// <summary>
        /// Brief title for this log record
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Message associated with this log record (text or XML)
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Optional category for this log record
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Optional reference ID for this log record
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Optional notification alert condition for this log record
        /// </summary>
        public string AlertCondition { get; set; }

        /// <summary>
        /// Optional Exception associated with this log record
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Optional application-defined additional info for this log record
        /// </summary>
        public string AdditionalInfo1 { get; set; }

        /// <summary>
        /// Optional application-defined additional info for this log record
        /// </summary>
        public string AdditionalInfo2 { get; set; }

        public LogRecord(LogSeverityType severity, string title, string message)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("title is required", "title");
            }

            if (message == null)
            {
                message = string.Empty;
            }

            this.Severity = severity;
            this.Title = title;
            this.Message = message;
            this.Category = DefaultCategory;
        }

        public override string ToString()
        {
            return string.Format("Severity = {0};Title = '{1}'; Message = '{2}';", this.Severity, this.Title, this.Message);
        }
    }
}
