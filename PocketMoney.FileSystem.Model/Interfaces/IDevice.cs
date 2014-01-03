using System;
using System.Collections.Generic;
using PocketMoney.Data;
using PocketMoney.Util.IO;

namespace PocketMoney.FileSystem
{
    public interface IDevice
    {
        IDeviceSettings Settings { get; }
        IDeviceInfo StorageInfo { get; }

        void Initialize(IDeviceSettings settings);
        void Store(File file, IStreamProvider stream);
        IStreamProvider Retrieve(File file);
        IStreamProvider Thumbnail(File file, ThumbnailOptions option);
        void DeleteFile(File file);
        void DeleteCatalog(IFamily website);
        IEnumerable<IFamily> GetCatalogs();
        void ClearAllData();
    }
}