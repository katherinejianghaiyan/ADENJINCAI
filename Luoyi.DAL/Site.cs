using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class Site
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SiteInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSite(");
            builder.Append("GUID,Code,CompNameCn,CompNameEn,BUGUID,Address,PostCode,TelNbr)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@Code,@CompNameCn,@CompNameEn,@BUGUID,@Address,@PostCode,@TelNbr)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
            {
                     new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
                     new SqlParameter("@Code",SqlDbType.VarChar,16) {Value =  info.Code},
                     new SqlParameter("@CompNameCn",SqlDbType.VarChar,32) {Value =  info.CompNameCn},
                     new SqlParameter("@CompNameEn",SqlDbType.VarChar,64) {Value =  info.CompNameEn},
                     new SqlParameter("@BUGUID",SqlDbType.Int,4) {Value =  info.BUGUID},
                     new SqlParameter("@Address",SqlDbType.VarChar,64) {Value =  info.Address},
                     new SqlParameter("@PostCode",SqlDbType.VarChar,8) {Value =  info.PostCode},
                     new SqlParameter("@TelNbr",SqlDbType.VarChar,32) {Value =  info.TelNbr}
            };

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SiteInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSite SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("Code=@Code,");
            builder.Append("CompNameCn=@CompNameCn,");
            builder.Append("CompNameEn=@CompNameEn,");
            builder.Append("BUGUID=@BUGUID,");
            builder.Append("Address=@Address,");
            builder.Append("PostCode=@PostCode,");
            builder.Append("TelNbr=@TelNbr ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
            {
                     new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
                     new SqlParameter("@Code",SqlDbType.VarChar,16){Value =  info.Code},
                     new SqlParameter("@CompNameCn",SqlDbType.VarChar,32){Value =  info.CompNameCn},
                     new SqlParameter("@CompNameEn",SqlDbType.VarChar,64){Value =  info.CompNameEn},
                     new SqlParameter("@BUGUID",SqlDbType.Int,4){Value =  info.BUGUID},
                     new SqlParameter("@Address",SqlDbType.VarChar,64){Value =  info.Address},
                     new SqlParameter("@PostCode",SqlDbType.VarChar,8){Value =  info.PostCode},
                     new SqlParameter("@TelNbr",SqlDbType.VarChar,32){Value =  info.TelNbr},
                     new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
            };

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblSite WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SiteInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,Code,CompNameCn,CompNameEn,BUGUID,Address,PostCode,TelNbr FROM tblSite WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SiteInfo>();
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SiteInfo GetInfo(string guid, string lang)
        {
            lang = lang.Replace("_", "");
            string sql = "SELECT TOP 1 a1.ID,GUID,a1.Code,a1.CompNameCn,a1.CompNameEn,a1.BUGUID,a1.Address,a1.PostCode,a1.TelNbr,isnull("
                + "(select top 1 convert(varchar(5), endtime, 8) from tblCalendars "
                + "where siteguid = '{0}' and startdate is null and enddate is null and "
                + "convert(varchar(5), starttime, 8) <= convert(varchar(5), getdate(), 8)"
                + "and convert(varchar(5), endtime, 8) >= convert(varchar(5), getdate(), 8))"

              + ",a1.EndHour) EndHour,a1.DeliveryDays,isnull(a1.LaunchDate,getdate()) LaunchDate,a1.IsPaging,isnull(a1.PickupTime,'') PickupTime,isnull(a1.WelcomeMsgCN,'') WelcomeMsgCN ,"
              + "isnull(a1.WelcomeMsgEN,'') WelcomeMsgEN,a1.paymentmethod,a1.paymentcomments,a1.DailyMaxOrderQty,a1.ShowDays,a1.candelorder,a1.showprice "
              + ",isnull(needshiptoaddr,0) needshiptoaddr,isnull(needwork,0) needwork,isnull(showproductinfoindetail,1) showproductinfoindetail," +
              "isnull(loadimg,'') loadimg,isnull(barcodeofso,'') barcodeofso,loadpages "
              + "FROM tblSite a1 "//left join tblusertypemast a2 on a1.usertype=a2.usertype "
              + "WHERE a1.GUID = '{0}'";

            sql = string.Format(sql, guid);
            SiteInfo data = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                data = reader.ToEntity<SiteInfo>();
            }

            sql = "select name{1} name,starttime,endtime,timestep from tbldeliverytimes where siteguid='{0}'" +
                " order by convert(varchar(5),starttime,24),sortname";
            sql = string.Format(sql, guid, lang);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return data;
                data.DeliveryTimes = reader.ToEntityList<DeliveryTime>();
            }

            return data;
        }

        public List<SiteInfo> GetList()
        {
            string sql = "SELECT ID,GUID,Code,CompNameCn,CompNameEn,BUGUID,Address,PostCode,TelNbr FROM tblSite ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SiteInfo>();
            }
        }

        /// <summary>
        /// 根据SiteGuid取得地址信息
        /// </summary>
        /// <param name="siteGuid"></param>
        /// <returns></returns>
        public List<SiteAddrs> GetAddrs(string siteGuid)
        {
            string strSql = " SELECT ID " +
                                 " , SITEGUID " +
                                 " , SHIPTOADDR1ZHCN " +
                                 " , SHIPTOADDR2ZHCN " +
                                 " , SHIPTOADDR3ZHCN " +
                                 " , SHIPTOADDR1ENUS " +
                                 " , SHIPTOADDR2ENUS " +
                                 " , SHIPTOADDR3ENUS " +
                                 " , SORTNAME " +
                                 " , ACTIVE " +
                              " FROM TBLSITEADDRS " +
                             " WHERE SITEGUID = '{0}' ";

            strSql = string.Format(strSql, siteGuid);

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, strSql))
            {
                return reader.ToEntityList<SiteAddrs>();
            }
        }
    }
}