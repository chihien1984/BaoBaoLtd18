using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;


namespace quanlysanxuat
{
    public class Datalayer
    {
        public string KeyAll = "hienxamxi";
        public string Encrypt(string strEnCrypt)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(strEnCrypt);
                byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.KeyAll));
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.Key = hash;
                cryptoServiceProvider.Mode = CipherMode.ECB;
                cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                byte[] inArray = cryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
                return Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch
            {
            }
            return "";
        }

        public string Decrypt(string strDecypt)
        {
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(strDecypt);
                byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(this.KeyAll));
                TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                cryptoServiceProvider.Key = hash;
                cryptoServiceProvider.Mode = CipherMode.ECB;
                cryptoServiceProvider.Padding = PaddingMode.PKCS7;
                return Encoding.UTF8.GetString(cryptoServiceProvider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return "";
        }
    }
}
