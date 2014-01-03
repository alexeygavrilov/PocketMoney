using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PocketMoney.Service.Behaviors;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Service.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ProcessInterceptor>()
                    .Named("process.interceptor"));

            container.Register(
                Component.For<IMessageService>()
                    .ImplementedBy<MessageService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .PerWebRequest);

            container.Register(
                Component.For<IFamilyService>()
                    .ImplementedBy<FamilyService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .PerWebRequest);
        }
    }
}
