// -----------------------------------------------------------------------
// <copyright file="BasicTypeDataParser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PocketMoney.Util.DataImport
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BasicTypeDataParser
    {
        private readonly IFormatProvider formatProvider;

        public BasicTypeDataParser()
        {
            this.formatProvider = Thread.CurrentThread.CurrentUICulture;
        }


        public DateTime ParseDate(string value, DateTime defaultValue)
        {
            DateTime result;
            return !this.TryParseDateTime(value, out result) ? defaultValue : result;
        }

        public DateTime ParseDate(string value)
        {
            DateTime result;
            if (!this.TryParseDateTime(value, out result))
                throw new FormatException("Invalid date format!");
            return result;
        }

        public decimal? ParseAmountNullable(string value, decimal? defaultValue)
        {
            decimal result;
            return !this.TryParseAmount(value, out result) ? defaultValue : result;
        }

        public decimal ParseAmount(string value, decimal defaultValue)
        {
            decimal result;
            return !this.TryParseAmount(value, out result) ? defaultValue : result;
        }

        public decimal ParseAmount(string value)
        {
            decimal result;
            if (!this.TryParseAmount(value, out result))
                throw new FormatException(String.Format("Invalid amount '{0}'", value));
            return result;
        }

        public int ParseInt(string value)
        {
            int result;
            if (!this.TryParseInt(value, out result))
                throw new FormatException(String.Format("Invalid integer '{0}'.", value));
            return result;
        }

        public bool TryParseEnum<T>(string enumString, out T value) where T : struct,IComparable, IFormattable, IConvertible
        {
            bool result = Enum.TryParse(enumString, out value);
            if (result) return result;
            foreach (var val in
                from object val in Enum.GetValues(typeof(T))
                let text = GetText((T)val)
                where text.Equals(enumString)
                select val)
            {
                value = (T)val;
                return true;
            }
            return false;
        }
        
        private static string GetText<TEnum>(TEnum value) where TEnum : IComparable, IFormattable, IConvertible
        {
            string name = value.ToString();

            DescriptionAttribute attribute = value.GetType()
                .GetField(name)
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                return name;
            }

            return string.IsNullOrWhiteSpace(attribute.Description)
                       ? name
                       : attribute.Description;
        }

        public bool TryParseDateTime(string date, out DateTime dateTime)
        {
            if (!String.IsNullOrWhiteSpace(date))
            {
                try
                {
                    dateTime = DateTime.Parse(date, formatProvider);
                    return true;
                }
                catch
                {
                }
                try
                {
                    dateTime = Convert.ToDateTime(date, formatProvider);
                    return true;
                }
                catch
                {
                }
            }
            dateTime = DateTime.MinValue;
            return false;
        }

        public bool TryParseAmount(string amountString, out decimal amount)
        {
            if (!String.IsNullOrWhiteSpace(amountString))
            {
                try
                {
                    amount = decimal.Parse(amountString, NumberStyles.Currency, formatProvider);
                    return true;
                }
                catch
                {
                }
                try
                {
                    amount = Convert.ToDecimal(amountString, formatProvider);
                    return true;
                }
                catch
                {
                }
            }
            amount = decimal.Zero;
            return false;
        }


        public bool TryParseInt(string intString, out int intValue)
        {
            if (!String.IsNullOrWhiteSpace(intString))
            {
                try
                {
                    intValue = int.Parse(intString, NumberStyles.Integer, formatProvider);
                    return true;
                }
                catch
                {
                }
                try
                {
                    intValue = Convert.ToInt32(intString, formatProvider);
                    return true;
                }
                catch
                {
                }
            }
            intValue = 0;
            return false;
        }
    }
}
