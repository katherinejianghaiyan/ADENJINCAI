using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.Web
{
    public class CalendarsHelper
    {
        public static void GetWorking(string siteGUID, string buGUID, out bool todayWorking, out bool tomorrowWorking)
        {
            todayWorking = false;
            tomorrowWorking = false;

            List<CalendarsInfo> listCaledars = BLL.Calendars.GetList(buGUID, siteGUID, DateTime.Now);

            Logger.Info(listCaledars.JSONSerialize());

            //今天
            var todayInfo = listCaledars.Find(c => c.SiteGUID.Trim() == siteGUID && c.StartDate.Date <= DateTime.Now.Date && c.EndDate.Date >= DateTime.Now.Date);
            //明天
            var tomorrowInfo = listCaledars.Find(c => c.SiteGUID.Trim() == siteGUID && c.StartDate.Date <= DateTime.Now.AddDays(1).Date && c.EndDate.Date >= DateTime.Now.AddDays(1).Date);

            if (todayInfo != null)
            {
                todayWorking = todayInfo.Working;
            }
            else
            {
                todayInfo = listCaledars.Find(c => c.BUGUID.Trim() == buGUID && c.StartDate.Date <= DateTime.Now.Date && c.EndDate.Date >= DateTime.Now.Date);

                if (todayInfo != null)
                {
                    todayWorking = todayInfo.Working;
                }
                else
                {
                    todayInfo = listCaledars.Find(c => c.BUGUID.Trim() == "" && c.SiteGUID.Trim() == "" && c.StartDate.Date <= DateTime.Now.Date && c.EndDate.Date >= DateTime.Now.Date);
                    if (todayInfo != null)
                    {
                        todayWorking = todayInfo.Working;
                    }
                    else
                    {
                        switch (DateTime.Now.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                            case DayOfWeek.Tuesday:
                            case DayOfWeek.Wednesday:
                            case DayOfWeek.Thursday:
                            case DayOfWeek.Friday:
                                //星期5
                                todayWorking = true;
                                break;
                            case DayOfWeek.Saturday:
                            case DayOfWeek.Sunday:
                                todayWorking = false;
                                //星期6,7
                                break;
                        }
                    }
                }
            }

            //明天
            if (tomorrowInfo != null)
            {
                tomorrowWorking = tomorrowInfo.Working;
            }
            else
            {
                tomorrowInfo = listCaledars.Find(c => c.BUGUID.Trim() == buGUID && c.StartDate.Date <= DateTime.Now.AddDays(1).Date && c.EndDate.Date >= DateTime.Now.AddDays(1).Date);
                if (tomorrowInfo != null)
                {
                    tomorrowWorking = tomorrowInfo.Working;
                }
                else
                {
                    tomorrowInfo = listCaledars.Find(c => c.BUGUID.Trim() == "" && c.SiteGUID.Trim() == "" && c.StartDate.Date <= DateTime.Now.AddDays(1).Date && c.EndDate.Date >= DateTime.Now.AddDays(1).Date);
                    if (tomorrowInfo != null)
                    {
                        tomorrowWorking = tomorrowInfo.Working;
                    }
                    else
                    {
                        switch (DateTime.Now.AddDays(1).DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                            case DayOfWeek.Tuesday:
                            case DayOfWeek.Wednesday:
                            case DayOfWeek.Thursday:
                            case DayOfWeek.Friday:
                                //星期5
                                tomorrowWorking = true;
                                break;
                            case DayOfWeek.Saturday:
                            case DayOfWeek.Sunday:
                                tomorrowWorking = false;
                                //星期6,7
                                break;
                        }
                    }
                }
            }

            Logger.Info("todayInfo:" + todayInfo.JSONSerialize());
            Logger.Info("tomorrowInfo:" + tomorrowInfo.JSONSerialize());

        }

        //Return:  true - 营业； false - 关门。 workingPeriod：下一段工作时间
        public static bool IsWorking( string siteGUID, string buGUID, out string thisWorkDate, out string nextWorkDate, out string workingPeriod)
        {
            thisWorkDate = "";
            nextWorkDate = "";
            workingPeriod = "";
            int nextDays = 10;
            DateTime date = DateTime.Now;

            List<CalendarsInfo> listCaledars = BLL.Calendars.GetList( buGUID, siteGUID, date, nextDays);

            Logger.Info(listCaledars.JSONSerialize());                     

            thisWorkDate = GetWorking(listCaledars, buGUID, siteGUID, date);

            for (int i = 0; i < nextDays; i++)
            {
                date = date.AddDays(1);
                nextWorkDate = GetWorking(listCaledars, buGUID, siteGUID, date);
                if (nextWorkDate != "") break;
            }

            var tmp = listCaledars.Where(q => q.StartDate == DateTime.Parse("0001-1-1") && q.EndDate == DateTime.Parse("0001-1-1"));
            //No working time limit
            if (!tmp.Any())
                return true;

            TimeSpan now = DateTime.Now.TimeOfDay;
            var t = tmp.OrderBy(q => q.EndTime.TimeOfDay >= now ? 1 : 2).ThenBy(q=>q.EndTime.TimeOfDay).FirstOrDefault();
            workingPeriod = string.Format("{0} ~ {1}", t.StartTime.ToString("h:m"), t.EndTime.ToString("h:m"));

            return tmp.Where(q => q.StartTime.TimeOfDay <= now && q.EndTime.TimeOfDay >= now).Any();
        }


        //工作日返回日期，非工作日返回“”；
        private static string GetWorking (List<CalendarsInfo> list, string buGUID, string siteGUID,DateTime date)
        {
            string workDate = "";
            date = date.Date;

            var dateInfo = list.Where(q=> q.StartDate != DateTime.Parse("0001-1-1") && q.EndDate != DateTime.Parse("0001-1-1") 
                && q.StartDate <= date && q.EndDate >= date).OrderByDescending(q => q.ID).ToList();

            //指定Site
            var tmpInfo = dateInfo.Where(q => q.SiteGUID.Trim() == siteGUID.Trim());
            if (tmpInfo.Any())
            {
                if (tmpInfo.First().Working) workDate = date.ToString("yyyy-MM-dd");
                return workDate;
            }

            //指定BU
            tmpInfo = dateInfo.Where(q => q.BUGUID.Trim() == buGUID.Trim());
            if (tmpInfo.Any())
            {
                if (tmpInfo.First().Working) workDate = date.ToString("yyyy-MM-dd");
                return workDate;
            }

            //全部
            tmpInfo = dateInfo.Where(q => q.BUGUID.Trim() == "" && q.SiteGUID.Trim() == "");
            if (tmpInfo.Any())
            {
                if (tmpInfo.First().Working) workDate = date.ToString("yyyy-MM-dd");
                return workDate;
            }

            //周一~五是工作日
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return workDate;

            workDate = date.ToString("yyyy-MM-dd");
            return workDate;
        }

        public static List<DateTime> GetWorkDates(string buGUID, string siteGUID, DateTime startDate, int workDays)
        {
            List<DateTime> dates = new List<DateTime>();
            if (workDays == 0) return dates;

            // 取得工作日列表
            List<CalendarsInfo> lstCalendar = BLL.Calendars.GetCalendar(buGUID, siteGUID);

            for (DateTime date = startDate; dates.Count < workDays; date = date.AddDays(1))
            {
                // 调用判断工作日方法
                if (GetWorking(lstCalendar, buGUID, siteGUID, date) == "") continue;

                dates.Add(date);
            }
            return dates;
        }
        //返回工作日    
        public static List<DateTime> GetWorkDates(string buGUID, string siteGUID, DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            // 日期范围不正确时返回0
            if (DateTime.Compare(startDate, endDate) > 0) return dates;

            // 取得工作日列表
            List<CalendarsInfo> lstCalendar = BLL.Calendars.GetCalendar(buGUID, siteGUID, startDate, endDate);  

            for(DateTime date = startDate; DateTime.Compare(date, endDate) <= 0; date= date.AddDays(1))
            {
                // 调用判断工作日方法
                if (GetWorking(lstCalendar, buGUID, siteGUID, date) == "") continue;
                dates.Add(date);
            }

            return dates;
        }


    }
}