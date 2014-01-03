using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using PocketMoney.FileSystem.Providers;

namespace PocketMoney.FileSystem.Configuration
{
    public sealed class DeviceElement : ConfigurationElement, IDeviceSettings
    {
        public DeviceElement()
        {
        }

        public DeviceElement(string name, string settingValue, bool online, bool remote, bool archive)
        {
            Name = name;
            Settings = settingValue;
            Online = online;
            Remote = remote;
            Archive = archive;
        }

        #region IDeviceSettings Members

        /// <summary>
        /// Device name or short description
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }


        /// <summary>
        /// Settings string for device. It can be a File System path, SQL connection string, etc.
        /// </summary>
        [ConfigurationProperty("settings", IsRequired = true)]
        public string Settings
        {
            get { return (string) this["settings"]; }
            set { this["settings"] = value; }
        }

        /// <summary>
        /// The current mode for device. If Online = true device is enabled 
        /// otherwise the device in offline mode and disabled.
        /// </summary>
        [ConfigurationProperty("online", DefaultValue = true)]
        public bool Online
        {
            get { return (bool) this["online"]; }
            set { this["online"] = value; }
        }

        /// <summary>
        /// The location for storage device. If Remote = true the device has remote location via network 
        /// otherwise the device has the local storage.
        /// </summary>
        [ConfigurationProperty("remote", DefaultValue = false)]
        public bool Remote
        {
            get { return (bool) this["remote"]; }
            set { this["remote"] = value; }
        }

        /// <summary>
        /// The storage type for device. If Archive = true the device uses only for the archiving and backup 
        /// otherwise device can store the fast data
        /// </summary>
        [ConfigurationProperty("archive", DefaultValue = false)]
        public bool Archive
        {
            get { return (bool) this["archive"]; }
            set { this["archive"] = value; }
        }


        /// <summary>
        /// The storage device provider class name and the assembly name where it is placed. 
        /// The device provider class must implement the PocketMoney.FileSystem.Storage.IDevice interface
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods"),
         ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        #endregion
    }
}