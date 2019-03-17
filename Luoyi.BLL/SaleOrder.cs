using System.Collections.Generic;
using System.Data;
using System.Linq;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class SaleOrder
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.SaleOrder dal = new DAL.SaleOrder();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SaleOrderInfo info, List<SaleOrderItemInfo> listItem)
        {
            return dal.Add(info, listItem);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SaleOrderInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int orderID)
        {
            return dal.Del(orderID);
        }

        /// <summary>
        /// 更新删除状态
        /// </summary>
        public static bool UpdateIsDel(string orderGUID, bool isDel)
        {
            return dal.UpdateIsDel(orderGUID, isDel);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public static bool UpdateStatus(int orderID, int status, int adminID)
        {
            return dal.UpdateStatus(orderID, status, adminID);
        }

        public static bool RemoveCoupon(int orderID)
        {
            return dal.RemoveCoupon(orderID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SaleOrderInfo GetInfo(int orderID)
        {
            return dal.GetInfo(orderID);
        }

        public static SaleOrderInfo GetInfo(int orderID, int userID)
        {
            return dal.GetInfo(orderID, userID);
        }

        public static SaleOrderInfo GetInfo(string orderCode)
        {
            return dal.GetInfo(orderCode);
        }

        public static SaleOrderInfo GetInfoByOutTradeNo(string outTradeNo)
        {
            return dal.GetInfoByOutTradeNo(outTradeNo);
        }
        public static List<SaleOrderUserInfo> GetPaiedUserList()
        {
            return dal.GetPaiedUserList();
        }

        public static List<SaleOrderInfo> GetList(int userID,string language = "")
        {
            return dal.GetList(userID,language) ?? new List<SaleOrderInfo>();
        }

        public static List<SaleOrderInfo> GetNotPaidList(int userID)
        {
            return dal.GetNotPaidList(userID);
        }

        public static List<SaleOrderInfo> GetList(string filter)
        {
            return dal.GetList(filter);
        }

        public static DataTable GetPage(SaleOrderFilter filter, out int recordCount)
        {
            return dal.GetPage(filter, out recordCount);
        }

        public static bool PaymentSuccess(string orderCode, string payTransCode)
        {
            bool paied = dal.PaymentSuccess(orderCode, payTransCode);
            try
            {
                if (paied)
                {
                    SaleOrderInfo info = dal.GetInfo(orderCode);
                    
                    //if(BLL.User.GetInfo(info.UserID).PaymentMethod.Equals("POD"))
                    //    return paied; //现场付没有优惠券 2017-5-10
                    //根据BU的抵扣规则，得到优惠券
                    List<CouponInfo> couponList = BLL.Coupon.GetList(User.GetInfo(info.UserID).BUGUID);
                    if (couponList != null && couponList.Count > 0)
                    {                        
                        
                        var query = couponList.OrderByDescending(c => c.Amount).ToList();                       
                        
                        int couponAmt = info.PaymentAmount.ToInt() / 50 * 8;
                        foreach (var q in query)
                        {
                            if (couponAmt < q.Amount) continue;
                            int tmp = couponAmt / q.Amount.ToInt();
                            if (tmp > 0)
                            {
                                BLL.UserCoupon.Add(new UserCouponInfo()
                                {
                                    CouponGUID = q.GUID,
                                    UserID = info.UserID,
                                    Qty = tmp,
                                    StartTime = info.PaidTime,
                                    StopTime = info.PaidTime.AddDays(25),
                                    CouponCode = string.Empty
                                });
                            }
                            couponAmt = couponAmt % q.Amount.ToInt();
                        }
                    }
                }
            }
            catch { }
            return paied;
        }

        public static Dictionary<int, string> GetStatusDict()
        {
            var list = EnumHelper.GetTypeItemList<SaleOrderInfo.StatusEnum>();
            return list.ToDictionary(item => item.Value.ToType<int>(), item => item.Description);
        }

        public static string GetStatusName(int status)
        {
            var list = GetStatusDict();
            return list.ContainsKey(status) ? list[status] : string.Empty;
        }

        public static bool Shipped(SaleOrderInfo info, List<StockTransactionInfo> stock, int adminID)
        {
            return dal.Shipped(info, stock, adminID);
        }
    }
}
