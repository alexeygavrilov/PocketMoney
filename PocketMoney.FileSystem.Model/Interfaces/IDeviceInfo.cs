namespace PocketMoney.FileSystem
{
    public interface IDeviceInfo
    {
        long SpaceTotal { get; }
        long SpaceAvailable { get; }
        bool IsAvailable { get; }
    }
}