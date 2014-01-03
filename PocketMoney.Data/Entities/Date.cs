using System;
using PocketMoney.Util;
using System.Globalization;
using System.ComponentModel;

namespace PocketMoney.Data
{
    [TypeConverter(typeof(DateTypeConverter))]
    [Serializable]
    public class Date : IComparable
    {
        public Date()
        {
            Value = Clock.UtcNow().Date;
        }

        public Date(String date)
        {
            Value = Parse(date).Value;
        }

        public Date(DateTime date)
        {
            if (date.Kind != DateTimeKind.Utc)
            {
                String message = String.Format(
                    "DateTime is not in UTC format. Kind is {0}", Enum.GetName(typeof(DateTimeKind), date.Kind));
                throw new InternalServiceException(message);
            }
            Value = date.Date;
        }

        public DateTime Value { get; protected set; }

        #region Static Methods

        public static String GetSpecificFormat()
        {
            return "d";
        }

        public static CultureInfo GetSpecificCulture()
        {
            return CultureInfo.CreateSpecificCulture("en-US");
        }

        public static Boolean TryParse(String value, out Date date)
        {
            DateTime dateTime;
            Boolean result = DateTime.TryParseExact(value, GetSpecificFormat(), GetSpecificCulture(), DateTimeStyles.None, out dateTime);
            date = new Date(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
            return result;
        }

        public static Date Parse(String value)
        {
            Date date;
            if (!TryParse(value, out date))
            {
                String message = String.Format("The string '{0}' has an incorrect format", value);
                throw new InternalServiceException(message);
            }
            return date;
        }

        #endregion Static Methods

        #region ToString

        public override String ToString()
        {
            return Value.ToString(GetSpecificFormat(), GetSpecificCulture());
        }

        #endregion

        #region IComparable

        public Int32 CompareTo(Object obj)
        {
            Date date = obj as Date;
            if (date == null)
            {
                throw new InternalServiceException("Object is not a Date");
            }
            return Value.CompareTo(date.Value);
        }

        #endregion IComparable
    }
}