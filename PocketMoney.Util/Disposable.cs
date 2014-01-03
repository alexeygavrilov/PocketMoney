using System;
using System.Diagnostics;

namespace PocketMoney.Util
{
    public abstract class Disposable : IDisposable
    {
        private bool _disposed;

        protected bool IsDisposed
        {
            get
            {
                return _disposed;
            }
        }

        protected void ThrowErrorIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().ToString());
        }
        #region IDisposable Members

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        [DebuggerStepThrough]
        ~Disposable()
        {
            Dispose(false);
        }

        [DebuggerStepThrough]
        protected virtual void DisposeCore()
        {
        }

        [DebuggerStepThrough]
        private void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
            {
                DisposeCore();
            }

            this._disposed = true;
        }
    }
}