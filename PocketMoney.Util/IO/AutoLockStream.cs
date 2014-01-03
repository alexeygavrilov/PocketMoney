using System;
using System.IO;
using System.Runtime.Remoting;
using System.Security.Permissions;
using PocketMoney.Util.Threading;

namespace PocketMoney.Util.IO
{
    public class AutoLockStream : Stream
    {
        private readonly bool _isReadOnly;
        private bool _isLocked;
        private ILock _lock;
        private Stream _target;
        private Func<Stream> _targetStreamProvider;

        public AutoLockStream(Func<Stream> targetStreamProvider, ILock lck)
        {
            if (targetStreamProvider == null)
                throw new ArgumentException("Stream provider cant be null!", "targetStreamProvider");
            _targetStreamProvider = targetStreamProvider;
            _lock = lck;
            _isReadOnly = !lck.CanWrite;
        }

        public override bool CanTimeout
        {
            get
            {
                MakeSureLock();
                return _target.CanTimeout;
            }
        }

        public override int ReadTimeout
        {
            get
            {
                MakeSureLock();
                return _target.ReadTimeout;
            }
            set
            {
                MakeSureLock();
                _target.ReadTimeout = value;
            }
        }


        public override int WriteTimeout
        {
            get
            {
                MakeSureLock();
                return _target.WriteTimeout;
            }
            set
            {
                MakeSureLock();
                _target.WriteTimeout = value;
            }
        }

        ///<summary>
        ///When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        ///</summary>
        ///
        ///<returns>
        ///true if the stream supports reading; otherwise, false.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public override bool CanRead
        {
            get
            {
                MakeSureLock();
                return _target.CanRead;
            }
        }

        ///<summary>
        ///When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        ///</summary>
        ///
        ///<returns>
        ///true if the stream supports seeking; otherwise, false.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public override bool CanSeek
        {
            get
            {
                MakeSureLock();
                return _target.CanSeek;
            }
        }

        ///<summary>
        ///When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        ///</summary>
        ///
        ///<returns>
        ///true if the stream supports writing; otherwise, false.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public override bool CanWrite
        {
            get
            {
                MakeSureLock();
                return !_isReadOnly && _target.CanWrite;
            }
        }

        ///<summary>
        ///When overridden in a derived class, gets the length in bytes of the stream.
        ///</summary>
        ///
        ///<returns>
        ///A long value representing the length of the stream in bytes.
        ///</returns>
        ///
        ///<exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
        ///<exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception><filterpriority>1</filterpriority>
        public override long Length
        {
            get
            {
                MakeSureLock();
                return _target.Length;
            }
        }

        ///<summary>
        ///When overridden in a derived class, gets or sets the position within the current stream.
        ///</summary>
        ///
        ///<returns>
        ///The current position within the stream.
        ///</returns>
        ///
        ///<exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        ///<exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
        ///<exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception><filterpriority>1</filterpriority>
        public override long Position
        {
            get
            {
                MakeSureLock();
                return _target.Position;
            }
            set
            {
                MakeSureLock();
                _target.Position = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_target == null) return;
            _target.Dispose();
            Unlock();
            _target = null;
            _targetStreamProvider = null;
        }

        private void MakeSureLock()
        {
            lock (_lock)
            {
                if (_isLocked) return;
                _isLocked = _lock.Acquire();
                if (!_isLocked)
                    throw new IOException("Failed to acquire lock!");
                _target = _targetStreamProvider();
            }
        }

        private void Unlock()
        {
            if (_lock != null)
                lock (_lock)
                {
                    if (!_isLocked) return;
                    _isLocked = false;
                    _lock.Release();
                    _lock = null;
                }
        }

        public override void Close()
        {
            if (_target != null)
                _target.Close();
            Unlock();
        }

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback,
                                               object state)
        {
            MakeSureLock();
            return _target.BeginRead(buffer, offset, count, callback, state);
        }

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback,
                                                object state)
        {
            MakeSureLock();
            return _target.BeginWrite(buffer, offset, count, callback, state);
        }

        public override int ReadByte()
        {
            MakeSureLock();
            return _target.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            MakeSureLock();
            _target.WriteByte(value);
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            MakeSureLock();
            return _target.CreateObjRef(requestedType);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            MakeSureLock();
            return _target.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            MakeSureLock();
            _target.EndWrite(asyncResult);
        }

        public override object InitializeLifetimeService()
        {
            MakeSureLock();
            return _target.InitializeLifetimeService();
        }


        ///<summary>
        ///When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        ///</summary>
        ///
        ///<exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>2</filterpriority>
        public override void Flush()
        {
            MakeSureLock();
            _target.Flush();
        }

        ///<summary>
        ///When overridden in a derived class, sets the position within the current stream.
        ///</summary>
        ///
        ///<returns>
        ///The new position within the current stream.
        ///</returns>
        ///
        ///<param name="offset">A byte offset relative to the origin parameter. </param>
        ///<param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"></see> indicating the reference point used to obtain the new position. </param>
        ///<exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        ///<exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
        ///<exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception><filterpriority>1</filterpriority>
        public override long Seek(long offset, SeekOrigin origin)
        {
            MakeSureLock();
            return _target.Seek(offset, origin);
        }

        ///<summary>
        ///When overridden in a derived class, sets the length of the current stream.
        ///</summary>
        ///
        ///<param name="value">The desired length of the current stream in bytes. </param>
        ///<exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
        ///<exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        ///<exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception><filterpriority>2</filterpriority>
        public override void SetLength(long value)
        {
            MakeSureLock();
            _target.SetLength(value);
        }

        ///<summary>
        ///When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        ///</summary>
        ///
        ///<returns>
        ///The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
        ///</returns>
        ///
        ///<param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream. </param>
        ///<param name="count">The maximum number of bytes to be read from the current stream. </param>
        ///<param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source. </param>
        ///<exception cref="T:System.ArgumentException">The sum of offset and count is larger than the buffer length. </exception>
        ///<exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        ///<exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
        ///<exception cref="T:System.ArgumentNullException">buffer is null. </exception>
        ///<exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        ///<exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception><filterpriority>1</filterpriority>
        public override int Read(byte[] buffer, int offset, int count)
        {
            MakeSureLock();
            return _target.Read(buffer, offset, count);
        }

        ///<summary>
        ///When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        ///</summary>
        ///
        ///<param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream. </param>
        ///<param name="count">The number of bytes to be written to the current stream. </param>
        ///<param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream. </param>
        ///<exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        ///<exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
        ///<exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
        ///<exception cref="T:System.ArgumentNullException">buffer is null. </exception>
        ///<exception cref="T:System.ArgumentException">The sum of offset and count is greater than the buffer length. </exception>
        ///<exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception><filterpriority>1</filterpriority>
        public override void Write(byte[] buffer, int offset, int count)
        {
            MakeSureLock();
            _target.Write(buffer, offset, count);
        }
    }
}