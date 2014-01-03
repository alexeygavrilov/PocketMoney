namespace PocketMoney.Util.Threading
{
    public interface ILock
    {
        bool CanWrite { get; }
        bool Acquire();
        void Release();
    }
}