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
    public class ItemSupplier
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ItemSupplierInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblItemSupplier(");
            builder.Append("ItemGUID,SupplierGUID,StartDate,EndDate,Price)");
            builder.Append("VALUES (");
            builder.Append("@ItemGUID,@SupplierGUID,@StartDate,@EndDate,@Price)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID},
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32) {Value =  info.SupplierGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4) {Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4) {Value =  info.EndDate},
					 new SqlParameter("@Price",SqlDbType.Decimal,5) {Value =  info.Price}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ItemSupplierInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblItemSupplier SET ");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("SupplierGUID=@SupplierGUID,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("Price=@Price ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32){Value =  info.SupplierGUID},
					 new SqlParameter("@StartDate",SqlDbType.Int,4){Value =  info.StartDate},
					 new SqlParameter("@EndDate",SqlDbType.Int,4){Value =  info.EndDate},
					 new SqlParameter("@Price",SqlDbType.Decimal,5){Value =  info.Price},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblItemSupplier WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemSupplierInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,ItemGUID,SupplierGUID,StartDate,EndDate,Price FROM tblItemSupplier WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemSupplierInfo>();
            }
        }


        public List<ItemSupplierInfo> GetList()
        {
            string sql = "SELECT ID,ItemGUID,SupplierGUID,StartDate,EndDate,Price FROM tblItemSupplier ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<ItemSupplierInfo>();
            }
        }
    }
}

