using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class TaskAction : Entity<TaskAction, TaskActionId, Guid>
    {
        protected TaskAction()
        {
        }

        public TaskAction(eTaskAction action, DateTime actionDate, Performer performer, string note)
        {
            this.Action = action;
            this.ActionDate = actionDate;
            this.Performer = performer;
            this.Note = note;
        }

        [Details]
        public virtual eTaskAction Action { get; set; }

        [Details]
        public virtual DateTime ActionDate { get; set; }

        [Details]
        public virtual Performer Performer { get; set; }

        [Details]
        public virtual string Note { get; set; }

        [Details]
        public virtual string Source { get; set; }

    }

    public enum eTaskAction : int
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
