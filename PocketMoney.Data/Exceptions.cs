using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using PocketMoney.Util;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Data.Security;
using System.Globalization;
using PocketMoney.Resources;

namespace PocketMoney.Data
{

    [Serializable, ComVisible(true)]
    public class SecurityException : SystemLevelException
    {
        public SecurityException()
            : base()
        {
        }

        public SecurityException(string message)
            : base(message)
        {
        }

        [SecuritySafeCritical]
        protected SecurityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public SecurityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    [Serializable, ComVisible(true)]
    public class AccessDeniedException : UserLevelException
    {
        public string PermissionName { get; private set; }

        public AccessDeniedException(IUserPermission permission)
            : base(string.Format(Error.ResourceManager.GetString("AccessDeniedPermission", Culture.CurrentUICulture), permission.Name))
        {
            this.PermissionName = permission.Name;
        }

        public AccessDeniedException(string message, string permissionName)
            : base(message)
        {
            this.PermissionName = permissionName;
        }
    }

    public class InternalServiceException : SystemLevelException
    {
        public InternalServiceException(String errorMessage)
            : base(errorMessage)
        {
        }
    }

    public class EmptyDataException : SystemLevelException
    {
        public string DataName { get; private set; }

        public EmptyDataException(string message, string dataName)
            : base(message)
        {
            this.DataName = dataName;
        }

        public EmptyDataException(string dataName)
            : base(string.Format(Error.ResourceManager.GetString("CannotBeEmpty", Culture.CurrentUICulture), dataName))
        {
            this.DataName = dataName;
        }
    }

    public class DataNotFoundException : SystemLevelException
    {
        public string DataName { get; private set; }

        public DataNotFoundException(string message, string dataName)
            : base(message)
        {
            this.DataName = dataName;
        }

        public DataNotFoundException(string dataName)
            : base(string.Format(Error.ResourceManager.GetString("NotFound", Culture.CurrentUICulture), dataName))
        {
            this.DataName = dataName;
        }
    }

    public class InvalidDataException : UserLevelException
    {
        public InvalidDataException(string message)
            : base(message)
        {
        }

        public InvalidDataException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

    }

}
