
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// CouponInfo
    /// </summary>
    [Serializable]
    public class CouponRuleInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int RuleID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OrderAmt
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DeductAmt
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

