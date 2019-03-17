using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.DAL
{
    public class Nutrition
    {
        /// <summary>
        /// 取得营养信息
        /// </summary>
        /// <returns></returns>
        public List<NutritionList> GetNutritionInfo(string langCode, string siteCode)
        {
            List<NutritionList> lstNutritionList = new List<NutritionList>();
            List<NutritionWeekDay> lstNutritionWeekDay = new List<NutritionWeekDay> ();
            List<NutritionInfo> lstNutritionInfo = new List<NutritionInfo>();
            #region Sql文
            string strSql = " SELECT ID " +
                                 " , BUSINESSTYPE " +
                                 " , DATATYPE " +
                                 " , LANGCODE " +
                                 " , VAL1 " +
                                 " , VAL2 " +
                                 " , VAL3 " +
                                 " , CONVERT(VARCHAR(10), STARTDATE, 23) AS STARTDATE " +
                                 " , CONVERT(VARCHAR(10), ENDDATE, 23) AS ENDDATE " +
                                 " , SORTNAME " +
                              " FROM TBLDATAS " +
                             " WHERE LANGCODE = '{0}' " +
                               " AND BUSINESSTYPE = '{1}' " +
                               " AND ((STARTDATE<= '{2}' AND ENDDATE>='{2}') " +
                               " OR (STARTDATE<= '{3}' AND ENDDATE>='{3}'))";
            strSql = string.Format(strSql, langCode.Replace("_",""), siteCode, 
                DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"));
            #endregion

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, strSql))
            {
                lstNutritionInfo = reader.ToEntityList<NutritionInfo>();
            }

            if (lstNutritionInfo == null || !lstNutritionInfo.Any())
                return lstNutritionList;

            var lg = (from nutritionInfo in lstNutritionInfo
                      group nutritionInfo by new
                      {
                          // DataType
                          dataType = nutritionInfo.dataType,
                          // 开始日期
                          startDate = nutritionInfo.startDate,
                          // 结束日期
                          endDate = nutritionInfo.endDate
                      }).ToList();

            NutritionList nObj = new NutritionList();

            // 生成用于显示的新数组
            foreach (var lq in lg)
            {
                nObj = new NutritionList();
                nObj.dataType = lq.Key.dataType;
                nObj.startDate = lq.Key.startDate;
                nObj.endDate = lq.Key.endDate;

                // 按日分组(val3)
                var lqWeek = (from nutritionInfo in lq
                              group nutritionInfo by new
                              {
                                  // 星期X
                                  day = nutritionInfo.val3
                              }).ToList();

                NutritionWeekDay dayly = new NutritionWeekDay();
                lstNutritionWeekDay = new List<NutritionWeekDay>();
                List<NutritionInfo> lstTemp = new List<NutritionInfo>();
                // 生成用于显示的新数组
                foreach (var gDay in lqWeek)
                {
                    dayly = new NutritionWeekDay();
                    // 星期X
                    dayly.day = gDay.Key.day;
                    // 营养信息（按SortName排序）
                    dayly.lstNutritionInfo = gDay.OrderBy(r => r.sortName).ToList();

                    if (string.IsNullOrEmpty(dayly.day))
                    {
                        lstTemp = dayly.lstNutritionInfo.Where(r => "img".Equals(r.val1)).ToList();
                        if (lstTemp != null && lstTemp.Any())
                            nObj.img = lstTemp.FirstOrDefault().val2;
                    }

                    lstNutritionWeekDay.Add(dayly);
                }
                // 排序：Day正序
                nObj.lstNutritionWeekDay = lstNutritionWeekDay.OrderBy(r => r.day).ToList();

                lstNutritionList.Add(nObj);
            }
            lstNutritionList = lstNutritionList.OrderBy(r => r.dataType).ThenBy(r=>r.startDate).ToList();

            return lstNutritionList;
        }

        public List<MealDateList> GetDishInfoMenu(string langCode, string siteCode)
        {
            List<MealDateList> lstNutritionList = new List<MealDateList>();
            List<MealTypeList> mealTypeList = new List<MealTypeList>();
            List<NutritionInfo> lstNutritionInfo = new List<NutritionInfo>();

            #region noUser
            //string strSql = "select * from(select distinct A.ID, A.BUSINESSTYPE, A.DATATYPE, A.LANGCODE, A.VAL1, A.VAL2, A.VAL3, STARTDATE, ENDDATE, "
            //    + "A.windowName, A.ProductCode, A.ProductDesc, isnull(C.className_ZH, '其他') as className "
            //    + "from (select * from (select distinct a1.ItemCode,a1.ID, a1.CostCenterCode BUSINESSTYPE, 'SUZ' as DATATYPE, 'ENUS' LANGCODE, 'Name' VAL1, "
            //    + "isnull(a1.ItemName_EN, '') VAL2, cast((datePart(weekday, a1.RequiredDate) - 1) as varchar(1)) VAL3, "
            //    + "cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 0) as varchar) STARTDATE, cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 6) as varchar) ENDDATE "
            //    + ",a2.productDesc,a2.ProductCode,a3.windowName "
            //    + "from MenuOrderHead a1,SalesOrderItem a2,CCWindowMeals a3 where isnull(a1.ItemName_EN, '') <> '' and a1.DeleteTime IS NULL "
            //    + "and a1.SOItemGUID=a2.ItemGuid and a3.WindowGUID=a1.WindowGUID "
            //    + "union "
            //    + "select distinct a1.ItemCode,a1.ID, a1.CostCenterCode BUSINESSTYPE, 'SUZ' as DATATYPE, 'ZHCN' LANGCODE, 'Name' VAL1, "
            //    + "isnull(a1.ItemName_ZH, '') VAL2, cast((datePart(weekday, a1.RequiredDate) - 1) as varchar(1)) VAL3, "
            //    + "cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 0) as varchar) STARTDATE, cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 6) as varchar) ENDDATE "
            //    + ",a2.productDesc,a2.ProductCode,a3.windowName "
            //    + "from MenuOrderHead a1,SalesOrderItem a2,CCWindowMeals a3 where isnull(a1.ItemName_ZH, '') <> '' and a1.DeleteTime IS NULL "
            //    + "and a1.SOItemGUID=a2.ItemGuid and a3.WindowGUID=a1.WindowGUID) A "
            //    + "where A.LANGCODE = '{0}' and A.BUSINESSTYPE = '{1}' and ((convert(varchar(10),cast(A.STARTDATE as datetime),23) <= '{2}' and convert(varchar(10),cast(A.ENDDATE as datetime),23) >= '{2}') "
            //    + "or (convert(varchar(10),cast(A.STARTDATE as datetime),23) <= '{3}' and convert(varchar(10),cast(A.ENDDATE as datetime),23) >= '{3}'))) A "
            //    + "left join tblItem B on A.ItemCode = B.ItemCode left join tblItemClass C on C.guid = B.CategoriesClassGUID )V "
            //    + "order by V.STARTDATE,V.VAL3,V.productCode,V.windowName ";
            #endregion

            DateTime date1 = DateTime.Now;
            date1 = date1.AddDays(-1 * date1.DayOfWeek.ToInt());
            DateTime date2 = date1.AddDays(8);
            string strSql = "select distinct isnull(a1.ItemName_ZH, '') val2,CAST(a1.RequiredDate AS varchar) startdate,a2.productDesc va11, "
                + "a2.ProductCode val3,a3.windowName,case when a6.pguid is null then a5.guid else a6.guid end sortname "
                + "from MenuOrderHead (nolock) a1 join SalesOrderItem (nolock) a2 on a1.SOItemGUID = a2.ItemGuid "
                + "join CCWindowMeals (nolock) a3 on a3.WindowGUID = a1.WindowGUID left "
                + "join tblitem (nolock) a4 on a1.ItemGUID = a4.GUID left join tblItemClass (nolock) a5 on a5.guid = a4.CategoriesClassGUID "
                + "left join tblItemClass (nolock) a6 on a6.guid=a5.pguid "
                + "where isnull(a1.ItemName_ZH, '') <> '' and a1.DeleteTime IS NULL "
                + "and a1.SOItemGUID = a2.ItemGuid and a3.WindowGUID = a1.WindowGUID and a1.costcentercode = '{0}' "
                + " and ((a1.requireddate>'{1}' and a1.requireddate <'{2}') or (a1.requireddate>='2018-10-1' and a1.requireddate <='2018-10-7'))"
                ;
                //+ "and ((cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 0) as datetime) <= '{1}' "
                //+ "and cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 6) as datetime) >= '{1}') "
                //+ "or ((cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 0) as datetime) <= '{2}' and cast(DATEADD(week, DATEDIFF(week, 0, a1.RequiredDate), 6) as datetime) >= '{2}')))";

            strSql = string.Format(strSql, date1.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"));

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden2, CommandType.Text, strSql))
            {
                lstNutritionInfo = reader.ToEntityList<NutritionInfo>();
            }

            if (lstNutritionInfo == null || !lstNutritionInfo.Any())
                return lstNutritionList;

            if (lstNutritionInfo.Where(q => DateTime.Parse(q.startDate) > date1 && DateTime.Parse(q.startDate) < date2).Any())
                lstNutritionInfo.RemoveAll(q => DateTime.Parse(q.startDate) < date1);

            var lg = (from nutritionInfo in lstNutritionInfo
                      group nutritionInfo by new
                      {
                          startDate = nutritionInfo.startDate,
                          day = (nutritionInfo.startDate.ToDateTime()).DayOfWeek,
                      }).Select(dr => new MealDateList() {
                          startDate = dr.Key.startDate,
                          day = dr.Key.day.ToInt().ToString(),
                          MealType = (from mealtype in dr
                                      group mealtype by new
                                      {
                                          dinner = mealtype.val3
                                      }).Select(m => new MealTypeList() {
                                          dinner = m.Key.dinner,
                                          Windowslines = (from wd in m
                                                          group wd by new
                                                          {
                                                              window = wd.windowName
                                                          }).Select(itm => new NutritionInfo() {
                                                              windowName=itm.Key.window,
                                                              val2= string.Join("<br />", itm.GroupBy(i=>i.sortName)
                                                                .Select(i=> string.Join("  ", i.Select(j=>j.val2).ToArray()) ).ToArray())// itm.GroupBy(i=>i.sortName).Select(i=>string.Join(",", i.Select(j => j.val2).ToArray())) )
                                                          }).OrderBy(itm=>itm.windowName).ToList()

                                    }).OrderBy(m=>m.dinner).ToList()
                      }).OrderBy(dr=>dr.day).ToList();

            MealDateList nObj = new MealDateList();
            foreach (var g in lg)
            {
                nObj = new MealDateList();
                nObj.startDate = g.startDate;
                nObj.day = g.day;

                nObj.MealType = g.MealType;
                lstNutritionList.Add(nObj);
            }

            #region reference
            //var tmp = lstNutritionInfo.GroupBy(q => q.startDate).Select(q => new
            //{
            //    startdate = q.Key,
            //    datelines = q.GroupBy(p => p.val3).Select(p => new {
            //        dinner = p.Key,
            //        windowslines = p.GroupBy(y => y.windowName).Select(y => new
            //        {
            //            window = y.Key,
            //            item = string.Join(", ", y.Select(z=>z.val2).ToArray())
            //        }).ToList()
            //    }).ToList()
            //}).ToList();
            #endregion

            return lstNutritionList.ToList();
           
        }

        public List<MealDateList> GetDishInfo(string langCode, string siteGuid)
        {
            List<MealDateList> lstNutritionList = new List<MealDateList>();
            List<MealTypeList> mealTypeList = new List<MealTypeList>();
            List<NutritionInfo> lstNutritionInfo = new List<NutritionInfo>();


            DateTime date1 = DateTime.Now;
            date1 = date1.AddDays(-1 * (date1.DayOfWeek.ToInt() == 0 ? 7 : date1.DayOfWeek.ToInt()));
            DateTime date2 = date1.AddDays(14);
            string strSql = "select ISNULL(FoodNames{3},FOODNAMES) val2,Convert(varchar(10),MealDate,23) startdate,MealType val1, "
                + "MEALTYPE val3,ISNULL(windowType{3},WINDOWTYPE) windowName,cast(id as varchar) sortname,CreateUser from SUZCATMenu (nolock) "
                + "where DeleteTime IS NULL "
                + " and MealDate>'{0}' and MealDate <'{1}' "
                + " and siteGuid='{2}' "
                + " order by mealdate,mealcode,sort";

            strSql = string.Format(strSql, date1.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"),siteGuid,
                langCode=="ZH_CN"?"":"_EN");

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden2, CommandType.Text, strSql))
            {
                lstNutritionInfo = reader.ToEntityList<NutritionInfo>();
            }

            if (lstNutritionInfo == null || !lstNutritionInfo.Any())
                return lstNutritionList;

            var lg = (from nutritionInfo in lstNutritionInfo
                      group nutritionInfo by new
                      {
                          startDate = nutritionInfo.startDate,
                          day = (nutritionInfo.startDate.ToDateTime()).DayOfWeek,
                      }).Select(dr => new MealDateList()
                      {                          
                          startDate = dr.Key.startDate,
                          day = dr.Key.day.ToInt().ToString(),
                          MealType = (from mealtype in dr
                                      group mealtype by new
                                      {
                                          dinner = mealtype.val3
                                      }).Select(m => new MealTypeList()
                                      {
                                          dinner = m.Key.dinner,
                                          Windowslines = (from wd in m
                                                          group wd by new
                                                          {
                                                              window = wd.windowName
                                                          }).Select(itm => new NutritionInfo()
                                                          {
                                                              windowName = itm.Key.window,
                                                              val2 = string.Join("<br />", itm.GroupBy(i => i.sortName)
                                                                .Select(i => string.Join("  ", i.Select(j => j.val2).ToArray())).ToArray())// itm.GroupBy(i=>i.sortName).Select(i=>string.Join(",", i.Select(j => j.val2).ToArray())) )
                                                          }).ToList()//.OrderBy(itm => itm.windowName).ToList()

                                      }).ToList()//.OrderBy(m => m.dinner).ToList()
                      }).ToList();//.OrderBy(dr => dr.day).ToList();


            return lg;

        }
    }
}
