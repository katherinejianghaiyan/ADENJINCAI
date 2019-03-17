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
    public class SiteAdmin
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SiteAdminInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSiteAdmin(");
            builder.Append("ID,SiteGUID,UserName,Password)");
            builder.Append("VALUES (");
            builder.Append("@ID,@SiteGUID,@UserName,@Password)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ID",SqlDbType.Int,4) {Value =  info.ID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32) {Value =  info.SiteGUID},
					 new SqlParameter("@UserName",SqlDbType.VarChar,32) {Value =  info.UserName},
					 new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  info.Password}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SiteAdminInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSiteAdmin SET ");
            builder.Append("ID=@ID,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("UserName=@UserName,");
            builder.Append("Password=@Password ");
            builder.Append("WHERE ");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32){Value =  info.SiteGUID},
					 new SqlParameter("@UserName",SqlDbType.VarChar,32){Value =  info.UserName},
					 new SqlParameter("@Password",SqlDbType.VarChar,32){Value =  info.Password}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SiteAdminInfo GetInfo(int id)
        {
            string sql = string.Format("SELECT TOP 1 ID,SiteGUID,UserName,Password FROM tblSiteAdmin WHERE  ID = {0}", id);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SiteAdminInfo>();
            }
        }

        public List<SiteAdminInfo> GetList()
        {
            string sql = "SELECT ID,SiteGUID,UserName,Password FROM tblSiteAdmin ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SiteAdminInfo>();
            }
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="userName">登录名称</param>
        /// <param name="userPwd">登录密码</param>
        /// <returns>登录成功返回管理员ID，失败返回 0 </returns>
        public int Signin(string userName, string userPwd)
        {
            string sql = "SELECT ID FROM tblSiteAdmin WHERE ( UserName = @UserName ) AND ( Password = @Password )";
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@Password", SqlDbType.VarChar,60)};
            parameters[0].Value = userName;
            parameters[1].Value = userPwd;

            int adminID = 0;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql, parameters))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    adminID = reader.GetSqlInt32(0).Value;
                }
            }

            if (adminID <= 0) return adminID;

            return adminID;
        }
    }
}

