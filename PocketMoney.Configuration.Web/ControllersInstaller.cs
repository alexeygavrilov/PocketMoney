using System;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Routing;
using System.Web;


namespace PocketMoney.Configuration.Web
{
    public class ControllersInstaller : MvcInfrastructureInstaller<Controller>
    {
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            base.Install(container, store);

            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifeStyle.Singleton);
            container.Register(Component.For<IViewPageActivator>().ImplementedBy<DefaultViewActivator>());
            container.Register(Component.For<IControllerActivator>().ImplementedBy<UnityControllerActivator>());

            //ControllerBuilder.Current.SetControllerFactory(container.Resolve<IControllerFactory>());
        }
    }

    public class DefaultViewActivator : IViewPageActivator
    {
        public object Create(ControllerContext controllerContext, Type type)
        {
            return Activator.CreateInstance(type);
        }
    }

    public class UnityControllerActivator : IControllerActivator
    {
        private readonly IWindsorContainer _container;

        public UnityControllerActivator(IWindsorContainer container)
        {
            this._container = container;
        }


        #region IControllerActivator Members


        public IController Create(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)_container.Kernel.Resolve(controllerType);

            //return DependencyResolver.Current.GetService(controllerType) as IController;
        }


        #endregion

    }

}