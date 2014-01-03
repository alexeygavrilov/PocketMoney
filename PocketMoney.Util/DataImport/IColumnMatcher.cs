namespace PocketMoney.Util.DataImport
{
    public interface IColumnMatcher
    {
        bool Matching(string column, ColumnMetadata metadata);
    }
}