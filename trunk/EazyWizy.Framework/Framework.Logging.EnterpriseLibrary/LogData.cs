using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace EazyWizy.Framework.Logging.EnterpriseLibrary
{
    internal class LogData
    {
        private const string ConnectionStringName = "EventLog";
        private SqlDatabase db;

        internal LogData()
        {
            db = new SqlDatabase(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString);
        }

        internal LogData(SqlDatabase db)
        {
            this.db = db;
        }

        internal void InsertLog(
            TraceEventType severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processName,
            string message, string applicationName, string processAccountName, string identityName, string categoryName)
        {
            using (SqlCommand cmd = this.db.GetStoredProcCommand("InsertLog") as SqlCommand)
            {
                db.AddParameter(cmd, "severityId", DbType.Int16, 2, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, GetSeverityId(severity));
                db.AddParameter(cmd, "title", DbType.String, 256, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, title);
                db.AddInParameter(cmd, "timestamp", DbType.DateTime, timeStamp);
                db.AddParameter(cmd, "machineName", DbType.String, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, machineName);
                db.AddParameter(cmd, "AppDomainName", DbType.String, 256, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, appDomainName);
                db.AddParameter(cmd, "ProcessName", DbType.String, 512, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, processName);
                db.AddParameter(cmd, "message", DbType.String, ParameterDirection.Input, null, DataRowVersion.Default, message);
                db.AddParameter(cmd, "ApplicationName", DbType.String, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, applicationName);

                if (!string.IsNullOrEmpty(processAccountName))
                {
                    db.AddParameter(cmd, "ProcessAccountName", DbType.String, 35, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, processAccountName);
                }

                if (!string.IsNullOrEmpty(identityName))
                {
                    db.AddParameter(cmd, "IdentityName", DbType.String, 25, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, identityName);
                }

                db.AddParameter(cmd, "CategoryName", DbType.String, 64, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Default, categoryName);

                int rowCount = db.ExecuteNonQuery(cmd);
            }
        }

        internal short GetSeverityId(TraceEventType tet)
        {
            switch (tet)
            {
                case TraceEventType.Critical:
                    return (short)LogSeverityType.Fatal;
                case TraceEventType.Error:
                    return (short)LogSeverityType.Error;
                case TraceEventType.Information:
                    return (short)LogSeverityType.Info;
                case TraceEventType.Verbose:
                    return (short)LogSeverityType.Trace;
                case TraceEventType.Warning:
                    return (short)LogSeverityType.Warn;
                case TraceEventType.Resume:
                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Transfer:
                    return (short)LogSeverityType.Info;
                default:
                    return (short)LogSeverityType.Error;
            }
        }
    }
}
