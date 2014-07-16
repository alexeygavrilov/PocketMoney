using PocketMoney.Data;
using PocketMoney.FileSystem;
using System;
using System.Collections.Generic;

namespace PocketMoney.Model.Internal
{
    public class Goal : Task
    {
        protected Goal() : base()
        {
        }

        public Goal(string text, Reward reward, User creator)
            : base(TaskType.Goal, text, reward, creator)
        {
        }

        public override string Name
        {
            get { return this.Details; }
        }
    }


}
