using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class TaskTypeMap : ComponentMap<TaskType>
    {
        public TaskTypeMap()
        {
            Map(x => x.Id).Column("TaskType").Not.Nullable();
            Map(x => x.Name).Column("TaskTypeName").Not.Nullable();
        }
    }
}
