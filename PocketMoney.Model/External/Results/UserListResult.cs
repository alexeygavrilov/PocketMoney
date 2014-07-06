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
    public class UserResult : ResultData<UserFullView>
    {
        public UserResult() { }

        public UserResult(string errorMessage) : base(errorMessage) { }

        public UserResult(UserFullView data) : base(data) { }
    }

}
