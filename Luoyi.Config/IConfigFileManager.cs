using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Luoyi.Config
{
    /// <summary>
    /// 配置管理类接口
    /// </summary>
    public interface IConfigFileManager
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        IConfigInfo LoadConfig();

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        bool SaveConfig();
    }
}
