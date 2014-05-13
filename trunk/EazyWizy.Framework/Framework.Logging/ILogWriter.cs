using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EazyWizy.Framework.Logging
{
    /// <summary>
    /// Returns a log message. Used to defer calculation of the log message until it's actually needed.
    /// </summary>
    /// <returns>Log message.</returns>
    public delegate string LogMessageGenerator();

    public interface ILogWriter
    {
        /// <summary>
        /// Gets the name of the log writer
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Debug level.
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Error level.
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Fatal level.
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Info level.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Trace level.
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether logging is enabled for the Warn level.
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Flushes pending log entries.
        /// </summary>
        void Flush();

        /// <summary>
        /// Writes the XML at the Debug level.
        /// </summary>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="xml">XML fragment.</param>
        void WriteXml(string title, string xml);

        /// <summary>
        /// Writes the exception at the Error level.
        /// </summary>
        /// <param name="ex">Exception object</param>
        void WriteException(Exception ex);

        /// <summary>
        /// Writes the exception at the Error level.
        /// </summary>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="ex">Exception object</param>
        void WriteException(Exception ex, string title);

        /// <summary>
        /// Writes the exception at the Error level.
        /// </summary>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="referenceId">Reference ID for the log entry</param>
        /// <param name="ex">Exception object</param>
        void WriteException(Exception ex, string title, string referenceId);

        /// <summary>
        /// Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="entry">The log entry.</param>
        void WriteLog(LogRecord record);

        /// <summary>
        /// Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="message">Log message.</param>
        void WriteLog(LogSeverityType level, string title, string message);

        /// <summary>
        /// Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="referenceId">Reference ID for the message</param>
        /// <param name="message">Log message.</param>
        void WriteLog(LogSeverityType level, string title, string message, string referenceId);

        /// <summary>
        /// Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
        void WriteLog(LogSeverityType level, string title, LogMessageGenerator messageFunc);

        /// <summary>
        /// Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="messageFunc">A function returning message to be written. Function is not evaluated if logging is not enabled.</param>
        void WriteLog(LogSeverityType level, string title, LogMessageGenerator messageFunc, Exception exception);

        /// <summary>
        /// Writes the diagnostic message at the specified level using the specified parameters.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="referenceId">Reference ID for the log entry</param>
        /// <param name="args">Arguments to format.</param>
        void WriteLog(LogSeverityType level, string title, string message, string referenceId, params object[] args);

        /// <summary>
        /// Writes the diagnostic message and exception at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        void WriteLog(LogSeverityType level, string title, string message, Exception exception);

        /// <summary>
        /// Writes the diagnostic message and exception at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="title">Short title for the log entry</param>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="referenceId">Reference ID for the log entry</param>
        /// <param name="exception">An exception to be logged.</param>
        void WriteLog(LogSeverityType level, string title, string message, string referenceId, Exception exception);

        /// <summary>
        /// Writes the security action.
        /// </summary>
        /// <param name="actionName">Name of security action</param>
        void WriteSecurityActionLog(string actionName);

        /// <summary>
        /// Writes the security action.
        /// </summary>
        /// <param name="actionName">Name of security action</param>
        /// <param name="description">Description of the security action</param>
        void WriteSecurityActionLog(string actionName, string description);

        /// <summary>
        /// Writes the security action.
        /// </summary>
        /// <param name="actionName">Name of security action</param>
        /// <param name="description">Description of the security action</param>
        /// <param name="userId">User ID of current user</param>
        void WriteSecurityActionLog(string actionName, string description, string userId);

        /// <summary>
        /// Writes the security action.
        /// </summary>
        /// <param name="actionName">Name of security action</param>
        /// <param name="description">Description of the security action</param>
        /// <param name="userId">User ID of current user</param>
        /// <param name="targetAccount">Impersonated user ID</param>
        void WriteSecurityActionLog(string actionName, string description, string userId, string targetAccount);
    }
}
