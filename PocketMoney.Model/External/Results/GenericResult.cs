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
    public class StringResult : ResultData<string>
    {
    }

    [DataContract]
    public class GuidResult : ResultData<Guid>
    {
    }

    [DataContract]
    public class IntResult : ResultData<int>
    {
    }
}
