using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EazyWizy.Framework.Configuration;
using System.Data.Common;
using EazyWizy.Framework.Data;
using System.Globalization;
using System.Data.SqlClient;

namespace EazyWizy.Framework.Localization
{
    public class CustomFormats
    {
        #region Properties

        public List<CustomDateFormat> DateFormats { get; set; }
        public List<CustomTimeFormat> TimeFormats { get; set; }
        public List<CustomNumberFormat> NumberFormats { get; set; }
        public List<string> LanguageCodes { get; set; }
        public const string DEFAULT_LANGUAGE = "en-US";

        #endregion

        #region Singleton

        private static readonly CustomFormats instance = new CustomFormats();

        CustomFormats()
        {
        }

        public static CustomFormats Instance
        {
            get 
            {
                if (instance.DateFormats == null)
                {
                    LoadLists();
                }
                return instance; 
            }
        }

        #endregion

        #region Private Functions

        private static SqlConnection GetEazyWizyDatabase()
        {
           // return new SqlConnection(ConfigManager.Current.GetConnectionString("ProfileReadWrite", "R4"));
            return new SqlConnection( System.Configuration.ConfigurationManager.ConnectionStrings["EasyWizy"].ToString()); 
        }

        private static void LoadLists()
        {
            instance.DateFormats = new List<CustomDateFormat>();
            instance.TimeFormats = new List<CustomTimeFormat>();
            instance.NumberFormats = new List<CustomNumberFormat>();
            instance.LanguageCodes = new List<string>();

            using (SqlConnection database = GetEazyWizyDatabase())
            {
                database.Open();

                using (SqlCommand command = new SqlCommand("PT_SP_SelectDateFormats", database))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (EazyWizyDataReader reader = new EazyWizyDataReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            CustomDateFormat format = new CustomDateFormat();
                            format.Id = reader.GetInt32("UserDateFormatID");
                            format.DateFormat = reader.GetString("Format");
                            instance.DateFormats.Add(format);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand("PT_SP_SelectNumberFormats", database))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (EazyWizyDataReader reader = new EazyWizyDataReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            CustomNumberFormat format = new CustomNumberFormat();
                            format.Id = reader.GetInt32("UserNumberFormatID");
                            format.PositiveNumberFormat = reader.GetString("PositiveFormat");
                            format.NegativeNumberFormat = reader.GetString("NegativeFormat");
                            format.DecimalSeparator = reader.GetString("DecimalCharacter", null, false);
                            format.GroupSeparator = reader.GetString("GroupingCharacter", null, false);
                            instance.NumberFormats.Add(format);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand("PT_SP_SelectTimeFormats", database))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (EazyWizyDataReader reader = new EazyWizyDataReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            CustomTimeFormat format = new CustomTimeFormat();
                            format.Id = reader.GetInt32("UserTimeFormatID");
                            format.TimeFormat = reader.GetString("Format");
                            instance.TimeFormats.Add(format);
                        }
                    }
                }
                using (SqlCommand command = new SqlCommand("PT_SP_GetSponsorLanguages", database))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (EazyWizyDataReader reader = new EazyWizyDataReader(command.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            instance.LanguageCodes.Add(reader.GetString("LanguageCode"));
                        }
                    }
                }
            }
        }

        #endregion
        #region Public functions
        public CustomFormatExamples GetExamplesPerLanguage(List<string>languageCodes)
        {
            CustomFormatExamples examples = new CustomFormatExamples();
            foreach (string languageCode in languageCodes)
            {
                CustomFormatExampleLanguage exampleLanguage = new CustomFormatExampleLanguage();
                exampleLanguage.LanguageCode = languageCode;
                foreach (CustomDateFormat dateFormat in DateFormats)
                {
                    exampleLanguage.DateFormatExamples.Add(dateFormat.Id.ToString(),dateFormat.GetExample(languageCode));
                }
                foreach (CustomNumberFormat numberFormat in NumberFormats)
                {
                    exampleLanguage.NumberFormatExamples.Add(numberFormat.Id.ToString(), numberFormat.GetExample(languageCode));
                }
                foreach (CustomTimeFormat timeFormat in TimeFormats)
                {
                    exampleLanguage.TimeFormatExamples.Add(timeFormat.Id.ToString(), timeFormat.GetExample(languageCode));
                }
                examples.CustomFormatExampleLanguages.Add(exampleLanguage);
            }
            return examples;
        }

        public string GetAllLanguageCodesAsString()
        {
            return GetAllLanguageCodesAsString(false);
        }

        public string GetAllLanguageCodesAsString(bool includeDefaultLanguage)
        {
            List<string> allLanguages = LanguageCodes.ToList();
            if (!includeDefaultLanguage)
            {
                allLanguages.Remove(DEFAULT_LANGUAGE);
            }
            return string.Join(",", allLanguages.ToArray());
        }

        public string GetAllMajorLanguageCodesAsString()
        {
            return GetAllMajorLanguageCodesAsString(false);
        }

        public string GetAllMajorLanguageCodesAsString(bool includeDefaultLanguage)
        {
            List<string> allLanguages = LanguageCodes.ToList();
            if (!includeDefaultLanguage)
            {
                allLanguages.Remove(DEFAULT_LANGUAGE);
            }
            string[] majorCodes = new string[allLanguages.Count];
            for (int i = 0; i < allLanguages.Count; i++)
            {
                majorCodes[i] = allLanguages[i].Substring(0, 2);
            }
            return string.Join(",", majorCodes);
        }
        #endregion
    }

    [Serializable]
    public class CustomDateFormat
    {
        static readonly DateTime EXAMPLE = new DateTime(2045, 1, 23, 13, 23, 45);

        public string DisplayString
        {
            get
            {
                return string.Format("{0}", DateFormat);
            }
        }
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string DateFormat { get; set; }
        public string GetExample(string languageCode)
        {
            CultureInfo cultureInfo = new CultureInfo(languageCode);
            return EXAMPLE.ToString(DateFormat, cultureInfo);
        }
    }

    [Serializable]
    public class CustomTimeFormat
    {
        static readonly DateTime EXAMPLE = new DateTime(2045, 1, 23, 13, 23, 45);
        public string DisplayString
        {
            get
            {
                return string.Format("{0}", TimeFormat);
            }

        }
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string TimeFormat { get; set; }
        public string GetExample(string languageCode)
        {
            CultureInfo cultureInfo = new CultureInfo(languageCode);
            return EXAMPLE.ToString(TimeFormat, cultureInfo);
        }
    }

    [Serializable]
    public class CustomNumberFormat
    {
        static readonly decimal POSITIVE_EXAMPLE = 1234567.89M;
        static readonly decimal NEGATIVE_EXAMPLE = -1234567.89M;
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string DisplayString
        {
            get
            {
                return string.Format("{0} {1}", PositiveNumberFormat, NegativeNumberFormat);
            }

        }
        public string PositiveNumberFormat { get; set; }
        public string NegativeNumberFormat { get; set; }
        public string GroupSeparator { get; set; }
        public string DecimalSeparator { get; set; }
        public string GetExample(string languageCode)
        {
            CultureInfo cultureInfo = new CultureInfo(languageCode);
            cultureInfo.NumberFormat.NumberDecimalSeparator = DecimalSeparator;
            cultureInfo.NumberFormat.NumberGroupSeparator = GroupSeparator;
            return string.Format("{0} {1}", POSITIVE_EXAMPLE.ToString("N", cultureInfo), NEGATIVE_EXAMPLE.ToString("N", cultureInfo));
        }
    }
    [Serializable]
    public class CustomFormatExamples
    {
        public CustomFormatExamples()
        {
            CustomFormatExampleLanguages = new List<CustomFormatExampleLanguage>();
        }
        public List<CustomFormatExampleLanguage> CustomFormatExampleLanguages{get;set;}

    }
    [Serializable]
    public class CustomFormatExampleLanguage
    {
        public CustomFormatExampleLanguage()
        {
            DateFormatExamples = new Dictionary<string,string>();
            NumberFormatExamples = new Dictionary<string, string>();
            TimeFormatExamples = new Dictionary<string, string>();

        }
        public string LanguageCode{get;set;}
        public Dictionary<string,string> DateFormatExamples{get;set;}
        public Dictionary<string, string> NumberFormatExamples { get; set; }
        public Dictionary<string, string> TimeFormatExamples { get; set; }
    }
}
