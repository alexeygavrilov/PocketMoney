using System;
using System.Runtime.Serialization;
using PocketMoney.Data.Security;
using PocketMoney.Util.Messaging;

namespace PocketMoney.Data
{
    public interface IUser 
    {
        Guid Id { get; set; }
        string UserName { get; }
        bool IsAnonymous { get; }
        DateTime? LastLoginDate { get; }
        IFamily Family { get; }

        bool IsInRole(IRole role);
        IRole[] Roles { get; }
        bool IsValid(string password);
        //bool IsAuthorized(IUserPermission permission, object value);
        string FullName();
    }

}