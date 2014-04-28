using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddCleanTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        public bool EnableScheduling { get; set; }

        [DataMember, Details]
        [Display(Name = "Days of Week")]
        public int[] DaysOfWeek { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.EnableScheduling && (this.DaysOfWeek == null || this.DaysOfWeek.Length == 0))
                yield return new ValidationResult("Days of Week are required value");
        }
    }
}
