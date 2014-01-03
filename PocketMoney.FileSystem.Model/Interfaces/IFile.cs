using System;

namespace PocketMoney.FileSystem
{
    public interface IFile : IStreamProvider, IFileFormatInfo
    {
        Guid Id { get; }
        string FileNameWithExtension { get; }
        string Extension { get; }
        long Size { get; }
        //string MimeTypes { get; }
        string MetaInfo { get; set; }
        DateTime? DateCreated { get; }
    }

}