using System.Diagnostics.CodeAnalysis;

namespace PocketMoney.FileSystem.Configuration
{
    public interface IFileStorageConfigurationProvider
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IFileStorageConfiguration GetConfiguration();
    }
}