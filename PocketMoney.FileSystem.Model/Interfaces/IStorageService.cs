using System;
using System.Collections.Generic;
using System.Text;

namespace PocketMoney.FileSystem
{
    public interface IStorageService
    {
        IDictionary<string, IDevice> Devices { get; }
        IDevice CurrentDevice { get; }
        IDevice GetDevice(string name);
    }
}
