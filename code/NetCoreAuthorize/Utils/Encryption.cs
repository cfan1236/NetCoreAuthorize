using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCoreAuthorize.Utils
{
    /// <summary>
    /// 加密
    /// </summary>
    public class Encryption
    {
        #region DES加密
        /// <summary>
        /// DES加密字
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string DesEncrypt(string encryptString, string encryptKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(encryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(GetMD5(encryptKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(GetMD5(encryptKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        private static string GetMD5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            md5.Clear();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                str.Append(data[i].ToString("X").PadLeft(2, '0'));
            }
            return str.ToString();
        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DesDecrypt(string decryptString, string decryptKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(GetMD5(decryptKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(GetMD5(decryptKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region base64加密
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="entryStr"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string entryStr)
        {
            string base64 = string.Empty;
            if (!string.IsNullOrWhiteSpace(entryStr))
            {
                byte[] bytes = Encoding.Default.GetBytes(entryStr);
                base64 = Convert.ToBase64String(bytes);
            }
            return base64;
        }
        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="entryStr"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string entryStr)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(entryStr))
            {
                byte[] bytes = new byte[] { };

                bytes = Convert.FromBase64String(entryStr);
                str = Encoding.Default.GetString(bytes);
            }
            return str;
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="entryStr"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string entryStr)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(entryStr);
            byte[] hash = md5.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", "");
        }
        #endregion

        #region AES加密
        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion

    }
}
