using System;
using System.Collections.Generic;

namespace PocketMoney.Util
{
    public interface ICacheManager
    {
        TValue GetValue<TValue>(string key);

        void SetValue<TValue>(string key, Func<TValue> value);

        void SetValue<TValue>(string key, DateTime timestamp, TValue value);

        void SetValue<TValue>(string key, TimeSpan duration, TValue value);

        void SetValue<TValue>(string key, TValue value, Action<bool> onRemoveCallback);

        void SetValue<TValue>(string key, DateTime timestamp, TValue value, Action<bool> onRemoveCallback);

        void SetValue<TValue>(string key, TimeSpan duration, TValue value, Action<bool> onRemoveCallback);

        void SetValue<TValue>(string key, IEnumerable<string> fileDependencies, TValue value);

        void SetValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies, TValue value);

        void SetValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies, TValue value);

        void SetValue<TValue>(string key, IEnumerable<string> fileDependencies, TValue value,
                              Action<bool> onRemoveCallback);

        void SetValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies, TValue value,
                              Action<bool> onRemoveCallback);

        void SetValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies, TValue value,
                              Action<bool> onRemoveCallback);

        TValue GetOrCreateValue<TValue>(string key, Func<TValue> factory);

        TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, Func<TValue> factory);

        TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, Func<TValue> factory);

        TValue GetOrCreateValue<TValue>(string key, Func<TValue> factory, Action<bool> onRemoveCallback);

        TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, Func<TValue> factory,
                                        Action<bool> onRemoveCallback);

        TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, Func<TValue> factory,
                                        Action<bool> onRemoveCallback);

        TValue GetOrCreateValue<TValue>(string key, IEnumerable<string> fileDependencies, Func<TValue> factory);

        TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies,
                                        Func<TValue> factory);

        TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies,
                                        Func<TValue> factory);

        TValue GetOrCreateValue<TValue>(string key, IEnumerable<string> fileDependencies, Func<TValue> factory,
                                        Action<bool> onRemoveCallback);

        TValue GetOrCreateValue<TValue>(string key, DateTime timestamp, IEnumerable<string> fileDependencies,
                                        Func<TValue> factory, Action<bool> onRemoveCallback);

        TValue GetOrCreateValue<TValue>(string key, TimeSpan duration, IEnumerable<string> fileDependencies,
                                        Func<TValue> factory, Action<bool> onRemoveCallback);

        void Remove(string key);
    }
}