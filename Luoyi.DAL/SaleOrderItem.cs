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
    public class SaleOrderItem
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SaleOrderItemInfo info, SqlTransaction trans)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSaleOrderItem(");
            builder.Append("GUID,UserID,SOGUID,ItemGUID,UOMGUID,Qty,Price,CreateTime,ShippingStatus,ShippedDate,IsComment,IsPrint,PromotionGUID)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@UserID,@SOGUID,@ItemGUID,@UOMGUID,@Qty,@Price,@CreateTime,@ShippingStatus,@ShippedDate,@IsComment,@IsPrint,@PromotionGUID)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,36) {Value =  info.GUID},
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@SOGUID",SqlDbType.VarChar,36) {Value =  info.SOGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36) {Value =  info.ItemGUID},
					 new SqlParameter("@UOMGUID",SqlDbType.VarChar,36) {Value =  info.UOMGUID},
					 new SqlParameter("@Qty",SqlDbType.Int,4) {Value =  info.Qty},
					 new SqlParameter("@Price",SqlDbType.Decimal,9) {Value =  info.Price},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime},
					 new SqlParameter("@ShippingStatus",SqlDbType.Int,4) {Value =  info.ShippingStatus},
					 new SqlParameter("@ShippedDate",SqlDbType.Int,4) {Value =  info.ShippedDate},
					 new SqlParameter("@IsComment",SqlDbType.Bit,1) {Value =  info.IsComment},
					 new SqlParameter("@IsPrint",SqlDbType.Bit,1) {Value =  info.IsPrint},
					 new SqlParameter("@PromotionGUID",SqlDbType.VarChar,36) {Value =  info.PromotionGUID ?? ""}
			};

            object obj = null;

            if (trans != null)
            {
                obj = SqlHelper.ExecuteScalar(trans, CommandType.Text, builder.ToString(), lstParameters.ToArray());

            }
            else
            {
                obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            }

            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SaleOrderItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSaleOrderItem SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("UserID=@UserID,");
            builder.Append("SOGUID=@SOGUID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("UOMGUID=@UOMGUID,");
            builder.Append("Qty=@Qty,");
            builder.Append("Price=@Price,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("ShippingStatus=@ShippingStatus,");
            builder.Append("ShippedDate=@ShippedDate,");
            builder.Append("IsComment=@IsComment,");
            builder.Append("IsPrint=@IsPrint ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,36){Value =  info.GUID},
					 new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID},
					 new SqlParameter("@SOGUID",SqlDbType.VarChar,36){Value =  info.SOGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36){Value =  info.ItemGUID},
					 new SqlParameter("@UOMGUID",SqlDbType.VarChar,36){Value =  info.UOMGUID},
					 new SqlParameter("@Qty",SqlDbType.Int,4){Value =  info.Qty},
					 new SqlParameter("@Price",SqlDbType.Decimal,9){Value =  info.Price},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime){Value =  info.CreateTime},
					 new SqlParameter("@ShippingStatus",SqlDbType.Int,4){Value =  info.ShippingStatus},
					 new SqlParameter("@ShippedDate",SqlDbType.Int,4){Value =  info.ShippedDate},
					 new SqlParameter("@IsComment",SqlDbType.Bit,1){Value =  info.IsComment},
					 new SqlParameter("@IsPrint",SqlDbType.Bit,1){Value =  info.IsPrint},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblSaleOrderItem WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SaleOrderItemInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,UserID,SOGUID,ItemGUID,UOMGUID,Qty,Price,CreateTime,ShippingStatus,ShippedDate,IsComment,IsPrint FROM tblSaleOrderItem WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SaleOrderItemInfo>();
            }
        }


        public List<SaleOrderItemInfo> GetList(string filter)
        {
            string sql = string.Format("SELECT i.*,i.Qty*i.Price AS Amount FROM tblSaleOrderItem AS i JOIN tblSaleOrder AS o ON i.SOGUID=o.GUID LEFT JOIN tblUser AS u ON i.UserID = u.UserID WHERE o.IsDel = 0 {0}", filter);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SaleOrderItemInfo>();
            }
        }

        public DataTable GetTable(string filter, string language)
        {
            language = language.Replace("_", "");
            string sql = string.Format("SELECT o.OrderID,o.RequiredDate,s.ID,s.GUID,s.UserID,s.SOGUID,s.ItemGUID,s.UOMGUID,s.Qty,s.Price,s.CreateTime,s.ShippingStatus,s.ShippedDate,s.IsComment,s.IsPrint,s.Qty*s.Price AS Amount,case isnull(i.ItemName{1},'') when '' then i.itemname else i.itemname{1} end itemname,i.ItemCode,i.Tips,t.Qty AS StockQty,e.CompNameCn,e.CompNameEn FROM tblSaleOrderItem AS s LEFT JOIN tblSaleOrder AS o ON s.SOGUID=o.GUID LEFT JOIN tblItem AS i ON s.ItemGUID = i.GUID LEFT JOIN tblStockTransaction AS t ON s.GUID = t.SODetailGUID LEFT JOIN tblSite AS e ON e.GUID = o.SiteGUID WHERE o.IsDel = 0 {0}", 
                filter,language);
            return SqlHelper.ExecuteDataSet(SqlHelper.dbAden, CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool UpdateIsComment(int id, bool isComment)
        {
            string sql = string.Format("UPDATE tblSaleOrderItem SET IsComment={1} WHERE ID = {0}", id, isComment ? 1 : 0);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        public bool UpdateIsPrint(int id, bool isPrint)
        {
            string sql = string.Format("UPDATE tblSaleOrderItem SET IsPrint={1} WHERE ID = {0}", id, isPrint ? 1 : 0);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

    }
}

