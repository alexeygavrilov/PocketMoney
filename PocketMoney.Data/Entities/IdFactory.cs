using System;

namespace PocketMoney.Data
{
    public static class IdFactory<TIdType>
    {
        public static T Create<T>(TIdType id)
        {
            return (T) Activator.CreateInstance(typeof (T), id);
        }
    }
}