using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.Web
{
    public partial class ItemSetDetail : PageBase
    {

        public int ItemID { get { return WebHelper.GetQueryInt("ItemID"); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Master.SetBack(true);

                var info = BLL.Item.GetInfo(ItemID);

                if (info != null)
                {
                    Bind(info);
                }
            }
        }

        protected void Bind(ItemInfo info)
        {
            
            var itemPriceInfo = BLL.ItemPrice.GetInfo(siteInfo.GUID, info.GUID, DateTime.Now.ToInt(), DateTime.Now.ToInt(), "Sales");

            ltlItemName.Text = info.ItemName;
            ltlPrice.Text = itemPriceInfo != null ? itemPriceInfo.Price.ToString("G0") : info.Price.ToString("G0");
            ltlDishSize.Text = info.DishSize;

            imgFav.Attributes.Add("data-itemguid", info.GUID);

            if (BLL.UserFavorite.Exists(info.GUID, _UserInfo.UserID))
            {
                imgFav.CssClass = "fav active";
                imgFav.ImageUrl = "/img/icon/icon-fav-active.png";
            }

            var propery = BLL.ItemPropery.GetList(info.GUID);
            string[] dictCode = new string[] { "Fat", "Protein", "CarboHydrate", "DietaryFibre" };



            rptBom.DataSource = BLL.ItemBom.GetRecipe(info.GUID,SysConfig.UserLanguage.ToString());//.GetTable(info.GUID);
            rptBom.DataBind();

            
            Image3.ImageUrl = info.Image3;
   

            var dtComment = BLL.UserComment.GetTable(string.Format(" AND c.ItemGUID='{0}'", info.GUID));
            ltlComment.Text = ltlComment1.Text = dtComment.Rows.Count.ToString();
            rptComment.DataSource = dtComment;
            rptComment.DataBind();
        }
    }
}