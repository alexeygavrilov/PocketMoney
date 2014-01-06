using PocketMoney.Data.Security;

namespace PocketMoney.Data
{
    public interface ICurrentUserProvider
    {
        void AddCurrentUser(IUser user, bool persist = false);
        IUser GetCurrentUser();
        void RemoveCurrentUser();
    }
}