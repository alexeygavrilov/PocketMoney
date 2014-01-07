using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model
{
    [DataContract]
    public class DayOfOne : ObjectBase
    {
        [DataMember]
        public byte Year { get; set; }

        [DataMember]
        public byte Month { get; set; }

        [DataMember]
        public byte Day { get; set; }
    }
}
