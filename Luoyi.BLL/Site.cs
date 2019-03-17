using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Site
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.Site dal = new DAL.Site();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SiteInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SiteInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int iD)
        {
            return dal.Del(iD);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SiteInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SiteInfo GetInfo(string guid, string lang="ZHCN")
        {
            return dal.GetInfo(guid,lang);
        }

        public static List<SiteInfo> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// 根据SiteGuid取得地址信息
        /// </summary>
        /// <param name="siteGuid"></param>
        /// <returns></returns>
        public static List<SiteAddrs> GetAddrs(string siteGuid)
        {
            return dal.GetAddrs(siteGuid);
        }
    }
}
