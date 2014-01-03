using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            using (IEnumerator<T> iterator = instance.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    action(iterator.Current);
                }
            }
        }

        [DebuggerStepThrough]
        public static void EachWithIndex<T>(this IEnumerable<T> instance, Action<T, int> action)
        {
            using (IEnumerator<T> iterator = instance.GetEnumerator())
            {
                int index = 0;

                while (iterator.MoveNext())
                {
                    action(iterator.Current, index++);
                }
            }
        }
    }
}