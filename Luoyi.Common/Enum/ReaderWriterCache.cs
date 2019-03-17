using System.Collections.Generic;
using System.Threading;

namespace Luoyi.Common
{

    /// <summary>
    /// 读写缓存
    /// 内部维护一个读写锁 实现一种如果存在则返回原来的数据否则就创建并且将其缓存的机制
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    internal abstract class ReaderWriterCache<TKey, TValue>
    {
        /// <summary>
        /// 创建数据选择性的将其缓存
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="cacheResult">是否缓存数据</param>
        /// <returns></returns>
        public delegate T CreatorOrCache<T>(out bool cacheResult);

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <returns></returns>
        public delegate T Creator<T>();

        private readonly Dictionary<TKey, TValue> cache;
        private readonly ReaderWriterLockSlim rwLockSlim = new ReaderWriterLockSlim();

        /// <summary>
        /// 构造函数
        /// </summary>
        protected ReaderWriterCache()
            : this(null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comparer">相等比较器</param>
        protected ReaderWriterCache(IEqualityComparer<TKey> comparer)
        {
            cache = new Dictionary<TKey, TValue>(comparer);
        }

       

        /// <summary>
        /// 缓存字典
        /// </summary>
        protected Dictionary<TKey, TValue> Cache
        {
            get
            {
                return cache;
            }
        }


        /// <summary>
        /// 如果存在则返回原来的数据否则就创建并且将其缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="creator">创建器</param>
        /// <returns></returns>
        protected TValue FetchOrCreateItem(TKey key, CreatorOrCache<TValue> creator)
        {

            rwLockSlim.EnterReadLock();
            try
            {
                TValue existingEntry;
                if (cache.TryGetValue(key, out existingEntry))
                {
                    return existingEntry;
                }
            }
            finally
            {
                rwLockSlim.ExitReadLock();
            }

            bool isCache;
            TValue newEntry = creator(out isCache);
            //如果需要缓存
            if (isCache)
            {
                rwLockSlim.EnterWriteLock();
                try
                {
                    TValue existingEntry;
                    if (cache.TryGetValue(key, out existingEntry))
                    {
                        return existingEntry;
                    }

                    cache[key] = newEntry;

                }
                finally
                {
                    rwLockSlim.ExitWriteLock();
                }
            }
            return newEntry;
        }

        /// <summary>
        /// 如果存在则返回原来的数据否则就创建并且将其缓存
        /// </summary>
        /// <param name="key">值</param>
        /// <param name="creator">创建器</param>
        /// <returns></returns>
        protected TValue FetchOrCreateItem(TKey key, Creator<TValue> creator)
        {
            return FetchOrCreateItem(key, (out bool b) =>
            {
                b = true;
                return creator();
            });
        }
    }
}
