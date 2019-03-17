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
    public class SupplierSite
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SupplierSiteInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSupplierSite(");
            builder.Append("SupplierGUID,BUGUID,SiteGUID,StartDate,EndDate)");
            builder.Append("VALUES (");
            builder.Append("@SupplierGUID,@BUGUID,@SiteGUID,@StartDate,@EndDate)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32) {Value =  info.SupplierGUID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32) {Value =  info.BUGUID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32) {Value =  info.SiteGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4) {Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4) {Value =  info.EndDate}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SupplierSiteInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSupplierSite SET ");
            builder.Append("SupplierGUID=@SupplierGUID,");
            builder.Append("BUGUID=@BUGUID,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32){Value =  info.SupplierGUID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32){Value =  info.BUGUID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32){Value =  info.SiteGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4){Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4){Value =  info.EndDate},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblSupplierSite WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SupplierSiteInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,SupplierGUID,BUGUID,SiteGUID,StartDate,EndDate FROM tblSupplierSite WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SupplierSiteInfo>();
            }
        }


        public List<SupplierSiteInfo> GetList()
        {
            string sql = "SELECT ID,SupplierGUID,BUGUID,SiteGUID,StartDate,EndDate FROM tblSupplierSite ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SupplierSiteInfo>();
            }
        }
    }
}

