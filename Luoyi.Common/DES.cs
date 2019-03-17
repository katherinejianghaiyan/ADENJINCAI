using System;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Configuration;

namespace Luoyi.Common
{
    /// <summary>
    /// 字符加解密
    /// </summary>
    public class DES
    {
        /// <summary>
        /// 加密方法  
        /// </summary>
        /// <param name="encryptString">待加密字符</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string encryptString,string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //把字符串放到byte数组中  
                //原来使用的UTF8编码，我改成Unicode编码了，不行  
                byte[] inputByteArray = Encoding.Default.GetBytes(encryptString);
                //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  
                //建立加密对象的密钥和偏移量  
                //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
                //使得输入密码必须输入英文文本  
                des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
                des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        //Write  the  byte  array  into  the  crypto  stream  
                        //(It  will  end  up  in  the  memory  stream)  
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    //Get  the  data  back  from  the  memory  stream,  and  into  a  string  
                    StringBuilder ret = new StringBuilder();
                    Array.ForEach(ms.ToArray(),
                    delegate(byte b)
                    {
                        //Format  as  hex  
                        ret.AppendFormat("{0:X2}", b);
                    });
                    ret.ToString();
                    return ret.ToString();
                }
            }
        }


        /// <summary>
        /// 解密方法  
        /// </summary>
        /// <param name="decryptString">待解密字符</param> 
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string decryptString,string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //Put  the  input  string  into  the  byte  array  
                byte[] inputByteArray = new byte[decryptString.Length / 2];
                for (int x = 0; x < decryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                //建立加密对象的密钥和偏移量，此值重要，不能修改  
                des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
                des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }
                    //Get  the  decrypted  data  back  from  the  memory  stream                      
                    return Encoding.Default.GetString(ms.ToArray());
                }
            }
        }
    }
}
