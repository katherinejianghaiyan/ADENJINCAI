/*
Author      : 沈进坤
Date        : 2013-10-29
Description : 对 System.String的扩展
*/

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace Luoyi.Common
{
    /// <summary>
    /// String 扩展方法
    /// </summary>
    public static class StringHelper
    {      

        #region 静态私有变量
        /// <summary>
        /// 需要转义的字符正则
        /// </summary>
        static readonly Regex _transferredRule = new Regex(@"('|""|\\)", RegexOptions.Compiled);
        /// <summary>
        /// 数字
        /// </summary>
        static readonly string[] numArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        /// <summary>
        /// 字母
        /// </summary>
        static readonly string[] letterArray = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "X" };
        /// <summary>
        /// 数字与字母混合
        /// </summary>
        static readonly string[] numberAndLetterArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "X" };
        #endregion
    
        #region 字符串长度与截取操作(处理中文字符)

        /// <summary>
        /// 单个汉字长度
        /// </summary>
        public enum SingleChineseLen
        {
            /// <summary>
            /// 1位
            /// </summary>
            ONE,
            /// <summary>
            /// 2位 一个中文按2个字符计算
            /// </summary>
            TWO
        }

        #region 截取字符串，并在末尾追加字符
        /// <summary>
        /// 截取字符串，并在末尾追加字符
        /// </summary>
        /// <param name="str">待操作字符串</param>
        /// <param name="start">截取起始索引号</param>
        /// <param name="len">截取长度</param>
        /// <param name="paddingChar">追加字符</param>
        /// <param name="repeat">追加长度</param>
        /// <returns></returns>
        public static string SubStringAndPaddingChar(this string str, int start, int len, char paddingChar = '*', int repeat = 2)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            string subStr = str;
            if (str.Length > start && str.Length >= (start + len))
            {
                subStr = str.Substring(start, len);
            }
            return subStr.PadRight(subStr.Length + repeat, paddingChar);
        }
        #endregion

        #region 获取字符串的长度
        /// <summary>
        /// 获取字符串的长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="cLen">汉字长度，默认一个汉字当2个字符处理</param>
        /// <returns></returns>
        public static int GetStringLength(this string str, SingleChineseLen cLen= SingleChineseLen.TWO)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            int len = 0;
            if (!string.IsNullOrEmpty(str))
            {
                ASCIIEncoding En = new ASCIIEncoding();
                Byte[] B = En.GetBytes(str.Trim());
                for (int i = 0; i < B.Length; i++)
                {
                    if (B[i] == 63 && cLen == SingleChineseLen.TWO)//表示是中文
                    {
                        len = len + 2;
                    }
                    else
                    {
                        len = len + 1;
                    }
                }
            }
            return len;
        }
        #endregion
        
        #region 截取字符串长度
        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="count">截取的长度</param>
        /// <param name="cLen">汉字长度</param>
        /// <returns></returns>
        public static string GetSubString(this string str, int count, SingleChineseLen cLen= SingleChineseLen.TWO)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            ASCIIEncoding En = new ASCIIEncoding();
            Byte[] B = En.GetBytes(str);
            int len = 0;
            for (int i = 0; i < B.Length; i++)
            {
                if (B[i] == 63 && cLen == SingleChineseLen.TWO)//表示是中文
                {
                    len = len + 2;
                }
                else
                {
                    len = len + 1;
                }
                if (len >= count)
                {
                    return str.Substring(0, i + 1);
                }
            }
            return str;
        }
        #endregion

        #endregion

        #region 编码与解码 UrlEncode And UrlDecode
        /// <summary>
        /// UrlEncode Url编码
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <param name="encoding">编码规则</param>
        /// <returns>编码后的字符串</returns>
        public static string UrlEncode(this string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return HttpUtility.UrlEncode(str, encoding);
        }
        /// <summary>
        /// UrlEncode Url编码,默认UTF-8编码
        /// </summary>
        /// <param name="str">待编码字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string UrlEncode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.UrlEncode(Encoding.UTF8);
        }

        /// <summary>
        /// UrlDecode Url解码
        /// </summary>
        /// <param name="str">待解码字符串</param>
        /// <param name="encoding">解码规则</param>
        /// <returns>解码后的字符串</returns>
        public static string UrlDecode(this string str, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return HttpUtility.UrlDecode(str, encoding);
        }
        /// <summary>
        /// UrlDecode Url解码,默认UTF-8编码
        /// </summary>
        /// <param name="str">待解码字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string UrlDecode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            return str.UrlDecode(Encoding.UTF8);
        }

        /// <summary>
        /// 超级解码(不用关心字符是UTF-8、Unicode或GB2312编码格式)，在分析不同搜索引擎来源的关键词时特别有用
        /// </summary>
        /// <param name="str">待解码字符串</param>
        /// <returns></returns>
        public static string SuperDecode(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            /* Unicode 编码时，由 %uxxx组成
             * UTF-8 时 一个汉字需要三组 %xx组成 符号需要一组%xx;
             * GB2312 时 一个汉字需要两组 %xx组成
             * 首先如果符合Unicode编码要求，则直接使用Unicode解码
             * 其次尝试用UTF-8解码，如果成功，则解码后的字符串长度等于 %xx组数/3+非编码字符串长度
             * 否则使用GB2312进行解码             * 
             * */
            const string flags = "%20%40%23%24%25%5E%26%2B%7B%7D%5B%5D%3B%22%2F%5C%3A%3F%3E%3C%09%2C%7C%27";  //半角符号
            var unicode = "%u([\\dABCDEF]{4})?";
            if (Regex.Match(str, unicode, RegexOptions.IgnoreCase).Success)
            {
                return System.Web.HttpUtility.UrlDecode(str, System.Text.Encoding.Unicode);
            }

            var pattern = "%([\\dABCDEF]{2})?";
            int matchcount = 0;
            int mode = 0;
            string tmp = Regex.Replace(str, pattern, new MatchEvaluator(match =>
            {
                if (flags.IndexOf(match.Groups[0].Value.ToLower(), StringComparison.OrdinalIgnoreCase) > -1)
                {
                    mode++;
                }
                else
                {
                    matchcount++;
                }
                return match.Result("");
            }), RegexOptions.IgnoreCase);

            string w = System.Web.HttpUtility.UrlDecode(str, System.Text.Encoding.UTF8);
            if (w.Length == (matchcount / 3 + mode + tmp.Length))
                return w;
            return System.Web.HttpUtility.UrlDecode(str, System.Text.Encoding.GetEncoding("GB2312"));
        }
        #endregion

        #region 移除所有HTML标记
        /// <summary>
        /// 移除所有HTML标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            const string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 移除不可见字符
        /// <summary>
        /// 移除不可见字符
        /// </summary>
        /// <param name="str">待操作字符串</param>
        /// <returns></returns>
        public static string RemoveInvisibleChar(this string str)
        {
            if (str.IsNullOrEmptyAndTrim())
            {
                return str;
            }
            var sb = new System.Text.StringBuilder(131);
            for (int i = 0; i < str.Length; i++)
            {
                int Unicode = str[i];
                if (Unicode >= 16)
                {
                    sb.Append(str[i].ToString());
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 转义为Javascript脚本字符串常量之后的字符串
        /// <summary>
        /// 返回 System.String 对象转义为Javascript脚本字符串常量之后的字符串
        /// </summary>
        /// <param name="str">一个 System.String 引用</param>
        /// <param name="addDoubleQuotes">是否添加双引号</param>
        /// <returns>返回js可输出的字符串</returns>
        public static string ToJavascriptString(this string str, bool addDoubleQuotes = true)
        {
            if (str == null)
                str = string.Empty;

            const string QUETE = "\"";

            if (str.Length == 0)
            {
                if (!addDoubleQuotes)
                    return str;
                else
                    return QUETE + QUETE;
            }

            str = _transferredRule.Replace(str, "\\$1");
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("\r", "\\r");
            sb.Replace("\n", "\\n");

            if (addDoubleQuotes)
            {
                sb.Insert(0, QUETE);
                sb.Append(QUETE);
            }
            return sb.ToString();
        }
        #endregion

        #region 隐藏手机号和邮箱的中间部分
        /// <summary>
        /// 获取隐藏中间数字的手机号码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        public static string HideMobile(string mobile)
        {
            if (!string.IsNullOrEmpty(mobile) && mobile.Length > 10)
            {
                return string.Format("{0}*****{1}", mobile.Substring(0, 3), mobile.Substring(mobile.Length - 3, 3));
            }
            else return mobile;
        }

        /// <summary>
        /// 获得隐藏部分邮箱名称后的Email地址
        /// </summary>
        /// <param name="email">Email地址</param>
        /// <returns></returns>
        public static string HideEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }
            if (email.IndexOf("@") > 0)
            {
                string name = email.Split('@')[0];
                string host = email.Split('@')[1];
                return string.Format("{0}***{1}@{2}", name.Substring(0, 1), name.Substring(name.Length - 1, 1), host);
            }
            else return email;
        }
        #endregion

        #region 随机字符串类型
        /// <summary>
        /// 随机字符串类型
        /// </summary>
        [Flags]
        public enum RandCodeType
        {
            /// <summary>
            /// 仅数字
            /// </summary>
            NUMBER = 1,
            /// <summary>
            /// 字母
            /// </summary>
            LETTER = 2,

        }
        #endregion

        #region 获取随机字符串
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <param name="randCodeType">字符类型</param>
        /// <returns>返回生成的字符串</returns>
        public static string GetRandCode(int length, RandCodeType randCodeType)
        {
            StringBuilder strb = new StringBuilder();
            long tick = DateTime.Now.Ticks;
            Random rnd = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            int randcodeType = (int)randCodeType;

            for (int i = 0; i < length; i++)
            {
                switch (randcodeType)
                {
                    case 1:
                        strb.Append(numArray[rnd.Next(0, numArray.Length)]); break;
                    case 2:
                        strb.Append(letterArray[rnd.Next(0, letterArray.Length)]); break;
                    case 3:
                        strb.Append(numberAndLetterArray[rnd.Next(0, numberAndLetterArray.Length)]); break;
                }
            }

            return strb.ToString();
        }
        #endregion

        #region 转全角
        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转半角
        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CommaToSerial()
        {
            char c1 = (char)171;
            char c2 = (char)176;
            char c3 = (char)177;
            char c4 = (char)178;
            char c5 = (char)221;
            char c6 = (char)226;
            char c7 = (char)227;
            char c8 = (char)228;

            return "{[(<" + c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + ">)]}";
        }
        #endregion

        #region 62进制转换

        const string Seq = "s9LFkgy5RovixI1aOf8UhdY3r4DMplQZJXPqebE0WSjBn7wVzmN2Gc6THCAKut";

        /// <summary>
        /// 10进制转换为62进制
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Convert10T62(this long number)
        {
            if (number < 62)
            {
                return Seq[(int)number].ToString();
            }
            int y = (int)(number % 62);
            long x = (long)(number / 62);

            return Convert10T62(x) + Seq[y];
        }

        /// <summary>
        /// 将62进制字符转为10进制
        /// </summary>
        /// <param name="code">代号</param>
        /// <returns></returns>
        public static long Convert62T10(this string code)
        {
            long v = 0;
            int Len = code.Length;
            for (int i = Len - 1; i >= 0; i--)
            {
                int t = Seq.IndexOf(code[i]);
                double s = (Len - i) - 1;
                long m = (long)(Math.Pow(62, s) * t);
                v += m;
            }
            return v;
        }
        #endregion

    }
}
