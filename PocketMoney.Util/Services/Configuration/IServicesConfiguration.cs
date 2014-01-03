using System.Collections.Generic;

namespace PocketMoney.Util.Services.Configuration
{
    public interface IServicesConfiguration
    {
        IDictionary<string, IServiceConfiguration> Services { get; }
    }
}