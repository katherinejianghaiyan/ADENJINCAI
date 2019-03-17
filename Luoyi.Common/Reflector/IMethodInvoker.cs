 
namespace Luoyi.Common
{
    /// <summary>
    /// 方法调用器接口
    /// </summary>
    public interface IMethodInvoker
    {
        /// <summary>
        /// 反射执行方法
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="parameters">方法参数</param>
        /// <returns>执行结果</returns>
        object Invoke(object instance, params object[] parameters);
    }
}
