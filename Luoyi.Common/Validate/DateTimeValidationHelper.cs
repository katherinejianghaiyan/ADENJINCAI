using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Common.Validate
{
    /// <summary>
    /// 日期数据验证
    /// </summary>
    public static class DateTimeValidationHelper
    {
        /// <summary>
        /// 验证日期不小于某个日期
        /// </summary>
        /// <param name="current"></param>
        /// <param name="maxDate">与之比较的日期，必须小于该日期</param>
        /// <param name="errMsg">自定义错误提示信息</param>
        /// <returns></returns>
        public static ValidationHelper<DateTime> NotLessThen(this ValidationHelper<DateTime> current, DateTime maxDate, string errMsg = null)
        {
            if (!current.Passed)
                return current;

            if (current.Value < maxDate)
            {
                current.Msg = string.IsNullOrEmpty(errMsg) ? String.Format("{0}不存在", current.Name) : errMsg;
                current.Passed = false;
            }

            return current;
        }
        /// <summary>
        /// 验证日期不大于某个日期
        /// </summary>
        /// <param name="current"></param>
        /// <param name="minDate">与之比较的日期，必须大于该日期</param>
        /// <param name="errMsg">自定义错误提示信息</param>
        /// <returns></returns>
        public static ValidationHelper<DateTime> NotMoreThen(this ValidationHelper<DateTime> current, DateTime minDate,
            string errMsg = null)
        {
            if (!current.Passed)
                return current;

            if (current.Value > minDate)
            {
                current.Msg = string.IsNullOrEmpty(errMsg) ? String.Format("{0}不存在", current.Name) : errMsg;
                current.Passed = false;
            }

            return current;
        }
    }
}
