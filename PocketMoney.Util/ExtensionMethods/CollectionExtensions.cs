using System.Collections.Generic;
using System.Diagnostics;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class CollectionExtensions
    {
        [DebuggerStepThrough]
        public static void AddAll<T>(this ICollection<T> instance, IEnumerable<T> collection)
        {
            collection.Each(instance.Add);
        }
    }
}