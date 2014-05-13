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
            Map(x => x.Value).Column("DayOfOne").Index("IX_DayOfOne");
        }
    }

}
