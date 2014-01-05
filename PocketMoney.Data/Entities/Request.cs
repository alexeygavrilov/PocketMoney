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

    [DataContract]
    public abstract class RequestClass<TClass> : Request where TClass : class
    {
        public RequestClass()
        {
        }

        [DataMember, Details]
        public new TClass Data { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Data == default(TClass))
                yield return new ValidationResult("Входные данные отсутствуют");

        }
    }
}
