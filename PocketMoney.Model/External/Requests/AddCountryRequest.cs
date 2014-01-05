using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddCountryRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Наименование страны - это обязательное поле")]
        [Display(Name = "Наименование страны")]
        [Details]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Код страны - это обязательное поле")]
        [Display(Name = "Код страны")]
        [Details]
        public int Code { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Наименование страны - это обязательное поле");

            if (this.Code == 0)
                yield return new ValidationResult("Код страны - это обязательное поле");
        }
    }
}
