using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddGoalRequest : RewardRequest
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Attainment is required field")]
        [Display(Name = "Attainment")]
        [Details]
        public string Text { get; set; }

    }

    public class UpdateGoalRequest : AddGoalRequest, IIdentity
    {
        [DataMember, Details]
        public Guid Id { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Id == Guid.Empty)
                yield return new ValidationResult("Goal identifier is required");

            foreach (var val in base.Validate(validationContext))
                yield return val;
        }
    }
}
