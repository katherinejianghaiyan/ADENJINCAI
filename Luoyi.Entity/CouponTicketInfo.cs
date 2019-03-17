
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// CouponInfo
    /// </summary>
    [Serializable]
    public class CouponTicketInfo : CouponInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int TicketID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string VerifyCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Qty
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

    }
}

