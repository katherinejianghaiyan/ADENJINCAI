
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// PO订单明细
    /// </summary>
    [Serializable]
    public class PurchaseOrderItemInfo : PurchaseOrderInfo
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
        /// PO采购单GUID
        /// </summary>
        public string POGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 采购原料GUID
        /// </summary>
        public string ItemGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal Price
        {
            set;
            get;
        }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal Qty
        {
            set;
            get;
        }
        /// <summary>
        /// 数量单位
        /// </summary>
        public string UOMID
        {
            set;
            get;
        }
        /// <summary>
        /// 要求交货日期
        /// </summary>
        public int RequiredDate
        {
            set;
            get;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set;
            get;
        }

    }
}

