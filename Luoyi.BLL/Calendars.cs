using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Calendars
    {
        private readonly static DAL.Calendars dal = new DAL.Calendars();

        public static List<CalendarsInfo> GetList(string buGUID, string siteGUID, DateTime today)
        {
            return dal.GetList(buGUID, siteGUID, today) ??  new List<CalendarsInfo>();
        }

        public static List<CalendarsInfo> GetList(string buGUID, string siteGUID, DateTime today, int nextDays)
        {
            return dal.GetList( buGUID, siteGUID, today, nextDays) ?? new List<CalendarsInfo>();
        }

        public static List<CalendarsInfo> GetCalendar(string buGUID, string siteGUID, DateTime startDate, DateTime endDate)
        {
            return dal.GetList(buGUID, siteGUID, startDate, endDate) ?? new List<CalendarsInfo>();
        }

        public static List<CalendarsInfo> GetCalendar(string buGUID, string siteGUID)
        {
            return dal.GetCalendar(buGUID, siteGUID) ?? new List<CalendarsInfo>();
        }
    }
}
