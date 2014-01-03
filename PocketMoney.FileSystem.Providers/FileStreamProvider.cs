using System;
using System.IO;
using PocketMoney.Util.ExtensionMethods;
using PocketMoney.Util.Threading;
using PocketMoney.Util.IO;

namespace PocketMoney.FileSystem
{
    public sealed class FileStreamProvider : IStreamProvider
    {
        private readonly string _file;

        private readonly Func<Stream, Stream> _streamTransformer;

        public FileStreamProvider(string file, Func<Stream, Stream> streamTransformer)
        {
            _file = file;

            _streamTransformer = streamTransformer;
        }

        #region IStreamProvider Members

        public Stream GetStream()
        {
            if (!System.IO.File.Exists(_file))
                throw new FileNotFoundException(_file).LogError();
            return System.IO.File.OpenRead(_file);
            //return new AutoLockStream(() => _streamTransformer(System.IO.File.OpenRead(_file)),
            //                          ReadWriteSynchronizer<string>.Instance.CreateReadLock(_file));
        }

        public byte[] Content
        {
            get { return this.GetStream().ToArray(); }
            set
            {
                if (!System.IO.File.Exists(_file))
                    throw new FileNotFoundException(_file).LogError();
                System.IO.File.WriteAllBytes(_file, value);
            }
        }

        #endregion
    }
}