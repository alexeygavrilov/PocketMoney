using System.Collections.Generic;
using System.Reflection;

namespace PocketMoney.Util
{
    public interface IBuildManager
    {
        IEnumerable<Assembly> PrivateAssemblies { get; }

        IEnumerable<Assembly> ApplicationAssemblies { get; }
    }
}