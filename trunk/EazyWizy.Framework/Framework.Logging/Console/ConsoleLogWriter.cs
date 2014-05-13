using EazyWizy.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EazyWizy.Framework.Logging.Console
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Flush()
        {
        }

        public bool IsFatalEnabled
        {
            get { return true; }
        }

        public bool IsDebugEnabled
        {
            get { return true; }
        }

        public bool IsErrorEnabled
        {
            get { return true; }
        }

        public bool IsInfoEnabled
        {
            get { return true; }
        }

        public bool IsTraceEnabled
        {
            get { return true; }
        }

        public bool IsWarnEnabled
        {
            get { return true; }
        }

        public string Name
        {
            get { return "ConsoleLogWriter"; }
        }

        public void WriteException(Exception ex, string title, string referenceId)
        {
            WriteLogInternal(new LogRecord(LogSeverityType.Error, title, title) { ReferenceId = referenceId, Exception = ex }, "T");
        }

        public void WriteException(Exception ex, string title)
        {
            WriteException(ex, title, null);
        }

        public void WriteException(Exception ex)
        {
            WriteException(ex, null, null);
        }

        public void WriteLog(LogSeverityType level, string title, string message, string referenceId, Exception exception)
        {
            WriteLogInternal(new LogRecord(level, title, message) { ReferenceId = referenceId, Exception = exception }, "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message, Exception exception)
        {
            WriteLogInternal(new LogRecord(level, title, message) { Exception = exception }, "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message, string referenceId, params object[] args)
        {
            WriteLogInternal(new LogRecord(level, title, message) { ReferenceId = referenceId }, "T", args);
        }

        public void WriteLog(LogSeverityType level, string title, LogMessageGenerator messageFunc, Exception exception)
        {
            if (messageFunc == null)
            {
                throw new ArgumentNullException("messageFunc");
            }

            WriteLogInternal(new LogRecord(level, title, messageFunc()) { Exception = exception }, "T");
        }

        public void WriteLog(LogSeverityType level, string title, LogMessageGenerator messageFunc)
        {
            if (messageFunc == null)
            {
                throw new ArgumentNullException("messageFunc");
            }

            WriteLogInternal(new LogRecord(level, title, messageFunc()), "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message, string referenceId)
        {
            WriteLogInternal(new LogRecord(level, title, message) { ReferenceId = referenceId }, "T");
        }

        public void WriteLog(LogSeverityType level, string title, string message)
        {
            WriteLogInternal(new LogRecord(level, title, message), "T");
        }

        public void WriteLog(LogRecord record)
        {
            WriteLogInternal(record, "T");
        }

        private void WriteLogInternal(LogRecord record, string messageType, params object[] args)
        {
            System.Console.Write(record.Severity.ToString() + ": " + record.Message);

            if (record.Exception != null)
            {
                System.Console.WriteLine("; Exception: " + record.Exception.Message);
            }
            else
            {
                System.Console.WriteLine();
            }
        }

        public void WriteSecurityActionLog(string actionName, string description, string userId, string targetAccount)
        {
            System.Console.WriteLine(string.Format("Action: {0}; Description: {1}; UserID: {2}; TargetAccount: {3}", actionName, description, userId, targetAccount));
        }

        public void WriteSecurityActionLog(string actionName, string description, string userId)
        {
            WriteSecurityActionLog(actionName, description, userId, null);
        }

        public void WriteSecurityActionLog(string actionName, string description)
        {
            WriteSecurityActionLog(actionName, description, null);
        }

        public void WriteSecurityActionLog(string actionName)
        {
            WriteSecurityActionLog(actionName, null);
        }

        public void WriteXml(string title, string xml)
        {
            WriteLogInternal(new LogRecord(LogSeverityType.Debug, title, xml), "X");
        }
    }
}
