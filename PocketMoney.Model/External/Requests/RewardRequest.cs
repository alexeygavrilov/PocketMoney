using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class RewardRequest : Request
    {
        [DataMember]
        [DataType(DataType.Currency)]
        [Display(Name = "Points")]
        [Details]
        public int Points { get; set; }

        [DataMember]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Gift")]
        [Details]
        public string Gift { get; set; }

        [DataMember, Details]
        [Display(Name = "Assigned To")]
        public Guid[] AssignedTo { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Gift) && this.Points <= 0)
                yield return new ValidationResult("Reward should contais points or gift");
        }

    }
}
