using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class CouponTicket
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.CouponTicket dal = new DAL.CouponTicket();

        public static CouponTicketInfo GetInfo(string verifyCode)
        {
            return dal.GetInfo(verifyCode);
        }

        public static bool UpdateQty(int ticketID, int qty)
        {
            return dal.UpdateQty(ticketID, qty);
        }
    }
}
