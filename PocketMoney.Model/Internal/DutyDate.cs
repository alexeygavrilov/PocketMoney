using System;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class DutyDate : Entity<DutyDate, DutyDateId, Guid>
    {
        protected DutyDate() { }
        
        public DutyDate(Duty duty, DayOfOne date)
        {
            this.Duty = duty;
            this.Date = date;
        }

        public virtual Duty Duty { get; set; }

        public virtual DayOfOne Date { get; set; }
    }

    [Serializable]
    public class DutyDateId : GuidIdentity
    {
        public DutyDateId()
            : base(Guid.Empty, typeof(DutyDate))
        {
        }

        public DutyDateId(string id)
            : base(id, typeof(DutyDate))
        {
        }

        public DutyDateId(Guid id)
            : base(id, typeof(DutyDate))
        {
        }
    }
}
