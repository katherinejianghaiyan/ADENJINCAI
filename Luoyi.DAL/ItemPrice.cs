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
    public class ItemPrice
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ItemPriceInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblItemPrice(");
            builder.Append("ItemGUID,BUGUID,StartDate,EndDate,Price,PriceType)");
            builder.Append("VALUES (");
            builder.Append("@ItemGUID,@BUGUID,@StartDate,@EndDate,@Price,@PriceType)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32) {Value =  info.BUGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4) {Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4) {Value =  info.EndDate},
					 new SqlParameter("@Price",SqlDbType.Int,4) {Value =  info.Price},
					 new SqlParameter("@PriceType",SqlDbType.Int,4) {Value =  info.PriceType}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ItemPriceInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblItemPrice SET ");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("BUGUID=@BUGUID,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("Price=@Price,");
            builder.Append("PriceType=@PriceType ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32){Value =  info.BUGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4){Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4){Value =  info.EndDate},
					 new SqlParameter("@Price",SqlDbType.Int,4){Value =  info.Price},
					 new SqlParameter("@PriceType",SqlDbType.Int,4){Value =  info.PriceType},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblItemPrice WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemPriceInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 * FROM tblItemPrice WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemPriceInfo>();
            }
        }

        public ItemPriceInfo GetInfo(string siteGUID, string itemGUID,  int startDate, int endDate, string priceType)
        {
            string sql = "select top 1 a1.* from tblitemprice a1,tblsite a2 where a1.itemguid=@ItemGUID and a2.guid=@SiteGUID "
                + "and isnull(case a2.itempriceinbu when 1 then a1.BUGUID else a1.siteguid end,'') = case a2.itempriceinbu when 1 then a2.BUGUID else a2.guid end "
                + "AND StartDate <=@StartDate AND isnull(EndDate,22221231)>=@EndDate AND PriceType=@PriceType ORDER BY ID DESC";
                //"SELECT TOP 1 * FROM tblItemPrice WHERE ItemGUID=@ItemGUID AND StartDate <=@StartDate AND (EndDate>=@EndDate OR EndDate IS NULL) AND PriceType=@PriceType ORDER BY ID DESC";

            SqlParameter[] parameter = {
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36){Value =  itemGUID},
                     new SqlParameter("@SiteGUID",SqlDbType.VarChar,36){Value =  siteGUID},
                     new SqlParameter("@StartDate",SqlDbType.Int,4){Value =  startDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4){Value =  endDate},
					 new SqlParameter("@PriceType",SqlDbType.VarChar,10){Value =  priceType}
			};

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql, parameter))
            {
                return reader.ToEntity<ItemPriceInfo>();
            }
        }

        public List<ItemPriceInfo> GetList()
        {
            string sql = "SELECT * FROM tblItemPrice ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<ItemPriceInfo>();
            }
        }
    }
}

