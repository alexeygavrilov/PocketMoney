using PocketMoney.Data;
using PocketMoney.FileSystem;
using System;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public abstract class Task : Entity<Task, TaskId, Guid>, IObject
    {
        protected Task()
        {
            this.AssignedTo = new List<Performer>();
            this.Attachments = new List<File>();
            this.Points = new Point(this, 0);
        }

        public Task(TaskType type, string details, int points, bool single)
            : this()
        {
            this.Active = true;
            this.Type = type;
            this.Details = details;
            this.Points = new Point(this, points);
            this.Single = single;
        }

        [Details]
        public virtual string Details { get; set; }

        [Details]
        public virtual IList<Performer> AssignedTo { get; set; }

        [Details]
        public virtual TaskType Type { get; set; }

        [Details]
        public virtual bool Active { get; set; }

        [Details]
        public virtual Point Points { get; set; }

        [Details]
        public virtual Family Family { get; set; }

        [Details]
        public virtual User Creator { get; set; }

        [Details]
        public virtual int Reminder { get; set; }

        [Details]
        public virtual IList<File> Attachments { get; set; }

        [Details]
        public virtual bool Single { get; set; }

        public virtual eObjectType ObjectType
        {
            get { return eObjectType.Task; }
        }
    }

    public class TaskId : GuidIdentity
    {
        public TaskId(Guid taskId)
            : base(taskId, typeof(Task))
        {
        }

        public TaskId()
            : base(Guid.Empty, typeof(Task))
        {
        }
    }

}
