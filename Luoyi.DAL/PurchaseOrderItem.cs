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
    public class PurchaseOrderItem
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PurchaseOrderItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblPurchaseOrderItem(");
            builder.Append("GUID,POGUID,ItemGUID,Price,Qty,UOMID,RequiredDate,CreateTime)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@POGUID,@ItemGUID,@Price,@Qty,@UOMID,@RequiredDate,@CreateTime)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@POGUID",SqlDbType.VarChar,32) {Value =  info.POGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID},
					 new SqlParameter("@Price",SqlDbType.Decimal,9) {Value =  info.Price},
					 new SqlParameter("@Qty",SqlDbType.Decimal,9) {Value =  info.Qty},
					 new SqlParameter("@UOMID",SqlDbType.VarChar,16) {Value =  info.UOMID},
					 new SqlParameter("@RequiredDate",SqlDbType.Int,4) {Value =  info.RequiredDate},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PurchaseOrderItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblPurchaseOrderItem SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("POGUID=@POGUID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("Price=@Price,");
            builder.Append("Qty=@Qty,");
            builder.Append("UOMID=@UOMID,");
            builder.Append("RequiredDate=@RequiredDate,");
            builder.Append("CreateTime=@CreateTime ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@POGUID",SqlDbType.VarChar,32){Value =  info.POGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@Price",SqlDbType.Decimal,9){Value =  info.Price},
					 new SqlParameter("@Qty",SqlDbType.Decimal,9){Value =  info.Qty},
					 new SqlParameter("@UOMID",SqlDbType.VarChar,16){Value =  info.UOMID},
					 new SqlParameter("@RequiredDate",SqlDbType.Int,4){Value =  info.RequiredDate},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime){Value =  info.CreateTime},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblPurchaseOrderItem WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PurchaseOrderItemInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,POGUID,ItemGUID,Price,Qty,UOMID,RequiredDate,CreateTime FROM tblPurchaseOrderItem WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<PurchaseOrderItemInfo>();
            }
        }


        public List<PurchaseOrderItemInfo> GetList(string filter)
        {
            string sql = string.Format("SELECT i.*,o.OrderID,o.SiteGUID,o.OrderDate,o.OrderCode,o.SupplierGUID FROM tblPurchaseOrderItem AS i LEFT JOIN tblPurchaseOrder AS o ON i.POGUID=o.GUID {0}", filter);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<PurchaseOrderItemInfo>();
            }
        }
    }
}

