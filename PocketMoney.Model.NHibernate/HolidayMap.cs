using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class HolidayMap : ClassMap<Holiday>
    {
        public HolidayMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            
            Map(x => x.Name).Not.Nullable().Length(254);

            Component(x => x.Date);

            References(x => x.Country).Not.Nullable().ForeignKey("FK_Holiday_Country");
        }
    }
}
