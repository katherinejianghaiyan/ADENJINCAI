using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

namespace Luoyi.Common
{
    /// <summary>
    /// DataReader扩展方法,实现高性能的ORM
    /// </summary>
    public static class DataReaderHelper
    {
        /// <summary>
        /// 装载实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="dr">SqlDataReader对象</param>
        /// <param name="action">自定义操作</param>
        /// <returns>实体对象</returns>
        public static T ToEntity<T>(this SqlDataReader dr, string language="", Action<T, SqlDataReader> action=null) {
            if (dr == null || !dr.HasRows)  return default(T);

            IDictionary<string, Func<T, object, object>> dic = GetMap<T>();
            if (dr.Read())
            {
                T model = Activator.CreateInstance<T>();
                LoadEntity(dr, dic, ref model,language);
                if (action != null) {
                    action(model, dr);
                }
                return model;
            }
            return default(T);
        }

        /// <summary>
        /// 装载实体
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="dr">SqlDataReader对象</param>
        /// <param name="action">自定义操作</param>
        /// <param name="model">实体对象</param>      
        public static void ToEntity<T>(this SqlDataReader dr, ref T model, Action<T, SqlDataReader> action=null)
        {
            if (dr == null || !dr.HasRows)
            {
                return;
            }
            IDictionary<string, Func<T, object, object>> dic = GetMap<T>();
            if (dr.Read())
            {
                if (model.Equals(null))
                {
                    model = Activator.CreateInstance<T>();
                }
                LoadEntity(dr, dic, ref model);
                if (action != null) {
                    action(model, dr);
                }
            }
        }


        /// <summary>
        /// 装载实体集合
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="dr">SqlDataReader对象</param>
        /// <param name="action">自定义操作</param>
        /// <returns>实体对象集合</returns>
        public static List<T> ToEntityList<T>(this SqlDataReader dr,  Action<T,SqlDataReader> action=null)
        {
            if (dr == null || !dr.HasRows)
            {
                return null;
            }
            List<T> list = new List<T>();
            IDictionary<string, Func<T, object, object>> dic = GetMap<T>();
            while (dr.Read())
            {
                T model = Activator.CreateInstance<T>();
                LoadEntity(dr, dic, ref model);
                if (action != null) {
                    action(model,dr);
                }
                list.Add(model);

            }
            return list;
        }

        /// <summary>
        /// 装载实体
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="dr">SqlDataReader对象</param>
        /// <param name="dic">缓存字典</param>
        /// <param name="model">返回实体</param>
        private static void LoadEntity<T>(SqlDataReader dr, IDictionary<string, Func<T, object, object>> dic, ref T model, string language = "")
        {
            language = language.Replace("_", "");
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string fieldName = dr.GetName(i);
                string fieldNameL = fieldName + language;

                if (dic.ContainsKey(fieldName))
                {
                    object val = dr[fieldName];
                    try
                    {
                        dr.GetOrdinal(fieldNameL);
                        val = dr[fieldNameL];
                    }
                    catch { }
                    if (val != null && val != DBNull.Value)
                    {
                        Func<T, object, object> fc = dic[fieldName];
                        fc(model, val);// dr[fieldName]);
                    }
                }
            }
        }

        #region
        static Func<T, object, object> SetDelegate<T>(MethodInfo m, Type type)
        {
            ParameterExpression paramObj = Expression.Parameter(typeof(T), "obj");
            ParameterExpression paramVal = Expression.Parameter(typeof(object), "val");
            UnaryExpression bodyVal = Expression.Convert(paramVal, type);
            MethodCallExpression body = Expression.Call(paramObj, m, bodyVal);
            Action<T, object> set = Expression.Lambda<Action<T, object>>(body, paramObj, paramVal).Compile();

            return (instance, v) =>
            {
                set(instance, v);
                return null;
            };
        }

        static IDictionary<Type, object> dicCache = new Dictionary<Type, object>();
        private static object lockobj = new object();
        static IDictionary<string, Func<T, object, object>> GetMap<T>()
        {
            object dic;
            Type type = typeof(T);

            if (!dicCache.TryGetValue(type, out dic))
            {
                lock (lockobj)
                {
                    if (!dicCache.TryGetValue(type, out dic))
                    {
                        return InitMap<T>(type);
                    }
                }
            }
            return (IDictionary<string, Func<T, object, object>>)dic;
        }
        /// <summary>
        /// 初始化对象映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IDictionary<string, Func<T, object, object>> InitMap<T>(Type type)
        {
            var _dic = new Dictionary<string, Func<T, object, object>>(StringComparer.OrdinalIgnoreCase);
            PropertyInfo[] ps =
                type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                   BindingFlags.IgnoreCase);
            foreach (PropertyInfo pi in ps)
            {
                MethodInfo setMethodInfo = pi.GetSetMethod(true);
                if (setMethodInfo == null)
                    continue;
                Func<T, object, object> func = SetDelegate<T>(setMethodInfo, pi.PropertyType);
                _dic.Add(pi.Name, func);
            }
            dicCache[type] = _dic;
            return _dic;
        }

        #endregion

    }
}
