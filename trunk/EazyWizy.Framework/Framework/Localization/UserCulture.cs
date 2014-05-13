using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Threading;
using System.Globalization;

namespace EazyWizy.Framework.Localization
{
    [Serializable]
    public class UserCulture
    {
        #region Properties
        // Properties are in lowercase for JS consistency
        public string cultureLanguage { get; set; }
        public string dateFormat { get; set; }
        public string dateSeparator { get; set; }
        public string timeFormat { get; set; }
        public string timeSeparator { get; set; }
        public string amDesignator { get; set; }
        public string pmDesignator { get; set; }
        public string numberGroupSeperator { get; set; }
        public string numberDecimalSeperator { get; set; }
        public string[] abbrivatedMonthNames { get; set; }
        public string[] abbrivatedMonthNamesUpperCase 
        {
            get
            {
                return abbrivatedMonthNames.Select(s => s.ToUpper()).ToArray();
            }            
        }

        #endregion

        #region Constructors

        public UserCulture()
        {

        }

        public UserCulture(string languageCode)
        {
            SetUserCulture(new CultureInfo(languageCode));
        }

        public UserCulture(CultureInfo ci)
        {
            SetUserCulture(ci);
        }

        #endregion

        #region Public Methods
        
        public void SetUserCulture(CultureInfo ci)
        {
            cultureLanguage = ci.Name;
            dateFormat = ci.DateTimeFormat.ShortDatePattern;
            timeFormat = ci.DateTimeFormat.ShortTimePattern;
            amDesignator = ci.DateTimeFormat.AMDesignator;
            pmDesignator = ci.DateTimeFormat.PMDesignator;
            numberDecimalSeperator = ci.NumberFormat.NumberDecimalSeparator;
            numberGroupSeperator = ci.NumberFormat.NumberGroupSeparator;
            dateSeparator = ci.DateTimeFormat.DateSeparator;
            timeSeparator = ci.DateTimeFormat.TimeSeparator;
            abbrivatedMonthNames = ci.DateTimeFormat.AbbreviatedMonthNames;
        }

        public string GetUserCultureAsJavaScript()
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();            
            return jsSerializer.Serialize(this);
        }

        #endregion

    }
}
