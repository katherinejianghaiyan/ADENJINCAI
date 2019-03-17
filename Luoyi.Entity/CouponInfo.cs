
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// CouponInfo
    /// </summary>
    [Serializable]
    public class CouponInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int CouponID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string GUID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponName
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Image
        {
            set;
            get;
        }
        /// <summary>
        /// 抵扣金额
        /// </summary>
        public decimal Amount
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UseBeginTime
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UseEndTime
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsUseDown
        {
            set;
            get;
        }
    }
}

