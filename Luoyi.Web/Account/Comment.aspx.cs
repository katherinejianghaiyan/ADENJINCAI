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
    public partial class Comment : PageBase
    {
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
            rptList.DataSource = BLL.SaleOrderItem.GetTable(string.Format(" AND s.UserID={0} AND o.IsPaid = 1 AND o.Status = {1} AND s.IsComment = 0", _UserInfo.UserID, (int)SaleOrderInfo.StatusEnum.YFH));
            rptList.DataBind();
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Submit"))
            {
                var id = e.CommandArgument.ToString().ToInt32();
                HiddenField hidOrderID = e.Item.FindControl("hidOrderID") as HiddenField;
                HiddenField hidItemGUID = e.Item.FindControl("hidItemGUID") as HiddenField;
                HiddenField hidStar = e.Item.FindControl("hidStar") as HiddenField;
                TextBox txtContent = e.Item.FindControl("txtContent") as TextBox;

                var info = new UserCommentInfo()
                {
                    OrderID = hidOrderID.Value.ToInt32(),
                    UserID = _UserInfo.UserID,
                    ItemGUID = hidItemGUID.Value,
                    Score = hidStar.Value.ToInt32(),
                    ScoreTaste = 0,
                    ScorePrice = 0,
                    ScoreService = 0,
                    Content = txtContent.GetSafeValue(),
                    Images = string.Empty,
                    CommentTime = DateTime.Now
                };

                if (BLL.UserComment.Add(info) > 0)
                {
                    BLL.SaleOrderItem.UpdateIsComment(id, true);
                    JavaScriptHelper.Show(this.Page, "评论成功");
                    Bind();
                }

            }
        }
    }
}