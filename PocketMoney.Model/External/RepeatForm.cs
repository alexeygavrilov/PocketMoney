using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External
{
    [DataContract, Serializable]
    public class RepeatForm : ObjectBase, IScheduleForm
    {
        [DataMember, Details]
        [Display(Name = "Start")]
        public DateTime DateRangeFrom { get; set; }

        [DataMember, Details]
        [Display(Name = "End by")]
        public DateTime? DateRangeTo { get; set; }

        [DataMember, Details]
        [Display(Name = "End after")]
        public int? OccurrenceNumber { get; set; }

        [DataMember, Details]
        public eOccurrenceType OccurrenceType { get; set; }

        [DataMember, Details]
        [Display(Name = "Every day(s)")]
        public int EveryDay { get; set; }

        [DataMember, Details]
        [Display(Name = "Every week(s)")]
        public int EveryWeek { get; set; }

        [DataMember, Details]
        [Display(Name = "Days of Week")]
        public int[] DaysOfWeek { get; set; }

        [DataMember, Details]
        [Display(Name = "Every month(s)")]
        public int EveryMonth { get; set; }

        [DataMember, Details]
        [Display(Name = "Day of Month")]
        public int DayOfMonth { get; set; }

        private IList<DayOfOne> _result = new List<DayOfOne>();

        public IList<DayOfOne> CalculateDates()
        {
            _result.Clear();

            switch (this.OccurrenceType)
            {
                case eOccurrenceType.Day:
                    this.ProcessRange(currentDate =>
                    {
                        _result.Add(new DayOfOne(currentDate));
                        return currentDate.AddDays(this.EveryDay);
                    });
                    break;
                case eOccurrenceType.Weekday:
                    this.ProcessRange(currentDate =>
                    {
                        if (currentDate.DayOfWeek != DayOfWeek.Sunday && currentDate.DayOfWeek != DayOfWeek.Saturday)
                            _result.Add(new DayOfOne(currentDate));
                        switch (currentDate.DayOfWeek)
                        {
                            case DayOfWeek.Friday:
                                return currentDate.AddDays(3);
                            case DayOfWeek.Saturday:
                                return currentDate.AddDays(2);
                            case DayOfWeek.Sunday:
                            default:
                                return currentDate.AddDays(1);
                        }
                    });
                    break;
                case eOccurrenceType.Week:
                    this.ProcessRange(currentDate =>
                    {
                        if (this.DaysOfWeek.Contains((int)currentDate.DayOfWeek))
                        {
                            _result.Add(new DayOfOne(currentDate));
                        }
                        int maxDay = this.DaysOfWeek.Max();
                        int minDay = this.DaysOfWeek.Min();
                        if ((int)currentDate.DayOfWeek == maxDay)
                        {
                            // jump to next week (begin of next week + weeks incrementer + the next day)
                            currentDate = currentDate.AddDays((7 - maxDay) + ((this.EveryWeek - 1) * 7) + minDay);
                        }
                        else
                        {
                            // jump to next day
                            currentDate = currentDate.AddDays(this.DaysOfWeek.Where(x => x > (int)currentDate.DayOfWeek).Min() - (int)currentDate.DayOfWeek);
                        }
                        return currentDate;
                    });
                    break;
                case eOccurrenceType.Month:
                    this.ProcessRange(currentDate =>
                    {
                        if (currentDate.Day == this.DayOfMonth)
                        {
                            _result.Add(new DayOfOne(currentDate));
                        }
                        // jump to begin of month
                        currentDate = currentDate.AddDays(1 - currentDate.Day);
                        // jump to next month
                        currentDate = currentDate.AddMonths(this.EveryMonth);
                        // jump to day of month
                        currentDate = currentDate.AddDays(this.DayOfMonth - 1);
                        
                        return currentDate;
                    });
                    break;
                default:
                    throw new ArgumentException("Unknown repeat type mode.");
            }
            return _result;
        }

        public void ProcessRange(Func<DateTime, DateTime> getNextDate)
        {
            DateTime currentDate = this.DateRangeFrom;
            if (this.DateRangeTo.HasValue)
            {
                while (currentDate <= this.DateRangeTo.Value)
                {
                    currentDate = getNextDate(currentDate);
                }
            }
            else if (this.OccurrenceNumber.HasValue)
            {
                while (_result.Count < this.OccurrenceNumber.Value)
                {
                    currentDate = getNextDate(currentDate);
                }
            }
        }
    }

    public enum eOccurrenceType : byte
    {
        None = 0,
        Day = 1,
        Weekday = 2,
        Week = 3,
        Month = 4
    }
}
