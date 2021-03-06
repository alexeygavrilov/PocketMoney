﻿using PocketMoney.Data.Security;
using System;

namespace PocketMoney.Data
{
    public interface ICurrentUserProvider
    {
        void AddCurrentUser(IUser user, bool persist = false);
        IUser GetCurrentUser();
        void RemoveCurrentUser();
        DateTime GetCurrentDate();

        void SetData(string key, object value);
        object GetDate(string key);
    }
}