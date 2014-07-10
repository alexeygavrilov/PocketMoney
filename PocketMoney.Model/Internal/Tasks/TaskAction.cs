using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class TaskAction : Entity<TaskAction, TaskActionId, Guid>
    {
        protected TaskAction()
        {
        }

        public TaskAction(eTaskStatus newStatus, TaskDate taskDate, Performer performer, string note)
        {
            this.NewStatus = newStatus;
            this.TaskDate = taskDate;
            this.Performer = performer;
            this.Note = note;
        }

        [Details]
        public virtual eTaskStatus NewStatus { get; set; }

        [Details]
        public virtual TaskDate TaskDate { get; set; }

        [Details]
        public virtual Performer Performer { get; set; }

        [Details]
        public virtual string Note { get; set; }

        [Details]
        public virtual string Source { get; set; }

    }

    

    public class TaskActionId : GuidIdentity
    {
        public TaskActionId(Guid id)
            : base(id, typeof(TaskAction))
        {
        }

        public TaskActionId()
            : base(Guid.Empty, typeof(TaskAction))
        {
        }
    }
}
