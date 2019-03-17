using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// SO订单成品
    /// </summary>
    [Serializable]
    public class SaleOrderItemInfo : ItemInfo
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
        /// 订单GUID
        /// </summary>
        public string SOGUID
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
        /// 合计金额
        /// </summary>
        public decimal Amount
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
        /// 发货状态
        /// </summary>
        public int ShippingStatus
        {
            set;
            get;
        }
        /// <summary>
        /// 发货时间
        /// </summary>
        public int ShippedDate
        {
            set;
            get;
        }
        /// <summary>
        /// 是否已评价
        /// </summary>
        public bool IsComment
        {
            set;
            get;
        }
        /// <summary>
        /// 是否已打印条码
        /// </summary>
        public bool IsPrint
        {
            set;
            get;
        }
        /// <summary>
        /// 优惠活动
        /// </summary>
        public string PromotionGUID
        {
            set;
            get;
        }
    }
}

