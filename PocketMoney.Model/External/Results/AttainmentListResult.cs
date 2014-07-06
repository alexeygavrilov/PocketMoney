using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    public class AttainmentListResult : ResultList<AttainmentView>
    {
        public AttainmentListResult() { }

        public AttainmentListResult(AttainmentView[] attainmentList, int count) : base(attainmentList, count) { }
    }

    public class AttainmentResult : ResultData<AttainmentView>
    {
        public AttainmentResult() { }

        public AttainmentResult(string errorMessage)
            : base(errorMessage)
        {
        }

        public AttainmentResult(AttainmentView data) : base(data) { }
    }

    public class AttainmentView : RewardView
    {
        public AttainmentView(Guid attainmentId, string text, Reward reward, Guid userId, string userName, DateTime createdDate)
            : base(reward)
        {
            this.AttainmentId = attainmentId;
            this.Text = text;
            this.UserId = userId;
            this.UserName = userName;
            this.CreatedDate = createdDate.ToShortDateString();
        }

        public AttainmentView(Attainment attainment) : base(attainment.Reward)
        {
            this.AttainmentId = attainment.Id;
            this.Text = attainment.Text;
            this.UserId = attainment.Creator.Id;
            this.UserName = attainment.Creator.FullName();
            this.CreatedDate = attainment.DateCreated.Value.ToShortDateString();
        }

        [DataMember, Details]
        public Guid AttainmentId { get; set; }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public Guid UserId { get; set; }

        [DataMember, Details]
        public string UserName { get; set; }

        [DataMember, Details]
        public string CreatedDate { get; set; }

    }
}
