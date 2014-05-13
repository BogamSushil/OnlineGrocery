using System;

namespace EazyWizy.Framework.Caching
{
    public interface ICacheManager : IDisposable
    {
        event EventHandler<CacheItemRemovedEventArgs> CacheItemExpired;

        object this[string name] { get; set; }
        object GetItem(string name);
        object GetItemAndAddIfMissing(string name, Func<ItemToCache> getItem);
        void SetItem(string name, object value);
        void SetItem(string name, object value, int timeoutInSeconds);
        void RemoveItem(string name);
        void Clear();

        void ReloadCache(Action<ICacheManager> loadItems);
    }
}
