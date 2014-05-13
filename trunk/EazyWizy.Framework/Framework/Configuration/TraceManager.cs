using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace EazyWizy.Framework.Configuration
{
    internal static class TraceManager
    {
        private static TraceSwitch _traceSwitch = new TraceSwitch("EazyWizy.Framework.Configuration.ConfigManager", "ConfigManager Component Trace");

        public static void TraceVerbose([Localizable(false)] string message)
        {
            if (_traceSwitch.TraceVerbose)
            {
                Trace.WriteLine(message, "ConfigManager [Verbose]");
            }
        }

        public static void TraceError([Localizable(false)] string message)
        {
            if (_traceSwitch.TraceError)
            {
                Trace.WriteLine(message, "ConfigManager [Error]");
            }
        }

        public static void TraceWarning([Localizable(false)] string message)
        {
            if (_traceSwitch.TraceWarning)
            {
                Trace.WriteLine(message, "ConfigManager [Warning]");
            }
        }

        public static void TraceInfo([Localizable(false)] string message)
        {
            if (_traceSwitch.TraceInfo)
            {
                Trace.WriteLine(message, "ConfigManager [Info]");
            }
        }
    }
}
