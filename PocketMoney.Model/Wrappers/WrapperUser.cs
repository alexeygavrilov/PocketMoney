using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PocketMoney.Model;

namespace PocketMoney.Data.Wrappers
{
    [DataContract]
    public class WrapperUser : IUser
    {
        public WrapperUser(IUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Family = new WrapperFamily(user.Family.Id, user.Family.Name);
            Roles = user.Roles;
        }

        [DataMember(IsRequired = true)]
        [JsonProperty(Required = Required.Always)]
        public Guid Id
        {
            get;
            set;
        }

        [DataMember]
        public string FirstName
        {
            get;
            private set;
        }

        [DataMember]
        public string LastName
        {
            get;
            private set;
        }

        [JsonIgnore]
        public bool IsAnonymous
        {
            get { return false; }
        }


        public DateTime? LastLoginDate
        {
            get;
            private set;
        }

        [DataMember]
        [JsonConverter(typeof(ConcreteTypeConverter<WrapperFamily>))]
        public IFamily Family
        {
            get;
            private set;
        }

        [DataMember]
        [JsonConverter(typeof(ConcreteTypeConverter<Role[]>))]
        public IRole[] Roles { get; set; }

        public bool IsInRole(IRole role)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string password)
        {
            throw new NotImplementedException();
        }

        public string FullName()
        {
            return (this.FirstName + " " + this.LastName).Trim();
        }
    }
}
