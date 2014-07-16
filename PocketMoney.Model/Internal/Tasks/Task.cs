using PocketMoney.Data;
using PocketMoney.FileSystem;
using System;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public abstract class Task : Entity<Task, TaskId, Guid>, IObject
    {
        public const string TITLE_FORMAT = "Task: {0}";

        protected Task()
        {
            this.AssignedTo = new List<Performer>();
            this.Attachments = new List<File>();
            this.Reward = new Reward(this, 0);
            this.Active = true;
        }

        public Task(TaskType type, string details, Reward reward, User creator)
            : this()
        {
            this.HasDates = type == TaskType.HomeworkTask || type == TaskType.RepeatTask;
            this.Type = type;
            this.Details = details;
            this.Reward = new Reward(this, reward.Points, reward.Gift);
            this.Creator = creator;
            this.Family = creator.Family;
        }

        [Details]
        public virtual bool HasDates { get; set; }

        [Details]
        public virtual string Details { get; set; }

        [Details]
        public virtual IList<Performer> AssignedTo { get; set; }

        [Details]
        public virtual TaskType Type { get; set; }

        [Details]
        public virtual Reward Reward { get; set; }

        [Details]
        public virtual Family Family { get; set; }

        [Details]
        public virtual User Creator { get; set; }

        [Details]
        public virtual int? Reminder { get; set; }

        [Details]
        public virtual bool Active { get; set; }

        [Details]
        public virtual IList<File> Attachments { get; set; }

        public virtual eObjectType ObjectType
        {
            get { return eObjectType.Task; }
        }

        public abstract string Name { get; }

        public virtual void Remove()
        {
            this.Active = false;
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
