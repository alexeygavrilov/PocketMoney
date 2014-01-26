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

        public const int ConfirmCodeLength = 4;
        public const int MinRequiredPasswordLength = 3;
        protected byte _roles;
        protected string _password;

        #endregion

        #region Ctors

        protected User()
        {
            _roles = 0x0;
            _password = string.Empty;
            this.UserName = string.Empty;
            this.Active = false;
            this.Points = 0;
            this.Connections = new List<UserConnection>();
        }

        public User(Family family, string userName)
            : this()
        {
            this.Family = family;
            this.UserName = userName;
            this.Active = false;
        }

        public User(Family family, string userName, Email email)
            : this(family, userName)
        {
            this.Email = email;
        }

        public User(Family family, string userName, PhoneNumber phone)
            : this(family, userName)
        {
            this.Phone = phone;
        }

        #endregion

        #region Properties

        [Details]
        public virtual string UserName { get; set; }

        [Details]
        public virtual string AdditionalName { get; set; }

        [Details]
        public virtual bool Active { get; set; }

        [Details]
        public virtual int Points { get; set; }

        public virtual string ConfirmCode { get; set; }

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

        public virtual void GeneratePassword()
        {
            string generatedPassword = this.Generation("abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-", MinRequiredPasswordLength);
            this.SetPassword(generatedPassword);
            //return generatedPassword;
        }

        public virtual void GenerateConfirmCode()
        {
            string code = this.Generation("0123456789", ConfirmCodeLength);
            this.ConfirmCode = code;
            //return code;
        }

        protected virtual string Generation(string characters, int length)
        {
            char[] chars = new char[length];
            Random rd = new Random();

            for (int i = 0; i < length; i++)
            {
                chars[i] = characters[rd.Next(0, characters.Length)];
            }

            return new string(chars);
        }


        public virtual string FullName()
        {
            return (this.UserName + " " + this.AdditionalName ?? string.Empty).Trim();
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
