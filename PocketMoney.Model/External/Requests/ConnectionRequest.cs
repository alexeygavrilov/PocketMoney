using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class ConnectionRequest : Request
    {
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Идентификатор - это обязательное поле")]
        [Details]
        public string Identity { get; set; }

        [DataMember]
        [Details]
        public ClientType ConnectionType { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Identity))
                yield return new ValidationResult(string.Format("Соединение '{0}' должно иметь идентификатор", this.ConnectionType));
        }
    }
}
