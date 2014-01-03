using System;
using System.Diagnostics;
using System.Threading;

namespace PocketMoney.Util.ExtensionMethods
{
    public static class ReaderWriterLockSlimExtensions
    {
        [DebuggerStepThrough]
        public static IDisposable ReadAndMaybeWrite(this ReaderWriterLockSlim instance)
        {
            instance.EnterUpgradeableReadLock();

            return new SynchronizedCodeBlock(instance.ExitUpgradeableReadLock);
        }

        [DebuggerStepThrough]
        public static IDisposable Read(this ReaderWriterLockSlim instance)
        {
            instance.EnterReadLock();

            return new SynchronizedCodeBlock(instance.ExitReadLock);
        }

        [DebuggerStepThrough]
        public static IDisposable Write(this ReaderWriterLockSlim instance)
        {
            instance.EnterWriteLock();

            return new SynchronizedCodeBlock(instance.ExitWriteLock);
        }

        #region Nested type: SynchronizedCodeBlock

        private sealed class SynchronizedCodeBlock : IDisposable
        {
            private readonly Action action;

            [DebuggerStepThrough]
            public SynchronizedCodeBlock(Action action)
            {
                this.action = action;
            }

            #region IDisposable Members

            [DebuggerStepThrough]
            public void Dispose()
            {
                action();
            }

            #endregion
        }

        #endregion
    }
}