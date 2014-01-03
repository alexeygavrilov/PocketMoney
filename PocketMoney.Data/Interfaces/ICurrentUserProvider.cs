using PocketMoney.Data.Security;

namespace PocketMoney.Data
{
    public interface ICurrentUserProvider
    {
        void Clear();

        IUser CurrentUser { get; }

        bool IsInRole(IRole role);
        IRole[] AllRoles();
    }
}