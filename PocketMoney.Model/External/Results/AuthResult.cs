using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class AuthResult : ResultData<IUser>
    {
        public AuthResult() { }

        public AuthResult(IUser user) : base(user) { }

        [DataMember, Details]
        public string Login { get; set; }

        [DataMember, Details("******")]
        public string Password { get; set; }

        [DataMember, Details("******")]
        public string Token { get; set; }

        protected override void ClearData()
        {
            base.ClearData();
            this.Token = this.Password = this.Login = null;
        }
    }
}
