using PocketMoney.Data;

namespace PocketMoney.Service.Interfaces
{
    public interface IBaseService
    {
        ICurrentUserProvider CurrentUserProvider { get; set; }
    }
}
