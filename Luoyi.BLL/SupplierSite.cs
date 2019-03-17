using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class SupplierSite
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.SupplierSite dal = new DAL.SupplierSite();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SupplierSiteInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SupplierSiteInfo info)
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
        public static SupplierSiteInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }


        public static List<SupplierSiteInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
