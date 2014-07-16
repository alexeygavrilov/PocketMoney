using FluentNHibernate.Mapping;
using NHibernate.Type;
using PocketMoney.Data.NHibernate;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class TaskActionMap : VersionedClassMap<TaskAction>
    {
        public TaskActionMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.NewStatus).Not.Nullable();
            Map(x => x.Note).Length(500);
            Map(x => x.Source).Length(1000);

            References(x => x.TaskDate).Nullable().ForeignKey("FK_TaskAction_TaskDate");
            References(x => x.Performer).Not.Nullable().ForeignKey("FK_TaskAction_Performer");
        }
    }
}
