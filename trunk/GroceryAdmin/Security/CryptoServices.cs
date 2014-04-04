using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Admin.Portal.Security
{
    public static class CryptoServices
    {
        #region "Encrypting/Decrypting data for temporary usage"
        /* ********************************************************
		 * These methods use Rijndael algorithm.
		 * The key is used from config file and can be changed. So use
		 * these methods for encrypting data on temperory basis. Don't use
		 * where encrypted text may get persisted (saved in database or 
		 * bookmarked as a part of url etc).
		 * *********************************************************/

        private static byte[] _Key = null;
        private static byte[] _IV = null;

        static CryptoServices()
        {
            // Generate the key used for encryption/decryption

            // TODO Read key from configuration
            /* ConfigManager.GetInstance().General.EncryptionKey; */
            string key = string.Empty;
            if (0 == key.Length)
            {
                // Use some default
                key = "Secret Key Used For Encryption/Decryption!";
            }

            // Lets salt the key - ideally, we could have use current host header
            // but the application is accessed by multiple host headers and in 
            // web farm scenario, it can be an issue if different servers were 
            // accessed with different host headers. As of now, I am using 
            // hard-coded string due to lack of better alternative.
            string salt = "some random stuff";
            // Make sure that salt is at lease 8 characters long
            if (salt.Length < 8)
            {
                salt += "lets add some padding";
            }

            // Create key generator for creating key and IV
            var generator = new Rfc2898DeriveBytes(key,
                ASCIIEncoding.ASCII.GetBytes(salt), 100);

            // We will use 256 bit key and 128 bit IV
            _Key = generator.GetBytes(32);
            _IV = generator.GetBytes(16);

        }

        private static SymmetricAlgorithm GetCryptoProvider()
        {
            var cryptoProvider = new RijndaelManaged();
            cryptoProvider.BlockSize = 128;
            cryptoProvider.Padding = PaddingMode.PKCS7;
            return cryptoProvider;
        }

        /// <summary>
        /// Encrypts the input text based on configured encryption key. Should be
        /// used for temperory (within session) encryption.
        /// </summary>
        public static string EncryptText(string text)
        {
            var cryptoProvider = GetCryptoProvider();
            try
            {
                var encrypto = cryptoProvider.CreateEncryptor(_Key, _IV);
                var result = new MemoryStream();
                using (var encryptoStream = new CryptoStream(result, encrypto,
                    CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(encryptoStream))
                    {
                        // Write all data to the stream.
                        writer.Write(text);
                    }
                }
                return Convert.ToBase64String(result.ToArray());
            }
            finally
            {
                // Clears the provider state
                cryptoProvider.Clear();
            }
        }

        /// <summary>
        /// Decrypts the input text based on configured encryption key. Should be
        /// used for temperory (within session) encryption.
        /// </summary>
        public static string DecryptText(string encryptedText)
        {
            var cryptoProvider = GetCryptoProvider();
            try
            {
                var decrypto = cryptoProvider.CreateDecryptor(_Key, _IV);
                using (var input = new MemoryStream(
                    Convert.FromBase64String(encryptedText)))
                {
                    using (var decryptoStream = new CryptoStream(input, decrypto,
                        CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(decryptoStream))
                        {
                            // read from the stream
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                // Clear the provider state
                cryptoProvider.Clear();
            }
        }

        #endregion

        #region "Hash Value generation"

        public enum HashType
        {
            MD5,
            Sha1,
            Sha256,
            Sha512
        }

        /// <summary>
        /// Computes the hash of given text using given hash algorithm
        /// </summary>
        public static string ComputeHash(HashType type, string text)
        {
            // Encode the text into byte array
            var data = (new UTF8Encoding()).GetBytes(text);
            // Create hash provider
            HashAlgorithm algorithm = null;
            switch (type)
            {
                case HashType.Sha1:
                    algorithm = new SHA1Managed();
                    break;

                case HashType.Sha256:
                    algorithm = new SHA1Managed();
                    break;

                case HashType.Sha512:
                    algorithm = new SHA1Managed();
                    break;

                default:
                    algorithm = new MD5CryptoServiceProvider();
                    break;
            }
            // Compute hash value
            var hash = algorithm.ComputeHash(data);
            // Decodes to string 
            return Convert.ToBase64String(hash);
        }
        #endregion

    }
}