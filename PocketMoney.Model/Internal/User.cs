using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class User : Entity<User, UserId, Guid>, IUser
    {
        #region Members

        public const int MinRequiredPasswordLength = 3;
        protected byte _roles;
        protected string _password;

        #endregion

        #region Ctors

        protected User()
        {
            _roles = 0x0;
            _password = string.Empty;
            this.FirstName = string.Empty;
            this.Active = false;
            this.Connections = new List<UserConnection>();
        }

        public User(Family family, string firstName)
            : this()
        {
            this.Family = family;
            this.FirstName = firstName;
            this.Active = false;
        }

        public User(Family family, string firstName, Email email)
            : this(family, firstName)
        {
            this.Email = email;
        }

        public User(Family family, string firstName, PhoneNumber phone)
            : this(family, firstName)
        {
            this.Phone = phone;
        }

        #endregion

        #region Properties

        [Details]
        public virtual string FirstName { get; set; }

        [Details]
        public virtual string LastName { get; set; }

        [Details]
        public virtual bool Active { get; set; }

        public virtual bool IsAnonymous { get { return false; } }

        [Details]
        public virtual Family Family { get; set; }

        public virtual Email Email { get; set; }

        public virtual PhoneNumber Phone { get; set; }

        public virtual DateTime? LastLoginDate { get; set; }

        public virtual IRole[] Roles
        {
            get
            {
                switch (_roles)
                {
                    case 0:
                        return new IRole[0];
                    case 1:
                        return new IRole[1] { Internal.Roles.Children };
                    case 2:
                    case 3:
                        return new IRole[1] { Internal.Roles.Parent };
                    case 4:
                    case 5:
                        return new IRole[1] { Internal.Roles.FamilyAdmin };
                    case 6:
                    case 7:
                        return new IRole[2] { Internal.Roles.Parent, Internal.Roles.FamilyAdmin };
                    case 8:
                        return new IRole[1] { Internal.Roles.Admin };
                    default:
                        return new IRole[0];
                }

            }
        }

        IFamily IUser.Family
        {
            get { return this.Family; }
        }

        public virtual IList<UserConnection> Connections { get; set; }
        #endregion

        #region Methods

        public virtual bool IsInRole(IRole role)
        {
            return (_roles & role.Id) == role.Id;
        }

        public virtual void AddRole(IRole role)
        {
            if (!this.IsInRole(role))
                _roles = (byte)(_roles | role.Id);
        }

        public virtual bool IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new EmptyDataException("Password");
            return password.Equals(UTF8Encoding.UTF8.GetString(MachineKey.Decode(_password, MachineKeyProtection.Encryption)));
        }

        public virtual void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new EmptyDataException("Password");
            _password = MachineKey.Encode(UTF8Encoding.UTF8.GetBytes(password), MachineKeyProtection.Encryption);
        }

        public virtual string GetPassword()
        {
            return UTF8Encoding.UTF8.GetString(MachineKey.Decode(_password, MachineKeyProtection.Encryption));
        }

        public virtual string GeneratePassword()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[MinRequiredPasswordLength];
            Random rd = new Random();

            for (int i = 0; i < MinRequiredPasswordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            string generatedPassword = new string(chars);
            this.SetPassword(generatedPassword);
            return generatedPassword;
        }

        public virtual string FullName()
        {
            return (this.FirstName + " " + this.LastName).Trim();
        }

        #endregion
    }

    #region Identity

    public class UserId : GuidIdentity
    {
        public UserId(Guid userId)
            : base(userId, typeof(User))
        {
        }

        public UserId()
            : base(Guid.Empty, typeof(User))
        {
        }
    }
    #endregion
}
