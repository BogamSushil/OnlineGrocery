using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EazyWizy.Framework.Configuration
{
    internal static class CryptoManager
    {
        private const string PrivateKey = "klnairzeq12@&weriouwezxcnieropioedaklfljipruwevnsdnfioweujriweurdaflqweiopiasdklmpoieqrnklnskdnfdsfknefndfnssdfsdfklnairzeq12@&weriouwezxcnieropioedaklfljipruwevnsdnfioweujriweurdaflqweiopiasdklmpoieqrnklnskdnfdsfknefndfnssdfsdfklnairzeq12@&weriouwezxcnieropioedaklfljipruwevnsdnfioweujriw";
        private static readonly byte[] Salt = new byte[] { 80, 111, 119, 101, 114, 84, 114, 97, 99, 107 };

        public static string EncryptDataWithStaticPrivateKey(string clearText, string key)
        {
            string completeKey = GetEncryptionKey(key);
            return Encrypt(clearText, completeKey);
        }

        public static string EncryptData(string clearText, string key)
        {
            string completeKey = key;
            return Encrypt(clearText, completeKey);
        }

        public static string DecryptDataWithStaticPrivateKey(string clearText, string key)
        {
            string completeKey = GetEncryptionKey(key);
            return Decrypt(clearText, completeKey);
        }

        public static string DecryptData(string clearText, string key)
        {
            string completeKey = key;
            return Decrypt(clearText, completeKey);
        }

        private static string Encrypt(string clearText, string key)
        {
            byte[] clearBytes = Encoding.ASCII.GetBytes(clearText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, Salt);

            byte[] encryptedData = _Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string cipherText, string key)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, Salt);

            byte[] decryptedData = _Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Encoding.ASCII.GetString(decryptedData);
        }

        private static string GetEncryptionKey(string key)
        {
            return PrivateKey + key;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private static byte[] _Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            using (Rijndael alg = Rijndael.Create())
            {
                alg.Key = Key;
                alg.IV = IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearData, 0, clearData.Length);
                        cs.FlushFinalBlock();
                    }

                    return ms.ToArray();
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private static byte[] _Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            try
            {
                using (Rijndael alg = Rijndael.Create())
                {
                    alg.Key = Key;
                    alg.IV = IV;

                    // Create a MemoryStream that is going to accept the decrypted bytes 
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherData, 0, cipherData.Length);
                            cs.FlushFinalBlock();
                        }

                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Decryption failed. Possibly due to incorrect key or corrupted data.", ex);
            }
        }
    }
}
