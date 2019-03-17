using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Common;

namespace Luoyi.Web.api
{
    /// <summary>
    /// CartAdd 的摘要说明
    /// </summary>
    public class CartAdd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var userInfo = UserHelper.GetUserInfo();
            int itemID = WebHelper.GetFromInt("ItemID");
            int qty = WebHelper.GetFromInt("Qty");
            var itemInfo = BLL.Item.GetInfo(itemID);

            AjaxJsonInfo ajax = new AjaxJsonInfo();

            if (itemInfo != null && !itemInfo.IsDel)
            {
                CartHelper cart = new CartHelper(userInfo);
                string msg = string.Empty;

                if (cart.AddCart(itemInfo, qty, out msg))
                {
                    ajax.Status = 1;
                    ajax.Message = "已加入购物车";
                    var cartInfo = new CartHelper.CartInfo();
                    cartInfo.Qty = cart.GetQty();
                    cartInfo.Total = cart.GetTotal();
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
                }
                else
                {
                    ajax.Status = 0;
                    ajax.Message = msg;
                }
            }
            else
            {
                ajax.Status = 0;
                ajax.Message = "商品不存在";
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