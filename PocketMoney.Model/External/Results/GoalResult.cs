using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class GoalResult : ResultData<GoalView>
    {
        public GoalResult() { }

        public GoalResult(string errorMessage)
            : base(errorMessage)
        {
        }

        public GoalResult(GoalView data) : base(data) { }
    }

    [DataContract]
    public class GoalListResult : ResultList<GoalView>
    {
        public GoalListResult() { }

        public GoalListResult(GoalView[] goalList, int count) : base(goalList, count) { }
    }

    [DataContract]
    public sealed class GoalView : ObjectBase
    {
        public GoalView(Guid goalId, string text, Reward reward, IDictionary<Guid, string> assignedTo)
        {
            this.GoalId = goalId;
            this.Text = text;
            this.Points = reward.Points;
            this.Gift = reward.Gift;
            this.AssignedTo = assignedTo;
        }

        public GoalView(Goal goal)
        {
            this.GoalId = goal.Id;
            this.Text = goal.Details;
            this.Points = goal.Reward.Points;
            this.Gift = goal.Reward.Gift;
            this.AssignedTo = goal.AssignedTo.ToDictionary(k => k.User.Id, e => e.User.FullName(), EqualityComparer<Guid>.Default);

        }

        [DataMember, Details]
        public Guid GoalId { get; set; }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public IDictionary<Guid, string> AssignedTo { get; set; }

        [DataMember, Details]
        public int Points { get; set; }

        [DataMember, Details]
        public string Gift { get; set; }

        [DataMember, Details]
        public string Reward
        {
            get
            {
                return !string.IsNullOrEmpty(this.Gift) ? this.Gift : this.Points.ToString() + " points";
            }
        }

        public string Responsibility
        {
            get
            {
                return string.Join(", ", this.AssignedTo.Select(x => x.Value).ToArray());
            }
        }


    }
}
