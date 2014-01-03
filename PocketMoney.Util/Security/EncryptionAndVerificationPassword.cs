using System;
using System.Text;
using System.Security.Cryptography;

namespace PocketMoney.Util.Security
{
    public class EncryptionAndVerificationPassword
    {
        private HashAlgorithm _hashProvider;
        private Int32 _salthLength;

        /// <summary>
        /// The constructor takes a HashAlgorithm as a parameter.
        /// </summary>
        /// <param name="HashAlgorithm">
        /// A <see cref="HashAlgorithm"/> HashAlgorihm which is derived from HashAlgorithm. C# provides
        /// the following classes: SHA1Managed,SHA256Managed, SHA384Managed, SHA512Managed and MD5CryptoServiceProvider
        /// </param>
        public EncryptionAndVerificationPassword(HashAlgorithm hashAlgorithm, Int32 theSaltLength)
        {
            _hashProvider = hashAlgorithm;
            _salthLength = theSaltLength;
        }

        /// <summary>
        /// Default constructor which initialises the SaltedHash with the SHA256Managed algorithm
        /// and a Salt of 10 bytes ( or 10*8 = 80 bits)
        /// </summary>
        public EncryptionAndVerificationPassword()
            : this(new SHA256Managed(), 10)
        {
        }

        /// <summary>
        /// The actual hash calculation is shared by both GetHashAndSalt and the VerifyHash functions
        /// </summary>
        /// <param name="Data">A byte array of the Data to Hash</param>
        /// <param name="salt">A byte array of the Salt to add to the Hash</param>
        /// <returns>A byte array with the calculated hash</returns>
        private Byte[] ComputeHash(Byte[] password, Byte[] salt)
        {
            // Allocate memory to store both the Data and Salt together
            Byte[] PasswordaAndSalt = new Byte[password.Length + _salthLength];

            // Copy both the data and salt into the new array
            Array.Copy(password, PasswordaAndSalt, password.Length);
            Array.Copy(salt, 0, PasswordaAndSalt, password.Length, _salthLength);

            // Calculate the hash
            // Compute hash value of our plain text with appended salt.
            return _hashProvider.ComputeHash(PasswordaAndSalt);
        }

        /// <summary>
        /// Given a data block this routine returns both a Hash and a Salt
        /// </summary>
        /// <param name="Data">
        /// A <see cref="System.Byte"/>byte array containing the data from which to derive the salt
        /// </param>
        /// <param name="hash">
        /// A <see cref="System.Byte"/>byte array which will contain the hash calculated
        /// </param>
        /// <param name="salt">
        /// A <see cref="System.Byte"/>byte array which will contain the salt generated
        /// </param>
        private void GetHashAndSalt(Byte[] password, out Byte[] hash, out Byte[] salt)
        {
            // Allocate memory for the salt
            salt = new Byte[_salthLength];

            // Strong runtime pseudo-random number generator, on Windows uses CryptAPI
            // on Unix /dev/urandom
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            // Create a random salt
            random.GetNonZeroBytes(salt);

            // Compute hash value of our data with the salt.
            hash = ComputeHash(password, salt);
        }

        /// <summary>
        /// The routine provides a wrapper around the GetHashAndSalt function providing conversion
        /// from the required byte arrays to strings. Both the Hash and Salt are returned as Base-64 encoded strings.
        /// </summary>
        /// <param name="Data">
        /// A <see cref="System.String"/> string containing the data to hash
        /// </param>
        /// <param name="hash">
        /// A <see cref="System.String"/> base64 encoded string containing the generated hash
        /// </param>
        /// <param name="salt">
        /// A <see cref="System.String"/> base64 encoded string containing the generated salt
        /// </param>
        public void GetHashAndSalt(String password, out String hash, out String salt)
        {
            Byte[] hashOut;
            Byte[] saltOut;

            // Obtain the Hash and Salt for the given string
            GetHashAndSalt(System.Text.Encoding.UTF8.GetBytes(password), out hashOut, out saltOut);

            // Transform the byte[] to Base-64 encoded strings
            hash = Convert.ToBase64String(hashOut);
            salt = Convert.ToBase64String(saltOut);
        }

        

        /// <summary>
        /// This routine verifies whether the data generates the same hash as we had stored previously
        /// </summary>
        /// <param name="Data">The data to verify </param>
        /// <param name="hash">The hash we had stored previously</param>
        /// <param name="salt">The salt we had stored previously</param>
        /// <returns>True on a succesfull match</returns>
        private Boolean VerifyHash(Byte[] password, Byte[] hash, Byte[] salt)
        {
            Byte[] newHash = ComputeHash(password, salt);

            //  No easy array comparison in C# -- we do the legwork
            if (newHash.Length != hash.Length) return false;

            for (Int32 Lp = 0; Lp < hash.Length; Lp++)
                if (!hash[Lp].Equals(newHash[Lp]))
                    return false;
            return true;
        }

        /// <summary>
        /// This routine provides a wrapper around VerifyHash converting the strings containing the
        /// data, hash and salt into byte arrays before calling VerifyHash.
        /// </summary>
        /// <param name="password">A UTF-8 encoded string containing the data to verify</param>
        /// <param name="hash">A base-64 encoded string containing the previously stored hash</param>
        /// <param name="salt">A base-64 encoded string containing the previously stored salt</param>
        /// <returns></returns>
        public Boolean VerifyHash(String password, String hash, String salt)
        {
            Byte[] HashToVerify = Convert.FromBase64String(hash);
            Byte[] SaltToVerify = Convert.FromBase64String(salt);
            Byte[] PasswordToVerify = System.Text.Encoding.UTF8.GetBytes(password);
            return VerifyHash(PasswordToVerify, HashToVerify, SaltToVerify);
        }
    }
}
