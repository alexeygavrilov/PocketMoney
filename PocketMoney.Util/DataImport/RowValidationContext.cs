namespace PocketMoney.Util.DataImport
{
    public class RowValidationContext
    {
        public RowValidationContext(ColumnMetadata metadata, string value, Row row)
        {
            Metadata = metadata;
            Value = value;
            Row = row;
        }

        public ColumnMetadata Metadata { get; private set; }

        public string Value { get; private set; }

        public Row Row { get; private set; }
    }
}