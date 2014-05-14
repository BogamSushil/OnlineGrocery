using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace EazyWizy.Framework.Caching
{
    /// <summary>
    /// Thread-safe cache
    /// </summary>
    internal class NetFxCacheManager : ICacheManager
    {
        private short _defaultItemTimeoutSec;
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private MemoryCache _cache;

        public event EventHandler<CacheItemRemovedEventArgs> CacheItemExpired;

        public NetFxCacheManager()
            : this("EazyWizyFrameworkCaching_Default")
        {
        }

        public NetFxCacheManager(string name)
            : this(name, 3600)
        {
        }

        public NetFxCacheManager(string name, short defaultItemTimeoutInSeconds)
        {
            _defaultItemTimeoutSec = defaultItemTimeoutInSeconds;
            _cache = new MemoryCache(name);
        }

        ~NetFxCacheManager()
        {
            Dispose(false);
        }

        public object this[string name]
        {
            get { return GetItem(name); }
            set { SetItem(name, value); }
        }

        public object GetItem(string name)
        {
            _lock.EnterReadLock();

            try
            {
                return _cache.Get(name);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public object GetItemAndAddIfMissing(string name, Func<ItemToCache> getItem)
        {
            if (getItem == null)
            {
                throw new ArgumentNullException("getItem");
            }

            _lock.EnterUpgradeableReadLock();

            try
            {
                object value = _cache.Get(name);

                if (value == null)
                {
                    _lock.EnterWriteLock();

                    try
                    {
                        value = _cache.Get(name);

                        if (value == null)
                        {
                            ItemToCache item = getItem();

                            if (item == null || item.Item == null)
                            {
                                return null;
                            }

                            value = item.Item;

                            short timeoutInSeconds = (item.TimeoutInSeconds == short.MaxValue) ? _defaultItemTimeoutSec : item.TimeoutInSeconds;
                            CacheItemPolicy policy = new CacheItemPolicy()
                            {
                                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(timeoutInSeconds),
                                RemovedCallback = CacheItemRemovedCallback
                            };

                            _cache.Add(name, value, policy);
                        }
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                }

                return value;
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        public void ReloadCache(Action<ICacheManager> loadItems)
        {
            _lock.EnterWriteLock();

            try
            {
                ClearCache();
                loadItems(this);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void SetItem(string name, object value)
        {
            SetItem(name, value, _defaultItemTimeoutSec);
        }

        public void SetItem(string name, object value, int timeoutInSeconds)
        {
            _lock.EnterWriteLock();

            try
            {
                CacheItemPolicy policy = new CacheItemPolicy()
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(timeoutInSeconds),
                    RemovedCallback = CacheItemRemovedCallback
                };

                _cache.Set(name, value, policy);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void RemoveItem(string name)
        {
            _lock.EnterWriteLock();

            try
            {
                _cache.Remove(name);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            _lock.EnterWriteLock();

            try
            {
                ClearCache();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
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
                _lock.Dispose();
                _lock = null;
            }
        }

        private void ClearCache()
        {
            // This method assumes that we are already within a lock.

            var keys = _cache.ToList().Select(x => x.Key);
            foreach (var key in keys)
            {
                _cache.Remove(key);
            }
        }

        private void CacheItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            EventHandler<CacheItemRemovedEventArgs> expiredEvent = this.CacheItemExpired;

            if (expiredEvent != null)
            {
                CacheItemRemovedReason reason = CacheItemRemovedReason.Other;

                switch (arguments.RemovedReason)
                {
                    case CacheEntryRemovedReason.Expired:
                        reason = CacheItemRemovedReason.Expired;
                        break;
                    case CacheEntryRemovedReason.Removed:
                        reason = CacheItemRemovedReason.Removed;
                        break;
                    case CacheEntryRemovedReason.Evicted:
                    case CacheEntryRemovedReason.CacheSpecificEviction:
                        reason = CacheItemRemovedReason.Evicted;
                        break;
                }

                expiredEvent(this, new CacheItemRemovedEventArgs(arguments.CacheItem.Key, reason));
            }
        }
    }
}
