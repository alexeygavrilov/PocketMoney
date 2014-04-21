using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketMoney.Data;

namespace PocketMoney.Model.Internal
{
    public class UserConnection : Entity<UserConnection, UserConnectionId, Guid>
    {
        protected UserConnection() { }

        public UserConnection(User user, eClientType type, string identity)
        {
            this.User = user;
            this.Identity = identity;
            this.ClientType = type;
        }

        [Details]
        public virtual User User { get; set; }
        [Details]
        public virtual eClientType ClientType { get; set; }
        [Details]
        public virtual string Identity { get; set; }
        [Details]
        public virtual DateTime? LastLoginDate { get; set; }
    }

    public class UserConnectionId : GuidIdentity
    {
        public UserConnectionId(Guid id)
            : base(id, typeof(UserConnection))
        {
        }

        public UserConnectionId()
            : base(Guid.Empty, typeof(UserConnection))
        {
        }
    }
}
