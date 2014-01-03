using System;
using PocketMoney.Util.Bootstrapping;
using log4net;

namespace PocketMoney.Configuration.Web
{
    public class ConfigureLog4Net : BootstrappingTask
    {
        public override void Execute()
        {
            try
            {
                GlobalContext.Properties["COMPUTERNAME"] = Environment.MachineName;
            }
            catch
            {
            }
        }
    }
}