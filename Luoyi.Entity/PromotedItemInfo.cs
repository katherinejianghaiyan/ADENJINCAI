
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// 优惠商品
    /// </summary>
    [Serializable]
    public class PromotedItemInfo : ItemInfo
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID
        {
            set;
            get;
        }
        /// <summary>
        /// 活动GUID
        /// </summary>
        public string PromotionGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string ItemGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 活动金额
        /// </summary>
        public decimal Price
        {
            set;
            get;
        }

    }
}

