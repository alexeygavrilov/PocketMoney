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
    public class UserResult : ResultData<UserView>
    {
        public UserResult() { }

        public UserResult(string errorMessage) : base(errorMessage) { }

        public UserResult(UserView data) : base(data) { }
    }

    [DataContract]
    public class UserFullResult : ResultData<UserFullView>
    {
        public UserFullResult() { }

        public UserFullResult(string errorMessage) : base(errorMessage) { }

        public UserFullResult(UserFullView data) : base(data) { }
    }

}
