using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PocketMoney.FileSystem.Service;

namespace PocketMoney.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("FileRoute",
                new Route(FileRouteHandler.FileURL, new FileRouteHandler()));

            routes.Add("ThumbnailRoute",
                new Route(FileRouteHandler.ThumbnailURL, new FileRouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}