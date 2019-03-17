using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Luoyi.Entity;
using Luoyi.Common;
using System.Text;

namespace Luoyi.Web
{
    public class CartHelper
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        public UserInfo userInfo = null;

        /// <summary>
        /// 是否优惠商品
        /// </summary>
        public bool isPromotion = false;

        /// <summary>
        /// 选择优惠商品个数
        /// </summary>
        public int promotionMaxQty = 0;
        public decimal minOrderAmt = 0;

        /// <summary>
        /// 购物车数据
        /// </summary>
        public List<SaleOrderCartInfo> listCart = null;
        public PromotionInfo promotionInfo = null;
        public List<PromotionInfo> listPromotion = null;

        public CartHelper(UserInfo info, bool promotion = false)
        {
            userInfo = info;
            isPromotion = promotion;

            //获取购物车商品
            listCart = BLL.SaleOrderCart.GetList(userInfo.UserID,SysConfig.UserLanguage.ToString());

            listPromotion = BLL.Promotion.GetList(userInfo.BUGUID);

            //清空优惠商品
            if (!isPromotion)
            {
                BLL.SaleOrderCart.EmptyPromotion(userInfo.UserID);
                ReGetCartList();
            }
            else
            {
                var total = listCart.Where(c => c.IsBuy && string.IsNullOrEmpty(c.PromotionGuid)).Sum(c => c.Price * c.Qty);

                promotionInfo = BLL.Promotion.GetInfo(userInfo.BUGUID, total);

                if (promotionInfo != null)
                {
                    promotionMaxQty = promotionInfo.MaxQty;
                }
                else
                {
                    if (listPromotion != null && listPromotion.Count > 0)
                    {
                        minOrderAmt = listPromotion.First().MinOrderAmt;
                    }
                }
            }

        }

        public int GetQty()
        {
            return listCart.Where(c => c.IsBuy).Sum(c => c.Qty);
        }

        public int GetCartQty()
        {
            return listCart.Sum(c => c.Qty);
        }

        public int GetPromotionBuyQty()
        {
            return listCart.Where(c => c.IsBuy && !string.IsNullOrEmpty(c.PromotionGuid)).Sum(c => c.Qty);
        }

        public decimal GetTotal()
        {
            return listCart.Where(c => c.IsBuy).Sum(c => c.Price * c.Qty);
        }

        public void ReGetCartList()
        {
            listCart = BLL.SaleOrderCart.GetList(userInfo.UserID,SysConfig.UserLanguage.ToString());
        }

        public bool AddCart(ItemInfo itemInfo, int qty, out string msg)
        {
            msg = string.Empty;

            //查找相同商品
            var cartInfo = listCart.Find(c => c.ItemGUID == itemInfo.GUID && c.IsBuy);
            if (cartInfo != null)
            {
                if (BLL.SaleOrderCart.UpdateQty(cartInfo.ID, cartInfo.Qty + qty, userInfo.UserID))
                {
                    //更新购物车数据
                    ReGetCartList();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            var info = new SaleOrderCartInfo();
            info.GUID = Guid.NewGuid().ToString("N");
            info.UserID = userInfo.UserID;
            info.ItemGUID = itemInfo.GUID;
            info.Qty = qty;

            var itemPriceInfo = BLL.ItemPrice.GetInfo(userInfo.SiteGUID, itemInfo.GUID, DateTime.Now.ToInt(), DateTime.Now.ToInt(), "Sales");
            info.Price = itemPriceInfo != null ? itemPriceInfo.Price : itemInfo.Price;

            info.CreateTime = DateTime.Now;
            info.Expire = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1);
            info.IsBuy = true;

            //是否存在促销活动

            if (!BLL.SaleOrderCart.Add(info))
            {
                listCart = BLL.SaleOrderCart.GetList(userInfo.UserID,SysConfig.UserLanguage.ToString());
                msg = "添加失败";
                return false;
            }

            //更新购物车数据
            ReGetCartList();
            return true;
        }

        public bool AddCart(SaleOrderCartInfo cartInfo)
        {
            if (BLL.SaleOrderCart.Add(cartInfo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateQty(int cartID, int qty)
        {
            if (BLL.SaleOrderCart.UpdateQty(cartID, qty, userInfo.UserID))
            {
                ReGetCartList();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateIsBuy(int cartID, bool isBuy)
        {
            if (BLL.SaleOrderCart.UpdateIsBuy(cartID, isBuy, userInfo.UserID))
            {
                ReGetCartList();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Del(int cartID)
        {
            if (BLL.SaleOrderCart.Del(cartID))
            {
                ReGetCartList();
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetMyCart()
        {
            return GetMyCart(true,true);
        }
        public string GetMyCart(bool canAdd,bool showPrice)
        {
            StringBuilder sb = new StringBuilder();

            var total = listCart.Where(c => c.IsBuy && string.IsNullOrEmpty(c.PromotionGuid)).Sum(c => c.Price * c.Qty);

            var promotion = listPromotion != null && listPromotion.Count > 0 ? listPromotion.FirstOrDefault(p => p.MinOrderAmt <= total) : null;

            if (!isPromotion && listPromotion != null && listPromotion.Count > 0)
            {

                promotionMaxQty = promotion != null ? promotion.MaxQty : 0;

                var listPromotedItem = BLL.PromotedItem.GetList(userInfo.BUGUID);

                foreach (var item in listPromotedItem)
                {
                    var cartInfo = new SaleOrderCartInfo();
                    cartInfo.GUID = Guid.NewGuid().ToString("N");
                    cartInfo.UserID = userInfo.UserID;
                    cartInfo.ItemGUID = item.ItemGUID;
                    cartInfo.PromotionGuid = item.PromotionGUID.Trim();
                    cartInfo.Qty = 1;
                    cartInfo.Price = item.Price;
                    cartInfo.CreateTime = DateTime.Now;
                    cartInfo.Expire = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1);
                    cartInfo.IsBuy = false;

                    AddCart(cartInfo);

                }

                ReGetCartList();
            }

            var cartlist = listCart.Where(c => string.IsNullOrEmpty(c.PromotionGuid)).ToList();

            foreach (var item in cartlist)
            {
                sb.Append("<div class=\"goods-choose\">");
                sb.Append("<span class=\"checkbox\">");
                sb.AppendFormat("<input type=\"checkbox\" value=\"{0}\" {1} data-promotion=\"false\"/>", item.ID, item.IsBuy ? "checked=\"checked\"" : "");
                sb.Append("<img class=\"focus\" src=\"./img/icon/cart-check-yes.png\" />");
                sb.Append("<img class=\"blur\" src=\"./img/icon/cart-check-no.png\" />");
                sb.Append("</span>");
                sb.AppendFormat("<a href=\"/ItemDetail.aspx?ItemID={0}\" class=\"goods-img\"><img src=\"{1}\" /></a>", 
                    item.ItemID, System.IO.Path.Combine(ConfigurationManager.AppSettings["ItemImagesURL"], item.Image1));
                sb.Append("<div class=\"sku\">");
                sb.AppendFormat("<p><span class=\"name\">{0}</span>", item.ItemName);
                if(showPrice)
                    sb.AppendFormat("<span class=\"price\">￥{0}</span>", item.Price.ToString("G0"));

                sb.AppendFormat("</p><div class=\"numBox\" {0}>", canAdd? "" : "style=\"width:5rem;\"");
                sb.AppendFormat("<span class=\"quantity\">{0}</span>", HtmlLang.Lang("QUANTITY", "数量", "/MyCart.aspx"));
                sb.Append("<b class=\"mins\">-</b>");
                sb.AppendFormat("<input readonly=\"readonly\" value=\"{0}\" type=\"text\" data-cartid=\"{1}\" />", item.Qty, item.ID);
                if (canAdd) sb.Append("<b class=\"plus\">+</b>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");


            }


            //var promotionlist = listCart.Where(c => !string.IsNullOrEmpty(c.PromotionGuid)).ToList();

            //if (promotionlist != null && promotionlist.Count > 0)
            //{

            //    sb.Append("<div class=\"goods-promotion\">");
            //    sb.AppendFormat("<p class=\"title\">优惠促销(单笔金额{0})</p>", string.Join(",", listPromotion.Select(p => string.Format("满{0}选{1}个", p.MinOrderAmt.ToString("G0"), p.MaxQty))));
            //    sb.Append("<ul>");
            //    foreach (var item in promotionlist)
            //    {
            //        sb.Append("<li>");
            //        sb.AppendFormat("<img src=\"{0}\" alt=\"{1}\" />", item.Image1, item.ItemName);
            //        sb.AppendFormat("<p>{0}</p>", item.ItemName);
            //        sb.AppendFormat("<p><del>￥{0}</del><span>￥{1}</span></p>", BLL.ItemPrice.GetInfo(item.ItemGUID, DateTime.Now.ToInt(), DateTime.Now.ToInt(), "Sales").Price.ToString("G0"), item.Price.ToString("G0"));
            //        sb.Append("<span class=\"checkbox\">");
            //        sb.AppendFormat("<input type=\"checkbox\" value=\"{0}\" {1} data-promotion=\"true\"/>", item.ID, item.IsBuy ? "checked=\"checked\"" : "");
            //        sb.Append("<img class=\"focus\" src=\"/img/icon/cart-check-yes.png\"/>");
            //        sb.Append("<img class=\"blur\" src=\"/img/icon/cart-check-no.png\"/>");
            //        sb.Append("</span>");
            //        sb.Append("</li>");
            //    }
            //    sb.Append("</ul>");
            //    sb.Append("</div>");
            //}

            return sb.ToString();
        }


        public string GetMyPromotion()
        {
            StringBuilder sb = new StringBuilder();

            //var total = listCart.Where(c => c.IsBuy && string.IsNullOrEmpty(c.PromotionGuid)).Sum(c => c.Price * c.Qty);

            //var promotion = listPromotion.FirstOrDefault(p => p.MinOrderAmt <= total);

            //if (!isPromotion)
            //{

            //    promotionMaxQty = promotion != null ? promotion.MaxQty : 0;

            //    var listPromotedItem = BLL.PromotedItem.GetList();

            //    foreach (var item in listPromotedItem)
            //    {
            //        var cartInfo = new SaleOrderCartInfo();
            //        cartInfo.GUID = Guid.NewGuid().ToString("N");
            //        cartInfo.UserID = userInfo.UserID;
            //        cartInfo.ItemGUID = item.ItemGUID;
            //        cartInfo.PromotionGuid = item.PromotionGUID;
            //        cartInfo.Qty = 1;
            //        cartInfo.Price = item.Price;
            //        cartInfo.CreateTime = DateTime.Now;
            //        cartInfo.Expire = DateTime.Now.AddDays(1);
            //        cartInfo.IsBuy = false;

            //        AddCart(cartInfo);

            //    }

            //    ReGetCartList();
            //}

            //var cartlist = listCart.Where(c => string.IsNullOrEmpty(c.PromotionGuid)).ToList();

            //foreach (var item in cartlist)
            //{
            //    sb.Append("<div class=\"goods-choose\">");
            //    sb.Append("<span class=\"checkbox\">");
            //    sb.AppendFormat("<input type=\"checkbox\" value=\"{0}\" {1} data-promotion=\"false\"/>", item.ID, item.IsBuy ? "checked=\"checked\"" : "");
            //    sb.Append("<img class=\"focus\" src=\"/img/icon/cart-check-yes.png\" />");
            //    sb.Append("<img class=\"blur\" src=\"/img/icon/cart-check-no.png\" />");
            //    sb.Append("</span>");
            //    sb.AppendFormat("<a href=\"/ItemDetail.aspx?ItemID={0}\" class=\"goods-img\"><img src=\"{1}\" /></a>", item.ItemID, item.Image1);
            //    sb.Append("<div class=\"sku\">");
            //    sb.AppendFormat("<p><span class=\"name\">{0}</span><span class=\"price\">￥{1}</span></p>", item.ItemName, item.Price.ToString("G0"));

            //    sb.Append("<div class=\"numBox\">");
            //    sb.AppendFormat("<span class=\"quantity\">{0}</span>", HtmlLang.Lang("QUANTITY", "数量", "/MyCart.aspx"));
            //    sb.Append("<b class=\"mins\">-</b>");
            //    sb.AppendFormat("<input readonly=\"readonly\" value=\"{0}\" type=\"text\" data-cartid=\"{1}\" />", item.Qty, item.ID);
            //    sb.Append("<b class=\"plus\">+</b>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");


            //}


            var promotionlist = listCart.Where(c => !string.IsNullOrEmpty(c.PromotionGuid)).ToList();

            if (listPromotion != null && listPromotion.Count > 0 && promotionlist != null && promotionlist.Count > 0)
            {
                sb.Append("<div class=\"goods-promotion\">");
                sb.AppendFormat("<p class=\"title\">优惠促销(单笔金额{0})</p>", string.Join(",", listPromotion.Select(p => string.Format("满{0}选{1}个", p.MinOrderAmt.ToString("G0"), p.MaxQty))));
                sb.Append("<ul>");
                foreach (var item in promotionlist)
                {
                    sb.Append("<li>");
                    sb.AppendFormat("<img src=\"{0}\" alt=\"{1}\" />", item.Image1, item.ItemName);
                    sb.AppendFormat("<p>{0}</p>", item.ItemName);
                    sb.AppendFormat("<p><del>￥{0}</del><span>￥{1}</span></p>", BLL.ItemPrice.GetInfo(userInfo.SiteGUID, item.ItemGUID, DateTime.Now.ToInt(), DateTime.Now.ToInt(), "Sales").Price.ToString("G0"), item.Price.ToString("G0"));
                    sb.Append("<span class=\"checkbox\">");
                    sb.AppendFormat("<input type=\"checkbox\" value=\"{0}\" {1} data-promotion=\"true\"/>", item.ID, item.IsBuy ? "checked=\"checked\"" : "");
                    sb.Append("<img class=\"focus\" src=\"/img/icon/cart-check-yes.png\"/>");
                    sb.Append("<img class=\"blur\" src=\"/img/icon/cart-check-no.png\"/>");
                    sb.Append("</span>");
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
                sb.Append("</div>");
            }

            return sb.ToString();
        }

        public class CartInfo
        {
            public int Qty { get; set; }
            public decimal Total { get; set; }
            public string MyCart { get; set; }
            public string MyPromotion { get; set; }
            public decimal CouponAmt { get; set; }
        }
    }
}