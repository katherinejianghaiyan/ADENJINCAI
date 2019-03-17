using System;
using System.Text;

namespace Luoyi.Common
{
    /// <summary>
    /// SQL翻页列表过滤条件拼接处理类
    /// </summary>
    public  class FilterHelper
    {
        /// <summary>
        /// 生成查询条件语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">过滤条件实体：只有含FilterAttribute的才作处理</param>
        /// <returns></returns>
        public static string BuilderFilter<T>( T filter )
        {
            var entity = Activator.CreateInstance<T>();
            var objPropertiesArray = entity.GetType().GetProperties();
            var builder = new StringBuilder();
            foreach (var objProperty in objPropertiesArray)
            {
                var obj = objProperty.GetCustomAttributes(false);
                if (obj.Length == 0) continue;
                var attribute = (FilterAttribute)obj[0];
                if(attribute == null) continue;

                var value = objProperty.GetValue(filter, null);
                if(value== null) continue;

                switch (objProperty.PropertyType.FullName)
                {
                    case "System.Int32":
                        if (value.ToString() != "0")
                        {
                            builder.AppendFormat(" AND ( {0} {1} {2} ) ", attribute.ColumnName, attribute.Operator, value);
                        }
                        break;
                    case "System.String":
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            builder.AppendFormat(" AND ( {0} {1} '{2}' ) ", attribute.ColumnName, attribute.Operator, value);
                        }
                        break;
                    case "System.Boolean":
                        if (value.ToString() != "False")
                        {
                            builder.AppendFormat(" AND ( {0} = 1) ", attribute.ColumnName);
                        }
                        break;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///  拼接区间筛选SQL语句
        /// </summary>
        /// <param name="dateField"></param>
        /// <param name="begin">起始</param>
        /// <param name="end">结束</param>
        /// <returns></returns>
        public static string BuilderBetween(string dateField, int begin,int end)
        {
            var builder = new StringBuilder();

            if (begin > 0 && end == 0)
            {
                builder.AppendFormat(" AND ( {0} >= {1} ) ", dateField, begin);
            }
            else if (begin == 0 && end > 0)
            {
                builder.AppendFormat(" AND ( {0} <= {1} ) ", dateField,end);
            }
            else if (begin > 0 && end > 0)
            {
                builder.AppendFormat(" AND ( {0} BETWEEN {1} AND {2} ) ",dateField, begin, end);
            }

            return builder.ToString();
        }
    }
}
