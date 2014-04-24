using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class FamilyResult : ResultData<IFamily>
    {
        public FamilyResult() { }
        public FamilyResult(IFamily family) : base(family) { }
    }
}
