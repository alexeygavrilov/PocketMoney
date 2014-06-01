using PocketMoney.Data;
using System;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class UserListResult : ResultList<UserView>
    {
        public UserListResult() { }

        public UserListResult(UserView[] userList, int count) : base(userList, count) { }
    }

    [DataContract]
    public class UserView : ObjectBase
    {
        [DataMember, Details]
        public Guid UserId { get; set; }

        [DataMember, Details]
        public string UserName { get; set; }

        [DataMember, Details]
        public int Points { get; set; }

        public override string ToString()
        {
            return this.UserName;
        }
    }
}
