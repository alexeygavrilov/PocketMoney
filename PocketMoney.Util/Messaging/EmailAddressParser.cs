// -----------------------------------------------------------------------
// <copyright file="EmailAddressParser.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Messaging
{
    public class EmailAddressParser : IEmailAddressParser
    {
        #region Implementation of IEmailAddressParser

        public IEnumerable<IEmailAddress> Parse(string addresses)
        {
            return EmailAddress.Parse(addresses);
        }

        public IEmailAddress Create(string emailAddress)
        {
            return this.Create(emailAddress, emailAddress);
        }

        public IEmailAddress Create(string emailAddress, string displayName)
        {
            return new EmailAddress(displayName, emailAddress);
        }

        public IEmailAddress Create(string emailAddress, string firstName, string lastName)
        {
            var name = string.Format("{0} {1}", firstName, lastName).Trim();
            if (String.IsNullOrEmpty(name)) name = emailAddress;
            return this.Create(emailAddress, name);
        }

        public string ConvertToString(IEnumerable<IEmailAddress> emailAddresses)
        {
            return EmailAddress.Convert(emailAddresses);
        }

        #endregion

        #region IEmailAddressParser Members

        public IEmailAddress Parse(MailAddress address)
        {
            return new EmailAddress(address.DisplayName, address.Address);
        }

        #endregion

        #region Nested type: EmailAddress

        private class EmailAddress : IEmailAddress, IEquatable<IEmailAddress>, IEquatable<EmailAddress>
        {
            private readonly string _sAddress;
            private readonly string _sName;
            private string _domain;
            private string _subdomain;
            private string _user;

            #region Constructors

            //public EmailAddress(string encodedEmail)
            //{
            //    IEmailAddress[] parsed = Parse(encodedEmail);
            //    if (parsed.Length != 1)
            //        throw new ArgumentException("Invalid encoding of the email: " + encodedEmail);
            //    sName = parsed[0].DisplayName;
            //    sEmailAddress = parsed[0].Email;
            //}

            public EmailAddress(string name, string address)
            {
                if (address == null)
                {
                    throw new ArgumentNullException("address");
                }
                _sAddress = address.Trim();
                if (String.IsNullOrWhiteSpace(_sAddress)) throw new ArgumentException("address");
                _sName = name.Trim().Trim('"');
                if (String.IsNullOrWhiteSpace(_sName)) _sName = address;
                _sAddress = address;
                ParseEmailAddressApart(_sAddress);
            }

            private void ParseEmailAddressApart(string email)
            {
                if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email").LogError();
                string[] parts = email.Split('@');
                if (parts.Length != 2)
                    throw new ArgumentException(String.Format("Malformed email address: {0} ", email), "email").LogError
                        ();
                _domain = parts[1].Trim().ToUpperInvariant();
                _user = parts[0].Trim().ToUpperInvariant();
                parts = _domain.Split('.');
                _subdomain = parts.Length > 2
                                 ? _domain.Replace("." + parts[parts.Length - 2] + "." + parts[parts.Length - 1], "")
                                 : string.Empty;
            }

            #endregion

            #region Methods

            public bool Equals(IEmailAddress other)
            {
                return other != null && Equals(Address, other.Address);
            }

            public override string ToString()
            {
                string sContact;
                if (_sName.Equals(string.Empty))
                    sContact = _sAddress;
                else if ((!_sAddress.Equals(string.Empty)) && (_sAddress.Contains("@")))
                    sContact = String.Format("\"{0}\" <{1}>",_sName.Trim('"'),_sAddress);
                else
                    sContact = _sName;
                return sContact;
            }

            #endregion

            #region Properties

            public string DisplayName
            {
                get { return _sName; }
            }

            public string Address
            {
                get { return _sAddress; }
            }

            public string Host
            {
                get { return _domain; }
            }

            public string Subdomain
            {
                get { return _subdomain; }
            }

            public string User
            {
                get { return _user; }
            }

            #endregion

            #region IEquatable<EmailAddress> Members

            public bool Equals(EmailAddress other)
            {
                if (ReferenceEquals(null, other)) return false;
                return ReferenceEquals(this, other) || Equals(other._sAddress, _sAddress);
            }

            #endregion

            public static IEnumerable<IEmailAddress> Parse(string addresses)
            {
                if (string.IsNullOrEmpty(addresses))
                    yield break;
                string[] emails = addresses.Split(new[]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string eml in emails.Select(email => email.Trim()))
                {
                    if (eml.Contains("<") && (eml.Contains(">")))
                    {
                        string[] split = eml.Split('<', '>');
                        yield return new EmailAddress(split[0], split[1]);
                    }
                    else
                        yield return new EmailAddress(eml, eml);
                }
            }

            public static string Convert(IEnumerable<IEmailAddress> recipientsEnum)
            {
                if (recipientsEnum == null)
                    return string.Empty;
                List<IEmailAddress> recipients = recipientsEnum.ToList();
                if (recipients.Count == 0)
                    return string.Empty;
                var sb = new StringBuilder();
                for (int i = 0; i < recipients.Count; i++)
                {
                    sb.Append(EmailToString(recipients[i].DisplayName, recipients[i].Address));
                    if (i < recipients.Count - 1)
                        sb.Append("; ");
                }
                return sb.ToString();
            }

            private static string EmailToString(string displayName, string emailAddress)
            {
                if ((emailAddress != null) & (emailAddress.Length > 0))
                    return string.Format("{0}<{1}>", displayName, emailAddress);
                return displayName;
            }


            public override int GetHashCode()
            {
                return (_sAddress != null ? _sAddress.GetHashCode() : 0);
            }
        }

        #endregion
    }

    public interface IEmailAddressParser
    {
        IEnumerable<IEmailAddress> Parse(string addresses);
        IEmailAddress Create(string emailAddress);
        IEmailAddress Create(string emailAddress, string displayName);
        IEmailAddress Create(string emailAddress, string firstName,string lastName);
        IEmailAddress Parse(MailAddress address);
        string ConvertToString(IEnumerable<IEmailAddress> emailAddresses);
    }
}