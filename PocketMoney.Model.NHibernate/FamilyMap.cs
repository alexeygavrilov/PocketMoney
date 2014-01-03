using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class FamilyMap : VersionedClassMap<Family>
    {
        public FamilyMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Name).Not.Nullable().Length(100).UniqueKey("UX_FamilyName");
            Map(x => x.Description).Length(500);

            HasMany(x => x.Members).LazyLoad().Cascade.Delete().ForeignKeyConstraintName("FK_Family_Users");
            
        }
    }
}
