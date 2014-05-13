using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model
{
    [DataContract, Serializable]
    public class RepeatForm : ObjectBase
    {
        [DataMember, Details]
        [Display(Name = "From")]
        public DateTime DateRangeFrom { get; set; }

        [DataMember, Details]
        [Display(Name = "To")]
        public DateTime? DateRangeTo { get; set; }

    }
}
