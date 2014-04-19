using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PocketMoney.Data;

namespace PocketMoney.App
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ICurrentUserProvider>()
                    .ImplementedBy<CurrentUserProvider>()
                    .LifeStyle
                    .PerWebRequest);
        }
    }
}
