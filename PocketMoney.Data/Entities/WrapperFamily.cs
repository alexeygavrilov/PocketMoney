using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PocketMoney.Data.Wrappers
{
    [DataContract]
    public class WrapperFamily : IFamily
    {
        public WrapperFamily(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        [DataMember(IsRequired = true)]
        [JsonProperty(Required = Required.Always)]
        public virtual Guid Id { get; set; }

        [DataMember]
        public virtual string Name { get; private set; }

        public virtual bool IsAnonymous
        {
            get { return false; }
        }

        public virtual IList<IUser> Members
        {
            get { return new List<IUser>(); }
        }
    }
}
