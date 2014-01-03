using System;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class DateExtensions
    {
        private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static string ToUniversalString(this DateTime value)
        {
            return value.ToString("yyyy'-'MM'-'ddTHH':'mm':'ss'Z'");
        }

        public static string ToLocalTime(this DateTime value, TimeSpan timeZoneOffset)
        {
            if(value.Kind != DateTimeKind.Utc)
                throw new ArgumentException("value must be of kind Utc", "value");

            string date = new DateTime(value.Ticks, DateTimeKind.Local).AddMilliseconds(timeZoneOffset.TotalMilliseconds).ToString();

            return date;
        }

        public static string ToUniversalString(this DateTime? value)
        {
            return value.HasValue ? ToUniversalString(value.Value) : string.Empty;
        }

        public static ulong ToEpochTime(this DateTime? value)
        {
            return value.HasValue ? value.Value.ToEpochTime() : 0;
        }

        public static ulong ToEpochTime(this DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
                throw new ArgumentException("value must be of kind Utc", "value");

            TimeSpan ts = value - epochStart;
            return Convert.ToUInt64(ts.TotalMilliseconds);
        }

        public static DateTime FromEpochTime(this ulong value)
        {
            return epochStart.AddMilliseconds(value);
        }

        public static DateTime GetEpochStart()
        {
            return epochStart;
        }
    }
}