using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class LoginRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Имя - это обязательное поле")]
        [Display(Name = "Имя или Email пользователя")]
        public string UserName { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Пароль - это обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
                yield return new ValidationResult("Имя - это обязательное поле");

            if (string.IsNullOrWhiteSpace(this.Password))
                yield return new ValidationResult("Пароль - это обязательное поле");
        }
    }
}
