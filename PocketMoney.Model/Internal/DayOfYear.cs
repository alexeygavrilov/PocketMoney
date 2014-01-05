using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Model.Internal
{
    [DataContract]
    public struct DayOfYear
    {
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public short Day { get; set; }
    }
}
