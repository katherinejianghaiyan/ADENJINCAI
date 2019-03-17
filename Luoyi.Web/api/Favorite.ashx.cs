using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.Web.api
{
    /// <summary>
    /// Favorite 的摘要说明
    /// </summary>
    public class Favorite : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var typeID = WebHelper.GetFromInt("TypeID");
            var itemGUID = WebHelper.GetFormString("ItemGUID");
            var userInfo = UserHelper.GetUserInfo();

            AjaxJsonInfo ajax = new AjaxJsonInfo();

            if (typeID == 1)
            {
                var info = new UserFavoriteInfo()
                {
                    UserID = userInfo.UserID,
                    ItemGUID = itemGUID
                };

                if (BLL.UserFavorite.Add(info) > 0)
                {
                    ajax.Status = 1;
                    ajax.Message = "加入收藏成功";
                }
                else
                {
                    ajax.Status = 0;
                    ajax.Message = "加入收藏失败";
                }
            }
            else
            {
                if (BLL.UserFavorite.Del(userInfo.UserID, itemGUID))
                {
                    ajax.Status = 1;
                    ajax.Message = "取消收藏成功";
                }
                else
                {
                    ajax.Status = 0;
                    ajax.Message = "取消收藏失败";
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