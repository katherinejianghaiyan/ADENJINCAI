using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// SO订单购物车
    /// </summary>
    [Serializable]
    public class SaleOrderCartInfo : ItemInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set;
            get;
        }
        /// <summary>
        /// GUID
        /// </summary>
        public string GUID
        {
            set;
            get;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
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
        /// 单位
        /// </summary>
        public string UOMGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty
        {
            set;
            get;
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price
        {
            set;
            get;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            set;
            get;
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expire
        {
            set;
            get;
        }
        /// <summary>
        /// 是否购买
        /// </summary>
        public bool IsBuy
        {
            set;
            get;
        }

        /// <summary>
        /// 优惠活动
        /// </summary>
        public string PromotionGuid
        {
            set;
            get;
        }

    }
}

