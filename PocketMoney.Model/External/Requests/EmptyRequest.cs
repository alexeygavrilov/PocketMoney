using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class EmptyRequest : Request
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
