using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;

namespace Luoyi.Web.Account
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Master.MasterAccount();

                imgHead.ImageUrl = _UserInfo.HeaderImgUrl;
                var list = BLL.SaleOrder.GetList(_UserInfo.UserID);
                ltlToPickUp.Text = list.Where(s => s.IsPaid && s.Status == (int)SaleOrderInfo.StatusEnum.WFH && s.RequiredDate.Date >= DateTime.Now.Date).Count().ToString();
                ltlNotPickUp.Text = list.Where(s => s.IsPaid && s.Status == (int)SaleOrderInfo.StatusEnum.WFH && s.RequiredDate.Date < DateTime.Now.Date).Count().ToString();
                ltlToComment.Text = BLL.SaleOrderItem.GetTable(string.Format(" AND s.UserID={0} AND o.IsPaid = 1 AND o.Status = {1} AND s.IsComment = 0", _UserInfo.UserID, (int)SaleOrderInfo.StatusEnum.YFH)).Rows.Count.ToString();
            }
        }
    }
}