using System;
using System.Collections.Generic;

namespace PocketMoney.Util.Services.Configuration
{
    public interface IServiceConfiguration
    {
        string Description { get; }

        string DisplayName { get; }

        string ServiceName { get; }
        string TypeName { get; }
        IDictionary<string, string> Settings { get; }
        Type GetServiceType();
    }
}