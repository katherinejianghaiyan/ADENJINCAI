using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class CouponRelease
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.CouponRelease dal = new DAL.CouponRelease();

        public static CouponReleaseInfo GetInfo(string buguid)
        {
            return dal.GetInfo(buguid);
        }

        public static List<CouponReleaseInfo> GetList(string buguid)
        {
            return dal.GetList(buguid);
        }
    }
}
