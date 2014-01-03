using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using PocketMoney.FileSystem;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class EmailMessageRequest : Request
    {
        public EmailMessageRequest(IUser sender, string email, string subject, string text)
        {
            this.Sender = sender;
            this.Email = email;
            this.Subject = subject;
            this.Text = text;
        }

        [DataMember(IsRequired = true)]
        [Required]
        public string Email { get; private set; }

        [DataMember(IsRequired = true)]
        [Required]
        public string Subject { get; private set; }

        [DataMember]
        public string Text { get; private set; }

        [DataMember]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperFile>))]
        public IFile Attachment { get; private set; }

        [DataMember]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperUser>))]
        public IUser Sender { get; private set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Sender == null)
                yield return new ValidationResult("Отправитель обязателен");

            if (string.IsNullOrWhiteSpace(this.Email))
                yield return new ValidationResult("Email - это обязательное поле");

            if (string.IsNullOrWhiteSpace(this.Subject))
                yield return new ValidationResult("Тема письма - это обязательное поле");
        }

    }
}
