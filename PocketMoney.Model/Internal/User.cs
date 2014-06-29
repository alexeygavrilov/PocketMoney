using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using PocketMoney.Data;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Model.Internal
{
    public class User : Entity<User, UserId, Guid>, IUser, IObject
    {
        #region Members

        public const int CONFIRM_CODE_LENGTH = 4;
        public const int MIN_REQUIRED_PASSWORD_LENGTH = 5;
        public const int TOKEN_KEY_LENGTH = 17;
        protected byte _roles;
        protected string _password;

        #endregion

        #region Ctors

        protected User()
        {
            _roles = 0x0;
            _password = string.Empty;
            this.UserName = string.Empty;
            this.ConfirmCode = string.Empty;
            this.Active = false;
            this.Connections = new List<UserConnection>();
            this.AssignedTasks = new List<Performer>();
            this.TaskCount = new TaskCount(this, 0, 0);
            this.Points = new Point(this, 0);
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
        public virtual Point Points { get; set; }

        [Details]
        public virtual TaskCount TaskCount { get; set; }

        public virtual string ConfirmCode { get; set; }

        public virtual bool IsAnonymous { get { return false; } }

        [Details]
        public virtual Family Family { get; set; }

        public virtual Email Email { get; set; }

        public virtual PhoneNumber Phone { get; set; }

        public virtual DateTime? LastLoginDate { get; set; }

        public virtual string TokenKey { get; set; }

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

        public virtual IList<Performer> AssignedTasks { get; set; }

        public virtual eObjectType ObjectType
        {
            get { throw new NotImplementedException(); }
        }
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
            string generatedPassword = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-".Generation(MIN_REQUIRED_PASSWORD_LENGTH);
            this.SetPassword(generatedPassword);
            return generatedPassword;
        }

        public virtual string GenerateConfirmCode()
        {
            string code = "0123456789".Generation(CONFIRM_CODE_LENGTH);
            this.ConfirmCode = code;
            return code;
        }

        public virtual string FullName()
        {
            return FullName(this.UserName, this.AdditionalName);
        }

        public static string FullName(string userName, string additionalName)
        {
            return (userName + " " + additionalName ?? string.Empty).Trim();
        }

        public virtual string GenerateTokenKey()
        {
            this.TokenKey = string.Concat("abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789".Generation(8), "-", this.Family.TokenKey);
            return this.TokenKey;
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
