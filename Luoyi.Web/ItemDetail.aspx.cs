using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.Web
{
    public partial class ItemDetail : PageBase
    {
        protected string itemType = "";
        public int ItemID { get { return WebHelper.GetQueryInt("ItemID"); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Master.SetBack(true);

                var info = BLL.Item.GetInfo(ItemID,SysConfig.UserLanguage.ToString());

                if (info != null)
                {
                    itemType = info.ItemType.ToUpper();
                    Bind(info);
                }
            }
        }

        protected void Bind(ItemInfo info)
        {            
            var itemPriceInfo = BLL.ItemPrice.GetInfo(siteInfo.GUID, info.GUID, DateTime.Now.ToInt(), DateTime.Now.ToInt(), "Sales");

            ltlItemName.Text = info.ItemName;

            if(siteInfo.ShowPrice)
                ltlPrice.Text =  (itemPriceInfo != null ? itemPriceInfo.Price : info.Price).ToString("￥0.##");
            ltlDishSize.Text = info.DishSize;
            ltlTips.Text = info.Tips;
            ltlNutrition.Text = info.Nutrition;

            imgFav.Attributes.Add("data-itemguid", info.GUID);

            if (BLL.UserFavorite.Exists(info.GUID, _UserInfo.UserID))
            {
                imgFav.CssClass = "fav active";
                imgFav.ImageUrl = "/img/icon/icon-fav-active.png";
            }

            rptBom.DataSource = BLL.ItemBom.GetRecipe(info.GUID, SysConfig.UserLanguage.ToString()); //GetTable(info.GUID);
            rptBom.DataBind();

            var propery = BLL.ItemPropery.GetList(info.GUID);
            string[] dictCode = new string[] { "Fat", "Protein", "CarboHydrate", "DietaryFibre" };
            if (itemType != "SET")
            {
                ltlCalcorie.Text = propery.Find(p => p.DictCode.Equals("Calorie")) != null ? propery.FirstOrDefault(p => p.DictCode.Equals("Calorie")).PropValue : "0";

                rptPropery.DataSource = propery.Where(p => dictCode.Contains(p.DictCode)).ToList();
                rptPropery.DataBind();
                Image2.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["ItemImagesURL"], info.Image2);
                

                if (info.Cooking != "无")
                    ltlCooking.Text = string.Join("", info.Cooking.Split(new string[] { "1.", "2.", "3.", "4.", "5.", "6.", "7.", "8.", "9.", "10." }, StringSplitOptions.RemoveEmptyEntries).Select((step, index) => string.Format("<p class=\"step\">烹饪步骤<span>{0}</span></p><p class=\"stepDetail\">{1}</p>", index + 1, step)));

            }
            Image3.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["ItemImagesURL"], info.Image3);

            var dtComment = BLL.UserComment.GetTable(string.Format(" AND c.ItemGUID='{0}'", info.GUID));
            ltlComment.Text = ltlComment1.Text = dtComment.Rows.Count.ToString();
            rptComment.DataSource = dtComment;
            rptComment.DataBind();
        }
    }
}