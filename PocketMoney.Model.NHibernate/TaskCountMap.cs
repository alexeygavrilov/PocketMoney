using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class TaskCountMap : ComponentMap<TaskCount>
    {
        public TaskCountMap()
        {
            Map(x => x.Completed).Column("CompletedTaskCount").Not.Nullable();
            Map(x => x.Grabbed).Column("GrabbedTaskCount").Not.Nullable();

            ParentReference(x => x.Parent);
        }
    }
}
