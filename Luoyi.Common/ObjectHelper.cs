using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Luoyi.Common
{
    /// <summary>
    /// 基础数据转换类
    /// </summary>
    public static class ObjectHedlper
    {
        public static string GetProperty(this Object obj, string strPropertyName)
        {
            // 取得对象类型
            Type t = obj.GetType();
            // 取得属性对象
            PropertyInfo propertyInfo = t.GetProperty(strPropertyName,
                            BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);

            return propertyInfo.GetValue(obj, null) == null ? null : propertyInfo.GetValue(obj, null).ToString();
        }
    }
}
