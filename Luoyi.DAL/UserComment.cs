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
    public class UserComment
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserCommentInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblUserComment(");
            builder.Append("OrderID,UserID,ItemGUID,Score,ScoreTaste,ScorePrice,ScoreService,Content,Images,CommentTime)");
            builder.Append("VALUES (");
            builder.Append("@OrderID,@UserID,@ItemGUID,@Score,@ScoreTaste,@ScorePrice,@ScoreService,@Content,@Images,@CommentTime)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@OrderID",SqlDbType.Int,4) {Value =  info.OrderID},
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36) {Value =  info.ItemGUID},
					 new SqlParameter("@Score",SqlDbType.Int,4) {Value =  info.Score},
					 new SqlParameter("@ScoreTaste",SqlDbType.Int,4) {Value =  info.ScoreTaste},
					 new SqlParameter("@ScorePrice",SqlDbType.Int,4) {Value =  info.ScorePrice},
					 new SqlParameter("@ScoreService",SqlDbType.Int,4) {Value =  info.ScoreService},
					 new SqlParameter("@Content",SqlDbType.VarChar,256) {Value =  info.Content},
					 new SqlParameter("@Images",SqlDbType.VarChar,128) {Value =  info.Images},
					 new SqlParameter("@CommentTime",SqlDbType.DateTime) {Value =  info.CommentTime}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserCommentInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblUserComment SET ");
            builder.Append("OrderID=@OrderID,");
            builder.Append("UserID=@UserID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("Score=@Score,");
            builder.Append("ScoreTaste=@ScoreTaste,");
            builder.Append("ScorePrice=@ScorePrice,");
            builder.Append("ScoreService=@ScoreService,");
            builder.Append("Content=@Content,");
            builder.Append("Images=@Images,");
            builder.Append("CommentTime=@CommentTime ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@OrderID",SqlDbType.Int,4){Value =  info.OrderID},
					 new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,36){Value =  info.ItemGUID},
					 new SqlParameter("@Score",SqlDbType.Int,4){Value =  info.Score},
					 new SqlParameter("@ScoreTaste",SqlDbType.Int,4){Value =  info.ScoreTaste},
					 new SqlParameter("@ScorePrice",SqlDbType.Int,4){Value =  info.ScorePrice},
					 new SqlParameter("@ScoreService",SqlDbType.Int,4){Value =  info.ScoreService},
					 new SqlParameter("@Content",SqlDbType.VarChar,256){Value =  info.Content},
					 new SqlParameter("@Images",SqlDbType.VarChar,128){Value =  info.Images},
					 new SqlParameter("@CommentTime",SqlDbType.DateTime){Value =  info.CommentTime},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblUserComment WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserCommentInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,OrderID,UserID,ItemGUID,Score,ScoreTaste,ScorePrice,ScoreService,Content,Images,CommentTime FROM tblUserComment WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserCommentInfo>();
            }
        }


        public List<UserCommentInfo> GetList(string filter)
        {
            string sql = string.Format("SELECT ID,OrderID,UserID,ItemGUID,Score,ScoreTaste,ScorePrice,ScoreService,Content,Images,CommentTime FROM tblUserComment WHERE 1=1 {0}", filter);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UserCommentInfo>();
            }
        }

        public DataTable GetTable(string filter)
        {
            string sql = string.Format("SELECT c.*,u.FirstName+u.LastName AS UserName,i.ItemID,i.ItemName,i.Image1 FROM tblUserComment AS c LEFT JOIN tblUser AS u ON c.UserID=u.UserID LEFT JOIN tblItem AS i ON c.ItemGUID = i.GUID WHERE 1=1 {0}", filter);
            return SqlHelper.ExecuteDataSet(SqlHelper.dbAden, CommandType.Text, sql).Tables[0];
        }
    }
}

