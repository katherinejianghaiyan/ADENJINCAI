
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// BU
    /// </summary>
    [Serializable]
    public class BUInfo
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
        /// BUGUID
        /// </summary>
        public string BUGUID
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
        /// 所属父级
        /// </summary>
        public string ParentGUID
        {
            set;
            get;
        }
        /// <summary>
        /// ERP数据库名
        /// </summary>
        public string ERPCode
        {
            set;
            get;
        }

        /// <summary>
        /// 上午截止时间
        /// </summary>
        public string EndHour { get; set; }

        /// <summary>
        /// 支付超时时间 秒
        /// </summary>
        public int TimeOut { get; set; }

        public int DeliveryDays { get; set; }
        public string PickupTime { get; set; }
    }
}

