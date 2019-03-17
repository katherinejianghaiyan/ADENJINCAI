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
    public class UserFavorite
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserFavoriteInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblUserFavorite(");
            builder.Append("UserID,ItemGUID)");
            builder.Append("VALUES (");
            builder.Append("@UserID,@ItemGUID)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36) {Value =  info.ItemGUID}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserFavoriteInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblUserFavorite SET ");
            builder.Append("UserID=@UserID,");
            builder.Append("ItemGUID=@ItemGUID ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36){Value =  info.ItemGUID},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int userID, string itemGUID)
        {
            string sql = string.Format("DELETE FROM tblUserFavorite WHERE UserID = {0} AND ItemGUID = '{1}'", userID, itemGUID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserFavoriteInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,UserID,ItemGUID FROM tblUserFavorite WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserFavoriteInfo>();
            }
        }

        public List<UserFavoriteInfo> GetList(int userID)
        {
            string sql = string.Format("SELECT f.*,i.ItemID,i.ItemName,i.Image1 FROM tblUserFavorite AS f LEFT JOIN tblItem AS i ON f.ItemGUID=i.GUID WHERE f.UserID = {0}", userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UserFavoriteInfo>();
            }
        }

        public bool Exists(string itemGUID, int userID)
        {
            string sql = "SELECT COUNT(1) FROM tblUserFavorite WHERE ItemGUID = @ItemGUID AND UserID = @UserID";
            SqlParameter[] parameter = {
                                     new SqlParameter("@ItemGUID",SqlDbType.VarChar,36){Value = itemGUID},
                                     new SqlParameter("@UserID",SqlDbType.Int,32){Value = userID}
                                 };
            return SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, sql, parameter).ToString() != "0";
        }
    }
}

