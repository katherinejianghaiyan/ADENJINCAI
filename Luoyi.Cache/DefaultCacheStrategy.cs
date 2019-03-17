using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

namespace Luoyi.Cache
{
    /// <summary>
    /// 默认缓存管理类
    /// </summary>
    internal class DefaultCacheStrategy : ICacheStrategy
    {
        private static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;

        private int timeOut = 1440; // 默认缓存存活期为1440分钟(24小时)

        /// <summary>
        /// 构造函数
        /// </summary>
        static DefaultCacheStrategy() { }

        #region 公有方法

        #region 加入当前对象到缓存中

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        public void AddObject(string key, object value)
        {
            AddObject(key, value, 0);
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        public void AddObject(string key, object value, int minutes)
        {
            AddObject(key, value, minutes, true);
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        public void AddObject(string key, object value, int minutes, bool absoluteExpiration)
        {
            AddObject(key, value, minutes, true, CacheItemPriority.Default);
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        public void AddObject(string key, object value, int minutes, bool absoluteExpiration, CacheItemPriority priority)
        {
            Add(key, value, null, minutes, absoluteExpiration, priority, "");
        }

        #endregion

        #region 加入当前对象到缓存中,并对相关文件建立依赖

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        public void AddObjectWithFile(string key, object value, string files)
        {
            AddObjectWithFile(key, value, files, 0);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        public void AddObjectWithFile(string key, object value, string files, int minutes)
        {
            AddObjectWithFile(key, value, files, minutes, true);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        public void AddObjectWithFile(string key, object value, string files, int minutes, bool absoluteExpiration)
        {
            AddObjectWithFile(key, value, files, minutes, true, CacheItemPriority.Default);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        public void AddObjectWithFile(string key, object value, string files, int minutes, bool absoluteExpiration, CacheItemPriority priority)
        {
            CacheDependency dep = new CacheDependency(files, DateTime.Now);
            Add(key, value, dep, minutes, absoluteExpiration, priority, files);
        }

        #endregion

        #region 加入当前对象到缓存中,并使用依赖键

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public void AddObjectWithDepend(string key, object value, string[] dependKey)
        {
            AddObjectWithDepend(key, value, dependKey, 0);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        public void AddObjectWithDepend(string key, object value, string[] dependKey, int minutes)
        {
            AddObjectWithDepend(key, value, dependKey, minutes, true);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        public void AddObjectWithDepend(string key, object value, string[] dependKey, int minutes, bool absoluteExpiration)
        {
            AddObjectWithDepend(key, value, dependKey, minutes, true, CacheItemPriority.Default);
        }

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        public void AddObjectWithDepend(string key, object value, string[] dependKey, int minutes, bool absoluteExpiration, CacheItemPriority priority)
        {
            CacheDependency dep = new CacheDependency(null, dependKey, DateTime.Now);
            Add(key, value, dep, minutes, absoluteExpiration, priority, string.Join(",", dependKey));
        }

        #endregion

        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="key">对象的关键字</param>
        public void RemoveObject(string key)
        {
            if (key == null || key.Length == 0)
                return;

            webCache.Remove(key);
        }

        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="key">对象的关键字</param>
        /// <returns>对象</returns>
        public object GetObject(string key)
        {
            if (key == null || key.Length == 0)
                return null;

            return webCache.Get(key);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加入当前对象到缓存中,并建立相关的依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dep">相应的依赖项</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        private void Add(string key, object value, CacheDependency dep, int minutes, bool absoluteExpiration, CacheItemPriority priority, string dependency)
        {
            if (key == null || key.Length == 0 || value == null)
                return;

            CacheItemRemovedCallback callBack = new CacheItemRemovedCallback(onRemove);

            //过期时间为0的，就用默认时间
            if (minutes == 0)
                minutes = timeOut;

            //绝对过期时间
            if (absoluteExpiration)
                webCache.Insert(key, value, dep, DateTime.Now.AddMinutes(minutes), System.Web.Caching.Cache.NoSlidingExpiration, priority, callBack);
            else
                webCache.Insert(key, value, dep, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes), priority, callBack);
        }
        
        /// <summary>
        /// 建立回调委托的一个实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="reason"></param>
        private void onRemove(string key, object value, CacheItemRemovedReason reason)
        {
            // 删除
        }
        #endregion
    }
}
