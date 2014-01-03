using System;
using System.Reflection;
using System.Web.Mvc;
using Castle.Facilities.AutoTx;
using Castle.Facilities.FactorySupport;
using Castle.Facilities.NHibernateIntegration;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Services.Transaction;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using PocketMoney.Util;
using PocketMoney.Util.IoC;
using PocketMoney.Data;
using PocketMoney.Data.Security;
using Microsoft.Practices.ServiceLocation;
using Castle.Facilities.Logging;
using System.Web;
using PocketMoney.Data.NHibernate;

namespace PocketMoney.Configuration.Web
{
    public static class ContainerRegistration
    {
        public static void Register(IBuildManager buildManager)
        {
            var container = new WindsorContainer(new XmlInterpreter());
            var serviceLocator = new WindsorServiceLocator(container);

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.AddFacility<FactorySupportFacility>();
            container.AddFacility<TypedFactoryFacility>();
            container.AddFacility<TransactionFacility>();
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.Log4net).WithConfig("log4net.config"));

            container.Register(Component.For<IBuildManager>().Instance(buildManager).LifeStyle.Singleton);
            container.Register(Component.For<IDependencyResolver>().Instance(serviceLocator).LifeStyle.Singleton);
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifeStyle.Singleton);
            container.Register(Component.For<IConfigurationBuilder>().ImplementedBy<FluentNHibernateConfigurationBuilder>());
            container.Register(Component.For<ITransactionManager>().ImplementedBy<DefaultTransactionManager>().LifeStyle.Transient);
            container.Register(
               Component.For<TypeMappingRegistry<object, IModelBinder>>().Instance(
                   new TypeMappingRegistry<object, IModelBinder>()).LifeStyle.Singleton);

            container.Register(
                Component.For<HttpContextBase>().UsingFactoryMethod(k => new HttpContextWrapper(HttpContext.Current)).LifeStyle.PerWebRequest);

            container.Register(
                Component.For<ICacheManager>().UsingFactoryMethod(k => new CacheManager(HttpRuntime.Cache)).LifeStyle.Singleton);

            container.Register(Component.For<ModelMetadataProvider>().ImplementedBy<DataAnnotationsModelMetadataProvider>());
            InstallInstallers(buildManager, container);

            DependencyResolver.SetResolver(serviceLocator);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private static void InstallInstallers(IBuildManager buildManager, IWindsorContainer container)
        {
            foreach (Assembly assembly in buildManager.ApplicationAssemblies)
            {
                container.Install(FromAssembly.Instance(assembly));
            }
        }
    }
}