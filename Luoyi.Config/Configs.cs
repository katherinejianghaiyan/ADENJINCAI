using System;
using System.Collections.Generic;
using System.Text;


namespace Luoyi.Config
{
    /// <summary>
    /// �̳�ȫ�����ò�����
    /// </summary>
    public class Configs
    {
        /// <summary>
        /// �����ֵ����
        /// </summary>
       private static string cacheKey = "Config";

        /// <summary>
        /// ��ȡ������ʵ��
        /// </summary>
        /// <returns></returns>
        public static ConfigInfo GetConfig()
        {
            
            object obj = Cache.CacheHelper.Instance.GetObject(cacheKey);
            if (obj == null)
            {
                obj = ConfigFileManager.LoadConfig();
                if (obj != null)
                {
                    // ����30����
                    Cache.CacheHelper.Instance.AddObject(cacheKey, obj,  30);
                }
            }

            return (ConfigInfo)obj;
        }

        /// <summary>
        /// ����������ʵ��
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(ConfigInfo configinfo)
        {
            ConfigFileManager acfm = new ConfigFileManager();
            ConfigFileManager.ConfigInfo = configinfo;
            if (acfm.SaveConfig())
            {
                 Cache.CacheHelper.Instance.RemoveObject(cacheKey);
                return true;
            }
            else return false;
        }
    }
}
