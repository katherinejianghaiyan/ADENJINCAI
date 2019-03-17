using System;

namespace Luoyi.Common.Validate
{
    /// <summary>
    /// bool类型数据验证
    /// </summary>
    public static class BooleanValidationHelper
    {
        /// <summary>
        /// 验证是否为True
        /// </summary>
        /// <param name="current"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ValidationHelper<bool> IsTrue(this ValidationHelper<bool> current, string errMsg = null)
        {
            if (!current.Passed)
                return current;

            if (!current.Value)
            {
                current.Msg = !string.IsNullOrEmpty(errMsg) ? errMsg : String.Format("{0}为false", current.Name);
                current.Passed = false;
            }

            return current;
        }
    }
}
