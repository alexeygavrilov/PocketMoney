using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Lifestyle;
using PocketMoney.Configuration.Web;
using PocketMoney.Service.Installers;
using PocketMoney.Util.Bootstrapping;

namespace PocketMoney.Admin
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            new PerWebRequestLifestyleModule().Init(this);
        }

        protected void Application_Start()
        {
            ContainerRegistration.Register(BuildManager.Instance);

#if DEBUG

            if (Properties.Settings.Default.DeploymentMode)
            {
                Bootstrapper.Instance
                    .Include<ConfigureLog4Net>()
                    .Include<EnsureLatestSchema>()
                    .Include<DataBuilder>();

                Bootstrapper.Instance.Init();
            }

#endif
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}