using System;

namespace PocketMoney.Data.Security
{
    [Flags]
    public enum ObjectPermissions
    {
        None = 0,
        Create = 1,
        Read = 2,
        Write = 4,
        Delete = 8,
        ReadSecurity = 16,
        WriteSecurity = 32,
        TakeOwnership = 64,
        FullControl = Create | Read | Write | Delete | ReadSecurity | WriteSecurity | TakeOwnership
    }
}