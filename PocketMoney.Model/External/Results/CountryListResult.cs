using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class CountryListResult : ResultList<CountryInfo>
    {
        public CountryListResult() { }

        public CountryListResult(CountryInfo[] countryList, int count) : base(countryList, count) { }
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
