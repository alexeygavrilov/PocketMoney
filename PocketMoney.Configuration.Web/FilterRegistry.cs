using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Configuration.Web
{
    public class FilterRegistry
    {
        public FilterRegistry(IDependencyResolver resolver)
        {
            ControllerRegistry = new List<FilterRegistryItem>();
            ActionRegistry = new List<FilterRegistryItem>();
            Resolver = resolver;
        }

        protected internal ICollection<FilterRegistryItem> ControllerRegistry { get; private set; }

        protected internal ICollection<FilterRegistryItem> ActionRegistry { get; private set; }

        protected internal IDependencyResolver Resolver { get; private set; }

        public virtual FilterRegistry Register<TController, TFilter>(IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : IMvcFilter
        {
            if (filters.Any())
            {
                ControllerRegistry.Add(new FilterRegistryControllerItem<TController>(ConvertFilters(filters)));
            }

            return this;
        }

        public virtual FilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action,
                                                                     IEnumerable<Func<TFilter>> filters)
            where TController : Controller
            where TFilter : IMvcFilter
        {
            if (filters.Any())
            {
                ActionRegistry.Add(new FilterRegistryActionItem<TController>(action, ConvertFilters(filters)));
            }

            return this;
        }

        public virtual IEnumerable<FilterRegistryItem> FindControllerFilters(ControllerContext controllerContext,
                                                                             ActionDescriptor actionDescriptor)
        {
            return ControllerRegistry.Where(i => i.IsMatching(controllerContext, actionDescriptor));
        }

        public virtual IEnumerable<FilterRegistryItem> FindActionFilters(ControllerContext controllerContext,
                                                                         ActionDescriptor actionDescriptor)
        {
            return ActionRegistry.Where(i => i.IsMatching(controllerContext, actionDescriptor));
        }

        private static IEnumerable<Func<IMvcFilter>> ConvertFilters<TFilter>(IEnumerable<Func<TFilter>> filters)
            where TFilter : IMvcFilter
        {
            return filters.Select(filter => new Func<IMvcFilter>(() => filter() as IMvcFilter));
        }
    }

    public static class FilterRegistryExtensions
    {
        private static readonly Type GenericControllerItemType = typeof (FilterRegistryControllerItem<>);

        public static FilterRegistry Register<TFilter>(this FilterRegistry instance, IEnumerable<Type> controllerTypes)
            where TFilter : IMvcFilter
        {
            return Register<TFilter>(instance, controllerTypes, filter => { });
        }

        public static FilterRegistry Register<TFilter>(this FilterRegistry instance, IEnumerable<Type> controllerTypes,
                                                       Action<TFilter> configureFilter)
            where TFilter : IMvcFilter
        {
            EnsureControllerTypes(controllerTypes);

            foreach (
                Type type in
                    controllerTypes.Select(controllerType => GenericControllerItemType.MakeGenericType(controllerType)))
            {
                instance.ControllerRegistry.Add(
                    Activator.CreateInstance(type,
                                             new object[] {CreateAndConfigureFilterFactory(instance, configureFilter)})
                    as FilterRegistryItem);
            }

            return instance;
        }

        public static FilterRegistry Register<TFilter1, TFilter2>(this FilterRegistry instance,
                                                                  IEnumerable<Type> controllerTypes)
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
        {
            return Register(instance, controllerTypes, typeof (TFilter1), typeof (TFilter2));
        }

        public static FilterRegistry Register<TFilter1, TFilter2, TFilter3>(this FilterRegistry instance,
                                                                            IEnumerable<Type> controllerTypes)
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
            where TFilter3 : IMvcFilter
        {
            return Register(instance, controllerTypes, typeof (TFilter1), typeof (TFilter2), typeof (TFilter3));
        }

        public static FilterRegistry Register<TFilter1, TFilter2, TFilter3, TFilter4>(this FilterRegistry instance,
                                                                                      IEnumerable<Type> controllerTypes)
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
            where TFilter3 : IMvcFilter
            where TFilter4 : IMvcFilter
        {
            return Register(instance, controllerTypes, typeof (TFilter1), typeof (TFilter2), typeof (TFilter3),
                            typeof (TFilter4));
        }

        public static FilterRegistry Register<TController, TFilter>(this FilterRegistry instance)
            where TController : Controller
            where TFilter : IMvcFilter
        {
            return Register<TController, TFilter>(instance, (TFilter filter) => { });
        }

        public static FilterRegistry Register<TController, TFilter>(this FilterRegistry instance,
                                                                    Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : IMvcFilter
        {
            instance.Register<TController, IMvcFilter>(CreateAndConfigureFilterFactory(instance, configureFilter));

            return instance;
        }

        public static FilterRegistry Register<TController, TFilter1, TFilter2>(this FilterRegistry instance)
            where TController : Controller
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
        {
            return
                instance.Register<TController, IMvcFilter>(
                    CreateFilterFactories(instance, typeof (TFilter1), typeof (TFilter2)).ToArray());
        }

        public static FilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>(this FilterRegistry instance)
            where TController : Controller
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
            where TFilter3 : IMvcFilter
        {
            return
                instance.Register<TController, IMvcFilter>(
                    CreateFilterFactories(instance, typeof (TFilter1), typeof (TFilter2), typeof (TFilter3)).ToArray());
        }

        public static FilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>(
            this FilterRegistry instance)
            where TController : Controller
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
            where TFilter3 : IMvcFilter
            where TFilter4 : IMvcFilter
        {
            return
                instance.Register<TController, IMvcFilter>(
                    CreateFilterFactories(instance, typeof (TFilter1), typeof (TFilter2), typeof (TFilter3),
                                          typeof (TFilter4)).ToArray());
        }

        public static FilterRegistry Register<TController, TFilter>(this FilterRegistry instance,
                                                                    Expression<Action<TController>> action)
            where TController : Controller
            where TFilter : IMvcFilter
        {
            return Register<TController, TFilter>(instance, action, filter => { });
        }

        public static FilterRegistry Register<TController, TFilter>(this FilterRegistry instance,
                                                                    Expression<Action<TController>> action,
                                                                    Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : IMvcFilter
        {
            return instance.Register(action, CreateAndConfigureFilterFactory(instance, configureFilter));
        }

        public static FilterRegistry Register<TController, TFilter1, TFilter2>(this FilterRegistry instance,
                                                                               Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
        {
            return instance.Register(action,
                                     CreateFilterFactories(instance, typeof (TFilter1), typeof (TFilter2)).ToArray());
        }

        public static FilterRegistry Register<TController, TFilter1, TFilter2, TFilter3>(this FilterRegistry instance,
                                                                                         Expression<Action<TController>>
                                                                                             action)
            where TController : Controller
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
            where TFilter3 : IMvcFilter
        {
            return instance.Register(action,
                                     CreateFilterFactories(instance, typeof (TFilter1), typeof (TFilter2),
                                                           typeof (TFilter3)).ToArray());
        }

        public static FilterRegistry Register<TController, TFilter1, TFilter2, TFilter3, TFilter4>(
            this FilterRegistry instance, Expression<Action<TController>> action)
            where TController : Controller
            where TFilter1 : IMvcFilter
            where TFilter2 : IMvcFilter
            where TFilter3 : IMvcFilter
            where TFilter4 : IMvcFilter
        {
            return instance.Register(action,
                                     CreateFilterFactories(instance, typeof (TFilter1), typeof (TFilter2),
                                                           typeof (TFilter3), typeof (TFilter4)).ToArray());
        }

        private static FilterRegistry Register(FilterRegistry instance, IEnumerable<Type> controllerTypes,
                                               params Type[] filterTypes)
        {
            EnsureControllerTypes(controllerTypes);

            foreach (
                Type type in
                    controllerTypes.Select(controllerType => GenericControllerItemType.MakeGenericType(controllerType)))
            {
                instance.ControllerRegistry.Add(
                    Activator.CreateInstance(type, new object[] {CreateFilterFactories(instance, filterTypes)}) as
                    FilterRegistryItem);
            }

            return instance;
        }

        private static void EnsureControllerTypes(IEnumerable<Type> types)
        {
            foreach (
                Type controllerType in
                    types.Where(controllerType => !typeof (Controller).IsAssignableFrom(controllerType)))
            {
                throw new InvalidOperationException("\"{0}\" is not a valid controller.".Interpolate(controllerType.FullName)).
                    LogError();
            }
        }

        private static IEnumerable<Func<IMvcFilter>> CreateFilterFactories(FilterRegistry registry,
                                                                           params Type[] filterTypes)
        {
            return
                filterTypes.Select(
                    filterType => new Func<IMvcFilter>(() => registry.Resolver.GetService(filterType) as IMvcFilter));
        }

        private static IEnumerable<Func<IMvcFilter>> CreateAndConfigureFilterFactory<TFilter>(FilterRegistry registry,
                                                                                              Action<TFilter>
                                                                                                  configureFilter)
            where TFilter : IMvcFilter
        {
            return new List<Func<IMvcFilter>>
                       {
                           () =>
                               {
                                   var filter = registry.Resolver.GetService<TFilter>();

                                   configureFilter(filter);

                                   return filter;
                               }
                       };
        }
    }
}