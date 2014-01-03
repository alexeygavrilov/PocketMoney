using System.Reflection;
using PocketMoney.Util;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace PocketMoney.Configuration.Web
{
    public abstract class MvcInfrastructureInstaller<TInfrastructure> : IWindsorInstaller
    {
        protected virtual LifestyleType Lifestyle
        {
            get { return LifestyleType.Transient; }
        }

        #region IWindsorInstaller Members

        public virtual void Install(IWindsorContainer container, IConfigurationStore store)
        {
            foreach (Assembly assembly in container.Resolve<IBuildManager>().ApplicationAssemblies)
            {
                container.Register(AllTypes.FromAssembly(assembly)
                                       .BasedOn<TInfrastructure>()
                                       .WithServiceAllInterfaces()
                                       .WithServiceSelf()
                                       .If(type => !type.IsAbstract && type.IsPublic)
                                       .Configure(c => c.LifeStyle.Is(Lifestyle)));
            }
        }

        #endregion
    }
}