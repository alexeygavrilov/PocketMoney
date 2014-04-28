using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddHomeworkTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        public HomeworkForm Form { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Form == null)
                yield return new ValidationResult("Invalid form");

            if (this.Form.DateRangeFrom >= this.Form.DateRangeTo)
                yield return new ValidationResult("Invalid date range");

            if (this.Form.DaysOfWeek == null || this.Form.DaysOfWeek.Length == 0)
                yield return new ValidationResult("Days of Week are required value");

            foreach (var val in base.Validate(validationContext))
                yield return val;
        }
    }
}
