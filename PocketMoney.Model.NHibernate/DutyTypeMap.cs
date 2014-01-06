using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class DutyTypeMap: ClassMap<DutyType>
    {
        public DutyTypeMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            
            Map(x => x.Name).Not.Nullable().UniqueKey("UX_DutyType").Length(254);

            References(x => x.Country).Not.Nullable().ForeignKey("FK_DutyType_Country").UniqueKey("UX_DutyType");
        }
    }
}
