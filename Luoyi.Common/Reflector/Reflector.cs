 using System;
using System.Collections.Generic;
using System.Reflection;

namespace Luoyi.Common
{
    /// <summary>
    /// 高性能反射器
    /// </summary>
    public static class Reflector
    {
        static IReflectorFactory _refFactory = new ILReflectorFactory();
        static Dictionary<PropertyInfo, IMemberAccessor> _propertyCache = new Dictionary<PropertyInfo, IMemberAccessor>();
        static Dictionary<FieldInfo, IMemberAccessor> _fieldCache = new Dictionary<FieldInfo, IMemberAccessor>();
        static Dictionary<MethodBase, IMethodInvoker> _methodCache = new Dictionary<MethodBase, IMethodInvoker>();

        #region 实例化对象
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argTypes">构造函数中参数类型</param>
        /// <param name="args">构造函数中参数</param>
        /// <returns>实例化对象</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID{get;set;}
        ///     }
        ///     UserInfo user=Reflector.CreateInstance<UserInfo>(); //高性能反射器
        /// ]]>
        /// </code>
        /// </example>
        public static T CreateInstance<T>(Type[] argTypes = null, object[] args = null)
        {
            Type type = typeof(T);
            return (T)GetMethod(type.GetConstructor(argTypes)).Invoke(null, args);
        }
        #endregion

        #region 获取字段成员信息
        /// <summary>
        /// 通过字段信息获得成员对象
        /// </summary>
        /// <param name="field">字段</param>
        /// <returns>字段成员对象</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID;
        ///         
        ///         public string Name;
        ///     }
        ///     UserInfo user= new UserInfo();
        ///     user.ID=1;
        ///     
        ///     Type type=typeof(UserInfo);
        ///     FiledInfo filed=type.GetField("ID");
        ///     var member=Reflector.GetField(field);
        ///     
        ///     int value=member.GetValue(user).ToType<int>();
        ///     
        ///     member.SetValue(user,2);
        ///     
        /// ]]>
        /// </code>
        /// </example>
        public static IMemberAccessor GetField(FieldInfo field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            IMemberAccessor accessor;
            if (_fieldCache.TryGetValue(field, out accessor))
            {
                return accessor;
            }

            lock (_fieldCache)
            {
                if (!_fieldCache.TryGetValue(field, out accessor))
                {
                    accessor = _refFactory.GetFieldAccessor(field);
                    _fieldCache[field] = accessor;
                }
            }

            return accessor;
        }

        /// <summary>
        /// 通过字段名获得成员连接器
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="bindingFlags">控制绑定标识</param>
        /// <returns>字段信息对象</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID;
        ///         
        ///         public string Name;
        ///     }
        ///     UserInfo user= new UserInfo();
        ///     user.ID=1;
        ///     var member=Reflector.GetField(typeof(UserInfo),"ID");
        ///     
        ///     int value=member.GetValue(user).ToType<int>();
        ///     
        ///     member.SetValue(user,2);
        ///     
        /// ]]>
        /// </code>
        /// </example>
        public static IMemberAccessor GetField(Type type, string fieldName,
            BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
        {
            FieldInfo field = type.GetField(fieldName, bindingFlags);
            if (field == null)
                throw new NullReferenceException(fieldName);
            return GetField(field);
        }
        #endregion

        #region 获取属性成员信息
        /// <summary>
        /// 根据属性名称获取属性成员连接器
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="bindingFlags">控制绑定标识</param>
        /// <returns>属性成员信息</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID{get;set;}
        ///         
        ///         public string Name{get;set;}
        ///     }
        ///     UserInfo user= new UserInfo();
        ///     user.ID=1;
        ///     var member=Reflector.GetProperty(typeof(UserInfo),"ID");
        ///     
        ///     int value=member.GetValue(user).ToType<int>();
        ///     
        ///     member.SetValue(user,2);
        ///     
        /// ]]>
        /// </code>
        /// </example>
        public static IMemberAccessor GetProperty(Type type, string propertyName,
            BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public
            )
        {
            PropertyInfo property = type.GetProperty(propertyName, bindingFlags);
            if (property == null)
            {
                throw new NullReferenceException(propertyName);
            }
            return GetProperty(property);
        }

        /// <summary>
        /// 通过属性信息获得成员连接器
        /// </summary>
        /// <param name="property">属性</param>
        /// <returns>属性对象</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID{get;set;}
        ///         
        ///         public string Name{get;set;}
        ///     }
        ///     UserInfo user= new UserInfo();
        ///     user.ID=1;
        ///     
        ///     PropertyInfo property=typeof(UserInfo).GetProperty("ID");
        ///     
        ///     var member=Reflector.GetProperty(property);
        ///     
        ///     int value=member.GetValue(user).ToType<int>();
        ///     
        ///     member.SetValue(user,2);
        ///     
        /// ]]>
        /// </code>
        /// </example>
        public static IMemberAccessor GetProperty(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            IMemberAccessor accessor;
            if (_propertyCache.TryGetValue(property, out accessor))
            {
                return accessor;
            }

            lock (_propertyCache)
            {
                if (!_propertyCache.TryGetValue(property, out accessor))
                {
                    accessor = _refFactory.GetPropertyAccessor(property);
                    _propertyCache[property] = accessor;
                }
            }

            return accessor;
        }

        #endregion

        #region 获取方法成员信息
        /// <summary>
        /// 反射方法执行
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns>方法成员对象</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID{get;set;}
        ///         
        ///         public string Name{get;set;}
        ///         
        ///         public void Print(DateTime dt){
        ///             //输出
        ///         }
        ///     }
        ///     UserInfo user= new UserInfo();
        ///     user.ID=1;
        ///     
        ///     MethodBase method=typeof(UserInfo).GetMethod("Print");
        ///     
        ///     var member=Reflector.GetMethod(method);
        ///     
        ///     member.Invoke(user,DateTime.Now);
        ///     
        /// ]]>
        /// </code>
        /// </example>
        public static IMethodInvoker GetMethod(MethodBase method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            IMethodInvoker invoker;
            if (_methodCache.TryGetValue(method, out invoker))
            {
                return invoker;
            }

            lock (_methodCache)
            {
                if (!_methodCache.TryGetValue(method, out invoker))
                {
                    invoker = _refFactory.GetMethodInvoker(method);
                    _methodCache[method] = invoker;
                }
            }

            return invoker;
        }

        /// <summary>
        /// 根据方法名获取方法反射器
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="methodName">方法名</param>
        /// <param name="bindingFlags">控制绑定标识</param>
        /// <returns>方法反射器</returns>
        /// <example>
        /// <code lang="c#">
        /// <![CDATA[
        ///     public class UserInfo{
        ///         public int ID{get;set;}
        ///         
        ///         public string Name{get;set;}
        ///         
        ///         public void Print(DateTime dt){
        ///             //输出
        ///         }
        ///     }
        ///     UserInfo user= new UserInfo();
        ///     user.ID=1;
        ///     
        ///     var member=Reflector.GetMethod(typeof(UserInfo),"Print");
        ///     
        ///     member.Invoke(user,DateTime.Now);
        ///     
        /// ]]>
        /// </code>
        /// </example>
        public static IMethodInvoker GetMethod(Type type, string methodName
            , BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
        {
            MethodInfo method = type.GetMethod(methodName, bindingFlags);
            if (method == null)
            {
                throw new NullReferenceException(methodName);
            }
            return GetMethod(method);
        }
        #endregion
    }
}
