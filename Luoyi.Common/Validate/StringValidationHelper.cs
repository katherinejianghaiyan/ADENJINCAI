

using System;

namespace Luoyi.Common.Validate
{


    /// <summary>
    /// 
    /// </summary>
    public static class StringValidationHelper
    {
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数不为空.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> NotEmpty(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不可为空字符串", current.Name);
                current.Passed = false;
            }

            return current;
        }

        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数的长度小于一定值.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <param name="length">可行的最大长度(包括此值).</param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> MaxLength(this ValidationHelper<string> current, int length, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;

            if (current.Value.Length > length)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}的长度不可超过{1}", current.Name, length);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数的长度大于一定值.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <param name="length">可行的最小长度(包括此值).</param>    
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> MinLength(this ValidationHelper<string> current, int length, string errMsg=null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value.Length < length)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}的长度不可小于{1}", current.Name, length);
                current.Passed = false;
            }
            return current;
        }


        /// <summary>
        /// 验证当前值是否与某个值相等
        /// </summary>
        /// <param name="current"></param>
        /// <param name="mEqualto"></param>
        /// <param name="name"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> EqualTo(this ValidationHelper<string> current, string mEqualto, string name, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value != mEqualto)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}与{1}不一致", current.Name, name);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 不等于
        /// </summary>
        /// <param name="current"></param>
        /// <param name="mEqualto"></param>
        /// <param name="name"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ValidationHelper<string> NotEqualTo(this ValidationHelper<string> current, string mEqualto, string name=null, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value == mEqualto)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}与{1}不能相同", current.Name, string.IsNullOrEmpty(name)?mEqualto:name);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 不包含
        /// </summary>
        /// <param name="current"></param>
        /// <param name="subValue"></param>
        /// <param name="name"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ValidationHelper<string> NotContains(this ValidationHelper<string> current, string subValue, string name=null, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value.Contains(subValue))
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不能包含{1}", current.Name, string.IsNullOrEmpty(name)?subValue:name);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>    
        /// 验证<see cref="System.String"/>类型的参数的长度在一定值之间.    
        /// </summary>    
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>    
        /// <param name="minLength">可行的最小长度(包括此值).</param>    
        /// <param name="maxLength">可行的最大长度(包括此值).</param>   
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param> 
        /// <returns><paramref name="current"/>的引用以方便链式调用.</returns>    
        public static ValidationHelper<string> LengthRange(this ValidationHelper<string> current, int minLength, int maxLength, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            int len = string.IsNullOrEmpty(current.Value) ? 0 : current.Value.Length;

            if (len < minLength || len > maxLength)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}的长度必须在{1}和{2}之间", current.Name, minLength, maxLength);
                current.Passed = false;
                //throw new ArgumentException(String.Format("{0}的长度必须在{1}和{2}之间", current.Name, minLength, maxLength), current.Name);
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>存在SQL注入.    
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> ExistSqlIn(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            //current.NotDefault();
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (current.Value.IsUnSafeSql())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}存在非法字符", current.Name);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是IP地址.   
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsIP(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsIP())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg)?errMsg:String.Format("{0}不是合法的IP地址", current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是合法的Email.   
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsEmail(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsEmail())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不是合法的邮箱地址", current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是合法的Url.   
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsUrl(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!string.IsNullOrEmpty(current.Value) && !current.Value.IsUrl())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不是有效的链接(URL)地址", current.Value);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.String"/>是否是有效的日期格式数据.   
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsDateTime(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsDateTime())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不是有效的日期格式", current.Value);
                current.Passed = false;
            }
            return current;
        }

       
        /// <summary>
        /// 验证<see cref="System.String"/>是否是中文.   
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsChinese(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsChinese())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}含有非中文字符", current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否为电话号码    
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsPhone(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsPhone())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不是有效的电话号码", current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是否为电话号码    
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsMobile(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsMobile())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不是有效的手机号号码", current.Value);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 验证<see cref="System.String"/>是有效的整数  
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<string> IsInteger(this ValidationHelper<string> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (string.IsNullOrEmpty(current.Value))
                return current;
            if (!current.Value.IsInteger())
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不是有效的整数", current.Value);
                current.Passed = false;
            }
            return current;
        }
    }
}
