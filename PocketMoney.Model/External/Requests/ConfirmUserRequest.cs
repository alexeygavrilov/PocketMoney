using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class ConfirmUserRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Код подтверждения - это обязательное поле")]
        [Display(Name = "Код подтверждения")]
        [Details]
        public string ConfirmCode { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.ConfirmCode))
                yield return new ValidationResult("Код подтверждения - это обязательное поле");
        }
    }
}
