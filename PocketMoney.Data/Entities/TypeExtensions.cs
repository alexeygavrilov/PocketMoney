using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketMoney.Data
{
    public static class TypeExtensions
    {
        public static Guid GetTypeId(this Type type)
        {
            IEnumerable<TypeIdAttribute> attr =
                type.GetCustomAttributes(typeof (TypeIdAttribute), true).Cast<TypeIdAttribute>();
            int cnt = attr.Count();
            if (cnt == 0)
                throw new ArgumentException(String.Format("Type {0} is not marked with {1} attribute!", type,
                                                          typeof (TypeIdAttribute)));
            if (cnt > 1)
                throw new ArgumentException(String.Format("Type {0} inherits more than one attribute {1}!", type,
                                                          typeof (TypeIdAttribute)));
            return attr.First().TypeGuid;
        }

        //TODO: Cache the type for Id
        public static Type GetTypeById(Guid id)
        {
            throw new NotImplementedException();
        }

        public static object GetObjectInstance<T>(Type objectType, T objectId)
        {
            throw new NotImplementedException();
        }
    }
}