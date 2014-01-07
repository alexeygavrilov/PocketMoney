using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace PocketMoney.Model.NHibernate
{
    public class DayOfOneMap : ComponentMap<DayOfOne>
    {
        public DayOfOneMap()
        {
            Map(x => x.Year).Index("IX_DayOfOne");
            Map(x => x.Month).Index("IX_DayOfOne");
            Map(x => x.Day).Index("IX_DayOfOne");
        }
    }
}
