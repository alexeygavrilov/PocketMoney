using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace PocketMoney.Resources.Installers
{
    public sealed class CultureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICurrentCulture>().ImplementedBy<CurrentCultureProvider>().LifeStyle.Singleton);
        }
    }
}
