using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Luoyi.Common
{
    /// <summary>
    /// 字符加解密工具类
    /// </summary>
    public static class SecurityHelper
    {
        #region MD5加密

        /// <summary>
        /// 返回 System.String 对象进行MD5加密后的32字符十六进制格式字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="encode">编码</param>
        /// <returns>返回加密字符串</returns>
        public static string ToMD5(this string str, Encoding encode)
        {
            if (str == null)
                throw new ArgumentNullException("str");


            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(encode.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                foreach (var i in bytes)
                {
                    sb.Append(i.ToString("x2"));
                }
                return sb.ToString().ToUpper();
            }
        }
        /// <summary>
        /// 返回 System.String 对象进行MD5加密后的32字符十六进制格式字符串,默认编码UTF8
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>返回加密字符串</returns>
        public static string ToMD5(this string str)
        {
            return str.ToMD5(Encoding.UTF8);
        }
        /// <summary>
        /// 返回 System.String 对象进行MD5加密后的16字符十六进制格式字符串
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="encode">编码</param>
        /// <returns>返回加密字符串</returns>
        public static string ToMD5Bit16(this string str, Encoding encode)
        {
            return str.ToMD5(encode).Substring(8, 16);
        }

        /// <summary>
        /// 返回 System.String 对象进行MD5加密后的16字符十六进制格式字符串,默认编码GB2312
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>返回加密字符串</returns>
        public static string ToMD5Bit16(this string str)
        {
            return str.ToMD5Bit16(Encoding.UTF8);
        }
        #endregion

        #region Base64加密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="encode">字符编码，默认UTD-8</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string str, string encode = "UTF-8")
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return Convert.ToBase64String(Encoding.GetEncoding(encode).GetBytes(str));
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str">待解密字符串</param>
        /// <param name="encode">字符编码，默认UTD-8</param>
        /// <returns></returns>
        public static string FromBase64String(this string str, string encode = "UTF-8")
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            try
            {
                return Encoding.GetEncoding(encode).GetString(Convert.FromBase64String(str));
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region DES加解密
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="text">需解密的密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string ToDesDecrypt(this string text, string key)
        {
            DESCryptoServiceProvider provider1 = new DESCryptoServiceProvider();
            int num1 = text.Length / 2;
            byte[] buffer1 = new byte[num1];
            for (int num2 = 0; num2 < num1; num2++)
            {
                int num3 = Convert.ToInt32(text.Substring(num2 * 2, 2), 0x10);
                buffer1[num2] = (byte)num3;
            }
            provider1.Key = Encoding.ASCII.GetBytes(key.ToMD5().Substring(0, 8));
            provider1.IV = Encoding.ASCII.GetBytes(key.ToMD5().Substring(0, 8));

            MemoryStream stream1 = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream1, provider1.CreateDecryptor(), CryptoStreamMode.Write);

            stream2.Write(buffer1, 0, buffer1.Length);
            stream2.FlushFinalBlock();
            return Encoding.Default.GetString(stream1.ToArray());
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="str">需加密的明文文本</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string ToDesEncrypt(this string str, string key)
        {
            DESCryptoServiceProvider provider1 = new DESCryptoServiceProvider();
            byte[] buffer1 = Encoding.Default.GetBytes(str);
            provider1.Key = Encoding.ASCII.GetBytes(key.ToMD5().Substring(0, 8));
            provider1.IV = Encoding.ASCII.GetBytes(key.ToMD5().Substring(0, 8));
            MemoryStream stream1 = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream1, provider1.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer1, 0, buffer1.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder1 = new StringBuilder();
            foreach (byte num1 in stream1.ToArray())
            {
                builder1.AppendFormat("{0:X2}", num1);
            }
            return builder1.ToString();
        }
        #endregion

        #region RSA加解密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="encryptString">需要加密的文本</param>
        /// <param name="xmlPrivateKey">加密私钥</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public static string ToRSAEncrypt(this string encryptString, string xmlPrivateKey)
        {
            string result;
            try
            {
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] bytes = new UnicodeEncoding().GetBytes(encryptString);
                result = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result;
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="decryptString">需要解密的文本</param>
        /// <param name="xmlPublicKey">解密公钥</param>
        /// <returns>解密后的数据</returns>
        public static string ToRSADecrypt(this string decryptString, string xmlPublicKey)
        {
            string result;
            try
            {
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] rgb = Convert.FromBase64String(decryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                result = new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result;
        }

        /// <summary>
        /// 生成公钥、私钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public static void CreateKey(out string privateKey, out string publicKey)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            privateKey = provider.ToXmlString(true);
            publicKey = provider.ToXmlString(false);
        }
        #endregion

    }
}
