using System;
using System.Runtime.Serialization;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util
{
    public abstract class UserLevelException : Exception
    {
        public UserLevelException(string message)
            : base(message)
        {
            this.LogDebug();
        }
    }

    public abstract class SystemLevelException : Exception
    {
        public SystemLevelException()
            : base()
        {
            this.LogError();
        }

        public SystemLevelException(string message)
            : base(message)
        {
            this.LogError();
        }

        public SystemLevelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.LogError();
        }

        public SystemLevelException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.LogError();
        }
    }
}
