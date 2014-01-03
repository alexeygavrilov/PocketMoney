using System;

namespace PocketMoney.FileSystem
{
    public interface ITempFile : IDisposable, IFile
    {
        string Path { get; }
    }

    public interface ITempFolder : IDisposable
    {
        string Path { get; }
    }
}