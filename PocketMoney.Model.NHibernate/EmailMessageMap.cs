using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using NHibernate.Type;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class EmailMessageMap : ClassMap<EmailMessage>
    {
        public EmailMessageMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Subject).Not.Nullable().Length(255);
            Map(x => x.Body).Not.Nullable().CustomSqlType("ntext").LazyLoad();
            Map(x => x.Success).Not.Nullable();
            Map(x => x.CreatedOn).Not.Nullable().CustomType<UtcDateTimeType>();
            Map(x => x.Processed).Not.Nullable();
            Map(x => x.SentOn).Nullable().CustomType<UtcDateTimeType>();

            References(x => x.Receiver).Not.Nullable().ForeignKey("FK_EmailMessage_Email");
            References(x => x.Creator).Not.Nullable().ForeignKey("FK_EmailMessage_User");
            References(x => x.Attachment).Nullable().ForeignKey("FK_EmailMessage_File");
        }
    }
}
