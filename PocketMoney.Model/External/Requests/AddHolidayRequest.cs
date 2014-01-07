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
    public class AddHolidayRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Наименование праздника - это обязательное поле")]
        [Display(Name = "Наименование праздника")]
        [Details]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Дата - это обязательное поле")]
        [Display(Name = "Дата")]
        [Details]
        public DateTime Date { get; set; }

        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = "Код страны - это обязательное поле")]
        [Display(Name = "Код страны")]
        [Details]
        public int CountryCode { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Наименование праздника - это обязательное поле");

            if (this.Date == DateTime.MinValue)
                yield return new ValidationResult("Дата праздника - это обязательное поле");

            if (this.CountryCode == 0)
                yield return new ValidationResult("Код страны - это обязательное поле");
        }

    }
}
