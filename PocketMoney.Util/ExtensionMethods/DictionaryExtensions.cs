using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class DictionaryExtensions
    {
        [DebuggerStepThrough]
        public static void Merge<K, V>(this IDictionary<K, V> instance, IEnumerable<KeyValuePair<K, V>> from,
                                       bool replaceExisting = false)
        {
            foreach (var pair in from.Where(pair => replaceExisting || !instance.ContainsKey(pair.Key)))
            {
                instance[pair.Key] = pair.Value;
            }
        }

        [DebuggerStepThrough]
        public static Dictionary<TK, TV> AddIf<TK, TV>(this Dictionary<TK, TV> data, Func<Boolean> condition,
                                                       Func<KeyValuePair<TK, TV>> result)
            where TK : class
            where TV : class
        {
            return AddIf(data, condition, () => new[] {result()});
        }

        [DebuggerStepThrough]
        public static Dictionary<TK, TV> AddIf<TK, TV>(this Dictionary<TK, TV> data, Func<Boolean> condition,
                                                       Func<IEnumerable<KeyValuePair<TK, TV>>> result)
            where TK : class
            where TV : class
        {
            if (condition())
            {
                foreach (var item in result())
                    data.Add(item.Key, item.Value);
            }
            return data;
        }
    }
}