using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class CouponRule
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.CouponRule dal = new DAL.CouponRule();

        public static List<CouponRuleInfo> GetList(string buguid)
        {
            return dal.GetList(buguid);
        }

        public static List<CouponRuleInfo> GetList(string buguid, decimal orderAmt)
        {
            return dal.GetList(buguid, orderAmt);
        }

        public static CouponRuleInfo GetInfo(string buguid, decimal orderAmt)
        {
            return dal.GetInfo(buguid, orderAmt);
        }
    }
}
