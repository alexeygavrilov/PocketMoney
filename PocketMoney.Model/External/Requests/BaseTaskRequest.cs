using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public abstract class BaseTaskRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Task details is required field")]
        [Display(Name = "Task details")]
        [Details]
        public string Text { get; set; }

        [DataMember, Details]
        [Display(Name = "Assigned To")]
        public Guid[] AssignedTo { get; set; }

        [DataMember(IsRequired = true)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Score Points should be positive number")]
        [Display(Name = "Score Points")]
        [Details]
        public int Points { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Reminder")]
        [Details]
        public TimeSpan? ReminderTime { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Points <= 0)
                yield return new ValidationResult("Score Points should be positive number");
        }

    }
}
