using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.Common
{
    /// <summary>
    /// 集合类型的扩展
    /// </summary>
    public static class EnumerableHelper
    {
        /// <summary>
        /// 集合中是否包含满足条件的某项
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="predicate">判定条件</param>
        /// <returns>是否存在</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     List<string> list={"1","3","5","7"};
        ///     
        ///     bool ret=list.Contains(c=>c=="3");  //True
        /// ]]>
        /// </code>
        /// </example>
        public static bool Contains<T>(this IEnumerable<T> list, Predicate<T> predicate)
        {

            return list.Any(item => predicate(item));
        }

        /// <summary>
        /// 对IEnumerable&lt;T&gt;的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="action">要执行的操作</param>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     string[] list={"1","3","5","7"};
        ///     list.ForEach(c=>{
        ///         Response.Write(c);
        ///     });
        /// ]]>
        /// </code>
        /// </example>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }

        /// <summary>
        /// 对IEnumerable的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="action">要执行的操作</param>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     string[] list={"1","3","5","7"};
        ///     list.ForEach(c=>{
        ///         Response.Write(c);
        ///     });
        /// ]]>
        /// </code>
        /// </example>
        public static void ForEach<T>(this IEnumerable list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }

        /// <summary>
        ///  在指定  IEnumerable`T 的每个元素之间串联指定的分隔符 System.String，从而产生单个串联的字符串。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="toText">将T转换为文本</param>
        /// <param name="separator">分隔符</param>
        /// <returns>拼接字符串</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     List<DateTime> list=new List<DateTime>();
        ///     
        ///     string ret=list.Join(d=>d.ToString("yyyyMMdd"),",");
        /// 
        /// ]]>
        /// </code>
        /// </example>
        public static string Join<T>(this IEnumerable<T> list, Func<T, string> toText, string separator)
        {
            if (list == null)
                return null;

            StringBuilder sb = new StringBuilder();
            var enumtor = list.GetEnumerator();
            if (enumtor.MoveNext())
            {
                sb.Append(toText(enumtor.Current));
                while (enumtor.MoveNext())
                {
                    sb.Append(separator);
                    sb.Append(toText(enumtor.Current));
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// 在指定  IEnumerable`T 的每个元素之间串联指定的分隔符 System.String，从而产生单个串联的字符串。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合对象</param>
        /// <param name="separator">分割字符</param>
        /// <returns>拼接字符串</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     int[] ary={1,2,3,4,5};
        ///     
        ///     string str=ary.Join();  //返回："1,2,3,4,5"
        /// ]]>
        /// </code>
        /// </example>
        public static string Join<T>(this IEnumerable<T> list, string separator = ",")
        {
            if (list == null)
                return null;

            return list.Join((T v) => v.ToString(), separator);
        }

        #region 扩展 Distinct 方法

        /// <summary>
        /// 通过使用指定的 System.Collections.Generic.IEqualityComparer&lt;T&gt; 对值进行比较返回序列中的非重复元素。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="V">比较器类型</typeparam>
        /// <param name="source">集合对象</param>
        /// <param name="keySelector">元素比较方法</param>
        /// <param name="comparer">比较器</param>
        /// <returns>去除重复元素的集合</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     UserInfo[] users=new UserInfo[10];
        ///     ary.Distinct(c => c.Name,StringComparer.OrdinalIgnoreCase);
        /// ]]>
        /// </code>
        /// </example>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Distinct(CustomEqualityComparer.CreateComparer<T, V>(keySelector, comparer));
        }

        /// <summary>
        /// 通过使用指定的 System.Collections.Generic.IEqualityComparer&lt;T&gt; 对值进行比较返回序列中的非重复元素。
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <typeparam name="V">比较器类型</typeparam>
        /// <param name="source">集合对象</param>
        /// <param name="keySelector">比较器</param>
        /// <returns>去重后的集合</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     UserInfo[] users=new UserInfo[10];
        ///     ary.Distinct(c => c.Name);
        /// ]]>
        /// </code>
        /// </example>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(keySelector, EqualityComparer<V>.Default);
        }
        #endregion
    }
}
