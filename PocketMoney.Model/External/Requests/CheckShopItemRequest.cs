using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class CheckShopItemRequest : Request
    {
        [DataMember, Details]
        public Guid TaskId { get; set; }

        [DataMember, Details]
        public int OrderNumber { get; set; }

        [DataMember, Details]
        public bool Checked { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.TaskId == Guid.Empty)
                yield return new ValidationResult("Task identifier is required");

        }


    }
}
