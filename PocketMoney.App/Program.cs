using Castle.Facilities.AutoTx;
using Castle.Facilities.FactorySupport;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Services.Transaction;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using Microsoft.Practices.ServiceLocation;
using PocketMoney.ParentApp.Properties;
using PocketMoney.Data;
using PocketMoney.Data.NHibernate;
using PocketMoney.Util;
using PocketMoney.Util.IoC;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PocketMoney.ParentApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static void Register()
        {
            var container = new WindsorContainer(new XmlInterpreter());
            var serviceLocator = new WindsorServiceLocator(container);
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.AddFacility<FactorySupportFacility>()
                .AddFacility<TypedFactoryFacility>();

            container.AddFacility<TransactionFacility>();

            container.Register(Component.For<ITransactionManager>().ImplementedBy<DefaultTransactionManager>());

            //container.Register(
            //    Component.For(typeof(IRepository<,,>)).ImplementedBy(typeof(Repository<,,>)).LifeStyle.Singleton);

            IBuildManager buildManager = new BuildManager();
            container.Register(Component.For<IBuildManager>().Instance(buildManager).LifeStyle.Singleton)
                .Register(Component.For<IWindsorContainer>().Instance(container).LifeStyle.Singleton);

            InstallInstallers(container, buildManager);

            container.Register(
                Component.For<ICurrentUserProvider>()
                    .ImplementedBy<CurrentUserProvider>()
                    .LifeStyle
                    .Singleton);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }

        private static void InstallInstallers(WindsorContainer container, IBuildManager buildManager)
        {
            foreach (Assembly assembly in buildManager.ApplicationAssemblies)
            {
                container.Install(FromAssembly.Instance(assembly));
            }
        }

    }
}
