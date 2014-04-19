using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class UserResult : ResultData<IUser>
    {
        [DataMember, Details]
        public string Login { get; set; }

        [DataMember, Details("******")]
        public string Password { get; set; }

        [DataMember, Details("******")]
        public string AuthToken { get; set; }
    }
}
