using System;
using System.Collections.Generic;
using System.Text;

namespace Luoyi.Config
{
    /// <summary>
    /// 全局配置
    /// </summary>
    [Serializable]
    public class ConfigInfo : IConfigInfo
    { 

        
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName
        {
            get;
            set;
        }
        /// <summary>
        /// 网站域名
        /// </summary>
        public string Domain
        {
            get;
            set;
        }
        
           
         
        
    }
}
