using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class ActionCountMap : ComponentMap<ActionCount>
    {
        public ActionCountMap()
        {
            Map(x => x.CompletedTasks).Column("CompletedTasksCount").Not.Nullable();
            Map(x => x.GrabbedTasks).Column("GrabbedTasksCount").Not.Nullable();
            Map(x => x.CompletedGoals).Column("CompletedGoalsCount").Not.Nullable();
            Map(x => x.GoodWorks).Column("GoodWorksCount").Not.Nullable();

            ParentReference(x => x.Parent);
        }
    }
}
