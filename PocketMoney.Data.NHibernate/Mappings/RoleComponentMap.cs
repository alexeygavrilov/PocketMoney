using FluentNHibernate.Mapping;

namespace PocketMoney.Data.NHibernate.Mappings
{
    public class RoleComponentMap : ComponentMap<Role>
    {
        public RoleComponentMap()
        {
            Map(x => x.Id).Not.Nullable().Column("RoleId");
        }
    }
}
