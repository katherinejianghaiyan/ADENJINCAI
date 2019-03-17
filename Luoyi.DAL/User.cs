using System;
using System.Linq;
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
    public class User
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblUser(");
            builder.Append("WechatID,FirstName,LastName,SiteGUID,BirthDay,Gender,Mobile,City,Department,Position,CreateTime,CreateDate,Email)");
            //builder.Append("VALUES (");
            builder.Append(" select ");
            builder.Append("@WechatID,@FirstName,@LastName,@SiteGUID,@BirthDay,@Gender,@Mobile,@City,@Department,@Position,@CreateTime,@CreateDate,@Email");
            builder.Append(" where not exists(select top 1 1 from tbluser where wechatid = @WechatID) ");
            //builder.Append(")");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@WechatID",SqlDbType.VarChar,64) {Value =  info.WechatID ?? ""},
                     new SqlParameter("@FirstName",SqlDbType.VarChar,32) {Value =  info.FirstName ?? ""},
					 new SqlParameter("@LastName",SqlDbType.VarChar,32) {Value =  info.LastName ?? ""},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,36) {Value =  info.SiteGUID ?? ""},
					 new SqlParameter("@BirthDay",SqlDbType.Int,4) {Value =  info.BirthDay},
					 new SqlParameter("@Gender",SqlDbType.VarChar,8) {Value =  info.Gender ?? ""},
					 new SqlParameter("@Mobile",SqlDbType.VarChar,16) {Value =  info.Mobile ?? ""},
					 new SqlParameter("@City",SqlDbType.VarChar,32) {Value =  info.City ?? ""},
					 new SqlParameter("@Department",SqlDbType.VarChar,64) {Value =  info.Department ?? ""},
					 new SqlParameter("@Position",SqlDbType.VarChar,32) {Value =  info.Position ?? ""},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime},
					 new SqlParameter("@CreateDate",SqlDbType.Int,4) {Value =  info.CreateDate},
					 new SqlParameter("@Email",SqlDbType.VarChar,64) {Value =  info.Email ?? ""}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        public void UserPageLog(UserInfo user, string pagename)
        {
            System.Threading.ThreadPool.QueueUserWorkItem((o) =>
            {
                if (user.UserID == 2 || user.UserID == 3 || user.UserID == 18) return;
                string sql = "insert into tbluserlog(pagename,userid) values('{0}','{1}')";
                sql = string.Format(sql, pagename, user.UserID);
                SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql);
            });
        }

        /// <summary>
        /// 检查用户是否可注册
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Boolean checkRegister(UserInfo info)
        {
            string sitGuid = info.SiteGUID;

            StringBuilder sbSql = new StringBuilder();

            //string strSql = "update tblregisteredusers set wechatid = '{1}' from tblsite "
            //    + "where ltrim(rtrim(lower(tblregisteredusers.NickName))) = '{0}' and ltrim(rtrim(isnull(tblregisteredusers.wechatid,'')))='' "
            //    + "and tblregisteredusers.SiteGUID = '{2}' and tblsite.guid = '{2}' "
            //    + "and tblregisteredusers.UserType = tblSite.UserType";

            string strSql = "update tblregisteredusers set wechatid = '{1}'  "
                + "where ltrim(rtrim(lower(tblregisteredusers.NickName))) = '{0}' "
                + "and ltrim(rtrim(isnull(tblregisteredusers.wechatid,'')))= '' "
                + "and tblregisteredusers.SiteGUID = '{2}' and "
                + "isnull(startdate,'2017-1-1')<= getdate() and dateadd(d,1,isnull(enddate, '2222-12-12'))> getdate()";

            //"update tblregisteredusers set wechatid='{1}' "
            //+ "where ltrim(rtrim(lower(NickName))) = '{0}' and wechatid is null and "
            //+ "siteguid in (select guid from tblsite where guid='{2}' and limitusers=1)";


            strSql = string.Format(strSql, info.FirstName.ToLower().Trim(), info.WechatID, info.SiteGUID);

            SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, strSql, null);

            strSql = "select count(a2.id),sum(case isnull(a2.wechatid, '') when '{0}' then 1 else 0 end) "
                + "from tblSite (nolock) a1  LEFT JOIN tblRegisteredUsers (nolock) a2 "
                + "on a1.guid = a2.SiteGUID " //and isnull(a1.UserType,'') = isnull(a2.UserType,'')"
                + "where a1.guid = '{1}'";
            //" SELECT LimitUsers " +
            //                 " , count(DISTINCT a2.SiteGuid)" +
            //              " FROM tblSite (nolock) a1 " +
            //         " LEFT JOIN tblRegisteredUsers (nolock) a2 " +
            //                " ON a1.Guid = a2.SiteGuid " +
            //               " AND ltrim(rtrim(lower(a2.NickName))) = '{0}' and isnull(a2.wechatid,'')='{1}' " +
            //             " WHERE [Guid] = '{2}' " +
            //             " GROUP BY LimitUsers ";

            strSql = string.Format(strSql, /*info.FirstName.Trim().ToLower(),*/info.WechatID, info.SiteGUID);

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, strSql))
            {
                if (!reader.HasRows) return false;

                reader.Read();

                //没有注册用户限制
                if (reader.GetSqlInt32(0).Value == 0) return true;

                if (reader.GetSqlInt32(1).Value > 0) return true;

                //bool boolLimitUsers = reader.GetSqlBoolean(0).Value;
                //int intCount = reader.GetSqlInt32(1).Value;

                //if (!boolLimitUsers) return true;
                //result = intCount > 0 ? true : false;
            }
            return false;
        }

        public void UpdateLanguage(int userID, string langCode)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblUser SET ");
            builder.Append("LangCode=@LangCode ");
            builder.Append("WHERE UserID=@UserID");
            var lstParameters = new List<SqlParameter>
            {
                 new SqlParameter("@LangCode",SqlDbType.VarChar,10){Value = langCode},
                 new SqlParameter("@UserID",SqlDbType.VarChar,32){Value = userID }
            };
            SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(UserInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblUser SET ");
            builder.Append("WechatID=@WechatID,");
           // builder.Append("UnionID=@UnionID,");//case isnull(unionid,'') when '' then @UnionID else unionid end,");
            builder.Append("FirstName=@FirstName,");
            builder.Append("LastName=@LastName,");
            builder.Append("UserName=@UserName,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("BirthDay=@BirthDay,");
            builder.Append("Gender=@Gender,");
            builder.Append("Mobile=@Mobile,");
            builder.Append("City=@City,");
            builder.Append("Department=@Department,");
            builder.Append("Section=@Section,");
            builder.Append("Position=@Position,");
            builder.Append("Email=@Email ");
            builder.Append("WHERE UserID=@UserID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@WechatID",SqlDbType.VarChar,64){Value =  info.WechatID},
					 new SqlParameter("@FirstName",SqlDbType.VarChar,32){Value =   info.FirstName ?? ""},
					 new SqlParameter("@LastName",SqlDbType.NVarChar,32){Value =  info.LastName ?? ""},
                     new SqlParameter("@UserName",SqlDbType.NVarChar,32){Value =  info.UserName ?? (info.FirstName ?? "")},
                     new SqlParameter("@SiteGUID",SqlDbType.VarChar,36){Value =  info.SiteGUID ?? ""},
                     new SqlParameter("@BirthDay",SqlDbType.Int,4){Value =  info.BirthDay},
                     new SqlParameter("@Gender",SqlDbType.VarChar,8){Value =  info.Gender ?? ""},
                     new SqlParameter("@Mobile",SqlDbType.VarChar,16){Value =  info.Mobile ?? ""},
                     new SqlParameter("@City",SqlDbType.VarChar,32){Value =  info.City ?? ""},
                     new SqlParameter("@Department",SqlDbType.VarChar,64){Value =  info.Department ?? ""},
                     new SqlParameter("@Section",SqlDbType.VarChar,64){Value =  info.Section ?? ""},
                     new SqlParameter("@Position",SqlDbType.VarChar,32){Value =  info.Position ?? ""},
                     new SqlParameter("@Email",SqlDbType.VarChar,64){Value =  info.Email ?? ""},
                     new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID}
            };

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int userID)
        {
            string sql = string.Format("DELETE FROM tblUser WHERE UserID = {0}", userID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserInfo GetInfo(int userID)
        {
            string sql = string.Format("SELECT TOP 1 u.*,s.Code,s.BUGUID,b.EndHour,b.TimeOut FROM tblUser AS u LEFT JOIN tblSite AS s ON u.SiteGUID=s.GUID LEFT JOIN tblBU AS b ON b.BUGUID=s.BUGUID WHERE u.UserID = '{0}'", userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserInfo>();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetRealName(int userID)
        {
            string sql = string.Format("SELECT TOP 1 FirstName+LastName FROM tblUser WHERE UserID = {0}", userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.IsDBNull(0) ? string.Empty : reader.GetSqlString(0).Value.Trim();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserInfo GetInfo(string wechatID)
        {
            string sql = string.Format("SELECT TOP 1 u.*,s.Code,s.BUGUID,b.EndHour,b.TimeOut FROM tblUser AS u LEFT JOIN tblSite AS s ON u.SiteGUID=s.GUID LEFT JOIN tblBU AS b ON b.BUGUID=s.BUGUID WHERE u.WechatID = '{0}'", wechatID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserInfo>();
            }
        }

        public List<UserInfo> GetList()
        {
            string sql = "SELECT UserID,WechatID,FirstName,LastName,SiteGUID,BirthDay,Gender,Mobile,City,Department,Position,CreateTime,CreateDate FROM tblUser ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UserInfo>();
            }
        }
        // 返回用户的成本中心是否在SQLMast中有对应值
        public bool UserCostCenter(string wechatID)
        {
            //得到openID所属的Cost Center
            string siteCode = GetInfo(wechatID).Code;

            if (string.IsNullOrEmpty(siteCode))
                return false;
            string sql = "SELECT COSTCENTERCODES AS CODE FROM SQLMAST ";

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden2, CommandType.Text, sql))
            {
                List<SiteInfo> list = reader.ToEntityList<SiteInfo>();

                //判断是否设置了sql命令
                return list.Any(q => q.Code.Split(',', ';').Contains(siteCode));
            };
        }
        //根据appName返回tblWechatConfig表中的字段
        public WechatConfig WeChatConfig (string appName)
        {
            string sql = string.Format("SELECT APPID,PATH FROM TBLWECHATCONFIG WHERE APPNAME = '{0}'",appName);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden2, CommandType.Text, sql))
            {
                return reader.ToEntity<WechatConfig>();
            }
        }
            
    }
}

