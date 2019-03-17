using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Luoyi.Config
{
    /// <summary>
    /// ���ù�����ӿ�
    /// </summary>
    public interface IConfigFileManager
    {
        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <returns></returns>
        IConfigInfo LoadConfig();

        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <returns></returns>
        bool SaveConfig();
    }
}
