using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class CountryListResult : ResultList<CountryInfo>
    {
    }

    [DataContract]
    public struct CountryInfo
    {
        [DataMember, Details]
        public int Code { get; set; }

        [DataMember, Details]
        public string Name { get; set; }
    }
}
