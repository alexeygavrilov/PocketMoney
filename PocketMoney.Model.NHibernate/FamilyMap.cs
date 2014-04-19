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
            Map(x => x.Culture).Length(10);
            Map(x => x.TokenKey).Not.Nullable().Length(Family.TOKEN_KEY_LENGTH);

            References(x => x.Country).Not.Nullable().ForeignKey("FK_Family_Country");

            HasMany(x => x.Members).LazyLoad().Cascade.Delete().ForeignKeyConstraintName("FK_Family_Users");
            
        }
    }
}
