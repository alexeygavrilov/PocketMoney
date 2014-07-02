using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public abstract class BaseTaskRequest : RewardRequest
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Task details is required field")]
        [Display(Name = "Task details")]
        [Details]
        public string Text { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Reminder")]
        [Details]
        public TimeSpan? ReminderTime { get; set; }
    }

}
