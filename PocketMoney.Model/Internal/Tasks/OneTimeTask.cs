﻿using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class OneTimeTask : Task
    {
        protected OneTimeTask() : base() { }

        public OneTimeTask(string name, string details, int points, DateTime? deadlineDate, User creator)
            : base(TaskType.OneTimeTask, details, points, creator)
        {
            this.OneTimeName = name;
            this.DeadlineDate = deadlineDate;
        }

        [Details]
        public virtual string OneTimeName { get; set; }

        [Details]
        public virtual DateTime? DeadlineDate { get; set; }

    }
}
