using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Listening.Server.Security;

namespace Listening.Infrastructure.Extensions
{
    public static class EncryptStringExtensions
    {
        public static byte[] EncryptStringToBytes(this string plainText)
        {
            var rijAlg = SecurityRulesSingleton.Instance.Rules.RijndaelManaged;

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (rijAlg.Key == null || rijAlg.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (rijAlg.IV == null || rijAlg.IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);

                encrypted = msEncrypt.ToArray();
                return encrypted;
            }
        }

        public static string DecryptStringFromBytes(this byte[] cipherText)
        {
            var rijAlg = SecurityRulesSingleton.Instance.Rules.RijndaelManaged;

            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (rijAlg.Key == null || rijAlg.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (rijAlg.IV == null || rijAlg.IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                // Read the decrypted bytes from the decrypting stream
                // and place them in a string.
                plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
    }
}
