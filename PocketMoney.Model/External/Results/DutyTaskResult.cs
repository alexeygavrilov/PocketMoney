using System.Runtime.Serialization;
using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class DutyTaskResult : Result
    {
        [DataMember, Details]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperUser>))]
        public IUser AssignedTo { get; set; }

        [DataMember, Details]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperUser>))]
        public IUser CreatedBy { get; set; }

        [Details, DataMember]
        public string Name { get; set; }

        [DataMember, Details]
        public int Reward { get; set; }

        [DataMember, Details]
        public HomeworkForm Form { get; set; }

        [Details, DataMember]
        public DayOfOne[] Days { get; set; }

        protected override void ClearData()
        {
            this.AssignedTo = null;
            this.CreatedBy = null;
            this.Form = null;
            this.Reward = 0;
            this.Name = string.Empty;
            this.Days = new DayOfOne[0];
        }

    }
}
