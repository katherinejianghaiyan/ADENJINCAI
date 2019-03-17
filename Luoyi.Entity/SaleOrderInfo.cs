using System;
using System.ComponentModel;

namespace Luoyi.Entity
{
    /// <summary>
    /// SO订单
    /// </summary>
    [Serializable]
    public class SaleOrderInfo : SiteInfo
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int OrderID
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
        /// 订单编号
        /// </summary>
        public string OrderCode
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
        /// 营运点GUID
        /// </summary>
        public string SiteGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 订购时间
        /// </summary>
        public DateTime OrderTime
        {
            set;
            get;
        }
        /// <summary>
        /// 订购日期
        /// </summary>
        public int OrderDate
        {
            set;
            get;
        }
        /// <summary>
        /// 送货地址
        /// </summary>
        public string ShipToAddr
        {
            set;
            get;
        }
        /// <summary>
        /// 要求交货日期
        /// </summary>
        public DateTime RequiredDate
        {
            set;
            get;
        }

        public string RequiredDinnerType
        {
            set;
            get;
        }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalAmount
        {
            set;
            get;
        }
        /// <summary>
        /// 优惠券号码
        /// </summary>
        public string CouponCode
        {
            set;
            get;
        }
        /// <summary>
        /// 优惠券抵扣金额
        /// </summary>
        public decimal CouponAmount
        {
            set;
            get;
        }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public decimal PaymentAmount
        {
            set;
            get;
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PaymentID
        {
            set;
            get;
        }
        /// <summary>
        /// 支付状态
        /// </summary>
        public bool IsPaid
        {
            set;
            get;
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PaidTime
        {
            set;
            get;
        }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayTransCode
        {
            set;
            get;
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status
        {
            set;
            get;
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel
        {
            set;
            get;
        }
        /// <summary>
        /// 发货日期
        /// </summary>
        public DateTime ShippedDate
        {
            set;
            get;
        }
        /// <summary>
        /// 发货人
        /// </summary>
        public int ShippedUser
        {
            set;
            get;
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Comments
        {
            set;
            get;
        }

        public enum StatusEnum
        {
            [Description("已发货")]
            YFH = 10,
            [Description("未发货")]
            WFH = 20,
        }

        public enum PaymentEnum
        {
            [Description("支付宝")]
            AliPay = 10,
            [Description("微信支付")]
            WechatPay = 20,
            [Description("现金支付")]
            Cash = 30
        }
    }
}

