namespace PocketMoney.Util.DataImport
{
    public class ImportHandlerFactory : IImportHandlerFactory
    {
        private readonly ColumnMetadataRegistry columnsRegistry;
        private readonly ColumnMatcherRegistry matchersRegistry;
        private readonly RowPersisterRegistry persistersRegistry;
        private readonly RowValidatorRegistry validatorsRegistry;

        public ImportHandlerFactory(
            ColumnMatcherRegistry matchersRegistry,
            ColumnMetadataRegistry columnsRegistry,
            RowValidatorRegistry validatorsRegistry,
            RowPersisterRegistry persistersRegistry)
        {
            this.matchersRegistry = matchersRegistry;
            this.columnsRegistry = columnsRegistry;
            this.validatorsRegistry = validatorsRegistry;
            this.persistersRegistry = persistersRegistry;
        }

        #region IImportHandlerFactory Members

        public IImportHandler Create(string template)
        {
            return new ImportHandler(
                matchersRegistry.Registrations[template],
                columnsRegistry.Registrations[template],
                validatorsRegistry.Registrations[template],
                persistersRegistry.Registrations[template]);
        }

        #endregion
    }
}