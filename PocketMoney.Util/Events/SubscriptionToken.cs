using System;
using System.Diagnostics;

namespace PocketMoney.Util.Events
{
    public class SubscriptionToken : IEquatable<SubscriptionToken>
    {
        private readonly Guid token = Guid.NewGuid();

        #region IEquatable<SubscriptionToken> Members

        [DebuggerStepThrough]
        public bool Equals(SubscriptionToken other)
        {
            return (other != null) && Equals(token, other.token);
        }

        #endregion

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || Equals(obj as SubscriptionToken);
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            return token.GetHashCode();
        }

        [DebuggerStepThrough]
        public override string ToString()
        {
            return token.ToString();
        }
    }
}