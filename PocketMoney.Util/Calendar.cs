using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace PocketMoney.Util
{
    public class Calendar
    {
        private static System.Globalization.Calendar _calendar;

        static Calendar()
        {
            _calendar = System.Globalization.CultureInfo.CurrentCulture.Calendar;
        }

        public static System.Globalization.Calendar Current
        {
            get { return _calendar; }
        }

        public static DateTime StartOfDay(DateTime dt)
        {
            return dt.
                AddHours(-dt.Hour).
                AddMinutes(-dt.Minute).
                AddSeconds(-dt.Second);
        }

        public static DateTime EndOfDay(DateTime dt)
        {
            return StartOfDay(dt).AddDays(1).AddTicks(-1);
        }

        public static DateTime GetSimpleDateTimeData(DateTime dt)
        {
            return DateTime.SpecifyKind(dt, dt.Equals(dt.ToUniversalTime()) ? DateTimeKind.Utc : DateTimeKind.Local);
        }

        public static DateTime SimpleDateTimeToMatch(DateTime dt, DateTime toMatch)
        {
            if (toMatch.Equals(toMatch.ToUniversalTime()) && dt.Equals(dt.ToUniversalTime()))
                return dt;
            else if (toMatch.Equals(toMatch.ToUniversalTime()))
                return toMatch.ToUniversalTime();
            else if (dt.Equals(dt.ToUniversalTime()))
                return dt.ToLocalTime();
            else
                return dt;
        }


        public static DateTime AddWeeks(System.Globalization.Calendar calendar, DateTime dt, int interval, DayOfWeek firstDayOfWeek)
        {
            // How the week increments depends on the WKST indicated (defaults to Monday)
            // So, basically, we determine the week of year using the necessary rules,
            // and we increment the day until the week number matches our "goal" week number.
            // So, if the current week number is 36, and our interval is 2, then our goal
            // week number is 38.
            // NOTE: fixes WeeklyUntilWkst2() eval.
            int current = calendar.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek),
                lastLastYear = calendar.GetWeekOfYear(new DateTime(dt.Year - 1, 12, 31, 0, 0, 0, DateTimeKind.Local), System.Globalization.CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek),
                last = calendar.GetWeekOfYear(new DateTime(dt.Year, 12, 31, 0, 0, 0, DateTimeKind.Local), System.Globalization.CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek),
                goal = current + interval;

            // If the goal week is greater than the last week of the year, wrap it!
            if (goal > last)
                goal = goal - last;
            else if (goal <= 0)
                goal = lastLastYear + goal;

            int i = interval > 0 ? 7 : -7;
            while (current != goal)
            {
                dt = dt.AddDays(i);
                current = calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, firstDayOfWeek);
            }
            while (dt.DayOfWeek != firstDayOfWeek)
                dt = dt.AddDays(-1);

            return dt;
        }

        public static DateTime FirstDayOfWeek(DateTime dt, DayOfWeek firstDayOfWeek, out int offset)
        {
            offset = 0;
            while (dt.DayOfWeek != firstDayOfWeek)
            {
                dt = dt.AddDays(-1);
                offset++;
            }
            return dt;
        }

    }
}
