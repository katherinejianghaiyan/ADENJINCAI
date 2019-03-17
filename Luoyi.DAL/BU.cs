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
    public class BU
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BUInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblBU(");
            builder.Append("ID,BUGUID,Code,ParentGUID,ERPCode)");
            builder.Append("VALUES (");
            builder.Append("@ID,@BUGUID,@Code,@ParentGUID,@ERPCode)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@ID",SqlDbType.Int,4) {Value =  info.ID},
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32) {Value =  info.BUGUID},
					 new SqlParameter("@Code",SqlDbType.VarChar,16) {Value =  info.Code},
					 new SqlParameter("@ParentGUID",SqlDbType.Char,36) {Value =  info.ParentGUID},
					 new SqlParameter("@ERPCode",SqlDbType.VarChar,32) {Value =  info.ERPCode}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BUInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblBU SET ");
            builder.Append("BUGUID=@BUGUID,");
            builder.Append("Code=@Code,");
            builder.Append("ParentGUID=@ParentGUID,");
            builder.Append("ERPCode=@ERPCode ");
            builder.Append("WHERE ID=@ID ");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@BUGUID",SqlDbType.VarChar,32){Value =  info.BUGUID},
					 new SqlParameter("@Code",SqlDbType.VarChar,16){Value =  info.Code},
					 new SqlParameter("@ParentGUID",SqlDbType.Char,36){Value =  info.ParentGUID},
					 new SqlParameter("@ERPCode",SqlDbType.VarChar,32){Value =  info.ERPCode},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int id)
        {
            string sql = string.Format("DELETE FROM tblBU WHERE  ID= {0}", id);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BUInfo GetInfo(int id)
        {
            string sql = string.Format("SELECT TOP 1 ID,BUGUID,Code,ParentGUID,ERPCode,EndHour,Timeout FROM tblBU WHERE  ID= {0}", id);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<BUInfo>();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BUInfo GetInfo(string buguid)
        {
            string sql = string.Format("SELECT TOP 1 ID,BUGUID,Code,ParentGUID,ERPCode,EndHour,Timeout,DeliveryDays,isnull(PickupTime,'') PickupTime FROM tblBU WHERE  BUGUID= '{0}'", buguid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<BUInfo>();
            }
        }

        public List<BUInfo> GetList()
        {
            string sql = "SELECT ID,BUGUID,Code,ParentGUID,ERPCode FROM tblBU ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<BUInfo>();
            }
        }
    }
}

