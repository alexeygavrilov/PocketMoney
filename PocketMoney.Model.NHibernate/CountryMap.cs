using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Id(x => x.Id).Column("Code").GeneratedBy.Assigned();

            Map(x => x.Name).Not.Nullable().UniqueKey("UX_Country").Length(254);
        }
    }
}
