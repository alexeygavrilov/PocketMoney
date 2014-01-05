using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.Model.Internal;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddDutyTaskRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Название обязаности - это обязательное поле")]
        [Display(Name = "Название обязаности")]
        [Details]
        public string Name { get; set; }

        [DataMember]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperUser>))]
        public IUser User { get; set; }

        [DataMember(IsRequired = true)]
        [Display(Name = "Расписание обязаностей")]
        [Details]
        public DayOfYear[] DutyDays { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Название обязаности - это обязательное поле");

            if (this.User == null)
                yield return new ValidationResult("Пользовтатель для назначения обязателен");

            if (this.DutyDays == null || this.DutyDays.Length == 0)
                yield return new ValidationResult("Расписание обязаностей не должно быть пустым");
        }
    }
}
