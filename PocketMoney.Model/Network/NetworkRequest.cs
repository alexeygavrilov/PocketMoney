using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PocketMoney.Data;

namespace PocketMoney.Model.Network
{
    [DataContract]
    public abstract class NetworkRequest : Request
    {
        [DataMember, Details]
        public NetworkType Type { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Type == NetworkType.None)
                yield return new ValidationResult("Тип социальной сети должен быть определен.");
        }
    }

    [DataContract]
    public abstract class NetworkRequestData<TData> : RequestData<TData>
    {
        [DataMember, Details]
        public NetworkType Type { get; set; }

    }

    [DataContract]
    public class StringNetworkRequest : NetworkRequestData<string>
    {
    }

}
