using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class OneTimeTask : Task
    {
        protected OneTimeTask() : base() { }

        public OneTimeTask(string details, int points, DateTime? deadlineDate, User creator)
            : base(TaskType.OneTimeTask, details, points, creator)
        {
            this.DeadlineDate = deadlineDate;
        }

        [Details]
        public virtual DateTime? DeadlineDate { get; set; }

    }
}
