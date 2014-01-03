using PocketMoney.Util.Bootstrapping;
using PocketMoney.Data;

namespace PocketMoney.Configuration.Web
{
    public class EnsureLatestSchema : BootstrappingTask
    {
        private readonly IDatabaseSchemaBuilder _databaseSchemaBuilder;

        public EnsureLatestSchema(IDatabaseSchemaBuilder databaseSchemaBuilder)
        {
            _databaseSchemaBuilder = databaseSchemaBuilder;
        }

        public override void Execute()
        {
            _databaseSchemaBuilder.CreateSchema();
        }
    }
}