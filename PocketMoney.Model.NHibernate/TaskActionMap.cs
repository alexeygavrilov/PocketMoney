using FluentNHibernate.Mapping;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class TaskActionMap : ClassMap<TaskAction>
    {
        public TaskActionMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(x => x.Id).GeneratedBy.GuidComb();

            Map(x => x.Action).Not.Nullable();
            Map(x => x.ActionDate).Not.Nullable();
            Map(x => x.Note).Length(500);
            Map(x => x.Source).Length(1000);

            References(x => x.Performer).Not.Nullable().ForeignKey("FK_TaskAction_Performer");
        }
    }
}
