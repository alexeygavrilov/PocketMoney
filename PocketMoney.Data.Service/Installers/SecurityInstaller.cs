using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PocketMoney.Data.Security;

namespace PocketMoney.Data.Service.Installers
{
    public class SecurityInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAuthorization>().ImplementedBy<ClaimBasedAuthorization>().LifeStyle.Singleton);
        }
    }
}
