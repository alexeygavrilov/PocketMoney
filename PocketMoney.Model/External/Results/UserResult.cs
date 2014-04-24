using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class UserResult : ResultData<IUser>
    {
        public UserResult() { }

        public UserResult(IUser user) : base(user) { }

        [DataMember, Details]
        public string Login { get; set; }

        [DataMember, Details("******")]
        public string Password { get; set; }

        [DataMember, Details("******")]
        public string AuthToken { get; set; }

        protected override void ClearData()
        {
            base.ClearData();
            this.AuthToken = this.Password = this.Login = null;
        }
    }
}
