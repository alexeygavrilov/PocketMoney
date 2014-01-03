using System;
using System.Runtime.InteropServices;

namespace PocketMoney.Util.Interop
{
    public class DisposableCOM : IDisposable
    {
        private object _comObject;

        #region Implementation of IDisposable

        private bool _disposed;

        public DisposableCOM(object comObject)
        {
            _comObject = comObject;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_comObject != null)
                {
                    if (Marshal.IsComObject(_comObject))
                        Marshal.ReleaseComObject(_comObject);
                    _comObject = null;
                }

                _disposed = true;
            }
        }

        ~DisposableCOM()
        {
            Dispose(false);
        }

        #endregion
    }
}