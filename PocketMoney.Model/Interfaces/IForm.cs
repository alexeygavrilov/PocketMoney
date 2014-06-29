using System.Collections.Generic;

namespace PocketMoney.Model
{
    public interface IScheduleForm 
    {
        IList<DayOfOne> CalculateDates();
    }
}
