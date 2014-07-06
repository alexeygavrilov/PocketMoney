using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddAttainmentRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Attainment is required field")]
        [Display(Name = "Attainment")]
        [Details]
        public string Text { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Text))
                yield return new ValidationResult("Attainment text is required field");
        }
    }

    [DataContract]
    public class AppointRewardRequest : RewardRequest
    {
        [DataMember, Details]
        public Guid Id { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Id == Guid.Empty)
                yield return new ValidationResult("Attainment identifier is required");

            foreach (var val in base.Validate(validationContext))
                yield return val;
        }

    }
}
