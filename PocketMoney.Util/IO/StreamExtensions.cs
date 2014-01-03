using System;
using System.IO;
using System.Security.Cryptography;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.IO
{
    public static class StreamExtensions
    {
        public static void CopyToEx(this Stream src, Stream dest)
        {
            if (src == null || dest == null)
            {
                throw new ArgumentNullException().LogError();
                ;
            }

            if (!src.CanRead)
                throw new InvalidOperationException("Source stream is not readable!").LogError();
            if (!dest.CanWrite)
                throw new InvalidOperationException("Destination stream is not writable!").LogError();
            try
            {
                if (src.Length == 0)
                {
                    return;
                }
            }
            catch (IOException e)
            {
                e.LogDebug();
            }
            catch (NotSupportedException e)
            {
                e.LogDebug();
            }


            try
            {
                if (dest.Length > 0)
                    dest.SetLength(0);
            }
            catch (IOException e)
            {
                e.LogDebug();
            }
            catch (NotSupportedException e)
            {
                e.LogDebug();
            }

            const int size = 2048;
            var bytes = new byte[size];
            long oldPos = src.CanSeek?src.Position:0;
            try
            {
                if (src.CanSeek)
                    src.Position = 0;
                int numBytes;
                while ((numBytes = src.Read(bytes, 0, size)) > 0)
                    dest.Write(bytes, 0, numBytes);
                if (dest.CanSeek)
                    dest.Position = 0;
            }
            finally
            {
                if ((src.CanSeek) && (src.Position != oldPos))
                    src.Position = oldPos;
            }
        }

        public static void GetMD5(this Stream src, out long md5hi, out long md5lo)
        {
            if (src == null) throw new ArgumentNullException("src").LogError();
            if (!src.CanRead)
                throw new InvalidOperationException("Source stream is not readable!").LogError();
            try
            {
                if (src.Length == 0)
                {
                    md5hi = 0;
                    md5lo = 0;
                    return;
                }
            }
            catch (IOException e)
            {
                e.LogDebug();
            }
            catch (NotSupportedException e)
            {
                e.LogDebug();
            }
            long oldPos = src.Position;
            if (src.CanSeek)
                src.Position = 0;
            if (src.Position != 0)
                throw new InvalidOperationException("Stream position is not set to begging of the stream").LogError();
            using (MD5 hash = MD5.Create())
                try
                {
                    byte[] result = hash.ComputeHash(src);
                    int i;
                    if (result.Length != 0x10)
                        throw new ArgumentException(String.Format("Invalid MD5 hash lenght: {0}", result.Length));
                    md5hi = md5lo = 0L;
                    for (i = 0; i < 8; i++)
                    {
                        md5hi |= (result[i] & 0xffL) << (i*8);
                        md5lo |= (result[i + 8] & 0xffL) << (i*8);
                    }
                }
                finally
                {
                    if ((src.CanSeek) && (src.Position != oldPos))
                        src.Position = oldPos;
                }
        }
        ///// <summary>
        ///// Opens the stream from provider, reads the data into byte array and closes the stream
        ///// WARNING! Avoid use of byte[] at all costs where possible. They may eatup all the memory if original stream is large/
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //public static byte[] ToArray(this IStreamProvider file)
        //{
        //    using (var srcStream = file.GetStream())
        //    {
        //        return ToArray(srcStream);
        //    }
        //}

        /// <summary>
        /// Copies stream to a byte array and DOES NOT CLOSE the stream! Please use some caution here and dont forget to close the stream as soon as possible.
        /// IStreamProvider.ToArray() can be a better alternative to this method.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        //[Obsolete("WARNING! Avoid use of byte[] at all costs where possible. They may chewup all the memory and cause performance/stability issues, if original stream is large or unatttentionally left open. Use IStreamProvider.ToArray() where possible.")]
        public static byte[] ToArray(this Stream stream)
        {
            var temp = stream as MemoryStream;
            if (temp != null) return temp.ToArray();
            using (var result = new MemoryStream())
            {
                stream.CopyToEx(result);
                return result.ToArray();
            }
        }
    }
}