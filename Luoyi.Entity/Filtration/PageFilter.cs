
namespace Luoyi.Entity
{
    /// <summary>
    /// 分页过滤通用条件
    /// </summary>
    public class PageFilter
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { set; get; }
        /// <summary>
        /// 每页返回数据数量
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public int BeginDate { set; get; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public  int EndDate { set; get; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public  string Keyword { set; get; }
    }
}
