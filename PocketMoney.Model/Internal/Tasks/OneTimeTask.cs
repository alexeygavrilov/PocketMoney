﻿using PocketMoney.Data;
using System;

namespace PocketMoney.Model.Internal
{
    public class OneTimeTask : Task
    {
        protected OneTimeTask() : base() { }

        public OneTimeTask(string name, string details, Reward reward, DateTime? deadlineDate, User creator)
            : base(TaskType.OneTimeTask, details, reward, creator)
        {
            this.OneTimeName = name;
            this.DeadlineDate = deadlineDate;
        }

        [Details]
        public virtual string OneTimeName { get; set; }

        [Details]
        public virtual DateTime? DeadlineDate { get; set; }

        [Details]
        public override string Name
        {
            get { return FormatTitle(this.OneTimeName); }
        }

        public static string FormatTitle(string name)
        {
            return string.Format(TITLE_FORMAT, name);
        }

    }
}
