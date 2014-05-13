using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Text;


namespace EazyWizy.Framework.Localization
{
    public class DataUtility
    {
        #region Date Formatting

        public static string GetLocalizedDate(DateTime date)
        {
            return date.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
        }

        public static string GetLocalizedDateTime(DateTime date)
        {
            return date.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortTimePattern);
        }

        public static string GetLocalizedDate(string standardDate)
        {
            return GetLocalizedDateTime(standardDate, DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
        }

        public static string GetLocalizedDateTime(string standardDateTime)
        {
            string format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
            return GetLocalizedDateTime(standardDateTime, format);
        }

        public static string GetLocalizedDateTime(string standardDateTime, string dateTimeFormat)
        {
            DateTime dtLocal = DateTime.Parse(standardDateTime, DateTimeFormatInfo.InvariantInfo);
            return dtLocal.ToString(dateTimeFormat);
        }

        public static string GetStandardDate(string localizedDate)
        {
            return GetStandardDate(DateTime.Parse(localizedDate));
        }

        public static string GetStandardDate(DateTime localizedDate)
        {
            return GetStandardDateTime(localizedDate, DateTimeFormatInfo.InvariantInfo.ShortDatePattern);
        }

        public static string GetStandardDateTime(DateTime localizedDateTime)
        {
            string format = DateTimeFormatInfo.InvariantInfo.ShortDatePattern + " " + DateTimeFormatInfo.InvariantInfo.ShortTimePattern;
            return GetStandardDateTime(localizedDateTime, format);
        }

        public static string GetStandardDateTime(DateTime localizedDateTime, string dateTimeFormat)
        {
            return localizedDateTime.ToString(dateTimeFormat, DateTimeFormatInfo.InvariantInfo);
        }

        #endregion

        #region Number Formating        
        public static string GetLocalizedNumberString(string standardNumber,string numberFormat)
        {
            return GetLocalizedNumber(standardNumber).ToString(numberFormat);
        }
        
        public static double GetLocalizedNumber(string standardNumber)
        {
            double retValue = 0;
            if (!string.IsNullOrEmpty(standardNumber))
            {
                retValue = double.Parse(standardNumber, NumberFormatInfo.InvariantInfo);
            }
            return retValue;
        }

        public static string GetStandardNumber(double localizedNumber)
        {
            return GetStandardNumber(localizedNumber, string.Empty);
        }
        
        public static string GetStandardNumber(double localizedNumber, string numberFormat)
        {
            return localizedNumber.ToString(numberFormat, NumberFormatInfo.InvariantInfo);
        }

        public static string GetStandardNumber(decimal localizedNumber)
        {
            return GetStandardNumber(localizedNumber, string.Empty);
        }

        public static string GetStandardNumber(decimal localizedNumber, string numberFormat)
        {
            return localizedNumber.ToString(numberFormat, NumberFormatInfo.InvariantInfo);
        }

        #endregion

        public static string EncodeSpecialCharactersForJavascript(string s)
        {
            string result = string.Empty;

            foreach (char c in s)
            {
                if (unicodeCharacters.ContainsKey(c))
                {
                    result += unicodeCharacters[c];
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }


        static Dictionary<char, string> unicodeCharacters = new Dictionary<char, string>
                                                                        {
                                                                            {'Ö',@"\u00D6"},
                                                                            {'ö', @"\u00F6"},
                                                                            {'Ü',@"\u00DC"},
                                                                            {'ü',@"\u00FC"},
                                                                            {'Ä',@"\u00C4"},
                                                                            {'ä',@"\u00E4"},
                                                                            {'ß',@"\u00DF"},
                                                                            {'é',@"\u00E9"},
                                                                            {'è',@"\u00E8"},
                                                                            {'ê',@"\u00EA"},
                                                                            {'à',@"\u00E0"},
                                                                            {'î',@"\u00EE"},
                                                                            {'\'',@"\'"}
                                                                        };

     } 
}
