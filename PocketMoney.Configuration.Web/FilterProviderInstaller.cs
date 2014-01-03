using System.Web.Mvc;
using Castle.Core;

namespace PocketMoney.Configuration.Web
{
    public class FilterProviderInstaller : MvcInfrastructureInstaller<IFilterProvider>
    {
        protected override LifestyleType Lifestyle
        {
            get { return LifestyleType.Singleton; }
        }
    }
}