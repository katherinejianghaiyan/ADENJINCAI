using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.DAL
{
    public class Calendars
    {
        public List<CalendarsInfo> GetList(string buGUID, string siteGUID, DateTime today)
        {
            string sql = string.Format("SELECT ID,Name,ISNULL(BUGUID,'') BUGUID,ISNULL(SiteGUID,'') SiteGUID,StartDate,EndDate,Working FROM tblCalendars WHERE (ISNULL(BUGUID,'')= '' OR BUGUID = '{0}') AND (ISNULL(SiteGUID,'') = '' OR SiteGUID = '{1}') AND DATEDIFF(day,'{2}',StartDate) <= 1 AND DATEDIFF(day,'{2}',EndDate) >= 0 ", buGUID, siteGUID, today.ToString("yyyy-MM-dd"));
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CalendarsInfo>();
            }
        }

        public List<CalendarsInfo> GetList(string buGUID, string siteGUID, DateTime today, int nextDays)
        {
            return GetList(buGUID, siteGUID, today, today.AddDays(nextDays));
            /* steve.weng 2018-6-13
            string sql = string.Format("SELECT ID,Name,ISNULL(BUGUID,'') BUGUID,ISNULL(SiteGUID,'') SiteGUID,StartDate,EndDate," +
                "StartTime,EndTime,Working FROM tblCalendars where " +
                //"(ISNULL(BUGUID,'')= '' OR BUGUID = '{0}') AND (ISNULL(SiteGUID,'') = '' OR SiteGUID = '{1}') " +
                "ISNULL(BUGUID,'{0}')= '{0}' AND ISNULL(SiteGUID,'{1}') = '{1}' " + 
                "AND isnull(StartDate,'1900-1-1') <= '{3}' AND isnull(EndDate,'2222-12-12') >= '{2}'", 
                buGUID, siteGUID, today.ToString("yyyy-MM-dd"), today.AddDays(nextDays).ToString("yyyy-MM-dd"));
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CalendarsInfo>();
            }*/
        }

        public List<CalendarsInfo> GetList(string buGUID, string siteGUID, DateTime date1, DateTime date2)
        {
            string sql = string.Format("SELECT ID,Name,ISNULL(BUGUID,'') BUGUID,ISNULL(SiteGUID,'') SiteGUID,StartDate,EndDate," +
                "StartTime,EndTime,Working FROM tblCalendars where " +
                //"(ISNULL(BUGUID,'')= '' OR BUGUID = '{0}') AND (ISNULL(SiteGUID,'') = '' OR SiteGUID = '{1}') " +
                "ISNULL(BUGUID,'{0}')= '{0}' AND ISNULL(SiteGUID,'{1}') = '{1}' " +
                "AND isnull(StartDate,'1900-1-1') <= '{3}' AND isnull(EndDate,'2222-12-12') >= '{2}'",
                buGUID, siteGUID, date1.ToString("yyyy-MM-dd"), date2.ToString("yyyy-MM-dd"));
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CalendarsInfo>();
            }
        }

        /// <summary>
        /// 取得Calendar列表
        /// </summary>
        /// <param name="buGUID"></param>
        /// <param name="siteGUID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<CalendarsInfo> GetCalendar(string buGUID, string siteGUID)
        {
            string strSql = " SELECT ID " +
                                 " , Name " +
                                 " , ISNULL(BUGUID,'') BUGUID " +
                                 " , ISNULL(SiteGUID,'') SiteGUID " +
                                 " , StartDate " +
                                 " , EndDate " +
                                 " , Working " +
                              " FROM tblCalendars " +
                             " WHERE (ISNULL(BUGUID,'')= '' " +
                                    " OR BUGUID = '{0}') " +
                               " AND (ISNULL(SiteGUID,'') = '' " +
                                    " OR SiteGUID = '{1}') ";

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat(strSql, buGUID, siteGUID);

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sbSql.ToString()))
            {
                return reader.ToEntityList<CalendarsInfo>();
            }
        }
    }
}
