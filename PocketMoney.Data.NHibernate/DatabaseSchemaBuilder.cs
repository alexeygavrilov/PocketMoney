using PocketMoney.Util;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace PocketMoney.Data.NHibernate
{
    public class DatabaseSchemaBuilder : Disposable, IDatabaseSchemaBuilder
    {
        private static Configuration _configuration;


        public DatabaseSchemaBuilder(IBuildManager buildManager, Configuration configuration)
        {
            _configuration = configuration;
            BuildManager = buildManager;
        }


        protected IBuildManager BuildManager { get; private set; }

        #region IDatabaseSchemaBuilder Members

        public virtual void CreateSchema()
        {
            GetSchema().Create(false, true);
        }

        public virtual void DropSchema()
        {
            GetSchema().Drop(false, true);
        }

        #endregion

        private static SchemaExport GetSchema()
        {
            return new SchemaExport(_configuration);
        }
    }
}