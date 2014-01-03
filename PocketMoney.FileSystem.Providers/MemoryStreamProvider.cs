namespace PocketMoney.FileSystem
{
    using System;
    using System.IO;

    public sealed class MemoryStreamProvider : IStreamProvider
    {
        private byte[] _data;

        private readonly Func<Stream, Stream> _streamTransformer;

        public MemoryStreamProvider(byte[] data, Func<Stream, Stream> streamTransformer)
        {
            _data = data;
            _streamTransformer = streamTransformer;
        }

        #region IStreamProvider Members

        public Stream GetStream()
        {
            return _streamTransformer(new MemoryStream(_data));
        }

        public byte[] Content
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion
    }
}