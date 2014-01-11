using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.Network
{
    [DataContract]
    public class AuthRequest : NetworkRequest
    {
        [DataMember, Details]
        public string ApiId { get; set; }

        [DataMember, Details]
        public string ApiKey { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var val in base.Validate(validationContext))
            {
                yield return val;
            }
            if (string.IsNullOrWhiteSpace(this.ApiId))
                yield return new ValidationResult("Социальная сеть должна иметь идентификтор приложения");

            if (string.IsNullOrWhiteSpace(this.ApiKey))
                yield return new ValidationResult("Социальная сеть должна иметь секретный ключ");
        }
    }

}
