using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    /// <summary>
    /// tblWechatConfig
    /// </summary>
    [Serializable]
    public class WechatConfig
    {
        public string appName
        {
            set;
            get;
        }
        public string appID
        {
            set;
            get;
        }
        public string path
        {
            set;
            get;
        }
    }
}