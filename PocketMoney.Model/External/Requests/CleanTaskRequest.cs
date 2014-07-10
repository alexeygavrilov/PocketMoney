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
        public string RoomName { get; set; }

        [DataMember, Details]
        public bool EveryDay { get; set; }

        [DataMember, Details]
        [Display(Name = "Days of Week")]
        public int[] DaysOfWeek { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.RoomName))
                yield return new ValidationResult("Room name is required field");

            if (!this.EveryDay && (this.DaysOfWeek == null || this.DaysOfWeek.Length == 0))
                yield return new ValidationResult("Days of Week are required value");

            foreach (var val in base.Validate(validationContext))
                yield return val;

        }

        public eDaysOfWeek GetDaysOfWeek()
        {
            eDaysOfWeek days = eDaysOfWeek.None;
            if (!this.EveryDay)
            {
                foreach (int d in this.DaysOfWeek)
                {
                    days |= ((DayOfWeek)d).To();
                }
            }
            return days;
        }
    }

    [DataContract]
    public class UpdateCleanTaskRequest : AddCleanTaskRequest, IIdentity
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
