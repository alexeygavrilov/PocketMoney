using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.External.Results
{
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
        public ScheduleForm Form { get; set; }

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
