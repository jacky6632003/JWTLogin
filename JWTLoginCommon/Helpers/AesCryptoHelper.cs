using System;
using System.Security.Cryptography;
using System.Text;

namespace JWTLoginCommon.Helpers
{
   public static class AesCryptoHelper
    {
        public static string Encrypt(string key, string iv, string plain_text)
        {

            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            byte[] bPlainText = Encoding.UTF8.GetBytes(plain_text);
            byte[] outputData = transform.TransformFinalBlock(bPlainText, 0, bPlainText.Length);
            return Convert.ToBase64String(outputData);
        }

        public static string Decrypt(string key, string iv, string base64String)
        {
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
            byte[] bEnBase64String = null;
            byte[] outputData = null;
            try
            {
                bEnBase64String = Convert.FromBase64String(base64String);
                outputData = transform.TransformFinalBlock(bEnBase64String, 0, bEnBase64String.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($@"解密出錯:{ex.Message}");
            }
            return Encoding.UTF8.GetString(outputData);
        }
    }
}
