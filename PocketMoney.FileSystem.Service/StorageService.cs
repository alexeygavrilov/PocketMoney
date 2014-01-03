using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PocketMoney.FileSystem.Configuration;
using PocketMoney.Util.ExtensionMethods;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.FileSystem.Service
{
    public class StorageService : IStorageService
    {
        private readonly IFileStorageConfiguration _configuration;
        private Dictionary<string, IDevice> _devices;

        public StorageService()
        {
            var provider = ServiceLocator.Current.GetInstance<IFileStorageConfigurationProvider>();
            _configuration = provider.GetConfiguration();
            LoadDevices();
        }

        #region IStorage Members

        public IDictionary<string, IDevice> Devices
        {
            get { return _devices; }
        }

        public IDevice CurrentDevice
        {
            get
            {
                IDevice dev = (from d in _devices.Values
                               where d.Settings.Online
                               orderby d.Settings.Archive ascending,
                                   d.Settings.Remote ascending
                               select d).FirstOrDefault();
                if (dev == null)
                    throw new InvalidOperationException("The storage manager hasn't the available device").LogError();
                return dev;
            }
        }

        public IDevice GetDevice(string name)
        {
            return _devices[name];
        }

        #endregion

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IDevice")]
        private void LoadDevices()
        {
            _devices = new Dictionary<string, IDevice>();
            foreach (IDeviceSettings currDevice in _configuration.Devices)
            {
                if (string.IsNullOrEmpty(currDevice.Name))
                    throw new ConfigurationErrorsException("You must specify a device name in settings.").LogError();
                if (string.IsNullOrEmpty(currDevice.Type))
                    throw new ConfigurationErrorsException("You must specify a Storage provider type name in settings.")
                        .LogError();
                if (string.IsNullOrEmpty(currDevice.Settings))
                    throw new ConfigurationErrorsException("You must specify a settings value in device configuration.")
                        .LogError();

                string typeName;
                Assembly assembly;
                if (currDevice.Type.Contains(","))
                {
                    int idx = currDevice.Type.IndexOf(",", StringComparison.OrdinalIgnoreCase);
                    typeName = currDevice.Type.Substring(0, idx).Trim();
                    string assemblyName = currDevice.Type.Substring(idx + 1, currDevice.Type.Length - idx - 1).Trim();
                    try
                    {
                        assembly = Assembly.Load(assemblyName);
                    }
                    catch (Exception ex)
                    {
                        ex.LogDebug();
                        throw new ConfigurationErrorsException("Couldn't load the " + assemblyName + " assembly.", ex);
                    }
                }
                else
                {
                    assembly = Assembly.GetExecutingAssembly();
                    typeName = currDevice.Type;
                }

                var device = assembly.CreateInstance(typeName) as IDevice;
                if (device == null)
                    throw new ConfigurationErrorsException("Couldn't create instance for the " + typeName +
                                                           " provider object. This object must implement the IDevice interface.")
                        .LogError();
                try
                {
                    device.Initialize(currDevice);
                }
                catch (Exception ex)
                {
                    ex.LogDebug();
                    throw new ConfigurationErrorsException(ex.Message, ex).LogError();
                }
                _devices.Add(currDevice.Name, device);
            }
        }
    }
}
