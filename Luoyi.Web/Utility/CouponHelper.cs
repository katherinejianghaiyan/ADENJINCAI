using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Luoyi.Common;
using System.Data;
using Luoyi.Entity;
using Luoyi.BLL;


namespace Luoyi.Web
{
    public class CouponHelper
    {
        public const string JINGCAIHT_ADEN_COUPON = "_JINGCAIHT_ADEN_COUPON";

        public static void RememberCoupon(UserCouponInfo info)
        {
            CookieHelper.SetCookieKeyValue(JINGCAIHT_ADEN_COUPON, "COUPONINFO", info.JSONSerialize());
        }

        public static UserCouponInfo GetCoupon()
        {
            UserCouponInfo info = null;
            var couponInfo = CookieHelper.GetCookieValueByKey(JINGCAIHT_ADEN_COUPON, "COUPONINFO");
            if (!string.IsNullOrEmpty(couponInfo))
            {
                info = couponInfo.JSONDeserialize<UserCouponInfo>();
            }
            return info;
        }

        public static void RemoveCoupon()
        {
            CookieHelper.SetCookieKeyValue(JINGCAIHT_ADEN_COUPON, "COUPONINFO", "");
        }

        public static bool UseCouponTicket(string verifyCode, out CouponTicketInfo info, out string msg)
        {
            msg = string.Empty;
            info = CouponTicket.GetInfo(verifyCode);
            if (info != null)
            {
                return true;
            }
            else
            {
                msg = "优惠码错误或已失效";
                return false;
            }
        }

        public static void RemoveOrderCoupon(int userID)
        {
            var listSaleOrder = SaleOrder.GetNotPaidList(userID);
            if (listSaleOrder != null)
            {
                foreach (var item in listSaleOrder)
                {
                    var couponInfo = UserCoupon.GetInfo(item.CouponCode.ToInt32());
                    SaleOrder.RemoveCoupon(item.OrderID);
                    UserCoupon.UpdateQty(couponInfo.ID, 1);
                }
            }
        }
    }
}