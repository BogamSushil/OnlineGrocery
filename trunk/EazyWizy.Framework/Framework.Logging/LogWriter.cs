using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EazyWizy.Framework.Logging
{
    public static class LogWriter
    {
        private static ILogWriter _logWriter = FrameworkContainer.Resolve<ILogWriter>();

        public static ILogWriter Current
        {
            get { return _logWriter; }
        }
    }
}
