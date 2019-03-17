using System;
using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class UserCoupon
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.UserCoupon dal = new DAL.UserCoupon();

        public static int Add(UserCouponInfo info)
        {
            return dal.Add(info);
        }

        public static bool UpdateQty(int id, int qty)
        {
            return dal.UpdateQty(id, qty);
        }

        //获取满足条件的默认优惠券
        public static UserCouponInfo GetDefaultUseCouponInfo(int userID, string buGuid, decimal orderAmt)
        {
            return dal.GetDefaultUseCouponInfo(userID, buGuid, orderAmt);
        }

        public static UserCouponInfo GetInfo(int userID, string couponGUID)
        {
            return dal.GetInfo(userID, couponGUID);
        }

        public static UserCouponInfo GetInfo(int userID, string couponGUID, string couponCode, DateTime StartTime, DateTime stopTime)
        {
            return dal.GetInfo(userID, couponGUID, couponCode, StartTime, stopTime);
        }

        public static UserCouponInfo GetInfo(int id)
        {
            return dal.GetInfo(id);
        }


        public static List<UserCouponInfo> GetList(int userID, string buGuid)
        {
            return dal.GetList(userID, buGuid) ?? new List<UserCouponInfo>();
        }

        public static List<UserCouponInfo> GetList(int userID, string buGuid, decimal orderAmt)
        {
            return dal.GetList(userID, buGuid, orderAmt) ?? new List<UserCouponInfo>();
        }
    }
}
