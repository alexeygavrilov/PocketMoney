using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Model.NHibernate
{
    public sealed class OneTimeTaskMap : SubclassMap<OneTimeTask>
    {
        public OneTimeTaskMap()
        {
            Map(x => x.DeadlineDate).Nullable();
        }
    }
}
