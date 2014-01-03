namespace PocketMoney.FileSystem
{
    public interface ITempFileService
    {
        ITempFolder CreateTempFolder();
        ITempFile CreateTempFile(ITempFolder folder, string fileName, FileFormat format);
    }
}