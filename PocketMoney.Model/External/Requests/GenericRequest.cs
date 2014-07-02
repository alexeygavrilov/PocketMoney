using System;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Requests
{
    [DataContract]
    public class StringRequest : RequestData<string>
    {
        public StringRequest(string factor) : base(factor) { }
    }

    [DataContract]
    public class IntRequest : RequestData<int>
    {
        public IntRequest(int id) : base(id) { }
    }

    [DataContract]
    public class GuidRequest : RequestData<Guid>
    {
        public GuidRequest(Guid id) : base(id) { }
    }
}
