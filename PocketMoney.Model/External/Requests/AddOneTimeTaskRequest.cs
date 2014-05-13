using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddOneTimeTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        [Display(Name = "Deadline Date")]
        public DateTime? DeadlineDate { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Name))
                yield return new ValidationResult("Name is required field");

            foreach (var val in base.Validate(validationContext))
                yield return val;

        }

    }
}
