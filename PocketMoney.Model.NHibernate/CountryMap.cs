using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Id).Column("CountryCode").GeneratedBy.Assigned();

            Map(x => x.Name).Not.Nullable().UniqueKey("UX_Country").Length(254);
        }
    }
}
