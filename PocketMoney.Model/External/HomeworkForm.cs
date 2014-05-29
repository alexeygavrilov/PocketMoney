using Microsoft.Practices.ServiceLocation;
using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External
{
    [DataContract, Serializable]
    public class HomeworkForm : ObjectBase
    {
        [DataMember, Details]
        [Display(Name = "Start")]
        public DateTime DateRangeFrom { get; set; }

        [DataMember, Details]
        [Display(Name = "End")]
        public DateTime DateRangeTo { get; set; }

        [DataMember, Details]
        [Display(Name = "Date Range")]
        public int DateRangeIndex { get; set; }

        [DataMember, Details]
        [Display(Name = "Days of Week")]
        public int[] DaysOfWeek { get; set; }

        [DataMember, Details]
        [Display(Name = "Include Holidays")]
        public bool IncludeHolidays { get; set; }

        public IList<DayOfOne> CalculateDates()
        {
            var result = new List<DayOfOne>();
            IList<DayOfOne> holidays = null;
            if (!this.IncludeHolidays)
            {
                var holidaysRepository = ServiceLocator.Current.GetInstance<IRepository<Holiday, HolidayId, Guid>>();
                var currentDataProvider = ServiceLocator.Current.GetInstance<ICurrentUserProvider>();
                var user = currentDataProvider.GetCurrentUser();
                holidays = holidaysRepository
                    .FindAll(x => x.Country.Id == user.Family.CountryCode &&
                        x.Date.Value >= DayOfOne.GetDayOfOneValue(this.DateRangeFrom) &&
                        x.Date.Value <= DayOfOne.GetDayOfOneValue(this.DateRangeTo))
                    .Select(x => x.Date).ToList();
            }
            DateTime currentDate = this.DateRangeFrom;
            while (currentDate <= this.DateRangeTo)
            {
                if (this.DaysOfWeek.Contains((int)currentDate.DayOfWeek))
                {
                    var day = new DayOfOne(currentDate);
                    if (holidays != null)
                    {
                        if (!holidays.Any(x => x == day))
                            result.Add(day);
                    }
                    else
                        result.Add(day);
                }
                currentDate = currentDate.AddDays(1);
            }

            return result;
        }
    }
}
