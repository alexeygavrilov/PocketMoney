using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.External.Results
{
    public class DutyTaskResult : Result
    {
        [Details, DataMember]
        public IUser User { get; set; }

        [Details, DataMember]
        public string Name { get; set; }

        [DataMember, Details]
        public ScheduleForm Form { get; set; }

        [Details, DataMember]
        public DayOfOne[] Days { get; set; }

        protected override void ClearData()
        {
            this.User = null;
            this.Name = string.Empty;
            this.Days = new DayOfOne[0];
        }

    }
}
