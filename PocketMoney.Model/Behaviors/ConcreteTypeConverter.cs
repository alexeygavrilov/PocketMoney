using System;
using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.FileSystem;
using PocketMoney.Model;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Model
{
    public class ConcreteTypeConverter<TConcrete> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<TConcrete>(reader);
        }

        private dynamic GetObject(object value)
        {
            if (value is IFile)
                return ((IFile)value).From();
            else if (value is IUser)
                return ((IUser)value).From();
            else if (value is IFamily)
                return ((IFamily)value).From();

            try
            {
                return (TConcrete)Convert.ChangeType(value, typeof(TConcrete));
            }
            catch (Exception ex)
            {
                ex.LogError();
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().IsArray)
            {
                object[] array = (object[])value;
                dynamic[] newarr = new dynamic[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    newarr[i] = this.GetObject(array[i]);
                }
                serializer.Serialize(writer, newarr);
            }
            else
                serializer.Serialize(writer, this.GetObject(value));
        }
    }
}
