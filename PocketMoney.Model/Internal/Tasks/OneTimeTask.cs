using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class OneTimeTask : Task
    {
        protected OneTimeTask() : base() { }

        public OneTimeTask(string name, string details, int points, DateTime? deadlineDate, User creator)
            : base(TaskType.OneTimeTask, details, points, creator)
        {
            this.Name = name;
            this.DeadlineDate = deadlineDate;
        }

        [Details]
        public virtual string Name { get; set; }

        [Details]
        public virtual DateTime? DeadlineDate { get; set; }

        public override string Title()
        {
            return "Task: " + this.Name;
        }
    }
}
