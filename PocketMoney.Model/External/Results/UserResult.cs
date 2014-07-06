using PocketMoney.Data;
using PocketMoney.Model.Internal;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace PocketMoney.Model.External.Results
{
    [DataContract]
    public class UserView : ObjectBase
    {
        public UserView(User user)
        {
            this.UserId = user.Id;
            this.UserName = user.UserName;
            this.Points = user.Points.Points;
            this.CompletedTaskCount = user.Counts.CompletedTasks;
            this.GoalsCount = user.Counts.CompletedGoals;
            this.GoodDeedCount = user.Counts.GoodWorks;
            this.GrabbedTaskCount = user.Counts.GrabbedTasks;
            this.RoleName = string.Join(", ", user.Roles.Select(r => r.Name).ToArray());
        }

        [DataMember, Details]
        public Guid UserId { get; set; }

        [DataMember, Details]
        public string UserName { get; set; }

        [DataMember, Details]
        public string RoleName { get; set; }

        [DataMember, Details]
        public int Points { get; set; }

        [DataMember, Details]
        public int CompletedTaskCount { get; set; }

        [DataMember, Details]
        public int GrabbedTaskCount { get; set; }

        [DataMember, Details]
        public int GoalsCount { get; set; }

        [DataMember, Details]
        public int GoodDeedCount { get; set; }

        public override string ToString()
        {
            return this.UserName;
        }
    }

    [DataContract]
    public class UserFullView : UserView
    {
        public UserFullView(User user, Func<string> historyLog)
            : base(user)
        {
            this.AdditionalName = user.AdditionalName;
            this.Email = user.Email != null ? user.Email.Address : string.Empty;
            this.Phone = user.Phone != null ? user.Phone.Number : string.Empty;
            this.LastLoginDate = user.LastLoginDate.HasValue ? user.LastLoginDate.Value.ToString() : string.Empty;
            this.HistoryLog = historyLog();
        }

        [DataMember, Details]
        public string AdditionalName { get; set; }

        [DataMember, Details]
        public string Email { get; set; }

        [DataMember, Details]
        public string Phone { get; set; }

        [DataMember, Details]
        public string LastLoginDate { get; set; }

        [DataMember, Details]
        public string HistoryLog { get; set; }

        public override string ToString()
        {
            return this.UserName;
        }
    }
}
