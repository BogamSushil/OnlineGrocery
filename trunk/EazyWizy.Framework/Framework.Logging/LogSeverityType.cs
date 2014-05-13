using System;
using System.Runtime.InteropServices;

namespace EazyWizy.Framework.Logging
{
    /// <summary>
    /// Severity of a log entry
    /// </summary>
    [ComVisible(true)]
    public enum LogSeverityType
    {
        /// <summary>
        /// Fatal log level.
        /// </summary>
        Fatal = 5,

        /// <summary>
        /// Error log level.
        /// </summary>
        Error = 4,

        /// <summary>
        /// Warning log level.
        /// </summary>
        Warn = 3,

        /// <summary>
        /// Informational log level.
        /// </summary>
        Info = 2,

        /// <summary>
        /// Debug log level.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Trace log level.
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Off log level.
        /// </summary>
        Off = 6
    }
}
