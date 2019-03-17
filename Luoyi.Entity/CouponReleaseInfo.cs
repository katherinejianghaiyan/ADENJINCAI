
using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// CouponInfo
    /// </summary>
    [Serializable]
    public class CouponReleaseInfo
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
        public string BUGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Infos
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        {
            set;
            get;
        }
    }
}

