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
        public override object Data
        {
            get
            {
                return this.Name;
            }
        }

        [Details, DataMember]
        public IUser User { get; set; }

        [Details, DataMember]
        public string Name { get; set; }

        [Details, DataMember]
        public DayOfYear[] DutyDays { get; set; }

    }
}
