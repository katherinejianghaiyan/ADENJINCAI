 
namespace Luoyi.Common
{
    /// <summary>
    /// 对象字段或属性的连接器接口
    /// </summary>
    public interface IMemberAccessor
    {
        /// <summary>
        /// 设置成员的值 如果为静态成员 instance 则为null
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="value"></param>
        void SetValue(object instance, object value);

        /// <summary>
        /// 获取成员的值 如果为静态成员 instance 则为null
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        object GetValue(object instance);
    }
}
