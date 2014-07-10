using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public class TaskDateMap : ClassMap<TaskDate>
    {
        public TaskDateMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Component(x => x.Date);

            References<Task>(x => x.Task).Column("TaskId").Not.Nullable().Not.LazyLoad().ForeignKey("FK_Task_Date");

            HasMany(x => x.Actions).Not.LazyLoad().Cascade.Delete();

        }
    }
}
