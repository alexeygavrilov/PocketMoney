using System;
using System.Collections.Generic;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Performer : Entity<Performer, PerformerId, Guid>
    {
        protected Performer()
        {
            this.Actions = new List<TaskAction>();
            this.Status = eTaskStatus.New;
        }

        public Performer(Task task, User user)
            : this()
        {
            this.Task = task;
            this.User = user;
        }

        [Details]
        public virtual Task Task { get; set; }

        [Details]
        public virtual User User { get; set; }

        [Details]
        public virtual eTaskStatus Status { get; set; }

        [Details]
        public virtual IList<TaskAction> Actions { get; set; }

        public virtual void AddAction(TaskAction action)
        {
            this.Actions.Add(action);
        }
    }

    public class PerformerId : GuidIdentity
    {
        public PerformerId(Guid performerId)
            : base(performerId, typeof(Performer))
        {
        }

        public PerformerId()
            : base(Guid.Empty, typeof(Performer))
        {
        }
    }


}
