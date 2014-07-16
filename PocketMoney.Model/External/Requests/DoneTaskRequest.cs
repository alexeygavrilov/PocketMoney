using PocketMoney.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class ProcessRequest : Request
    {
        [DataMember, Details]
        public Guid Id { get; set; }

        [DataMember, Details]
        public string Note { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Id == Guid.Empty)
                yield return new ValidationResult("Identifier is required");
        }
    }

    [DataContract]
    public class DoneTaskRequest : ProcessRequest
    {
        [DataMember, Details]
        public eDateType DateType { get; set; }
    }
}
