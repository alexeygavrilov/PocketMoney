using System;
using System.Globalization;

namespace PocketMoney.Data
{
    public static class GuidIdentityExtnesions
    {
        public static T Validate<T>(this T id) where T : GuidIdentity, new()
        {
            Type typeId = typeof (T);
            if (id == null) throw new ArgumentNullException(typeId.Name);
            var emptyId = new T();
            if (emptyId.Equals(id))
                throw new ArgumentException(typeId.Name + " is empty", typeId.Name);
            return id;
        }

        public static string ToBase16(this GuidIdentity id)
        {
            string result = string.Empty;
            byte[] array = id.Id.ToByteArray();
            for (int i = 0; i < array.Length; i++)
                result += array[i].ToString("x2", CultureInfo.InvariantCulture).ToUpperInvariant();
            return result;
        }
    }
}