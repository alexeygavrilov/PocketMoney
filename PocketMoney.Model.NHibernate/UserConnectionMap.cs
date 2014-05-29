using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class UserConnectionMap : VersionedClassMap<UserConnection>
    {
        public UserConnectionMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.ClientType).Not.Nullable().UniqueKey("UX_Connecton_Identity");
            Map(x => x.Identity).Not.Nullable().Length(255).UniqueKey("UX_Connecton_Identity");
            Map(x => x.LastLoginDate);

            References(x => x.User).Not.Nullable().ForeignKey("FK_UserConnection_User");
        }
    }
}
