
namespace Luoyi.Common.Validate
{
    /// <summary>
    /// 验证数据合法性
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// 初始化数据合法性验证助手
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public static ValidationHelper<T> InitValidation<T>(this T value, string argName=null)
        {
            if (argName == null)
            {
                argName = "";
            }
            return new ValidationHelper<T>(value, argName);
           
        }
    }
}
