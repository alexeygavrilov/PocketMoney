using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class HolidayMap : ClassMap<Holiday>
    {
        public HolidayMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            
            Map(x => x.Name).Not.Nullable().UniqueKey("UX_Holiday").Length(254);
            Map(x => x.Date).Not.Nullable().UniqueKey("UX_Holiday");

            References(x => x.Country).Not.Nullable().ForeignKey("FK_Holiday_Country").UniqueKey("UX_Holiday");
        }
    }
}
