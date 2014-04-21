using System;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class OneTimeTaskResult : Result
    {
        [Details, DataMember]
        public Guid TaskId { get; set; }

        protected override void ClearData()
        {
            this.TaskId = Guid.Empty;
        }
    }
}
