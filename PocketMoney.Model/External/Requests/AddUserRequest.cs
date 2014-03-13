using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class AddUserRequest : Request
    {
        [DataMember(IsRequired = true)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Имя - это обязательное поле")]
        [Display(Name = "Имя пользователя")]
        [Details]
        public string UserName { get; set; }

        [DataMember]
        [RegularExpression(@"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Некорректный Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [Details]
        public string Email { get; set; }

        //[DataMember, Details]
        //public ConnectionRequest[] Connections { get; set; }

        [DataMember]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperFamily>))]
        public IFamily Family { get; set; }

        [DataMember, Details]
        public bool SendNotification { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Family == null)
                yield return new ValidationResult("Текущая семья обязательна");

            if (string.IsNullOrWhiteSpace(this.UserName))
                yield return new ValidationResult("Имя - это обязательное поле");

            if(this.SendNotification && string.IsNullOrWhiteSpace(this.Email))
                yield return new ValidationResult("Нельзя послать уведомления, если нет эл. почты.");

            //if (this.Connections == null || this.Connections.Length == 0)
            //    yield return new ValidationResult("Полььзователь должен иметь хотя бы одно соединение");

            //foreach (var conn in this.Connections)
            //{
            //    if (string.IsNullOrWhiteSpace(conn.Identity))
            //        yield return new ValidationResult(string.Format("Соединение '{0}' должно иметь идентификатор", conn.ConnectionType));
            //}
        }
    }
}
