using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class WithdrawRequest  : Request
    {
        [DataMember]
        [DataType(DataType.Currency)]
        [Display(Name = "Points")]
        [Details]
        public int Points { get; set; }

        [DataMember, Details]
        public Guid UserId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Points <= 0)
                yield return new ValidationResult("Points number should be positive");
        }

    }
}
