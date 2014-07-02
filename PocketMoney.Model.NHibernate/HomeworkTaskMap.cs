using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class HomeworkTaskMap : SubclassMap<HomeworkTask>
    {
        public HomeworkTaskMap()
        {
            Map(x => x.Form).Length(4000);
            Map(x => x.Lesson).Nullable().Length(50);

            HasMany(x => x.Dates).KeyColumn("TaskId").LazyLoad().Not.KeyUpdate();
        }
    }
}
