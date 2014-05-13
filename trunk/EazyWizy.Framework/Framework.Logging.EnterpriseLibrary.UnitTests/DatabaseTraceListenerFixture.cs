using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EazyWizy.Framework.Configuration;

namespace EazyWizy.Framework.Logging.EnterpriseLibrary.UnitTests
{
    [TestClass]
    public class DatabaseTraceListenerFixture
    {
           public readonly string ConnectionString = ConfigManager.Current.GetConnectionString("EventLog", "R4");//"Data Source=GAURAV-PC;Initial Catalog=EventLog;Integrated Security=True";//string ConnectionString = ConfigManager.Current.GetConnectionString("EventLog", "R4");
        public const string WrongConnectionString = @"server=(local);database=Northwind;Integrated Security=true";

        [TestInitialize]
        public void SetUp()
        {
        }

        [TestCleanup]
        public void Teardown()
        {
        }

        [TestMethod]
        public void ShouldFilterLogToDatabase()
        {
            DatabaseTraceListener listener = new DatabaseTraceListener(new SqlDatabase(ConnectionString));
            listener.Attributes.Add("ApplicationName", "LoggingUnitTests");

            // Disable filtering
            listener.Filter = new EventTypeFilter(SourceLevels.Off);

            string message = DateTime.UtcNow.ToLongTimeString();

            listener.TraceData(new TraceEventCache(), "source", TraceEventType.Error, 0, new LogEntry(message, "cat1", 0, 0, TraceEventType.Error, "title", null));

            string result = GetLastLogMessage();
            Assert.AreNotEqual(message, result, true);
        }

        [TestMethod]
        public void LogToDatabaseApplyingFilter()
        {
            DatabaseTraceListener listener = new DatabaseTraceListener(new SqlDatabase(ConnectionString));
            listener.Attributes.Add("ApplicationName", "LoggingUnitTests");

            // Apply filter
            listener.Filter = new EventTypeFilter(SourceLevels.Information);

            listener.TraceData(new TraceEventCache(), "source", TraceEventType.Error, 0, new LogEntry("LogToDatabaseApplyingFilter", "cat1", 0, 0, TraceEventType.Verbose, "title", null));

            string result = GetLastLogMessage();
            Assert.AreNotEqual(0, result.Length);
            Assert.AreEqual("LogToDatabaseApplyingFilter", result);
        }

        [TestMethod]
        public void FormatterListenerWithStoredProcsAndDbInstance()
        {
            DatabaseTraceListener listener = new DatabaseTraceListener(new SqlDatabase(ConnectionString));
            listener.Attributes.Add("ApplicationName", "LoggingUnitTests");

            listener.TraceData(new TraceEventCache(), "source", TraceEventType.Error, 0, new LogEntry("FormatterListenerWithStoredProcsAndDbInstance", "cat1", 0, 0, TraceEventType.Error, "title", null));

            string messageContents = GetLastLogMessage();
            Assert.AreEqual("FormatterListenerWithStoredProcsAndDbInstance", messageContents);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void FormatterListenerWithStoredProcsAndWrongDbInstance()
        {
            DatabaseTraceListener listener = new DatabaseTraceListener(new SqlDatabase(WrongConnectionString));
            listener.Attributes.Add("ApplicationName", "LoggingUnitTests");

            listener.TraceData(new TraceEventCache(), "source", TraceEventType.Error, 0, new LogEntry("message", "cat1", 0, 0, TraceEventType.Error, "title", null));
        }

        [TestMethod]
        public void FormatterListenerWriteWithStoredProcsAndDbInstance()
        {
            DatabaseTraceListener listener = new DatabaseTraceListener(new SqlDatabase(ConnectionString));
            listener.Attributes.Add("ApplicationName", "LoggingUnitTests");

            listener.Write("FormatterListenerWriteWithStoredProcsAndDbInstance");

            string messageContents = GetLastLogMessage();
            Assert.AreEqual("FormatterListenerWriteWithStoredProcsAndDbInstance", messageContents, true);
        }

        [TestMethod]
        public void FormatterListenerAsString()
        {
            DatabaseTraceListener listener = new DatabaseTraceListener(new SqlDatabase(ConnectionString));
            listener.Attributes.Add("ApplicationName", "LoggingUnitTests");

            listener.TraceData(new TraceEventCache(), "source", TraceEventType.Error, 0, "FormatterListenerAsString");

            string messageContents = GetLastLogMessage();

            Assert.AreEqual("FormatterListenerAsString", messageContents, true);
        }

        private void ClearLogs()
        {
            //clear the log entries from the database
            SqlDatabase db = new SqlDatabase(ConnectionString);
            DbCommand command = db.GetStoredProcCommand("ClearLogs");
            db.ExecuteNonQuery(command);
        }

        private string GetLastLogMessage()
        {
            SqlDatabase db = new SqlDatabase(ConnectionString);
            DbCommand command = db.GetSqlStringCommand("SELECT TOP 1 Message FROM Log ORDER BY CreateDate DESC");
            string messageContents = Convert.ToString(db.ExecuteScalar(command));
            return messageContents;
        }

        public void test()
        { 
           
        }
    }
}
