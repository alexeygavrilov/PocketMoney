using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PocketMoney.Data;

namespace PocketMoney.Admin
{
    public class SiteInstaller : IWindsorInstaller
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