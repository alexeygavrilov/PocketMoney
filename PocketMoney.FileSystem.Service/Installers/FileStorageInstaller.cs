using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using PocketMoney.FileSystem.Configuration;

namespace PocketMoney.FileSystem.Service.Installers
{
    public class FileStorageInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFileService>().ImplementedBy<FileService>().LifeStyle.Singleton);
            container.Register(Component.For<IStorageService>().ImplementedBy<StorageService>().LifeStyle.Singleton);
            container.Register(Component.For<ITempFileService>().ImplementedBy<TempFileService>().LifeStyle.Singleton);
            container.Register(Component.For<IFileStorageConfigurationProvider>().ImplementedBy<ConfigFileBasedStorageConfigurationProvider>().LifeStyle.Singleton);
        }
    }
}
