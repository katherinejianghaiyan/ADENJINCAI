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
    public class ItemPropery
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ItemProperyInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblItemPropery(");
            builder.Append("ItemGUID,DictCode,PropName,PropValue)");
            builder.Append("VALUES (");
            builder.Append("@ItemGUID,@DictCode,@PropName,@PropValue)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID},
					 new SqlParameter("@DictCode",SqlDbType.VarChar,32) {Value =  info.DictCode},
					 new SqlParameter("@PropName",SqlDbType.VarChar,32) {Value =  info.PropName},
					 new SqlParameter("@PropValue",SqlDbType.VarChar,512) {Value =  info.PropValue}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ItemProperyInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblItemPropery SET ");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("DictCode=@DictCode,");
            builder.Append("PropName=@PropName,");
            builder.Append("PropValue=@PropValue ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@DictCode",SqlDbType.VarChar,32){Value =  info.DictCode},
					 new SqlParameter("@PropName",SqlDbType.VarChar,32){Value =  info.PropName},
					 new SqlParameter("@PropValue",SqlDbType.VarChar,512){Value =  info.PropValue},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblItemPropery WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemProperyInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,ItemGUID,DictCode,PropName,PropValue FROM tblItemPropery WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemProperyInfo>();
            }
        }


        public List<ItemProperyInfo> GetList(string itemGUID)
        {
            string sql = string.Format("SELECT ID,ItemGUID,DictCode,PropName,PropValue FROM tblItemPropery WHERE ItemGUID = '{0}'", itemGUID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<ItemProperyInfo>();
            }
        }
    }
}

