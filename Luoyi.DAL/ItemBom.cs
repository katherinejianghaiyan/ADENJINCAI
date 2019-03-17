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
    public class ItemBom
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ItemBomInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblItemBom(");
            builder.Append("ProductGUID,ItemGUID,StdQty,ActQty,UOMID)");
            builder.Append("VALUES (");
            builder.Append("@ProductGUID,@ItemGUID,@StdQty,@ActQty,@UOMID)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ProductGUID",SqlDbType.VarChar,32) {Value =  info.ProductGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID},
					 new SqlParameter("@StdQty",SqlDbType.Int,4) {Value =  info.StdQty},
					 new SqlParameter("@ActQty",SqlDbType.Int,4) {Value =  info.ActQty},
					 new SqlParameter("@UOMID",SqlDbType.VarChar,16) {Value =  info.UOMID}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ItemBomInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblItemBom SET ");
            builder.Append("ProductGUID=@ProductGUID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("StdQty=@StdQty,");
            builder.Append("ActQty=@ActQty,");
            builder.Append("UOMID=@UOMID ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ProductGUID",SqlDbType.VarChar,32){Value =  info.ProductGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@StdQty",SqlDbType.Int,4){Value =  info.StdQty},
					 new SqlParameter("@ActQty",SqlDbType.Int,4){Value =  info.ActQty},
					 new SqlParameter("@UOMID",SqlDbType.VarChar,16){Value =  info.UOMID},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblItemBom WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemBomInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,ProductGUID,ItemGUID,StdQty,ActQty,UOMID FROM tblItemBom WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemBomInfo>();
            }
        }

        public List<ItemBomInfo> GetList()
        {
            string sql = "SELECT ID,ProductGUID,ItemGUID,StdQty,ActQty,UOMID FROM tblItemBom ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<ItemBomInfo>();
            }
        }

        public DataTable GetTable(string productGUID, string language)
        {
            language = language.Replace("_", "");
            string sql = string.Format("SELECT b.*,i.ItemName{1} itemname,u.NameEn,i.ItemType from tblItemBom b left join tblItem i ON i.GUID=b.ItemGUID left join tblUOM u ON u.GUID=b.UOMGUID where b.ProductGUID = '{0}' order by b.sortname,i.sort",
                productGUID,language);
            return SqlHelper.ExecuteDataSet(SqlHelper.dbAden, CommandType.Text, sql).Tables[0];
        }
    }
}

