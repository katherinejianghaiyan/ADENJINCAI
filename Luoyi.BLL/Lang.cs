using System;
using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Lang
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.Lang dal = new DAL.Lang();
        private static List<LangInfo> listLang = null;
        public static LangInfo GetInfo(string pageName, string controlID)
        {
            try
            {
                if (listLang == null)
                    listLang = dal.GetInfo();
                if (listLang == null) return null;

                var tmp = listLang.Where(q => pageName.ToLower().Trim() == q.PageName.ToLower().Trim()
                   && q.ControlID.ToLower().Trim() == controlID.ToLower().Trim()).ToList();

                if (!tmp.Any()) throw new Exception("No text");
                return tmp.FirstOrDefault();
                //return dal.GetInfo(pageName, controlID);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
