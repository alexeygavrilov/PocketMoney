using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Data
{

    [DataContract]
    public abstract class RequestData<TData> : Request
    {
        [DataMember, Details]
        public new TData Data { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Data.Equals(default(TData)))
                yield return new ValidationResult("Входные данные отсутствуют");
        }
    }

    [DataContract]
    public abstract class Request : ObjectBase, IValidatableObject
    {
        public static readonly Request Empty = new EmptyRequest();

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        private sealed class EmptyRequest : Request
        {
            public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                return new List<ValidationResult>();
            }
        }
    }
}
