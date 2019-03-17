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
    public class SaleOrderCart
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SaleOrderCartInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSaleOrderCart(");
            builder.Append("GUID,UserID,ItemGUID,UOMGUID,Qty,Price,CreateTime,Expire,IsBuy,PromotionGuid)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@UserID,@ItemGUID,@UOMGUID,@Qty,@Price,@CreateTime,@Expire,@IsBuy,@PromotionGuid)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,36) {Value =  info.GUID ?? ""},
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36) {Value =  info.ItemGUID ?? ""},
					 new SqlParameter("@UOMGUID",SqlDbType.VarChar,36) {Value =  info.UOMGUID ?? ""},
					 new SqlParameter("@Qty",SqlDbType.Int,4) {Value =  info.Qty},
					 new SqlParameter("@Price",SqlDbType.Decimal,9) {Value =  info.Price},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime},
					 new SqlParameter("@Expire",SqlDbType.DateTime) {Value =  info.Expire},
					 new SqlParameter("@IsBuy",SqlDbType.Bit) {Value =  info.IsBuy},
					 new SqlParameter("@PromotionGuid",SqlDbType.VarChar,36) {Value =  info.PromotionGuid ?? ""}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SaleOrderCartInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSaleOrderCart SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("UserID=@UserID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("UOMGUID=@UOMGUID,");
            builder.Append("Qty=@Qty,");
            builder.Append("Price=@Price ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,36){Value =  info.GUID},
					 new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36){Value =  info.ItemGUID},
					 new SqlParameter("@UOMGUID",SqlDbType.VarChar,36){Value =  info.UOMGUID},
					 new SqlParameter("@Qty",SqlDbType.Int,4){Value =  info.Qty},
					 new SqlParameter("@Price",SqlDbType.Decimal,9){Value =  info.Price},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 更新购物车数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="qty"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool UpdateQty(int id, int qty, int userID)
        {
            string sql = string.Format("UPDATE tblSaleOrderCart SET Qty = {0}  WHERE  ID = {1}  AND  UserID = {2}", qty, id, userID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        public bool UpdateIsBuy(int id, bool isBuy, int userID)
        {
            string sql = string.Format("UPDATE tblSaleOrderCart SET IsBuy = {0}  WHERE  ID = {1}  AND  UserID = {2}", isBuy ? 1 : 0, id, userID);
            Logger.Info("update is buy" + sql);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblSaleOrderCart WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool EmptyCart(int userID)
        {
            string sql = string.Format("DELETE FROM tblSaleOrderCart WHERE UserID = {0} AND IsBuy = 1", userID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        public bool EmptyPromotion(int userID)
        {
            string sql = string.Format("DELETE FROM tblSaleOrderCart WHERE UserID = {0} AND PromotionGuid != ''", userID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SaleOrderCartInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,UserID,ItemGUID,UOMGUID,Qty,Price,CreateTime,Expire,IsBuy FROM tblSaleOrderCart WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SaleOrderCartInfo>();
            }
        }


        public List<SaleOrderCartInfo> GetList(int userID, string language)
        {
            language = language.Replace("_", "");
            string sql = string.Format("SELECT c.*,i.ItemID,case isnull(i.ItemName{0},'') when '' then i.itemnamezhcn else i.itemname{0} end itemname,i.Image1 FROM tblSaleOrderCart AS c LEFT JOIN tblItem AS i ON c.ItemGUID = i.GUID WHERE c.Expire > '{1}' AND c.UserID = {2}", language,DateTime.Now.ToString(), userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SaleOrderCartInfo>();
            }
        }
    }
}

