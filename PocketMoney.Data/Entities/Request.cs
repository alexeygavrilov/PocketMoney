using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Data
{
    [DataContract]
    public abstract class Request : ObjectBase, IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
