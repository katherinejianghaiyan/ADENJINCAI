using System;
using System.Collections.Generic;

namespace Luoyi.Entity
{
    /// <summary>
    /// 营运中心
    /// </summary>
    [Serializable]
    public class SiteInfo : BUInfo
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
        /// 代号
        /// </summary>
        public string Code
        {
            set;
            get;
        }
        /// <summary>
        /// 公司名-中文
        /// </summary>
        public string CompNameCn
        {
            set;
            get;
        }
        /// <summary>
        /// 公司名-英文
        /// </summary>
        public string CompNameEn
        {
            set;
            get;
        }
        /// <summary>
        /// BUGUID
        /// </summary>
        public string BUGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set;
            get;
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode
        {
            set;
            get;
        }
        /// <summary>
        /// 鐢佃瘽鎬绘満
        /// </summary>
        public string TelNbr
        {
            set;
            get;
        }

        /* by steve 2018-11-15 use the field in parent
        public string EndHour { get; set; }

        /// <summary>
        /// 支付超时时间 秒
        /// </summary>

        public int DeliveryDays { get; set; }
        public string PickupTime { get; set; }
*/
        public DateTime LaunchDate { get; set; }
        public bool IsPaging { get; set; }
        
        

        public string WelcomeMsgCN
        {
            set;
            get;
        }
        public string WelcomeMsgEN
        {
            set;
            get;
        }

        public string PaymentMethod { get; set; }

        public string PaymentComments
        {
            set;
            get;
        }

        public int DailyMaxOrderQty { set; get; }
        public int ShowDays { set; get; }
        //public bool ShowByClass { set; get; }
        public bool ShowProductInfoInDetail { set; get; }


        public List<DeliveryTime> DeliveryTimes { set; get; }
        public bool CanDelOrder { set; get; }

        public bool ShowPrice { set; get; }

        public bool NeedWork { set; get; }
        public bool NeedShipToAddr { set; get; }
        public string BarcodeOfSO { set; get; }
        public string LoadImg { set; get; }
        public string LoadPages { set; get; }
    }


    public class DeliveryTime
    {
        public string Name { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public int TimeStep { set; get; }

    }
}