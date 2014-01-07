using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class DutyDateMap : ClassMap<DutyDate>
    {
        public DutyDateMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Component(x => x.Date);

            References(x => x.Duty).ForeignKey("FK_Duty_Date");
        }
    }
}
