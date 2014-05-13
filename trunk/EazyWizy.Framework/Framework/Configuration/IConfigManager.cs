using System;
using System.Collections.Generic;
using EazyWizy.Framework.Caching;


namespace EazyWizy.Framework.Configuration
{
    public interface IConfigManager
    {
        event EventHandler<CacheItemRemovedEventArgs> CachedItemExpired;

        string GetConnectionString(string category, string version);
        string GetConfigurationAppSettingValueForKey(string appSettingKey);
       
    }
}
