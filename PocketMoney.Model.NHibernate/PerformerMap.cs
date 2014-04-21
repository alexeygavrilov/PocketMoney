using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class PerformerMap : VersionedClassMap<Performer>
    {
        public PerformerMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Active).Not.Nullable();

            References(x => x.User).Not.Nullable().Not.LazyLoad().ForeignKey("FK_Performer_User").UniqueKey("UX_Performer");
            References(x => x.Task).Not.Nullable().Not.LazyLoad().ForeignKey("FK_Performer_Task").UniqueKey("UX_Performer");

            HasMany(x => x.Actions).Cascade.Delete().ForeignKeyConstraintName("FK_TaskAction_Performer");
        }
    }
}
