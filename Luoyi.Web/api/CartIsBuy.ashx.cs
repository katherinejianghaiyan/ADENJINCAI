using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Common;
using Luoyi.BLL;

namespace Luoyi.Web.api
{
    /// <summary>
    /// CartIsBuy 的摘要说明
    /// </summary>
    public class CartIsBuy : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userInfo = UserHelper.GetUserInfo();
            int cartID = WebHelper.GetFromInt("CartID");
            bool isBuy = WebHelper.GetForm<bool>("IsBuy", true);
            bool promotion = WebHelper.GetForm<bool>("Promotion", false);

            AjaxJsonInfo ajax = new AjaxJsonInfo();

            CartHelper cart = new CartHelper(userInfo, promotion);

            if (promotion)
            {
                //优惠活动
                if (isBuy)
                {
                    if (cart.GetPromotionBuyQty() < cart.promotionMaxQty)
                    {
                        if (cart.UpdateIsBuy(cartID, isBuy))
                        {
                            ajax.Status = 1;
                            ajax.Message = "优惠商品选择成功";

                        }
                        else
                        {
                            ajax.Status = 0;
                            ajax.Message = "优惠商品选择失败";
                        }
                    }
                    else
                    {
                        ajax.Status = 2;
                        if (cart.promotionMaxQty == 0)
                        {
                            ajax.Message = string.Format("你的订单金额低于{0}元，不能选购优惠品", cart.minOrderAmt.ToString("G0"));
                        }
                        else
                        {
                            ajax.Message = string.Format("只能选择{0}个优惠商品", cart.promotionMaxQty);
                        }
                    }
                }
                else
                {
                    if (cart.UpdateIsBuy(cartID, isBuy))
                    {
                        ajax.Status = 1;
                        ajax.Message = "优惠商品取消成功";

                    }
                    else
                    {
                        ajax.Status = 0;
                        ajax.Message = "优惠商品取消失败";
                    }
                }
            }
            else
            {
                if (cart.UpdateIsBuy(cartID, isBuy))
                {
                    ajax.Status = 1;
                    ajax.Message = "商品选择成功";

                }
                else
                {
                    ajax.Status = 0;
                    ajax.Message = "商品选择失败";
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