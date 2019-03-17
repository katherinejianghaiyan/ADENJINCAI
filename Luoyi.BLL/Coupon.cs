using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Luoyi.BLL
{
    public class Coupon
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.Coupon dal = new DAL.Coupon();
        public static List<Entity.CouponInfo> GetList(string BUGUID)
        {
            return dal.GetBUList(BUGUID);
        }
    }
}
