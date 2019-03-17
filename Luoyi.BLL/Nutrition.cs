using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Nutrition
    {
        private readonly static DAL.Nutrition dal = new DAL.Nutrition();

        /// <summary>
        /// 取得营养信息
        /// </summary>
        /// <returns></returns>
        public static List<NutritionList> GetNutritionInfo(string langCode, string siteCode)
        {
            return dal.GetNutritionInfo(langCode, siteCode);
        }

        public static List<MealDateList> GetDishInfoMenu(string langCode, string siteCode)
        {
            return dal.GetDishInfoMenu(langCode, siteCode);
        }

        public static List<MealDateList> GetDishInfo(string langCode, string siteGuid)
        {
            return dal.GetDishInfo(langCode, siteGuid);
        }
    }
}
