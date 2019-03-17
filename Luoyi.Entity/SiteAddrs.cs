using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// 营运中心
    /// </summary>
    [Serializable]
    public class SiteAddrs : BUInfo
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
        public string SiteGuid
        {
            set;
            get;
        }
        /// <summary>
        /// 代号
        /// </summary>
        public string ShipToAddr1ZHCN
        {
            set;
            get;
        }
        /// <summary>
        /// 公司名-中文
        /// </summary>
        public string ShipToAddr2ZHCN
        {
            set;
            get;
        }
        /// <summary>
        /// 公司名-英文
        /// </summary>
        public string ShipToAddr3ZHCN
        {
            set;
            get;
        }
        /// <summary>
        /// BUGUID
        /// </summary>
        public string ShipToAddr1ENUS
        {
            set;
            get;
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string ShipToAddr2ENUS
        {
            set;
            get;
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ShipToAddr3ENUS
        {
            set;
            get;
        }
        /// <summary>
        /// 电话总机
        /// </summary>
        public string SortName
        {
            set;
            get;
        }
        public bool Active
        {
            get;
            set;
        }
    }
}

