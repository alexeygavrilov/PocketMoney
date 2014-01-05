using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class RegisterUserRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Наименование семьи - это обязательное поле")]
        [Display(Name = "Наименование семьи")]
        [Details]
        public string FamilyName { get; set; }

        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Имя - это обязательное поле")]
        [Display(Name = "Имя пользователя")]
        [Details]
        public string UserName { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Email - это обязательное поле")]
        [RegularExpression(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Некорректный Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Details]
        public string Email { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataMember(IsRequired = true)]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [StringLength(100, ErrorMessage = "{0} должен быть больше чем {2} символа.", MinimumLength = User.MinRequiredPasswordLength)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают, Попробуйте еще раз.")]
        public string ConfirmPassword { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Страна - это обязательное поле")]
        [Display(Name = "Страна")]
        [Details]
        public int CountryCode { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.FamilyName))
                yield return new ValidationResult("Наименование семьи - это обязательное поле");

            if (string.IsNullOrWhiteSpace(this.UserName))
                yield return new ValidationResult("Глава семьи - это обязательное поле");

            if (string.IsNullOrWhiteSpace(this.Email))
                yield return new ValidationResult("Email - это обязательное поле");

            if (this.CountryCode == 0)
                yield return new ValidationResult("Страна - это обязательное поле");

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult("Пароль обязателен");

            if (!Internal.Email.REGEX_VALIDATION.IsMatch(this.Email))
                yield return new ValidationResult("Некорректный символ в Email");

            if (Password.Length < User.MinRequiredPasswordLength)
                yield return new ValidationResult(string.Format("Пароль должен быть больше чем {0} символа.", User.MinRequiredPasswordLength));

            if (!Password.Equals(ConfirmPassword))
                yield return new ValidationResult("Пароли не совпадают, Попробуйте еще раз.");

        }
    }
}
