using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class TaskAction : Entity<TaskAction, TaskActionId, Guid>
    {
        protected TaskAction()
        {
        }

        public TaskAction(eTaskActionType actionType, TaskDate taskDate, Performer performer, string note)
        {
            this.ActionType = actionType;
            this.TaskDate = taskDate;
            this.Performer = performer;
            this.Note = note;
        }

        [Details]
        public virtual eTaskActionType ActionType { get; set; }

        [Details]
        public virtual TaskDate TaskDate { get; set; }

        [Details]
        public virtual Performer Performer { get; set; }

        [Details]
        public virtual string Note { get; set; }

        [Details]
        public virtual string Source { get; set; }

    }

    public enum eTaskActionType : int
    {
        None = 0,
        Processed = 1,
        Closed = 2
        
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
