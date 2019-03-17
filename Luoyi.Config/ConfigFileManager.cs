using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Luoyi.Config
{
    /// <summary>
    /// �̳����ù�����
    /// </summary>
    class ConfigFileManager : DefaultConfigFileManager
    {
        private static ConfigInfo m_configinfo;

        /// <summary>
        /// �ļ��޸�ʱ��
        /// </summary>
        private static DateTime m_fileoldchange;

        /// <summary>
        /// ��ʼ���ļ��޸�ʱ��Ͷ���ʵ��
        /// </summary>
        static ConfigFileManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (ConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(ConfigInfo));
        }

        /// <summary>
        /// ��ǰ��������ʵ��
        /// </summary>
        public new static IConfigInfo ConfigInfo
        {
            get { return m_configinfo; }
            set { m_configinfo = (ConfigInfo)value; }
        }

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        public static string filename = null;

        /// <summary>
        /// ��ȡ�����ļ�����·��
        /// </summary>
        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    //����վ���ʱ��֧������Ŀ¼
                    string name = "/Config/Luoyi.config";
                    filename = System.Web.HttpContext.Current.Server.MapPath(name);
                }

                return filename;
            }
        }

        /// <summary>
        /// ����������ʵ��
        /// </summary>
        /// <returns></returns>
        public static ConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo);
            return ConfigInfo as ConfigInfo;
        }

        /// <summary>
        /// ����������ʵ��
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}
