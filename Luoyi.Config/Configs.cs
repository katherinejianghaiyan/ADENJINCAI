using System;
using System.Collections.Generic;
using System.Text;


namespace Luoyi.Config
{
    /// <summary>
    /// 商城全局配置操作类
    /// </summary>
    public class Configs
    {
        /// <summary>
        /// 缓存键值名称
        /// </summary>
       private static string cacheKey = "Config";

        /// <summary>
        /// 获取配置类实例
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
                    // 缓存30分钟
                    Cache.CacheHelper.Instance.AddObject(cacheKey, obj,  30);
                }
            }

            return (ConfigInfo)obj;
        }

        /// <summary>
        /// 保存配置类实例
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
