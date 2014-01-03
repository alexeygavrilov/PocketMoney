using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace PocketMoney.Util.Bootstrapping
{
    public class BootstrapperTasksInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (Assembly assembly in container.Resolve<IBuildManager>().ApplicationAssemblies)
            {
                container.Register(
                    AllTypes.FromAssembly(assembly).BasedOn<BootstrappingTask>().If(
                        type => !type.IsAbstract && type.IsPublic).Configure(c => c.LifestyleSingleton()));
            }
        }

        #endregion
    }
}