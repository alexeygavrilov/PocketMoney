using System;

namespace PocketMoney.Model
{
    public static class EnumExtensions
    {
        public static eDaysOfWeek To(this DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return eDaysOfWeek.Monday;
                case DayOfWeek.Tuesday:
                    return eDaysOfWeek.Tuesday;
                case DayOfWeek.Wednesday:
                    return eDaysOfWeek.Wednesday;
                case DayOfWeek.Thursday:
                    return eDaysOfWeek.Thursday;
                case DayOfWeek.Friday:
                    return eDaysOfWeek.Friday;
                case DayOfWeek.Saturday:
                    return eDaysOfWeek.Saturday;
                case DayOfWeek.Sunday:
                    return eDaysOfWeek.Sunday;
                default:
                    return eDaysOfWeek.None;
            }
        }
    }
}
