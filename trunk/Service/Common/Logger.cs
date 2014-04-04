using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    /// <summary>
    /// Helper class for logging. Current implementation is a wrapper around
    /// log4net library.
    /// </summary>
    /// <remarks>It offers logging at four levels. Also has overloads that accept
    /// category so that logged out can be catgorized (and conditionally turned 
    /// off).</remarks>
    public static class Logger
    {
        private static ILog mLogger;

        public static void Init(string configPath)
        {
            log4net.Config.XmlConfigurator.Configure(
                new System.IO.FileInfo(configPath));
            mLogger = LogManager.GetLogger("Common");
        }

        #region "Methods for logging"

        public static void Info(object message, bool isMobile = false)
        {
            if (isMobile)
            {
                mLogger.Info("Info Source: Mobile App");
            }
            mLogger.Info(message);
        }

        public static void Info(object message, Exception exception)
        {
            mLogger.Info(message, exception);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            mLogger.InfoFormat(format, args);
        }

        public static void Info(string category, object message, Exception exception)
        {
            ILog log = LogManager.GetLogger(category);
            log.Info(message, exception);
        }

        public static void Fatal(object message)
        {
            mLogger.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            mLogger.Fatal(message, exception);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            mLogger.FatalFormat(format, args);
        }

        public static void Fatal(string category, object message, Exception exception)
        {
            ILog log = LogManager.GetLogger(category);
            log.Fatal(message, exception);
        }

        public static void Error(object message, bool isMobile = false)
        {
            if (isMobile)
            {
                mLogger.Error("Error Source: Mobile App");
            }
            mLogger.Error(message);
        }


        public static void Error(object message, Exception exception)
        {
            mLogger.Error(message, exception);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            mLogger.ErrorFormat(format, args);
        }

        public static void Error(string category, object message, Exception exception)
        {
            ILog log = LogManager.GetLogger(category);
            log.Error(message, exception);
        }

        public static void Debug(object message)
        {
            mLogger.Debug(message);
        }

        public static void Debug(object message, Exception exception)
        {
            mLogger.Debug(message, exception);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            mLogger.DebugFormat(format, args);
        }

        public static void Debug(string category, object message, Exception exception)
        {
            ILog log = LogManager.GetLogger(category);
            log.Debug(message, exception);
        }

        #endregion

        #region "Methods for checking if logging is enabled"

        public static bool IsDebugEnabled
        {
            get
            {
                return mLogger.IsDebugEnabled;
            }
        }

        public static bool IsInfoEnabled
        {
            get
            {
                return mLogger.IsInfoEnabled;
            }
        }

        public static bool IsErrorEnabled
        {
            get
            {
                return mLogger.IsErrorEnabled;
            }
        }

        public static bool IsFatalEnabled
        {
            get
            {
                return mLogger.IsFatalEnabled;
            }
        }

        #endregion

    }
}
