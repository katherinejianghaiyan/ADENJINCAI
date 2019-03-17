using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Promotion
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.Promotion dal = new DAL.Promotion();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PromotionInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(PromotionInfo info)
        {
            return dal.Update(info);
        }

        public static PromotionInfo GetInfo(string buguid, decimal minOrderAmt)
        {
            return dal.GetInfo(buguid, minOrderAmt);
        }

        public static List<PromotionInfo> GetList(string buguid)
        {
            return dal.GetList(buguid);
        }
    }
}
