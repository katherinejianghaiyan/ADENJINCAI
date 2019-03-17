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
    public class Promotion
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PromotionInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblPromotion(");
            builder.Append("ID,GUID,SiteGUID,BUGUID,StartDate,EndDate,MinOrderAmt,MaxQty)");
            builder.Append("VALUES (");
            builder.Append("@ID,@GUID,@SiteGUID,@BUGUID,@StartDate,@EndDate,@MinOrderAmt,@MaxQty)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ID",SqlDbType.Int,4) {Value =  info.ID},
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32) {Value =  info.SiteGUID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32) {Value =  info.BUGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4) {Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4) {Value =  info.EndDate},
					 new SqlParameter("@MinOrderAmt",SqlDbType.Decimal,9) {Value =  info.MinOrderAmt},
					 new SqlParameter("@MaxQty",SqlDbType.Int,4) {Value =  info.MaxQty}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PromotionInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblPromotion SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("BUGUID=@BUGUID,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("MinOrderAmt=@MinOrderAmt,");
            builder.Append("MaxQty=@MaxQty ");
            builder.Append("WHERE ID=@ID ");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32){Value =  info.SiteGUID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32){Value =  info.BUGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4){Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4){Value =  info.EndDate},
					 new SqlParameter("@MinOrderAmt",SqlDbType.Decimal,9){Value =  info.MinOrderAmt},
					 new SqlParameter("@MaxQty",SqlDbType.Int,4){Value =  info.MaxQty},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        public PromotionInfo GetInfo(string buguid, decimal minOrderAmt)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,SiteGUID,BUGUID,StartDate,EndDate,MinOrderAmt,MaxQty FROM tblPromotion WHERE StartDate <= {0} AND EndDate >= {0} AND MinOrderAmt <= {1} AND BUGUID = '{2}' ORDER BY MinOrderAmt DESC", DateTime.Now.ToInt(), minOrderAmt, buguid);

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<PromotionInfo>();
            }
        }


        public List<PromotionInfo> GetList(string buguid)
        {
            string sql = string.Format("SELECT ID,GUID,SiteGUID,BUGUID,StartDate,EndDate,MinOrderAmt,MaxQty FROM tblPromotion WHERE StartDate <= {0} AND EndDate >= {0} AND BUGUID = '{1}' ORDER BY MinOrderAmt ASC", DateTime.Now.ToInt(), buguid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<PromotionInfo>();
            }
        }

    }
}

