using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace EazyWizy.Framework.Logging
{
    internal enum TraceEvent
    {
        Message = 0,
        BeginConfiguration = 1,
        BeginGlobalConfiguration = 2,
        EndGlobalConfiguration = 3,
        BeginInstanceConfiguration = 4,
        EndInstanceConfiguration = 5,
        EndConfiguration = 6,
        BeginRefreshActiveLogLevel = 7,
        EndRefreshActiveLogLevel = 8,
        BeginFlush = 9,
        EndFlush = 10,
        BeginLogLevelRefreshTimerElapsed = 11,
        EndLogLevelRefreshTimerElapsed = 12,
        BeginWriteLog = 13,
        EndWriteLog = 14,
        WriteLog = 15
    };

    internal static class TraceManager
    {
        private static TraceSource _traceSwitch = new TraceSource("EazyWizy.Framework.Logging", SourceLevels.Off);

        public static void TraceEvent(TraceEventType eventType, TraceEvent id)
        {
            _traceSwitch.TraceEvent(eventType, (int)id);
        }

        public static void TraceEvent(TraceEventType eventType, TraceEvent id, [Localizable(false)] string message)
        {
            _traceSwitch.TraceEvent(eventType, (int)id, message);
        }

        public static void TraceEvent(TraceEventType eventType, TraceEvent id, [Localizable(false)] string format, params object[] args)
        {
            _traceSwitch.TraceEvent(eventType, (int)id, format, args);
        }

        public static void TraceData(TraceEventType eventType, TraceEvent id, string data)
        {
            _traceSwitch.TraceData(eventType, (int)id, data);
        }

        public static bool ShouldTrace(TraceEventType eventType)
        {
            return _traceSwitch.Switch.ShouldTrace(eventType);
        }
    }
}
