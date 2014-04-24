using System;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class TaskDate : Entity<TaskDate, TaskDateId, Guid>
    {
        protected TaskDate() { }
        
        public TaskDate(Task task, DayOfOne date)
        {
            this.Task = task;
            this.Date = date;
        }

        public virtual Task Task { get; set; }

        public virtual DayOfOne Date { get; set; }
    }

    [Serializable]
    public class TaskDateId : GuidIdentity
    {
        public TaskDateId()
            : base(Guid.Empty, typeof(TaskDate))
        {
        }

        public TaskDateId(string id)
            : base(id, typeof(TaskDate))
        {
        }

        public TaskDateId(Guid id)
            : base(id, typeof(TaskDate))
        {
        }
    }
}
