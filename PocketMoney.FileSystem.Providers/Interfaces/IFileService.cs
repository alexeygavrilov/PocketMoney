using PocketMoney.Data;

namespace PocketMoney.FileSystem
{
    public interface IFileService
    {
        File Store(IFile file, IFamily currentFamily, string securityDescriptor, IUser currentUser);
        File Retrieve(FileId fileId);
        File RenameFile(FileId fileId, string fileName);
        void RemoveAll();
    }
}
