using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public abstract class BaseUserRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "User Name is required field")]
        [Display(Name = "User Name")]
        [Details]
        public string UserName { get; set; }

        [DataMember, Details]
        [DataType(DataType.Text)]
        [Display(Name = "Additional User Name")]
        public string AdditionalName { get; set; }

        [DataMember, Details]
        [RegularExpression(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Некорректный Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataMember, Details]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
                yield return new ValidationResult("User Name is required field");
        }
    }

    [DataContract]
    public class AddUserRequest : BaseUserRequest
    {
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Password is required field")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataMember(IsRequired = true)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(100, ErrorMessage = "{0} should be more than {2} symbols.", MinimumLength = User.MIN_REQUIRED_PASSWORD_LENGTH)]
        [Compare("Password", ErrorMessage = "Passwords are not match. Please try again.")]
        public string ConfirmPassword { get; set; }

        [DataMember, Details]
        public int RoleId { get; set; }

        [DataMember, Details]
        public bool SendNotification { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var val in base.Validate(validationContext))
                yield return val;

            if (this.SendNotification && string.IsNullOrWhiteSpace(this.Email))
                yield return new ValidationResult("Cannot send notification if email is not exist.");

            if (string.IsNullOrEmpty(this.Password))
                yield return new ValidationResult("Password cannot be empty");

            if (this.Password.Length < User.MIN_REQUIRED_PASSWORD_LENGTH)
                yield return new ValidationResult(string.Format("Password should be more than {0} symbols", User.MIN_REQUIRED_PASSWORD_LENGTH));

            if (this.Password != this.ConfirmPassword)
                yield return new ValidationResult("Passwords are not match. Please try again.");

            if (this.RoleId == 0)
                yield return new ValidationResult("Role is not defined");
        }
    }

    [DataContract]
    public class UpdateUserRequest : BaseUserRequest
    {
        [DataMember, Details]
        public Guid UserId { get; set; }
    }
}
