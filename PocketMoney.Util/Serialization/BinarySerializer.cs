using System;
using System.IO;
using System.Runtime.Serialization;

namespace PocketMoney.Util.Serialization
{
    public static class BinarySerializer
    {
        public static object Deserialaize(byte[] data, Type objectType)
        {
            var dataContractSerializer = new DataContractSerializer(objectType);
            using (var ms = new MemoryStream(data))
            {
                return dataContractSerializer.ReadObject(ms);
            }
        }

        public static byte[] Serialaize(object obj)
        {
            var dataContractSerializer = new DataContractSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                dataContractSerializer.WriteObject(ms, obj);
                return ms.ToArray();
            }
        }
    }
}