using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.FileSystem.Configuration
{
    public class ConfigFileBasedStorageConfigurationProvider : IFileStorageConfigurationProvider
    {
        private const string SectionName = "filesystem";

        #region Implementation of IFileStorageConfigurationProvider

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
            MessageId = "connectionStringName"),
         SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "FileStorage")]
        public IFileStorageConfiguration GetConfiguration()
        {
            var configuration = ConfigurationManager.GetSection(SectionName) as FileStorageConfiguration;
            if (configuration == null)
                throw new ConfigurationErrorsException(
                    "Couldn't load the File Storage configuration. The configuration file must contains the PocketMoney.FileSystem tag.")
                    .LogError();
            ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings[configuration.ConnectionStringName];
            if (conn == null)
                throw new ConfigurationErrorsException(
                    "You must specify a valid connectionStringName value in File Storage settings.").LogError();
            return configuration;
        }

        #endregion
    }
}