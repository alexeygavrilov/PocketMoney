using PocketMoney.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using RealBuildManager = System.Web.Compilation.BuildManager;

namespace PocketMoney.Configuration.Web
{
    public sealed class BuildManager : IBuildManager
    {
        private static readonly BuildManager instance = new BuildManager();
        private IEnumerable<Assembly> applicationAssemblies;
        private IEnumerable<Assembly> privateAssemblies;

        public static IBuildManager Instance
        {
            [DebuggerStepThrough]
            get { return instance; }
        }

        #region IBuildManager Members

        public IEnumerable<Assembly> PrivateAssemblies
        {
            get
            {
                return privateAssemblies ?? (privateAssemblies = RealBuildManager.GetReferencedAssemblies()
                                                                     .Cast<Assembly>()
                                                                     .Where(assembly => !assembly.GlobalAssemblyCache)
                                                                     .ToList());
            }
        }


        public IEnumerable<Assembly> ApplicationAssemblies
        {
            get { return applicationAssemblies ?? (applicationAssemblies = GenerateApplicationAssemblyList()); }
        }

        #endregion

        private IEnumerable<Assembly> GenerateApplicationAssemblyList()
        {
            // Debugger.Break();
            var knownAssemblyPrefixes = new[] { "PocketMoney" };

            IEnumerable<Assembly> assemblies =
                PrivateAssemblies.Where(
                    assembly =>
                    knownAssemblyPrefixes.Any(
                        prefix => assembly.GetName().Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))).
                    ToList();

            return assemblies;
        }
    }
}