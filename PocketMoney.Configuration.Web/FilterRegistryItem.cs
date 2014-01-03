using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PocketMoney.Configuration.Web
{
    public abstract class FilterRegistryItem
    {
        protected FilterRegistryItem(IEnumerable<Func<IMvcFilter>> filters)
        {
            Filters = filters;
        }

        public IEnumerable<Func<IMvcFilter>> Filters { get; protected set; }

        public abstract bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}