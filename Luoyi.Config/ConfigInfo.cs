using System;
using System.Collections.Generic;
using System.Text;

namespace Luoyi.Config
{
    /// <summary>
    /// ȫ������
    /// </summary>
    [Serializable]
    public class ConfigInfo : IConfigInfo
    { 

        
        /// <summary>
        /// ��վ����
        /// </summary>
        public string SiteName
        {
            get;
            set;
        }
        /// <summary>
        /// ��վ����
        /// </summary>
        public string Domain
        {
            get;
            set;
        }
        
           
         
        
    }
}
