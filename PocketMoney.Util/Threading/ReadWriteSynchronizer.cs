using System;
using System.Collections.Generic;
using System.Threading;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Threading
{
    public class ReadWriteSynchronizer<T> where T : IEquatable<T>
    {
        private static volatile ReadWriteSynchronizer<T> instance;
        private static readonly object syncRoot = new Object();

        private readonly Dictionary<LockKey, ReaderWriterLock> _runningLocks =
            new Dictionary<LockKey, ReaderWriterLock>();

        private ReadWriteSynchronizer()
        {
        }

        public static ReadWriteSynchronizer<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ReadWriteSynchronizer<T>();
                    }
                }

                return instance;
            }
        }

        public ILock CreateReadLock(T key)
        {
            ReaderWriterLockSlim locker = GetLock(key);
            Func<bool> enterLock = delegate
                                       {
                                           locker.EnterReadLock();
                                           return true;
                                       };
            Action exitLock = delegate
                                  {
                                      locker.ExitReadLock();
                                      RemoveLock(key);
                                  };
            return new Lock(enterLock, exitLock, false);
        }


        public ILock CreateWriteLock(T key)
        {
            ReaderWriterLockSlim locker = GetLock(key);
            Func<bool> enterLock = delegate
                                       {
                                           locker.EnterWriteLock();
                                           return true;
                                       };
            Action exitLock = delegate
                                  {
                                      locker.ExitWriteLock();
                                      RemoveLock(key);
                                  };
            return new Lock(enterLock, exitLock, true);
        }

        public ILock CreateReadLock(T key, TimeSpan timeout, Action onLockTimeout)
        {
            ReaderWriterLockSlim locker = GetLock(key);
            Func<bool> enterLock = delegate
                                       {
                                           bool result = locker.TryEnterReadLock(timeout);
                                           if (!result)
                                           {
                                               //      RemoveLock(key);
                                               onLockTimeout();
                                           }
                                           return result;
                                       };
            Action exitLock = delegate
                                  {
                                      if (locker.IsReadLockHeld)
                                          locker.ExitReadLock();
                                      RemoveLock(key);
                                  };

            return new Lock(enterLock, exitLock, false);
        }

        public ILock CreateWriteLock(T key, TimeSpan timeout, Action onLockTimeout)
        {
            ReaderWriterLockSlim locker = GetLock(key);
            Func<bool> enterLock = delegate
                                       {
                                           bool result = locker.TryEnterWriteLock(timeout);
                                           if (!result)
                                           {
                                               // RemoveLock(key);
                                               onLockTimeout();
                                           }
                                           return result;
                                       };
            Action exitLock = delegate
                                  {
                                      if (locker.IsWriteLockHeld)
                                          locker.ExitWriteLock();
                                      RemoveLock(key);
                                  };
            return new Lock(enterLock, exitLock, true);
        }


        public void WriteUnitary(T key, Action<T> action)
        {
            ReaderWriterLockSlim locker = GetLock(key);

            locker.EnterWriteLock();
            try
            {
                action(key);
            }
            finally
            {
                locker.ExitWriteLock();
                RemoveLock(key);
            }
        }

        public void ReadUnitary(T key, Action<T> action)
        {
            ReaderWriterLockSlim locker = GetLock(key);

            locker.EnterReadLock();
            try
            {
                action(key);
            }
            finally
            {
                locker.ExitReadLock();
                RemoveLock(key);
            }
        }

        private ReaderWriterLockSlim GetLock(T key)
        {
            lock (_runningLocks)
            {
                ReaderWriterLock lck = null;
                var newKey = new LockKey(key);
                if (_runningLocks.ContainsKey(newKey))
                {
                    lck = _runningLocks[newKey];
                    lck.RequestCount += 1;
                    return lck;
                }
                lck = new ReaderWriterLock();
                _runningLocks.Add(newKey, lck);
                return lck;
            }
        }

        private void RemoveLock(T key)
        {
            lock (_runningLocks)
            {
                var newKey = new LockKey(key);
                if (_runningLocks.ContainsKey(newKey))
                {
                    ReaderWriterLock lck = _runningLocks[newKey];
                    lck.RequestCount -= 1;
                    if (lck.RequestCount < 1)
                        _runningLocks.Remove(newKey);
                }
            }
        }

        #region Nested type: Lock

        private class Lock : ILock
        {
            private readonly bool _canWrite;
            private readonly Func<bool> _enterLock;
            private readonly Action _exitLock;

            public Lock(Func<bool> enterLock, Action exitLock, bool canWrite)
            {
                _enterLock = enterLock;
                _exitLock = exitLock;
                _canWrite = canWrite;
            }

            #region ILock Members

            public bool Acquire()
            {
                return _enterLock();
            }

            public void Release()
            {
                _exitLock();
            }

            public bool CanWrite
            {
                get { return _canWrite; }
            }

            #endregion
        }

        #endregion

        #region Nested type: LockKey

        private class LockKey
        {
            private readonly T _value;

            public LockKey(T value)
            {
                if (ReferenceEquals(null, value))
                    throw new ArgumentNullException().LogError();
                _value = value;
            }

            public override bool Equals(object obj)
            {
                var p = obj as LockKey;
                if (ReferenceEquals(this, p))
                    return true;
                return !ReferenceEquals(null, p) && _value.Equals(p._value);
            }

            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }
        }

        #endregion

        #region Nested type: ReaderWriterLock

        private class ReaderWriterLock : ReaderWriterLockSlim
        {
            public int RequestCount { get; set; }
        }

        #endregion
    }
}