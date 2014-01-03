using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using PocketMoney.Util;

namespace PocketMoney.Configuration.Web
{
    public class CacheManager : ICacheManager
    {
        private static readonly string KeyPrefix = typeof(CacheManager).FullName;
        private static readonly object SyncLock = new object();

        private readonly Cache _cache;

        public CacheManager(Cache cache)
        {
            this._cache = cache;
        }

        #region ICacheManager Members

        public TValue GetValue<TValue>(string key)
        {
            return (TValue)_cache[MakeKey(key)];
        }

        public void SetValue<TValue>(string key, Func<TValue> value)
        {
            Set(key, null, null, null, value, null);
        }

        public void SetValue<TValue>(string key, DateTime timestamp, TValue value)
        {
            Set(key, timestamp, null, null, value, null);
        }

        public void SetValue<TValue>(string key, TimeSpan duration, TValue value)
        {
            Set(key, null, duration, null, value, null);
        }

        public void SetValue<TValue>(string key, TValue value, Action<bool> onRemoveCallback)
        {
            Set(key, null, null, null, value, onRemoveCallback);
        }

        public void SetValue<TValue>(string key, DateTime timestamp, TValue value, Action<bool> onRemoveCallback)
        {
            Set(key, timestamp, null, null, value, onRemoveCallback);
        }

        public void SetValue<TValue>(string key, TimeSpan duration, TValue value, Action<bool> onRemoveCallback)
        {
            Set(key, null, duration, null, value, onRemoveCallback);
        }

        public void SetValue<TValue>(string key, IEnumerable<string> fileDependencies, TValue value)
        {
            Set(key, null, null, fileDependencies, value, null);
        }

        public void SetValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies, TValue value)
        {
            Set(key, timestamp, null, fileDependencies, value, null);
        }

        public void SetValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies, TValue value)
        {
            Set(key, null, duration, fileDependencies, value, null);
        }

        public void SetValue<TValue>(string key, IEnumerable<string> fileDependencies, TValue value,
                                     Action<bool> onRemoveCallback)
        {
            Set(key, null, null, fileDependencies, value, onRemoveCallback);
        }

        public void SetValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies, TValue value,
                                     Action<bool> onRemoveCallback)
        {
            Set(key, timestamp, null, fileDependencies, value, onRemoveCallback);
        }

        public void SetValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies, TValue value,
                                     Action<bool> onRemoveCallback)
        {
            Set(key, null, duration, fileDependencies, value, onRemoveCallback);
        }

        public TValue GetOrCreateValue<TValue>(string key, Func<TValue> factory)
        {
            return GetOrCreate(key, null, null, null, factory, null);
        }

        public TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, Func<TValue> factory)
        {
            return GetOrCreate(key, timestamp, null, null, factory, null);
        }

        public TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, Func<TValue> factory)
        {
            return GetOrCreate(key, null, duration, null, factory, null);
        }

        public TValue GetOrCreateValue<TValue>(string key, Func<TValue> factory, Action<bool> onRemoveCallback)
        {
            return GetOrCreate(key, null, null, null, factory, onRemoveCallback);
        }

        public TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, Func<TValue> factory,
                                               Action<bool> onRemoveCallback)
        {
            return GetOrCreate(key, timestamp, null, null, factory, onRemoveCallback);
        }

        public TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, Func<TValue> factory,
                                               Action<bool> onRemoveCallback)
        {
            return GetOrCreate(key, null, duration, null, factory, onRemoveCallback);
        }

        public TValue GetOrCreateValue<TValue>(string key, IEnumerable<string> fileDependencies, Func<TValue> factory)
        {
            return GetOrCreate(key, null, null, fileDependencies, factory, null);
        }

        public TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies,
                                               Func<TValue> factory)
        {
            return GetOrCreate(key, timestamp, null, fileDependencies, factory, null);
        }

        public TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies,
                                               Func<TValue> factory)
        {
            return GetOrCreate(key, null, duration, fileDependencies, factory, null);
        }

        public TValue GetOrCreateValue<TValue>(string key, IEnumerable<string> fileDependencies, Func<TValue> factory,
                                               Action<bool> onRemoveCallback)
        {
            return GetOrCreate(key, null, null, fileDependencies, factory, onRemoveCallback);
        }

        public TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies,
                                               Func<TValue> factory, Action<bool> onRemoveCallback)
        {
            return GetOrCreate(key, timestamp, null, fileDependencies, factory, onRemoveCallback);
        }

        public TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies,
                                               Func<TValue> factory, Action<bool> onRemoveCallback)
        {
            return GetOrCreate(key, null, duration, fileDependencies, factory, onRemoveCallback);
        }

        public void Remove(string key)
        {
            _cache.Remove(MakeKey(key));
        }

        #endregion

        private static string MakeKey(string key)
        {
            return KeyPrefix + ":" + key;
        }

        private void Set<TValue>(string key, DateTime? timestamp, TimeSpan? duration,
                                 IEnumerable<string> fileDependencies, TValue value, Action<bool> onRemoveCallback)
        {
            string fullKey = MakeKey(key);

            lock (SyncLock)
            {
                _cache.Remove(fullKey);

                InsertInCache(fullKey, value, fileDependencies, timestamp, duration, onRemoveCallback);
            }
        }

        private TValue GetOrCreate<TValue>(string key, DateTime? timestamp, TimeSpan? duration,
                                           IEnumerable<string> fileDependencies, Func<TValue> factory,
                                           Action<bool> onRemoveCallback)
        {
            string fullKey = MakeKey(key);

            object value = _cache.Get(fullKey);

            if (value == null)
            {
                lock (SyncLock)
                {
                    value = _cache.Get(fullKey);

                    if (value == null)
                    {
                        value = factory();

                        if (value != null)
                        {
                            InsertInCache(fullKey, value, fileDependencies, timestamp, duration, onRemoveCallback);
                        }
                    }
                }
            }

            return (TValue)value;
        }

        private void InsertInCache(string key, object value, IEnumerable<string> fileDependencies, DateTime? timestamp,
                                   TimeSpan? duration, Action<bool> onRemoveCallback)
        {
            Action<string, object, CacheItemRemovedReason> raiseOnRemoveCallback =
                (cacheKey, state, reason) => onRemoveCallback(reason == CacheItemRemovedReason.DependencyChanged);

            CacheDependency cacheDependency = null;

            if (fileDependencies != null)
            {
                cacheDependency = new CacheDependency(fileDependencies.ToArray());
            }

            try
            {
                _cache.Add(key, value, cacheDependency, timestamp ?? Cache.NoAbsoluteExpiration,
                          duration ?? Cache.NoSlidingExpiration, CacheItemPriority.Normal,
                          onRemoveCallback != null ? new CacheItemRemovedCallback(raiseOnRemoveCallback) : null);
            }
            finally
            {
                if (cacheDependency != null)
                {
                    cacheDependency.Dispose();
                }
            }
        }
    }
}