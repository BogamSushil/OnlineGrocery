using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EazyWizy.Framework.Configuration;
using EazyWizy.Framework.Logging.Properties;

namespace EazyWizy.Framework.Logging
{
    public class LogWriterConfiguration
    {
        private IConfigManager _configManager;
        private const string DEFAULT_LOG_LEVEL = "Error";
        private const string DEFAULT_FALLBACK_FOLDER = "D:\\EazyWizy\\Logs";

        public LogWriterConfiguration(IConfigManager configManager)
        {
            this._configManager = configManager;
        }

        public static string GetDefaultApplicationName()
        {
            string setting = ConfigurationManager.AppSettings["EazyWizyFrameworkLogging_AppName"];

            if (string.IsNullOrEmpty(setting))
            {
                return "EazyWizyFrameworkLogging";
            }

            return setting;
        }

        public string GetEventLogConnectionString()
        {
            string connectionString = null;

            //if (ShouldUseConfigManager())
            //{
            //    connectionString = _configManager.GetConnectionString("EventLog", "R4");
            //}
            //else
            //{
            ConnectionStringSettings connectionStringSetting = ConfigurationManager.ConnectionStrings["EventLog"];

            if (connectionStringSetting == null)
            {
                throw new ConfigurationErrorsException(Resources.Error_NoEventLogConnectionStringInConfigFile);
            }

            connectionString = connectionStringSetting.ConnectionString;
            //}

            return connectionString;
        }

        public string GetActiveLogLevel(string applicationName)
        {
            string logLevelString = null;

            logLevelString = _configManager.GetConfigurationAppSettingValueForKey("EventLogLevel");
            if (string.IsNullOrEmpty(logLevelString))
            {
                logLevelString = DEFAULT_LOG_LEVEL;
            }
            return logLevelString;
        }

        public string GetFallbackLogFolderPath()
        {
            string path = _configManager.GetConfigurationAppSettingValueForKey("LogFolderPath"); ;

            if (string.IsNullOrEmpty(path))
            {
                path = DEFAULT_FALLBACK_FOLDER;

            }
            return path;
        }

        

        private string GetDefaultLogLevelConfigXml()
        {
            XDocument xml = new XDocument();

            XElement configElement = new XElement("Config");
            xml.Add(configElement);

            XElement logLevelElement = new XElement("LogLevel");
            logLevelElement.Value = DEFAULT_LOG_LEVEL;
            configElement.Add(logLevelElement);

            return xml.ToString(SaveOptions.DisableFormatting);
        }

        private string GetDefaultLogFallbackFolderXml()
        {
            XDocument xml = new XDocument();

            XElement configElement = new XElement("Config");
            xml.Add(configElement);

            XElement logLevelElement = new XElement("FallbackLogFolderPath");
            logLevelElement.Value = DEFAULT_FALLBACK_FOLDER;
            configElement.Add(logLevelElement);

            return xml.ToString(SaveOptions.DisableFormatting);
        }
    }
}
