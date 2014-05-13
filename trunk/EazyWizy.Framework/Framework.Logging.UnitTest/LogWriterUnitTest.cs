using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EazyWizy.Framework;
using EazyWizy.Framework.Caching;
using EazyWizy.Framework.Configuration;
using EazyWizy.Framework.Logging;
using EazyWizy.Framework.Logging.NLog;

namespace EazyWizy.Framework.Logging.UnitTests
{
    [TestClass]
    public class LogWriterUnitTest
    {
        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteLogWithLevelEnabledThenDisabledCreatesSingleEntry()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("WriteLogWithLevelEnabledThenDisabledCreatesSingleEntry", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object, "WriteLogWithLevelEnabledThenDisabledCreatesSingleEntry");
            logWriter.WriteLog(LogSeverityType.Error, "WriteLogWithLevelEnabledThenDisabledShouldGenerateOneEntry", "Message text");

            string configXml = "<Config><LogLevel>Fatal</LogLevel></Config>";
          //  configManager.Setup(cm => cm.TryGetXML("EventLog", "WriteLogWithLevelEnabledThenDisabledCreatesSingleEntry", "1.0", out configXml)).Returns(true);

            const int MillisecondsPerSecond = 60000;
            const int SleepTimeMs = 250;
            const int ExtraSecondsToWait = 15;

            Assert.IsTrue(logWriter.IsDebugEnabled);

            // Wait for the timer to time out and reload the log level
            for (int x = 0; x < ((LogWriterBase.LogLevelTimeoutMins * MillisecondsPerSecond) / SleepTimeMs) + (4 * ExtraSecondsToWait); x++)
            {
                System.Windows.Forms.Application.DoEvents();
                Thread.Sleep(SleepTimeMs);
            }

            // Force immediate log level refresh
            //logWriter.RefreshActiveLogLevel();

            Assert.IsFalse(logWriter.IsDebugEnabled);

            logWriter.WriteLog(LogSeverityType.Error, "This should never be logged", "This should never be logged");

            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteLogWithMessage()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);
            logWriter.WriteLog(LogSeverityType.Info , "WriteLogWithMessage", "Message text");

            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteLogWithLogRecord()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            LogRecord r =
                new LogRecord(LogSeverityType.Error, "WriteLogWithLogRecord", "Message text")
                {
                    AdditionalInfo1 = "AddlInfo1",
                    AdditionalInfo2 = "AddlInfo2",
                    AlertCondition = "Condition",
                    ReferenceId = "RefID123"
                };

            logWriter.WriteLog(r);

            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteExceptionWithException()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            try
            {
                throw new ApplicationException("Oops");
            }
            catch (Exception ex)
            {
                logWriter.WriteException(ex);
            }

            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteExceptionWithExceptionAndTitle()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            try
            {
                throw new ApplicationException("Oops");
            }
            catch (Exception ex)
            {
                logWriter.WriteException(ex, "WriteExceptionWithExceptionAndTitle");
                logWriter.Flush();
            }
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteExceptionWithExceptionAndTitleAndReferenceId()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            try
            {
                throw new ApplicationException("Oops");
            }
            catch (Exception ex)
            {
                logWriter.WriteException(ex, "WriteExceptionWithExceptionAndTitleAndReferenceId", "ExcRefID123");
                logWriter.Flush();
            }
        }

        [TestMethod]
        [DeploymentItem("Sample.xml")]
        [TestCategory("LogWriter")]
        public void WriteXml()
        {
            string xml = File.ReadAllText("Sample.xml");

            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            logWriter.WriteXml("WriteXml", xml);
            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteSecurityActionWithActionName()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            logWriter.WriteSecurityActionLog("ForgotPassword_Success");
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteSecurityActionWithActionNameAndDescription()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            logWriter.WriteSecurityActionLog("ForgotPassword_Success", "Action from unit test");
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteSecurityActionWithActionNameAndDescriptionAndUserId()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            logWriter.WriteSecurityActionLog("ForgotPassword_Success", "Action from unit test", "UnitTestUser");
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteSecurityActionWithActionNameAndDescriptionAndUserIdAndTargetAccount()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            logWriter.WriteSecurityActionLog("ForgotPassword_Success", "Action from unit test", "UnitTestUser", "ImpersonatedUser");
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteLogWithTwoLogWriters()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);

            LogRecord r =
                new LogRecord(LogSeverityType.Error, "WriteLogWithTwoLogWriters", "Message text")
                {
                    AdditionalInfo1 = "AddlInfo1",
                    AdditionalInfo2 = "AddlInfo2",
                    AlertCondition = "Condition",
                    ReferenceId = "RefID123"
                };

            logWriter.WriteLog(r);

            NLogLogWriter logWriter2 = new NLogLogWriter(configManager.Object, "UnitTest2");

            r.ReferenceId = "RefID345";

            logWriter.WriteLog(r);

            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteLogWithMultipleThreads()
        {
            int count = 5;

            List<Thread> threads = new List<Thread>();

            for (int x = 0; x <= count; x++)
            {
                threads.Add(new Thread(WriteThread));
            }

            for (int x = 0; x < count; x++)
            {
                threads[x].Start();
            }

            for (int x = 0; x < count; x++)
            {
                Assert.IsTrue(threads[x].Join(1000000), "Thread timed out");
            }

            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object, "WriteThread");

            logWriter.Flush();
        }

        [TestMethod]
        [TestCategory("LogWriter")]
        public void WriteLogWithPrimaryTargetFailure()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug", false);

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object);
            logWriter.WriteLog(LogSeverityType.Error, "WriteLogWithPrimaryTargetFailure", "Message text");
            logWriter.Flush();

            logWriter.WriteLog(LogSeverityType.Error, "WriteLogWithPrimaryTargetFailure", "Message text 2nd try");
            logWriter.Flush();
        }

        private static void WriteThread()
        {
            Mock<IConfigManager> configManager = GetMockConfigManager("LoggingUnitTests", "Debug");

            NLogLogWriter logWriter = new NLogLogWriter(configManager.Object, "WriteThread");

            string threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            string title = "WriteLogWithMultipleThreads (" + threadId + ")";

            for (int x = 0; x < 150; x++)
            {
                LogRecord r =
                    new LogRecord(LogSeverityType.Error, title + ":" + x.ToString(), "Message text")
                    {
                        AdditionalInfo1 = "AddlInfo1",
                        AdditionalInfo2 = "AddlInfo2",
                        AlertCondition = "Condition",
                        ReferenceId = "RefID123"
                    };

                logWriter.WriteLog(r);
            }
        }

        private static Mock<IConfigManager> GetMockConfigManager(string applicationName, string activeLogLevel)
        {
            return GetMockConfigManager(applicationName, activeLogLevel, false);
        }

        private static Mock<IConfigManager> GetMockConfigManager(string applicationName, string activeLogLevel, bool useInvalidEventLogConnectionString)
        {
            Mock<IConfigManager> configManager = new Mock<IConfigManager>();

            string connectionString = "Data Source=GAURAV-PC;Initial Catalog=EventLog;Integrated Security=True";

            //if (!useInvalidEventLogConnectionString)
            //{
              connectionString = ConfigManager.Current.GetConnectionString("EventLog", "R4");
            //}

            //configManager.Setup(cm => cm.GetConnectionString("EventLog", "R4")).Returns(connectionString);

            //string configXml = "<Config><LogLevel>Debug</LogLevel></Config>".Replace("Debug", activeLogLevel);
            //configManager.Setup(cm => cm.TryGetXML("EventLog", applicationName, "1.0", out configXml)).Returns(true);

            return configManager;
        }


        [TestMethod]
        [TestCategory("LogWriter")]
        public void TEST()
        {
            LogWriter.Current.WriteLog(LogSeverityType.Info, "PerformSearch ResponseTime ", " (Seconds) : " + DateTime.Now.Second.ToString());
        }
    }
}
