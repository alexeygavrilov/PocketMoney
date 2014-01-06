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
    public class AddDutyTypeRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Тип обязоностей - это обязательное поле")]
        [Display(Name = "Тип обязоностей")]
        [Details]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Код страны - это обязательное поле")]
        [Display(Name = "Код страны")]
        [Details]
        public int CountryCode { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Тип обязоностей - это обязательное поле");

            if (this.CountryCode == 0)
                yield return new ValidationResult("Код страны - это обязательное поле");
        }

    }
}
