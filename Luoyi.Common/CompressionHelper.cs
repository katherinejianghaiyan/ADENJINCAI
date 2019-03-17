using System;
using System.Collections.Generic;

namespace Luoyi.Common
{
    /// <summary>
    /// 字符62进制压缩算法
    /// </summary>
    public  class CompressionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        const string CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
       private  static int Length
        {
            get
            {
                return CHARACTERS.Length;
            }
        }
   
 
        /// <summary>
        /// 数字转换为指定的进制形式字符串
        /// </summary>
        /// <param name="number">要压缩的数字</param>
        /// <returns></returns>
        public static string ToString(long number)
        {
            List<string> result = new List<string>();
            long t = number;
 
            while (t > 0)
            {
                var mod = t % Length;
                t = Math.Abs(t / Length);
                var character = CHARACTERS[Convert.ToInt32(mod)].ToString();
                result.Insert(0, character);
            }
            return string.Join("", result.ToArray());
        }
 
        /// <summary>
        /// 指定字符串转换为指定进制的数字形式
        /// </summary>
        /// <param name="str">要压缩的字符</param>
        /// <returns></returns>
        public static long FromString(string str)
        {
            long result = 0;
            int j = 0;
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
 
            foreach (var ch in new string(arr))
            { 
                if (CHARACTERS.Contains(ch.ToString()))
                {
                    result += CHARACTERS.IndexOf(ch) * ((long)Math.Pow(Length, j));
                    j++;
                }
            }
            return result;
        }
    }
}
