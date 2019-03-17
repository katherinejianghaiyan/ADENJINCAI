
namespace Luoyi.Cache
{
    /// <summary>
    /// 缓存类    
    /// </summary>
    public class CacheHelper
    {
        private static ICacheStrategy instance;
        private static readonly object lockHelper = new object();
      

        /// <summary>
        /// 构造函数
        /// </summary>
        private CacheHelper()
        {
        }
         
        /// <summary>
        /// 单体模式返回类的实例，并于扩展
        /// </summary>
        /// <returns></returns>
        public static ICacheStrategy Instance
        {
            get
            {
                if (instance != null) return instance;

                lock (lockHelper)
                {
                    return  new DefaultCacheStrategy(); 
                } 
            }
        }

        
    }
}
