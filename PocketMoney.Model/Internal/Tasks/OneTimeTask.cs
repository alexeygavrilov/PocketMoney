using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class OneTimeTask : Task
    {
        protected OneTimeTask() : base() { }

        public OneTimeTask(string details, int points, DateTime? deadlineDate)
            : base(TaskType.OneTimeTask, details, points, true)
        {
            this.DeadlineDate = deadlineDate;
        }

        [Details]
        public virtual DateTime? DeadlineDate { get; set; }

    }
}
