using System;

namespace Luoyi.Common.Validate
{
    /// <summary>
    /// 通用类型数据验证
    /// </summary>
    public static class ObjectValidationHelper
    {
        /// <summary>
        /// 验证数据不能为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="current"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ValidationHelper<T> NotNull<T>(this ValidationHelper<T> current, string errMsg = null) where T : class
        {
            if (!current.Passed)
                return current;

            if (current.Value == default(T))
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}不存在", current.Name);
                current.Passed = false;
            }

            return current;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="current"></param>
        /// <param name="compareValue"></param>
        /// <param name="compareName"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ValidationHelper<T> EqualTo<T>(this ValidationHelper<T> current, T compareValue, string compareName, string errMsg = null)
            where T : class
        {
            if (!current.Passed)
                return current;
            if (!current.Value.Equals(compareValue))
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}与{1}不相等", current.Name,compareName);
                current.Passed = false;
            }
            return current;
        }
    }
}
