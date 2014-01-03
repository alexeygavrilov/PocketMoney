using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using PocketMoney.Util.ExtensionMethods;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.Util.IoC
{
    public class WindsorServiceLocator : ServiceLocatorImplBase, IDependencyResolver
    {
        public WindsorServiceLocator(IWindsorContainer container)
        {
            Container = container;
        }

        public IWindsorContainer Container { get; private set; }

        #region IDependencyResolver Members

        [DebuggerStepThrough]
        object IDependencyResolver.GetService(Type serviceType)
        {
#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (Exception e)
            {
                e.LogDebug();
                // ASP.NET MVC expects null when the underlying container cannot provide a service
                return null;
            }
#if DEBUG

            finally
            {

                sw.Stop();
                Debug.WriteLine("Resolving type {0} too {1} ms", serviceType.ToString(), sw.ElapsedMilliseconds);

            }
#endif
        }

        IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
        {
            try
            {
                return DoGetAllInstances(serviceType);
            }
            catch (Exception e)
            {
                e.LogDebug();
                // ASP.NET MVC expects empty list when the underlying container cannot resolve
                return Enumerable.Empty<object>();
            }
        }

        #endregion

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                       ? Container.Resolve(serviceType)
                       : Container.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Container.ResolveAll(serviceType).Cast<object>();
        }
    }
}