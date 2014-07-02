using PocketMoney.Data.NHibernate;
using PocketMoney.FileSystem;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class AttainmentMap : VersionedClassMap<Attainment>
    {
        public AttainmentMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(m => m.Id).GeneratedBy.GuidComb();

            Map(x => x.Text).Not.Nullable().Length(4000);
            Map(x => x.Processed).Not.Nullable();

            Component(x => x.Reward);

            References(x => x.Family).Not.Nullable().ForeignKey("FK_Attainment_Family");
            References(x => x.Creator).Not.Nullable().ForeignKey("FK_Attainment_Creator");
        }
    }
}
