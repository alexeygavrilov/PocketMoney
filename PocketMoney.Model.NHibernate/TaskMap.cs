using PocketMoney.Data.NHibernate;
using PocketMoney.FileSystem;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.NHibernate
{
    public sealed class TaskMap : VersionedClassMap<Task>
    {
        public TaskMap()
        {
            Cache.ReadWrite().Region("oftenused");

            Id(m => m.Id).GeneratedBy.GuidComb();

            Map(x => x.Details).Not.Nullable().Length(4000);
            Map(x => x.Active).Not.Nullable();
            Map(x => x.EnableDateRange).Not.Nullable();
            Map(x => x.Reminder).Nullable();

            Component(x => x.Type);
            Component(x => x.Points);

            References(x => x.Family).Not.Nullable().ForeignKey("FK_Task_Family");
            References(x => x.Creator).Not.Nullable().ForeignKey("FK_Task_Creator");

            HasMany(x => x.AssignedTo).LazyLoad().ForeignKeyConstraintName("FK_Performer_Task").Not.KeyUpdate();

            HasManyToMany(x => x.Attachments).LazyLoad().Table("TasksFiles").ForeignKeyConstraintNames("FK_Task_File", "FK_File_Task");
        }
    }

    public class TasksFilesMapping : HasManyToManyMapping<Task, File> { public TasksFilesMapping() : base("TasksFiles", "TaskId", "FileId", x => x.ParentKey, x => x.ChildKey) { } }
}
