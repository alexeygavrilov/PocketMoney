using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PocketMoney.Configuration.Web
{
    public class FilterRegistryControllerItem<TController> : FilterRegistryItem where TController : Controller
    {
        private readonly Type _controllerType = typeof (TController);

        public FilterRegistryControllerItem(IEnumerable<Func<IMvcFilter>> filters) : base(filters)
        {
        }

        public override bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return (controllerContext != null) &&
                   _controllerType.IsAssignableFrom(controllerContext.Controller.GetType());
        }
    }
}