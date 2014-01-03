using PocketMoney.Util;
using PocketMoney.Data;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate.Cfg;

namespace PocketMoney.Data.NHibernate.Installers
{
    public class DataAccessInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDatabaseSchemaBuilder>().ImplementedBy<DatabaseSchemaBuilder>().LifeStyle.Singleton.
                    UsingFactoryMethod(
                        k =>
                        new DatabaseSchemaBuilder(k.Resolve<IBuildManager>(), k.Resolve<Configuration>())))
                .Register(
                    Component.For(typeof(IRepository<,,>)).ImplementedBy(
                        //typeof(Repository<,,>)).LifeStyle.HybridPerWebRequestTransient());
                        typeof(Repository<,,>)).LifeStyle.Transient); //PerWebRequest
        }

        #endregion
    }
}
