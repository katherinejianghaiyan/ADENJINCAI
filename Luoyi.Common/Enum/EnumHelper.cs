using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Luoyi.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        class EnumCache : ReaderWriterCache<Type, Dictionary<long, EnumItem>>
        {
            public Dictionary<long, EnumItem> GetEnumMap(Type t, Creator<Dictionary<long, EnumItem>> cr)
            {
                return FetchOrCreateItem(t, cr);
            }
        }

        #region 私有成员
        static readonly EnumCache _instance = new EnumCache();

        static Dictionary<long, EnumItem> fetchOrCreateEnumMap(Type t)
        {
            return _instance.GetEnumMap(t, () => createEnumMap(t));
        }
        static Dictionary<long, EnumItem> createEnumMap(Type t)
        {
            Dictionary<long, EnumItem> _map = new Dictionary<long, EnumItem>();
            FieldInfo[] fields = t.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo f in fields)
            {
                long v = Convert.ToInt64(f.GetValue(null));
                DescriptionAttribute[] ds = (DescriptionAttribute[])f.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (ds.Length > 0)
                {
                    _map[v] = new EnumItem { Value = v, Description = ds[0].Description, EnumName = f.Name};

                  
                }
            }
            return _map;
        }


        #endregion

        /// <summary>
        /// 返回该枚举类型的所有枚举项成员以及描述 
        /// </summary>
        /// <typeparam name="TEnumType">枚举类型</typeparam>
        /// <returns>枚举的所有项的描述信息</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public enum UserType{
        ///     
        ///         [Description("普通会员")]
        ///         Normal=0,
        ///         
        ///         [Description("高级会员")]
        ///         VIP=1
        ///     }
        ///     
        ///     List<EnumItem> list=EnumHelper.GetTypeItemList<UserType>();
        ///     //返回：[{Value:0,Description:"普通会员",EnumName:"Normal"},{Value:1,Description:"高级会员",EnumName:"VIP"}]
        /// ]]>
        /// </code>
        /// </example>
        public static List<EnumItem> GetTypeItemList<TEnumType>()
        {
            Type t = typeof(TEnumType);
            return fetchOrCreateEnumMap(t).Values.ToList();
        }

        /// <summary>
        /// 返回该枚举类型的所有枚举项成员以及描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns>枚举的所有项的描述信息</returns>
        public static List<EnumItem> GetTypeItemList(Type type)
        {
            return fetchOrCreateEnumMap(type).Values.ToList();
        } 

        /// <summary>
        ///返回单枚举值的描述信息
        /// </summary>
        /// <param name="v">枚举值</param>
        /// <returns>枚举值的描述信息</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public enum UserType{
        ///     
        ///         [Description("普通会员")]
        ///         Normal=0,
        ///         
        ///         [Description("高级会员")]
        ///         VIP=1
        ///     } 
        ///     string desc=UserType.Normal.GetDescription();   //返回:普通会员
        /// ]]>
        /// </code>
        /// </example>
        public static string GetDescription(this Enum v)
        {
            Type t = v.GetType();

            var map = fetchOrCreateEnumMap(t);
            EnumItem item;

            if (map.TryGetValue(Convert.ToInt64(v), out item))
            {
                return item.Description;
            }

            return string.Empty;
        }

        /// <summary>
        /// 返回按位组合枚举值 所构成的每一个值
        /// </summary>
        /// <param name="values">枚举值</param>
        /// <returns>枚举值集合</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     [Flags]
        ///     public enum UserType{
        ///         
        ///         Normal=0,
        ///         
        ///         VIP=1,
        ///         
        ///         VIP2=2,
        ///         
        ///         VIP3=4,
        ///         
        ///         VIP4=8
        ///     }
        ///     
        ///     UserType userType=UserType.VIP1|UserType.VIP3;  //1,4
        ///     
        ///     var list =userType.GetValues();
        ///       //返回：[1,4]
        /// ]]>
        /// </code>
        /// </example>
        public static List<long> GetValues(this Enum values)
        {
            Type t = values.GetType();
            long lv = Convert.ToInt64(values);
            Dictionary<long, EnumItem> _map = fetchOrCreateEnumMap(t);
            var items = new List<long>();
            foreach (var item in _map)
            {
                var v = item.Key;
                if ((v & lv) == v)
                {
                    items.Add(v);
                }
            }

            return items;
        }


        /// <summary>
        ///  返回将按位组合枚举值的每一个值描述连接起来的字符串
        /// </summary>
        /// <param name="v">枚举值</param>
        /// <returns>描述信息</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     [Flags]
        ///     public enum UserType{
        ///         [Description("普通会员")]
        ///         Normal=0,
        ///         
        ///         [Description("普通VIP")]
        ///         VIP=1,
        ///         
        ///         [Description("一级VIP")]
        ///         VIP2=2,
        ///         
        ///         [Description("二级VIP")]
        ///         VIP3=4,
        ///         
        ///         [Description("三级VIP")]
        ///         VIP4=8
        ///     }
        ///     
        ///     UserType userType=UserType.VIP1|UserType.VIP3;  //1,4
        ///     
        ///     var string =userType.GetDescriptions();
        ///       //返回： 普通VIP,二级VIP
        /// ]]>
        /// </code>
        /// </example>
        public static string GetDescriptions(this Enum v)
        {
            Type t = v.GetType();
            Dictionary<long, EnumItem> _map = fetchOrCreateEnumMap(t);
            long lv = Convert.ToInt64(v);
            StringBuilder sb = new StringBuilder();
            var emtor = _map.Where(i => (i.Key & lv) == i.Key).GetEnumerator();
            if (emtor.MoveNext())
            {
                sb.Append(emtor.Current.Value.Description);
            }
            while (emtor.MoveNext())
            {
                sb.AppendFormat(",{0}", emtor.Current.Value.Description);
            }
            return sb.ToString();
        }

    }
}
