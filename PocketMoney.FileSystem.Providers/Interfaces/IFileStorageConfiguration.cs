using System.Collections.Generic;
using PocketMoney.FileSystem.Providers;

namespace PocketMoney.FileSystem.Configuration
{
    public interface IFileStorageConfiguration
    {
        IEnumerable<IDeviceSettings> Devices { get; }
    }
}