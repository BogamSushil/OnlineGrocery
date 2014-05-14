using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EazyWizy.Framework.Instrumentation.ExceptionHandling
{
    public static class IncidentIDGenerator
    {
        public static string Next()
        {
            return Path.GetRandomFileName().Replace(".", String.Empty).Substring(0, 10).ToUpper();
        }
    }
}
