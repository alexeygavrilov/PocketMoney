using FluentNHibernate;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class EmailMap : VersionedClassMap<Email>
    {
        public EmailMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Address).Column("EmailAddress").Not.Nullable().UniqueKey("UX_EmailAddress").Length(254);
            Map(x => x.IsPrimary);
            Map(x => x.Name).Not.Nullable();
        }
    }
}
