using System;
using PocketMoney.Data;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class TaskDate : Entity<TaskDate, TaskDateId, Guid>
    {
        protected TaskDate()
        {
            this.Actions = new List<TaskAction>();
        }

        public TaskDate(Task task, DayOfOne date)
            : this()
        {
            this.Task = task;
            this.Date = date;
        }

        public virtual Task Task { get; set; }

        public virtual DayOfOne Date { get; set; }

        public virtual IList<TaskAction> Actions { get; set; }
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
