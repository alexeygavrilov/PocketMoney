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
    }

    public class GoalId : GuidIdentity
    {
        public GoalId(Guid goalId)
            : base(goalId, typeof(Goal))
        {
        }

        public GoalId()
            : base(Guid.Empty, typeof(Goal))
        {
        }
    }

}
