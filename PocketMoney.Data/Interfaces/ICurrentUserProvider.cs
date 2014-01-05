using PocketMoney.Data.Security;

namespace PocketMoney.Data
{
    public interface ICurrentUserProvider
    {
        void AddCurrentUser(IUser user, bool persistCookie = false);
        IUser GetCurrentUser();
        void RemoveCurrentUser();
    }
}