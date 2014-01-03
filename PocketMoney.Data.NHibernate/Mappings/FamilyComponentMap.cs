using FluentNHibernate.Mapping;
using PocketMoney.Data;

namespace PocketMoney.Data.NHibernate.Mappings
{

    public class FamilyComponentMap : ComponentMap<IFamily>
    {
        public FamilyComponentMap()
        {
            Map(x => x.Id).Column("FamilyId").Index("IX_FamilyId");
        }
    }
}
