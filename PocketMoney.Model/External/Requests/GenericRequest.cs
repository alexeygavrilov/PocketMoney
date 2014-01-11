using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class StringRequest : RequestData<string>
    {
    }

    [DataContract]
    public class IntRequest : RequestData<int>
    {
    }
}
