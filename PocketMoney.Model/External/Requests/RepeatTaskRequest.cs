﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;
using System;


namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddRepeatTaskRequest : BaseTaskRequest
    {
        [DataMember, Details]
        public string Name { get; set; }

        [DataMember, Details]
        public RepeatForm Form { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Name))
                yield return new ValidationResult("Name is required field");

            if (this.Form == null)
                yield return new ValidationResult("Invalid form");

            if (this.Form.DateRangeTo.HasValue && this.Form.DateRangeFrom >= this.Form.DateRangeTo.Value)
                yield return new ValidationResult("Invalid date range");

            if (this.Form.OccurrenceType == eOccurrenceType.None)
                yield return new ValidationResult("You should select the repeat mode.");

            if (this.Form.OccurrenceType == eOccurrenceType.Week && (this.Form.DaysOfWeek == null || this.Form.DaysOfWeek.Length == 0))
                yield return new ValidationResult("Days of Week are required value");

            foreach (var val in base.Validate(validationContext))
                yield return val;

        }
    }

    [DataContract]
    public class UpdateRepeatTaskRequest : AddRepeatTaskRequest, IIdentity
    {
        [DataMember, Details]
        public Guid Id { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Id == Guid.Empty)
                yield return new ValidationResult("Task identifier is required");

            foreach (var val in base.Validate(validationContext))
                yield return val;

        }
    }

}
