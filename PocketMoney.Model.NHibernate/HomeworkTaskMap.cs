using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Model.NHibernate
{
    public sealed class HomeworkTaskMap : SubclassMap<HomeworkTask>
    {
        public HomeworkTaskMap()
        {
            Map(x => x.Form).Length(4000);

            HasMany(x => x.Dates).KeyColumn("TaskId").LazyLoad().Cascade.Delete();
        }
    }
}
