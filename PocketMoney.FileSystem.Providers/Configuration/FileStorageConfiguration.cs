using System.Collections.Generic;
using System.Configuration;
using PocketMoney.FileSystem.Providers;

namespace PocketMoney.FileSystem.Configuration
{
    public sealed class FileStorageConfiguration : ConfigurationSection, IFileStorageConfiguration
    {
        [ConfigurationProperty("connectionStringName", IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string) this["connectionStringName"]; }
            set { this["connectionStringName"] = value; }
        }

        [ConfigurationProperty("devices", IsDefaultCollection = true)]
        public DeviceElementCollection Devices
        {
            get { return (DeviceElementCollection) this["devices"]; }
        }

        #region IFileStorageConfiguration Members

        IEnumerable<IDeviceSettings> IFileStorageConfiguration.Devices
        {
            get { return Devices; }
        }

        #endregion
    }
}