using System;
using PocketMoney.Util.Services.Configuration;

namespace PocketMoney.Util.Services
{
    public class ServiceFactory
    {
        public static Service CreateService(string serviceName)
        {
            IServiceConfiguration configuration = ServicesConfiguration.Current.Services[serviceName];
            Type serviceType = configuration.GetServiceType();
            var serviceBase = (Service) Activator.CreateInstance(serviceType, configuration);
            serviceBase.ServiceName = configuration.ServiceName;
            return serviceBase;
        }
    }
}