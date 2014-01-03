namespace PocketMoney.Util.DataImport
{
    public interface IRowPersister
    {
        bool Persist(Row row);
    }
}