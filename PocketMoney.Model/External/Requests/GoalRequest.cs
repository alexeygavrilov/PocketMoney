﻿using PocketMoney.Data;
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
        [Required(ErrorMessage = "Goal is required field")]
        [Display(Name = "Goal")]
        [Details]
        public string Text { get; set; }

        [DataMember, Details]
        [Display(Name = "Assigned To")]
        public Guid[] AssignedTo { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Text))
                yield return new ValidationResult("Goal text is required field");

            foreach (var val in base.Validate(validationContext))
                yield return val;
        }
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
