using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PocketMoney.Data.Security
{
    public interface IUserPermission
    {
        int PermissionId { get; }
        string Name { get; }
        PermissionType Type { get; }
        bool IsAuthorized(object value, IUser user);
    }

    public enum PermissionType
    {
        Boolean,
        MemberList,
        ModifyObject
    }

}
