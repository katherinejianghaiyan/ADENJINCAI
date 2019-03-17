 using System.Reflection;

namespace Luoyi.Common
{
    /// <summary>
    /// 反射器工厂
    /// </summary>
    internal interface IReflectorFactory
    {
        /// <summary>
        /// 获取字段成员对象
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        IMemberAccessor GetFieldAccessor(FieldInfo field);

        /// <summary>
        /// 获取属性成员对象
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        IMemberAccessor GetPropertyAccessor(PropertyInfo property);

        /// <summary>
        /// 获取方法成员对象
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        IMethodInvoker GetMethodInvoker(MethodBase method);
    }


}
