using PocketMoney.ChildApp.Properties;
using PocketMoney.Data;
using PocketMoney.Data.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketMoney.ChildApp
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private static IUser _currentUser = null;

        public void AddCurrentUser(IUser user, bool persist = false)
        {
            _currentUser = user;
            Settings.Default.UserName = user.UserName;
            Settings.Default.UserId = user.Id;
            Settings.Default.FamilyId = user.Family.Id;
        }

        public IUser GetCurrentUser()
        {
            if (_currentUser == null)
            {
                _currentUser = new WrapperUser(
                    Settings.Default.UserName,
                    Settings.Default.UserId,
                    Settings.Default.FamilyId);
            }
            return _currentUser;
        }

        public void RemoveCurrentUser()
        {
            _currentUser = null;
            Settings.Default.Context.Clear();
        }

        public void SetData(string key, object value)
        {
            Settings.Default.CustomData[key] = value;
        }

        public object GetDate(string key)
        {
            return Settings.Default.CustomData[key];
        }
    }
}
