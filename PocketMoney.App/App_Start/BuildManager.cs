using PocketMoney.Data.NHibernate;
using PocketMoney.Data.NHibernate.Installers;
using PocketMoney.Data.Service.Installers;
using PocketMoney.FileSystem;
using PocketMoney.FileSystem.NHibernate;
using PocketMoney.FileSystem.Providers;
using PocketMoney.FileSystem.Service.Installers;
using PocketMoney.Model.Internal;
using PocketMoney.Model.NHibernate;
using PocketMoney.Service;
using PocketMoney.Service.Installers;
using PocketMoney.Util;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PocketMoney.ParentApp
{
    public class BuildManager : IBuildManager
    {
        private IEnumerable<Assembly> applicationAssemblies;

        public IEnumerable<Assembly> PrivateAssemblies
        {
            get { return new List<Assembly>(); }
        }

        public IEnumerable<Assembly> ApplicationAssemblies
        {
            get {
                return applicationAssemblies ?? (applicationAssemblies = (new[]
                           {
                               this.GetType().Assembly,
                               typeof(User).Assembly,
                               typeof(UserMap).Assembly,
                               typeof(ServicesInstaller).Assembly,
                               typeof(DataAccessInstaller).Assembly,
                               typeof(SecurityInstaller).Assembly,
                               typeof(File).Assembly,
                               typeof(FileMap).Assembly,
                               typeof(DeviceBase).Assembly,
                               typeof(FileStorageInstaller).Assembly
                           }).AsEnumerable());
            }
        }
    }
}
