namespace Luoyi.Common
{
    /// <summary>
    /// 过滤属性
    /// </summary>
    public class FilterAttribute : System.Attribute
    {
        /// <summary>
        /// 操作符：=，>=, LIKE
        /// </summary>
        public string Operator { set; get; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ColumnName">列名</param>
        /// <param name="_Operator">操作符：=，>=, LIKE</param>
        public FilterAttribute(string _ColumnName, string _Operator)
        {
            ColumnName = _ColumnName;
            Operator = _Operator;
        }
    }
}
