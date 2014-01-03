using System;
using System.Text.RegularExpressions;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class Email : Entity<Email, EmailId, Guid>
    {
        public static Email Empty { get { return new Email() { Address = string.Empty }; } }

        public const String REGEX_VALIDATION_MASK =
            @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public static Regex REGEX_VALIDATION = new Regex(REGEX_VALIDATION_MASK);

        protected Email()
        {
        }

        public Email(String emailAddress, IUser user)
        {
            SetEmail(emailAddress);
            this.Name = user.FullName();
        }

        public Email(String emailAddress, String emailName)
        {
            SetEmail(emailAddress);
            if (String.IsNullOrWhiteSpace(emailName))
                emailName = emailAddress;
            this.Name = emailName;
        }

        public virtual String Address { get; set; }

        public virtual String Name { get; set; }

        public virtual Boolean IsPrimary { get; set; }

        protected virtual void SetEmail(String emailAddress)
        {
            if (String.IsNullOrEmpty(emailAddress))
            {
                throw new EmptyDataException("Email");
            }

            emailAddress = emailAddress.ToLower();

            if (!REGEX_VALIDATION.IsMatch(emailAddress))
            {
                throw new InvalidDataException("Malformed email!", "emailAddress", emailAddress);
            }

            this.Address = emailAddress;
        }

        public override String ToString()
        {
            return string.IsNullOrWhiteSpace(Name) ? Address : Name;
        }
    }


    [Serializable]
    public class EmailId : GuidIdentity
    {
        public EmailId()
            : base(Guid.Empty, typeof(Email))
        {
        }

        public EmailId(string id)
            : base(id, typeof(Email))
        {
        }

        public EmailId(Guid emailId)
            : base(emailId, typeof(Email))
        {
        }
    }
}
