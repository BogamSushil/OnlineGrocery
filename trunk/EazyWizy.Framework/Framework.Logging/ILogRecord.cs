using System;
using System.Runtime.InteropServices;

namespace EazyWizy.Framework.Logging
{
    /// <summary>
    /// Proxy interface registered with COM to allow ASP/VBScript/C++ code to call the logging facility.
    /// todo remove
    /// </summary>
    [ComVisible(true)]
    [Guid("0FB08F76-2153-46D9-A96D-B4927462B420")]
    public interface ILogRecord
    {
        string AlertCondition { get; set; }
        string Category { get; set; }
        string Message { get; }
        string ReferenceId { get; set; }
        LogSeverityType Severity { get; }
        string Title { get; }
    }
}
