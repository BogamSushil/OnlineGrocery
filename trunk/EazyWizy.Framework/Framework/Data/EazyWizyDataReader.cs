using System;
using System.Text;
using System.Data;

namespace EazyWizy.Framework.Data
{
    public class EazyWizyDataReader : IDisposable
    {
        private IDataReader dataReader;

        public IDataReader DataReader
        {
            get { return dataReader; }
            set { dataReader = value; }
        }

        public static DateTime DefaultDate
        {
            get { return DateTime.MinValue; }
        }


        public EazyWizyDataReader(IDataReader pDataReader)
        {
            dataReader = pDataReader;
        }

        ~EazyWizyDataReader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dataReader != null)
                {
                    dataReader.Dispose();
                    dataReader = null;
                }
            }
        }

        public bool IsClosed
        {
            get { return dataReader.IsClosed; }
        }

        public int RecordsAffected
        {
            get { return dataReader.RecordsAffected; }
        }

        public void Close()
        {
            if (dataReader != null)
            {
                dataReader.Close();
                dataReader.Dispose();
            }
        }

        public bool Read()
        {
            return dataReader.Read();
        }

        public bool NextResult()
        {
            return dataReader.NextResult();
        }

        public bool IsDBNull(int columnIndex)
        {
            return dataReader.IsDBNull(columnIndex);
        }

        public int GetOrdinal(string name)
        {
            return dataReader.GetOrdinal(name);
        }

        public int GetInt32(string column)
        {
            return GetInt32(column, 0);
        }

        public int GetInt32(string column, int defaultNullValue)
        {
            int data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? defaultNullValue : int.Parse(dataReader[column].ToString());
            }
            catch { data = 0; }
            return data;
        }

        public int GetTinyInt(string column)
        {
            int data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? 0 : int.Parse(dataReader[column].ToString());
            }
            catch { data = 0; }
            return data;
        }

        public int GetSmallInt(string column)
        {
            int data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? 0 : int.Parse(dataReader[column].ToString());
            }
            catch { data = 0; }
            return data;
        }

        public float GetFloat(string column)
        {
            float data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? 0 : float.Parse(dataReader[column].ToString());
            }
            catch { data = 0; }
            return data;
        }

        public long GetLong(string column)
        {
            long data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? 0 : long.Parse(dataReader[column].ToString());
            }
            catch { data = 0; }
            return data;
        }

        public decimal GetDecimal(string column)
        {
            decimal data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? 0 : decimal.Parse(dataReader[column].ToString());
            }
            catch { data = 0; }
            return data;
        }

        public bool GetBoolean(string column)
        {
            bool data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? false : bool.Parse(dataReader[column].ToString());
            }
            catch { data = false; }
            return data;
        }

        public string GetString(string column)
        {
            return GetString(column, null);
        }

        public string GetString(string column, string defaultNullValue)
        {
            return GetString(column, defaultNullValue, true);
        }
        public string GetString(string column, string defaultNullValue, bool trim)
        {
            string data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? defaultNullValue : dataReader[column].ToString();
            }
            catch { data = null; }
            if ((!string.IsNullOrEmpty(data)) && trim)
            {
                data = data.Trim();
            }
            return data;
        }
        public string GetUnicodeString(string column)
        {
            string data;
            try
            {
                data = GetString(column);

                Encoding iso = Encoding.Unicode;
                Encoding unicode = Encoding.UTF8;
                byte[] isoBytes = iso.GetBytes(data);
                data = unicode.GetString(isoBytes);
            }
            catch { data = null; }
            return data;
        }

        public DateTime GetDateTime(string column)
        {
            DateTime data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? DefaultDate : DateTime.Parse(dataReader[column].ToString());
            }
            catch { data = DefaultDate; }
            return data;
        }

        public byte[] GetBytes(string column)
        {
            byte[] data;
            try
            {
                data = (dataReader.IsDBNull(dataReader.GetOrdinal(column)))
                    ? new byte[0] : (byte[])dataReader[column];
            }
            catch { data = new byte[0]; }
            return data;
        }
    }
}
