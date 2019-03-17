using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web.Controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        private string[] showClassPages = new string[] { "/Default.aspx", "/OtherPages/Menus/SUZHYC.aspx" , "/OtherPages/Menus/SUZLOR.aspx" };
        protected string title = HtmlLang.Lang("Title", "菜品清单");
        private List<ItemClassInfo> listClass = null;
        public string classGUID = "";
        public string pagePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                //SetClass();
                if (listClass == null || !listClass.Any() || listClass.Count < 2)
                {
                    HideDefault();
                    return;
                }
                rptItemClass.DataSource = listClass;
                rptItemClass.DataBind();
                
                List<ItemCustomClassInfo> list1 = BLL.ItemCustomClass.GetList();
                if (list1 == null) list1 = new List<ItemCustomClassInfo>();
                
                rptItemCustomClass.DataSource = list1;
                rptItemCustomClass.DataBind();               
            }
        }

        private void SetTilteByClass()//bool showByClass) 2018-10-29
        {
            
            if(string.IsNullOrWhiteSpace(classGUID))
            {
                if(HttpContext.Current.Request.Url.AbsolutePath != WebHelper.GetUrlPath("/Default.aspx") && //特殊页面
                    listClass.Any(q => !string.IsNullOrWhiteSpace(q.PagePath)))
                    classGUID = listClass.FirstOrDefault(q => !string.IsNullOrWhiteSpace(q.PagePath)).GUID;
            }

            if (string.IsNullOrWhiteSpace(classGUID)) return;
            //if (string.IsNullOrWhiteSpace(classGUID))
            //    classGUID = listClass[0].GUID;

            if (listClass == null || !listClass.Any() /* || !showByClass*/) return;
            
            var tmp = listClass.Single(q => q.GUID == classGUID);
            //if(showClassPages.Contains(HttpContext.Current.Request.Url.AbsolutePath))//showByClass &&  2018-10-29
            if(showClassPages.Any(q=> HttpContext.Current.Request.Url.AbsolutePath.Contains(q)))
                title = tmp.ClassName;
            pagePath = tmp.PagePath;
            return;
        }

        public void SetClass()
        {
            classGUID = WebHelper.GetQueryString("ClassGUID");
            if (listClass != null) return;

            
            Page p = this.Page;
            SiteInfo site = p.GetType().GetProperty("siteInfo").GetValue(p) as SiteInfo;
            if (site == null) return;

            listClass = BLL.ItemClass.GetList(site.BUGUID, site.GUID,SysConfig.UserLanguage.ToString());
            if (listClass == null || !listClass.Any()) return;
            SetTilteByClass();

            //不显示全部
            //if (site.ShowByClass || !HttpContext.Current.Request.Url.AbsolutePath.Contains("Default.aspx"))      2018-10-29         
            //    return;

            //2018-10-29
            if (listClass.Count(q => string.IsNullOrWhiteSpace(q.PagePath)) < 2) return; //通用大类只有一个，不显示全部

            if(listClass.Count(q => !string.IsNullOrWhiteSpace(q.PagePath)) > 0) ////有定制化页面放在前面，也显示全部
            {
                listClass = listClass.Select(q =>
                {
                    if (string.IsNullOrWhiteSpace(q.PagePath))
                        q.ClassName = "&nbsp;&nbsp;&nbsp;&nbsp;" + q.ClassName;
                    return q;
                }).ToList();
            }

            listClass.Insert(listClass.Count(q=>!string.IsNullOrWhiteSpace(q.PagePath)), //有定制化页面放在前面
                new ItemClassInfo() { GUID = "", ClassName = HtmlLang.Lang("AllClass", "全部", "/Default.aspx") });
        }
        public void HideDefault()
        {
            ShowDefault(false, false);
        }

        public void SetBack(bool isshow = false)
        {
            phBack.Visible = isshow;
        }

        public void ShowDefault(bool showClass, bool showSearch)
        {
            phClass.Visible = showClass;
            phSearch.Visible = showSearch;
        }


        protected void lbtnLanguage_Click(object sender, EventArgs e)
        {
            SysConfig.UserLanguage = SysConfig.UserLanguage == SysConfig.LanguageType.ZH_CN ? SysConfig.LanguageType.EN_US : SysConfig.LanguageType.ZH_CN;
            Response.Redirect(Request.RawUrl);
        }
    }
}