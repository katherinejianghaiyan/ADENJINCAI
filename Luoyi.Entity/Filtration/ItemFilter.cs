
using Luoyi.Common;
namespace Luoyi.Entity
{
    /// <summary>
    /// 分页过滤通用条件
    /// </summary>
    public class ItemFilter : PageFilter
    {
        /// <summary>
        /// ClassGUID
        /// </summary>
        [Filter("i.ClassGUID", "=")]
        public string ClassGUID
        {
            set;
            get;
        }

        /// <summary>
        /// SiteGUID
        /// </summary>
        [Filter("s.GUID", "=")]
        public string SiteGUID
        {
            set;
            get;
        }

        /// <summary>
        /// PriceType
        /// </summary>
        [Filter("p.PriceType", "=")]
        public string PriceType
        {
            set;
            get;
        }

        public string Columns { get; set; }
        public string TableName { get; set; }
        public string Filter { get; set; }
        //public string UserType { get; set; }
        //public string ItemSortNbrs { get; set; }
        public string OrderBy { get; set; }
        public string IsStop { get; set; }
        public string Sort { get; set; }
    }
}
