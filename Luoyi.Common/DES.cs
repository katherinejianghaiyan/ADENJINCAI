using System;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Configuration;

namespace Luoyi.Common
{
    /// <summary>
    /// �ַ��ӽ���
    /// </summary>
    public class DES
    {
        /// <summary>
        /// ���ܷ���  
        /// </summary>
        /// <param name="encryptString">�������ַ�</param>
        /// <param name="key">��Կ</param>
        /// <returns></returns>
        public static string Encrypt(string encryptString,string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                //���ַ����ŵ�byte������  
                //ԭ��ʹ�õ�UTF8���룬�Ҹĳ�Unicode�����ˣ�����  
                byte[] inputByteArray = Encoding.Default.GetBytes(encryptString);
                //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  
                //�������ܶ������Կ��ƫ����  
                //ԭ��ʹ��ASCIIEncoding.ASCII������GetBytes����  
                //ʹ�����������������Ӣ���ı�  
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
        /// ���ܷ���  
        /// </summary>
        /// <param name="decryptString">�������ַ�</param> 
        /// <param name="key">��Կ</param>
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
                //�������ܶ������Կ��ƫ��������ֵ��Ҫ�������޸�  
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
