using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EazyWizy.Framework.Caching;
using EazyWizy.Framework.Properties;
using EazyWizy.Framework.Data;
using Microsoft.Practices.Unity;

namespace EazyWizy.Framework.Configuration
{
    /// <summary>
    /// Accesses configuration settings stored in a database or a local config file.
    /// </summary>
    /// <remarks>
    /// To enable tracing, use:
    /// <![CDATA[
    /// <configuration>
    ///   <system.diagnostics>
    ///     <switches>
    ///       <add name="EazyWizy.Framework.Configuration.ConfigManager" value="Verbose" />
    ///     </switches>
    ///   </system.diagnostics>
    /// </configuration>
    /// ]]>
    /// </remarks>
    public class ConfigManager : IConfigManager
    {
        private const string EASYWIZYDBKEY = "EASYWIZY";
        private const short DefaultCacheItemTimeoutSeconds = 900;

        private static object _currentLock = new object();
        private static IConfigManager _configManager = null;

        public static IConfigManager Current
        {
            get
            {
                if (_configManager == null)
                {
                    lock (_currentLock)
                    {
                        if (_configManager == null)
                        {
                            _configManager = FrameworkContainer.Resolve<IConfigManager>();
                        }
                    }
                }
                return _configManager;
            }
        }

        private string _cacheKeyPrefix;
        private ICacheManager _cache = FrameworkContainer.Resolve<ICacheManager>("EazyWizyFrameworkConfiguration_ConfigManager");
        string _rootConfigDirectory = string.Empty;
        public event EventHandler<CacheItemRemovedEventArgs> CachedItemExpired;

        public string RootConfigDirectory
        {
            get { return _rootConfigDirectory; }
        }

        [InjectionConstructor]
        public ConfigManager()
        {
           // Connect(GetPTConfigConnectionString());
        }

        public ConfigManager(string connectionString)
        {
           // Connect(connectionString);
        }

        /// <summary>
        /// Encrypt data with explicit partial key
        /// </summary>
        /// <param name="clearText">Text to encrypt</param>
        /// <param name="key">Partial key to use for encryption</param>
        /// <returns>Encrypted data</returns>
        public string GetConnectionString(string category, string version)
        {
            string connectionString = null;
            GetConnectionString(category, version, out connectionString, true);
            return connectionString;
        }

        public bool TryGetConnectionString(string category, string version, out string connectionString)
        {
            return GetConnectionString(category, version, out connectionString, false);
        }
      
        private bool GetConnectionString(string category, string version, out string connectionString, bool throwExceptionOnFail)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(version))
            {
                throw new ArgumentException(Resources.Error_CategoryAndVersionRequired);
            }

            string versionTemp = version;
            string tempConnectionString;

            // version could look like R4.01.02. If there's no exact match, drop the rightmost piece of the
            // version number and try again.
            do
            {
                if ((tempConnectionString = GetConnectionStringInternal(category, versionTemp)) != null)
                {
                    connectionString = tempConnectionString;
                    return true;
                }

                if (versionTemp.LastIndexOf('.') == -1)
                {
                    if (!throwExceptionOnFail)
                    {
                        connectionString = null;
                        return false;
                    }
                    else
                    {
                        string message = string.Format(Resources.Error_NoConnectionStringFoundForNameAndVersion, category, versionTemp);
                        TraceManager.TraceError(message);
                        throw new ArgumentException(message);
                    }
                }

                versionTemp = versionTemp.Substring(0, versionTemp.LastIndexOf('.'));
            }
            while (true);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "UserId")]
        private string GetConnectionStringInternal(string category, string version)
        {
            string cacheKey = string.Format("{0}-{1}-{2}", _cacheKeyPrefix, category, version);


            string cacheValue = (string)_cache.GetItemAndAddIfMissing(
                cacheKey,
                () =>
                {
                    short cacheTimeoutSecs = DefaultCacheItemTimeoutSeconds;

                    TraceManager.TraceInfo(
                        string.Format("Loading connection string from DATABASE for name '{0}' and version '{1}'...", category, version));

                    SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();

                    csBuilder.ConnectionString = ConfigurationManager.ConnectionStrings[category].ConnectionString;

                    if (string.IsNullOrEmpty(csBuilder.ConnectionString))
                    {
                        throw new Exception("Invalid connection string in database.");
                    }

                    cacheValue = csBuilder.ConnectionString;

                    if (!cacheValue.EndsWith(";"))
                    {
                        cacheValue += ";";
                    }

                    TraceManager.TraceInfo(
                        string.Format("Loaded connection string from DATABASE for name '{0}' and version '{1}'.", category, version));

                    return new ItemToCache(cacheValue, cacheTimeoutSecs);
                });

            TraceManager.TraceInfo(
                string.Format("Found connection string for name '{0}' and version '{1}'.", category, version));

            return cacheValue;
        }

        private void OnCacheItemExpired(object sender, CacheItemRemovedEventArgs e)
        {
            if (CachedItemExpired != null)
            {
                CachedItemExpired(this, e);
            }
        }

        public string GetConfigurationAppSettingValueForKey(string key)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[key]);
        }
    }


}
