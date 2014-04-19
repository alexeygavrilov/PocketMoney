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
            UserName = user.UserName;
            Family = new WrapperFamily(user.Family.Id, user.Family.Name);
            Roles = user.Roles;
        }

        public WrapperUser(string username, Guid userId, Guid familyId)
        {
            this.Id = userId;
            this.UserName = username;
            this.Family = new WrapperFamily(familyId, "Wrapper Family");
        }

        [DataMember(IsRequired = true)]
        [JsonProperty(Required = Required.Always)]
        public Guid Id
        {
            get;
            set;
        }

        [DataMember]
        public string UserName
        {
            get;
            private set;
        }

        [JsonIgnore]
        public bool IsAnonymous
        {
            get { return this.Id == Guid.Empty; }
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
            return this.UserName.Trim();
        }
    }
}
