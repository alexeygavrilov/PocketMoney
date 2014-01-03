namespace PocketMoney.FileSystem.Providers
{
    public sealed class AzureStorageDeviceInfo : IDeviceInfo
    {
        private readonly bool _isAvailable;
        private readonly long _spaceAvailable;
        private readonly long _spaceTotal;

        public AzureStorageDeviceInfo(bool isAvailable, long spaceAvailable, long spaceTotal)
        {
            _isAvailable = isAvailable;
            _spaceAvailable = spaceAvailable;
            _spaceTotal = spaceTotal;
        }

        #region IDeviceInfo Members

        public long SpaceTotal
        {
            get { return _spaceTotal; }
        }

        public long SpaceAvailable
        {
            get { return _spaceAvailable; }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
        }

        #endregion
    }
}