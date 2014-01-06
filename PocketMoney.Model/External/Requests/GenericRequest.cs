using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class InputStringRequest : RequestData<string>
    {
    }

    [DataContract]
    public class InputIntRequest : RequestData<int>
    {
    }
}
