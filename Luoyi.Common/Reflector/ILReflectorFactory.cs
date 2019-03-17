 using System.Reflection;

namespace Luoyi.Common
{
    /// <summary>
    /// 通过 MSIL 实现的反射器工厂
    /// </summary>
    internal class ILReflectorFactory : IReflectorFactory
    {
        /// <summary>
        /// 获取字段成员对象
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public IMemberAccessor GetFieldAccessor(FieldInfo field)
        {
            return new ILFieldAccessor(field);
        }
        /// <summary>
        /// 获取属性成员对象
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public IMemberAccessor GetPropertyAccessor(PropertyInfo property)
        {
            return new ILPropertyAccessor(property);
        }
        /// <summary>
        /// 获取方法成员对象
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IMethodInvoker GetMethodInvoker(MethodBase method)
        {
            return new ILMethodInvoker(method);
        }
    }
}
