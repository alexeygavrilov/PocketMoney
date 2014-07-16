using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results.Clients
{
    [DataContract]
    public class AttainmentListResult : ResultList<AttainmentView>
    {
        public AttainmentListResult() { }

        public AttainmentListResult(AttainmentView[] attainmentList, int count) : base(attainmentList, count) { }
    }

    [DataContract]
    public class AttainmentResult : ResultData<AttainmentView>
    {
        public AttainmentResult() { }

        public AttainmentResult(string errorMessage)
            : base(errorMessage)
        {
        }

        public AttainmentResult(AttainmentView data) : base(data) { }
    }

    [DataContract]
    public class AttainmentView : ObjectBase
    {
        public AttainmentView(Guid attainmentId, string text, Reward reward, DateTime createdDate)
        {
            this.AttainmentId = attainmentId;
            this.Text = text;
            this.Reward = reward.ToString();
            this.CreatedDate = createdDate.ToShortDateString();
        }

        public AttainmentView(Attainment attainment)
        {
            this.AttainmentId = attainment.Id;
            this.Text = attainment.Text;
            this.Reward = attainment.Reward.ToString();
            this.CreatedDate = attainment.DateCreated.Value.ToShortDateString();
        }

        [DataMember, Details]
        public Guid AttainmentId { get; set; }

        [DataMember, Details]
        public string Text { get; set; }

        [DataMember, Details]
        public string Reward { get; private set; }

        [DataMember, Details]
        public string CreatedDate { get; set; }
    }

}
