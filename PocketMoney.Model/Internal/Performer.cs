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
        }

        public Performer(Task task, User user)
            : this()
        {
            this.Task = task;
            this.User = user;
            this.Active = true;
        }

        [Details]
        public virtual Task Task { get; set; }

        [Details]
        public virtual User User { get; set; }

        [Details]
        public virtual IList<TaskAction> Actions { get; set; }

        [Details]
        public virtual bool Active { get; set; }

        public virtual void AddAction(TaskAction action)
        {
            this.Actions.Add(action);
        }

        public virtual void Remove()
        {
            this.Active = false;
        }
    }


    public class PerformerId : GuidIdentity
    {
        public PerformerId(Guid id)
            : base(id, typeof(Performer))
        {
        }

        public PerformerId()
            : base(Guid.Empty, typeof(Performer))
        {
        }
    }

}
