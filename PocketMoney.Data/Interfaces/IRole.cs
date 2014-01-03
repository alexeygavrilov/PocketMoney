using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using PocketMoney.Data.Security;

namespace PocketMoney.Data
{
    public interface IRole
    {
        byte Id { get; set; }
        string Name { get; }

        //IUserPermission[] GetDefaultPermissions();
    }



}