using System;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.IO;
using PocketMoney.Data;
using System.Collections.Generic;

namespace PocketMoney.FileSystem.Providers
{
    public abstract class DeviceBase : IDevice
    {
        #region IDevice Members

        public IDeviceSettings Settings { get; protected set; }

        public virtual void Initialize(IDeviceSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings", "You must specify the settings for initiation").LogError();
            Settings = settings;
        }

        public abstract IDeviceInfo StorageInfo { get; }

        public abstract void Store(File file, IStreamProvider stream);

        public abstract IStreamProvider Retrieve(File file);

        public abstract IStreamProvider Thumbnail(File file, ThumbnailOptions option);

        public abstract void DeleteFile(File file);

        public abstract void DeleteCatalog(IFamily website);

        public abstract IEnumerable<IFamily> GetCatalogs();

        public abstract void ClearAllData();

        #endregion
    }

}