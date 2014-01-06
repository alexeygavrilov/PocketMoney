using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class UserListResult : ResultList<UserInfo>
    {
    }

    [DataContract]
    public class UserInfo
    {
        [DataMember, Details]
        public Guid UserId { get; set; }

        [DataMember, Details]
        public string UserName { get; set; }

        [DataMember, Details]
        public int Points { get; set; }
    }
}
