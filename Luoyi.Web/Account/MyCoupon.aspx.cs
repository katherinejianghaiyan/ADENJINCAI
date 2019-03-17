using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web.Account
{
    public partial class MyCoupon : PageBase
    {
        List<CouponRuleInfo> listRule;
        private decimal cartTotalAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.MasterAccount();
                Bind();
            }
        }

        protected void Bind()
        {

            CouponHelper.RemoveOrderCoupon(_UserInfo.UserID);

            CartHelper cart = new CartHelper(_UserInfo);
            cartTotalAmount = cart.GetTotal();

            listRule = BLL.CouponRule.GetList(_UserInfo.BUGUID);
            var listUserCoupon = BLL.UserCoupon.GetList(_UserInfo.UserID, _UserInfo.BUGUID);

            if (listRule != null && listRule.Count > 0)
            {
                ltlCouponRule.Text = string.Format("单笔订单{0}，", string.Join(",", listRule.Select(r => string.Format("满{0}减{1}", r.OrderAmt.ToString("G0"), r.DeductAmt.ToString("G0")))));
            }

            List<UserCouponInfo> listCoupon = new List<UserCouponInfo>();

            foreach (var item in listUserCoupon)
            {
                for (int i = 0; i < item.Qty; i++)
                {
                    listCoupon.Add(item);
                }
            }

            rptList.DataSource = listCoupon;
            rptList.DataBind();
        }

        protected void btnCouponAdd_Click(object sender, EventArgs e)
        {
            var couponCode = txtCouponCode.GetSafeValue();
            string msg = string.Empty;
            var ticketInfo = new CouponTicketInfo();
            if (CouponHelper.UseCouponTicket(couponCode, out ticketInfo, out msg))
            {
                var couponInfo = BLL.UserCoupon.GetInfo(_UserInfo.UserID, ticketInfo.CouponGUID, couponCode, ticketInfo.UseBeginTime, ticketInfo.UseEndTime);

                if (couponInfo != null)
                {
                    if (BLL.UserCoupon.UpdateQty(couponInfo.ID, 1))
                    {
                        JavaScriptHelper.ResponseScript(this.Page, "$(function(){$(\".weui_dialog_alert .weui_dialog_bd\").html(\"新增优惠券成功\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();});});");

                    }
                    else
                    {
                        JavaScriptHelper.ResponseScript(this.Page, "$(function(){$(\".weui_dialog_alert .weui_dialog_bd\").html(\"新增优惠券失败\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();});});");
                    }
                }
                else
                {
                    var info = new UserCouponInfo()
                    {
                        UserID = _UserInfo.UserID,
                        CouponGUID = ticketInfo.CouponGUID,
                        StartTime = ticketInfo.UseBeginTime,
                        StopTime = ticketInfo.UseEndTime,
                        Qty = 1,
                        CouponCode = ticketInfo.VerifyCode
                    };

                    if (BLL.UserCoupon.Add(info) > 0)
                    {
                        JavaScriptHelper.ResponseScript(this.Page, "$(function(){$(\".weui_dialog_alert .weui_dialog_bd\").html(\"新增优惠券成功\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();});});");

                    }
                    else
                    {
                        JavaScriptHelper.ResponseScript(this.Page, "$(function(){$(\".weui_dialog_alert .weui_dialog_bd\").html(\"新增优惠券失败\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();});});");
                    }
                }

                BLL.CouponTicket.UpdateQty(ticketInfo.TicketID, -1);

                Bind();

            }
            else
            {
                JavaScriptHelper.ResponseScript(this.Page, "$(function(){$(\".weui_dialog_alert .weui_dialog_bd\").html(\"" + msg + "\");$(\".weui_dialog_alert\").show().on('click', '.weui_btn_dialog', function () {$('.weui_dialog_alert').off('click').hide();});});");
            }
        }
    }
}