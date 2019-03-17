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
    public class PurchaseOrder
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PurchaseOrderInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblPurchaseOrder(");
            builder.Append("OrderID,GUID,SiteGUID,OrderDate,OrderCode,SupplierGUID,RequiredDate,IsClosed)");
            builder.Append("VALUES (");
            builder.Append("@OrderID,@GUID,@SiteGUID,@OrderDate,@OrderCode,@SupplierGUID,@RequiredDate,@IsClosed)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@OrderID",SqlDbType.Int,4) {Value =  info.OrderID},
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32) {Value =  info.SiteGUID},
					 new SqlParameter("@OrderDate",SqlDbType.Int,4) {Value =  info.OrderDate},
					 new SqlParameter("@OrderCode",SqlDbType.VarChar,32) {Value =  info.OrderCode},
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32) {Value =  info.SupplierGUID},
					 new SqlParameter("@RequiredDate",SqlDbType.Int,4) {Value =  info.RequiredDate},
					 new SqlParameter("@IsClosed",SqlDbType.Bit,1) {Value =  info.IsClosed}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PurchaseOrderInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblPurchaseOrder SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("OrderDate=@OrderDate,");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("SupplierGUID=@SupplierGUID,");
            builder.Append("RequiredDate=@RequiredDate,");
            builder.Append("IsClosed=@IsClosed ");
            builder.Append("WHERE OrderID=@OrderID ");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32){Value =  info.SiteGUID},
					 new SqlParameter("@OrderDate",SqlDbType.Int,4){Value =  info.OrderDate},
					 new SqlParameter("@OrderCode",SqlDbType.VarChar,32){Value =  info.OrderCode},
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32){Value =  info.SupplierGUID},
					 new SqlParameter("@RequiredDate",SqlDbType.Int,4){Value =  info.RequiredDate},
					 new SqlParameter("@IsClosed",SqlDbType.Bit,1){Value =  info.IsClosed},
					 new SqlParameter("@OrderID",SqlDbType.Int,4){Value =  info.OrderID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        public List<PurchaseOrderInfo> GetList()
        {
            string sql = "SELECT OrderID,GUID,SiteGUID,OrderDate,OrderCode,SupplierGUID,RequiredDate,IsClosed FROM tblPurchaseOrder ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<PurchaseOrderInfo>();
            }
        }
    }
}

