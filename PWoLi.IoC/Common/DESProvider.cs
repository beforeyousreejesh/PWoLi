using System;
using System.Security.Cryptography;
using System.Text;

namespace PWoLi.IoC
{
    internal class DESProvider
    {
        internal static string Decrypt(string encryptedText, string passPhrase)
        {
            byte[] b = Convert.FromBase64String(encryptedText);
            TripleDES tDes = CreateDES(passPhrase);
            ICryptoTransform ct = tDes.CreateDecryptor();
            byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
            return Encoding.Unicode.GetString(output);
        }

        #region Private Methods

        private static TripleDES CreateDES(string passphrase)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES tDes = new TripleDESCryptoServiceProvider();
            tDes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(passphrase));
            tDes.IV = new byte[tDes.BlockSize / 8];
            return tDes;
        }

        #endregion Private Methods
    }
}