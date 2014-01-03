// -----------------------------------------------------------------------
// <copyright file="AESCryptoStream.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PocketMoney.Util.IO
{
    using System;
    using System.IO;
    using System.Security.Cryptography;




    public class ReadOnlyStream: MemoryStream
    {

        protected ReadOnlyStream()
        {
            
        }
        protected ReadOnlyStream(byte[] buffer):base(buffer)
        {

        }

        protected ReadOnlyStream(byte[] buffer, bool writable): base(buffer, writable)
        {
            
        }
        protected ReadOnlyStream(byte[] buffer, int index, int count) : this(buffer, index, count, true, false){}
        protected ReadOnlyStream(byte[] buffer, int index, int count, bool writable) : this(buffer, index, count, writable, false){}
        protected ReadOnlyStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible):base(buffer,index,count,writable,publiclyVisible){}


        protected ReadOnlyStream(int capacity)
        {
            
        }
        public static Stream Encrypt(Stream unencryptedStream, byte[] password, byte[] salt)
        {
            var ms = new MemoryStream();
            using(var aes = new AESCryptoStream(ms, password, salt, false))
            {
                unencryptedStream.CopyToEx(aes);
                aes.FlushFinalBlock();
                var bytes = ms.ToArray();
                unencryptedStream.Dispose();
                return new ReadOnlyStream(bytes);
            }
        }
        public static Stream Decrypt(Stream toDecrypt, byte[] password, byte[] salt)
        {
          using (var aes = new AESCryptoStream(toDecrypt, password, salt, true))
          {
              var bytes = aes.ToArray();
              toDecrypt.Dispose();
              return new ReadOnlyStream(bytes, false);
          }
        }

        /// <summary>
        /// TODO: Update summary.
        /// </summary>
        private class AESCryptoStream : CryptoStream
        {

            public AESCryptoStream(Stream target, byte[] password, byte[] salt, bool read)
                : base(target, GetTransform(password, salt, read), read ? CryptoStreamMode.Read : CryptoStreamMode.Write)
            {
                if (target == null)
                {
                    throw new ArgumentNullException("target");
                }
            }

            private static ICryptoTransform GetTransform(byte[] password, byte[] salt, bool read)
            {

                if (password == null)
                {
                    throw new ArgumentNullException("password");
                }
                if (password.Length <= 8)
                {
                    throw new ArgumentException("Password must be at least 8 bytes long");
                }
                if (salt == null)
                {
                    throw new ArgumentNullException("salt");
                }
                if (salt.Length <= 8)
                    throw new ArgumentException("Salt must be at least 8 bytes long");
                Aes aes = new AesManaged();
                var deriveBytes = new Rfc2898DeriveBytes(password, salt, 1000);
                aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);
                aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                aes.Padding = PaddingMode.PKCS7;

                return read ? aes.CreateDecryptor() : aes.CreateEncryptor();
            }

        }
    }



}
