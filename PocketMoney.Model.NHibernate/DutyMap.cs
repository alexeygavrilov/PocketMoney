using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class DutyMap : VersionedClassMap<Duty>
    {
        public DutyMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.Form).Length(4000);
            Map(x => x.Reward).Not.Nullable();

            References(x => x.AssignedTo).ForeignKey("FK_Duty_AssignedTo");
            References(x => x.CreatedBy).ForeignKey("FK_Duty_CreatedBy");

            HasMany(x => x.Dates).Cascade.Delete().ForeignKeyConstraintName("FK_Duty_Day");
        }
    }
}
