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
    public class PromotedItem
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PromotedItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblPromotedItem(");
            builder.Append("PromotionGUID,ItemGUID,Price)");
            builder.Append("VALUES (");
            builder.Append("@PromotionGUID,@ItemGUID,@Price)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@PromotionGUID",SqlDbType.VarChar,32) {Value =  info.PromotionGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID},
					 new SqlParameter("@Price",SqlDbType.Decimal,9) {Value =  info.Price}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(PromotedItemInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblPromotedItem SET ");
            builder.Append("PromotionGUID=@PromotionGUID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("Price=@Price ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@PromotionGUID",SqlDbType.VarChar,32){Value =  info.PromotionGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@Price",SqlDbType.Decimal,9){Value =  info.Price},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblPromotedItem WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PromotedItemInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,PromotionGUID,ItemGUID,Price FROM tblPromotedItem WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<PromotedItemInfo>();
            }
        }


        public List<PromotedItemInfo> GetList(string buGUID)
        {
            string sql = string.Format("SELECT DISTINCT p.*,i.ItemID,i.ItemName,i.Image1,i.Image2,i.Image3 FROM tblPromotedItem p INNER JOIN tblPromotion t ON t.GUID = p.PromotionGUID INNER JOIN tblItem i ON i.GUID = p.ItemGUID WHERE i.ToSell = 1 AND i.IsDel = 0 AND t.BUGUID = '{0}'", buGUID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<PromotedItemInfo>();
            }
        }
    }
}

