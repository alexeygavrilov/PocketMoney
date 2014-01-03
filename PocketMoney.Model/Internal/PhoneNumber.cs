using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class PhoneNumber : Entity<PhoneNumber, PhoneNumberId, Guid>
    {
        #region Members

        private const string ALLOWED_CHARS = " ().[]{}|\\/_";
        public static PhoneNumber Empty { get { return new PhoneNumber { Number = string.Empty }; } }

        #endregion

        #region Ctors

        protected PhoneNumber()
        {
        }

        public PhoneNumber(string number)
            : this()
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");//.LogError();
            Number = number;
            this.PhoneType = (number.Length >= 10) ? Internal.PhoneType.Mobile : Internal.PhoneType.Home;
        }

        public PhoneNumber(string number, PhoneType phoneType)
        {
            if (string.IsNullOrEmpty(number)) throw new ArgumentNullException("number");//.LogError();
            Number = number;
            this.PhoneType = phoneType;
        }

        #endregion

        #region Prop

        public virtual string Number { get; set; }

        public virtual string Description { get; set; }

        public virtual PhoneType PhoneType { get; set; }

        #endregion

        #region Method

        public override string ToString()
        {
            return string.IsNullOrEmpty(Number) ? string.Empty : Number;
        }

        #endregion
    }

    public enum PhoneType
    {
        Mobile,
        Home,
        Work
    }


    [Serializable]
    public class PhoneNumberId : GuidIdentity
    {
        public PhoneNumberId()
            : base(Guid.Empty, typeof(PhoneNumber))
        {
        }

        public PhoneNumberId(string id)
            : base(id, typeof(PhoneNumber))
        {
        }

        public PhoneNumberId(Guid phoneId)
            : base(phoneId, typeof(PhoneNumber))
        {
        }
    }
}
