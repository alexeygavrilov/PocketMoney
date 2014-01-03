namespace PocketMoney.Util.DataImport
{
    public interface IImportHandlerFactory
    {
        IImportHandler Create(string template);
    }
}