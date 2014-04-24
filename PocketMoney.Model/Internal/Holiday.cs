using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Holiday : Entity<Holiday, HolidayId, Guid>
    {
        protected Holiday() { }

        public Holiday(Country country, string name, DayOfOne date)
        {
            this.Country = country;
            this.Name = name;
            this.Date = date;
        }

        public Holiday(Country country, string name, DateTime date)
            : this(country, name, new DayOfOne(date))
        {
        }

        public virtual Country Country { get; set; }

        public virtual string Name { get; set; }

        public virtual DayOfOne Date { get; set; }
    }

    [Serializable]
    public class HolidayId : GuidIdentity
    {
        public HolidayId()
            : base(Guid.Empty, typeof(Holiday))
        {
        }

        public HolidayId(string id)
            : base(id, typeof(Holiday))
        {
        }

        public HolidayId(Guid id)
            : base(id, typeof(Holiday))
        {
        }
    }

}
