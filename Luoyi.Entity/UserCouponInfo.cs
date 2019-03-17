
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// UserCouponInfo
    /// </summary>
    [Serializable]
    public class UserCouponInfo : CouponInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
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
        public int Qty
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string CouponCode
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StopTime
        {
            set;
            get;
        }
    }
}

