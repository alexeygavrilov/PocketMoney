using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class RewardView : ObjectBase
    {
        protected RewardView(Reward reward)
        {
            if (reward != null)
            {
                this.Points = reward.Points;
                this.Gift = reward.Gift;
                this.Reward = !string.IsNullOrEmpty(this.Gift) ? this.Gift : this.Points > 0 ? this.Points.ToString() + " points" : "Unassigned";
            }
            else
            {
                this.Reward = "Unassigned";
            }
        }

        [DataMember, Details]
        public int Points { get; set; }

        [DataMember, Details]
        public string Gift { get; set; }

        [DataMember, Details]
        public string Reward { get; private set; }



    }
}
