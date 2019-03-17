using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Common;
using Luoyi.BLL;

namespace Luoyi.Web.api
{
    /// <summary>
    /// CartAdd 的摘要说明
    /// </summary>
    public class CartQty : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userInfo = UserHelper.GetUserInfo();
            int cartID = WebHelper.GetFromInt("CartID");
            int qty = WebHelper.GetFromInt("Qty");

            CartHelper cart = new CartHelper(userInfo);

            AjaxJsonInfo ajax = new AjaxJsonInfo();

            if (qty > 0)
            {
                if (cart.UpdateQty(cartID, qty))
                {
                    ajax.Status = 1;
                    ajax.Message = "更新数量成功";
                }
                else
                {
                    ajax.Status = 0;
                    ajax.Message = "更新数量失败";
                }
            }
            else
            {
                if (cart.Del(cartID))
                {
                    ajax.Status = 1;
                    ajax.Message = "删除成功";

                }
                else
                {
                    ajax.Status = 0;
                    ajax.Message = "删除失败";
                }
            }

            var cartInfo = new CartHelper.CartInfo();
            cartInfo.Qty = cart.GetQty();
            cartInfo.Total = cart.GetTotal();
            cartInfo.MyCart = cart.GetMyCart();
            cartInfo.MyPromotion = cart.GetMyPromotion();
            ajax.Data = cartInfo;

            //取消优惠券
            CouponHelper.RemoveCoupon();

            //设置默认优惠券
            var couponInfo = BLL.UserCoupon.GetDefaultUseCouponInfo(userInfo.UserID, userInfo.BUGUID, cartInfo.Total);
            if (couponInfo != null)
            {

                CouponHelper.RememberCoupon(couponInfo);
                //是否可以向下抵扣
                if (couponInfo.IsUseDown)
                {
                    //是否存在向下抵扣规则
                    var rule = BLL.CouponRule.GetInfo(userInfo.BUGUID, cartInfo.Total);

                    if (rule != null)
                    {
                        if (couponInfo.Amount == rule.DeductAmt)
                        {
                            cartInfo.CouponAmt = couponInfo.Amount;
                            cartInfo.Total -= couponInfo.Amount;
                        }
                        else if (couponInfo.Amount > rule.DeductAmt)
                        {
                            cartInfo.CouponAmt = rule.DeductAmt;
                            cartInfo.Total -= rule.DeductAmt;
                        }
                    }
                }
                else
                {
                    cartInfo.CouponAmt = couponInfo.Amount;
                    cartInfo.Total -= couponInfo.Amount;
                }
            }

            context.Response.Write(JsonHelper.JSONSerialize(ajax));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}