using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;

namespace Luoyi.Cache
{
    /// <summary>
    /// 公共缓存策略接口
    /// </summary>
    public interface ICacheStrategy
    {
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        void AddObject(string key, object value);

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        void AddObject(string key, object value, int minutes);

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        void AddObject(string key, object value, int minutes, bool absoluteExpiration);

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        void AddObject(string key, object value, int minutes, bool absoluteExpiration, CacheItemPriority priority);

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        void AddObjectWithFile(string key, object value, string files);

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        void AddObjectWithFile(string key, object value, string files, int minutes);

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        void AddObjectWithFile(string key, object value, string files, int minutes, bool absoluteExpiration);

        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="files">文件地址</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        void AddObjectWithFile(string key, object value, string files, int minutes, bool absoluteExpiration, CacheItemPriority priority);

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        void AddObjectWithDepend(string key, object value, string[] dependKey);

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        void AddObjectWithDepend(string key, object value, string[] dependKey, int minutes);

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        void AddObjectWithDepend(string key, object value, string[] dependKey, int minutes, bool absoluteExpiration);

        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        /// <param name="minutes">缓存的时间，单位分钟(默认1440分钟)</param>
        /// <param name="absoluteExpiration">是否为绝对过期时间</param>
        /// <param name="priority">存储项的优先级</param>
        void AddObjectWithDepend(string key, object value, string[] dependKey, int minutes, bool absoluteExpiration, CacheItemPriority priority);

        /// <summary>
        /// 移除指定ID的对象
        /// </summary>
        /// <param name="key">对象的键值</param>
        void RemoveObject(string key);

        /// <summary>
        /// 返回指定ID的对象
        /// </summary>
        /// <param name="key">对象的键值</param>
        /// <returns></returns>
        object GetObject(string key);
    }
}
