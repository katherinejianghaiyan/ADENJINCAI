#region
/* 
 *作者：shenjk http://www.shenjk.com
 *时间：2009-10-26
 *描述：整数验证
 */
#endregion

using System;

namespace Luoyi.Common.Validate
{
    /// <summary>
    /// 
    /// </summary>
    public static class IntValidationHelper
    {
        /// <summary>
        /// 验证<see cref="System.Int32"/>类型的参数的值大于一定值.    
        /// </summary>
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>
        /// <param name="min">最小值</param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<int> Min(this ValidationHelper<int> current, int min, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (current.Value < min)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不能小于{1}", current.Name, min);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 验证<see cref="System.Int32"/>类型的参数的值小于一定值.    
        /// </summary>
        /// <param name="current">用于验证的<see cref="ValidationHelper&lt;T&gt;"/></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static ValidationHelper<int> Max(this ValidationHelper<int> current, int max, string errMsg = null)
        {

            if (current.Value > max)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不能大于{1}", current.Name, max);
                current.Passed = false;
            }
            return current;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<int> Range(this ValidationHelper<int> current, int min, int max, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (current.Value < min || current.Value > max)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不能大于{1}，且不能小于{2}", current.Name, max, min);
                current.Passed = false;
            }
            return current;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="value"></param>
        /// <param name="errMsg">自定义错误提示信息，如果为null，则会返回默认的错误提示消息</param>
        /// <returns></returns>
        public static ValidationHelper<int> EqualTo(this ValidationHelper<int> current, int value, string errMsg = null)
        {
            if (!current.Passed)
                return current;
            if (current.Value!=value)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不能必须等于{1}", current.Name, value);
                current.Passed = false;
            }
            return current;
        }
    }
}
