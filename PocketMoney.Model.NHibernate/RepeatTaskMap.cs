using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class RepeatTaskMap : SubclassMap<RepeatTask>
    {
        public RepeatTaskMap()
        {
            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.Form).Length(4000);

            HasMany(x => x.Dates).KeyColumn("TaskId").LazyLoad().Cascade.Delete();
        }
    }
}
