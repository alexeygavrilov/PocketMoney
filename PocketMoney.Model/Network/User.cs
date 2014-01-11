using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Network
{
    [DataContract]
    public class NetworkAccount : ObjectBase
    {
        [DataMember, Details]
        public string UserId { get; set; }

        [DataMember, Details]
        public string FirstName { get; set; }

        [DataMember, Details]
        public string LastName { get; set; }

        [DataMember, Details]
        public string Photo { get; set; }
    }

    [DataContract]
    public class NetworkAccountResult : ResultData<NetworkAccount> { }

    [DataContract]
    public class NetworkAccountList : ResultList<NetworkAccount> { }
}
