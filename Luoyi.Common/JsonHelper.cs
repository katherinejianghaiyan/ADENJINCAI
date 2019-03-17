using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Luoyi.Common
{
    /// <summary>
    /// Json格式化帮助类
    /// </summary>
    public static class JsonHelper
    {
        #region Json反序列化
        /// <summary>
        /// 将指定的 JSON 字符串转换为 T 类型的对象
        /// </summary>
        /// <typeparam name="T">所生成对象的类型</typeparam>
        /// <param name="str">要进行反序列化的 JSON 字符串</param>
        /// <returns>反序列化的对象</returns>
        public static T JSONDeserialize<T>(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(str);
        }

        /// <summary>
        /// 将指定的 JSON 字符串转换为对象图
        /// </summary>
        /// <param name="str">要进行反序列化的 JSON 字符串</param>
        /// <returns>反序列化对象</returns>
        public static object JSONDeserialize(this string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            var jss = new JavaScriptSerializer();
            return jss.DeserializeObject(str);
        }
        #endregion

        #region Json序列化
        /// <summary>
        /// 将对象进行JSON格式序列化
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>JSON字符串</returns>
        public static string JSONSerialize(this object obj)
        {
            var jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }
        #endregion
    }
}
