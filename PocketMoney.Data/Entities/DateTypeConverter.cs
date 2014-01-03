using System;
using System.ComponentModel;
using System.Globalization;

namespace PocketMoney.Data
{
    public class DateTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(String);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return Date.Parse(value.ToString());
        }
    }
}