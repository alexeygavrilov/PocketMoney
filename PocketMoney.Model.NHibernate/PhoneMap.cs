using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class PhoneNumberMapping : VersionedClassMap<PhoneNumber>
    {
        public PhoneNumberMapping()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Description).Nullable().Length(250);
            Map(x => x.Number).Not.Nullable().UniqueKey("UX_PhoneNumber").Length(30);
            Map(x => x.PhoneType).Not.Nullable();
        }
    }
}
