using System;
using System.Data;
using Luoyi.BLL;
using Luoyi.Common;

namespace Luoyi.Web.OtherPages.Comments
{
    public partial class SUZHYC : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {       
            try
            {
                if (comments.Text.Trim() == "")
                {
                    JavaScriptHelper.Show(this.Page, "请填写您的建议");
                    return;
                }


                    string cmt = comments.Text.Trim();
                    int userId = _UserInfo.UserID;
                    string siteGuid = siteInfo.GUID;

                    bool result = UserComment.SUZHYCAdd(new Entity.UserCommentInfo
                    {
                        SiteGUID = siteGuid,
                        Content = cmt,
                        UserID = userId
                    });

                    if (result)
                    {
                        comments.Text = "";
                        Bind();
                        JavaScriptHelper.Show(this.Page, "谢谢您的宝贵意见！");
                        
                    }

            }
            catch(Exception ex)
            {
                //throw ex;
            }
        }

        private void Bind()
        {
            rptList.DataSource = UserComment.GetData(_UserInfo.UserID.ToString());
            rptList.DataBind();
        }
    }
}