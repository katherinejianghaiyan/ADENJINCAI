#region Version Info
/* ========================================================================
* 【本类功能概述】
*
* 作者：shenjk 时间：2011/12/22 8:45:44
* 文件名：CustomComparison
* 版本：V1.0.1
*
* 修改者： 时间：
* 修改说明：
* ========================================================================
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Common
{
    /// <summary>
    /// 自定义比较器(暂不公开)
    /// </summary>
     internal class CustomComparison
    {
        /// <summary>
        /// 创建比较器
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="V">比较器类型</typeparam>
        /// <param name="keySelector">比较方法</param>
        /// <returns></returns>
        public static IComparer<T> CreateComparer<T,V>(Func<T, V> keySelector)
        {
            return new CommonComparer<T,V>(keySelector);
        }

        /// <summary>
        /// 创建比较器
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <typeparam name="V">比较器类型</typeparam>
        /// <param name="keySelector">比较方法</param>
        /// <param name="comparer">比较器</param>
        /// <returns></returns>
        public static IComparer<T> CreateComparer<T,V>(Func<T, V> keySelector, IComparer<V> comparer)
        {
            return new CommonComparer<T,V>(keySelector, comparer);
        }

        class CommonComparer<T,V> : IComparer<T>
        {
            private Func<T, V> keySelector;
            private IComparer<V> comparer;

            public CommonComparer(Func<T, V> keySelector, IComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }
            public CommonComparer(Func<T, V> keySelector)
                : this(keySelector, Comparer<V>.Default)
            { }

            public int Compare(T x, T y)
            {
                return comparer.Compare(keySelector(x), keySelector(y));
            }
        }
    }
}
