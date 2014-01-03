using System;

namespace PocketMoney.FileSystem
{
    public interface IDeviceSettings
    {
        String Name { get; }
        String Settings { get; }
        Boolean Online { get; }
        Boolean Remote { get; }
        Boolean Archive { get; }
        String Type { get; }
    }
}