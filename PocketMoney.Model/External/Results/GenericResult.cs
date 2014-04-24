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
        public StringResult() { }

        public StringResult(string str) : base(str) { }
    }

    [DataContract]
    public class GuidResult : ResultData<Guid>
    {
        public GuidResult() { }

        public GuidResult(Guid id) : base(id) { }
    }

    [DataContract]
    public class IntResult : ResultData<int>
    {
        public IntResult() { }

        public IntResult(int id) : base(id) { }
    }
}
