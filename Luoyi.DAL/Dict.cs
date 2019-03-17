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
    public class Dict
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DictInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblDict(");
            builder.Append("DictType,Code,Name)");
            builder.Append("VALUES (");
            builder.Append("@DictType,@Code,@Name)");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@DictType",SqlDbType.Int,4) {Value =  info.DictType},
					 new SqlParameter("@Code",SqlDbType.VarChar,32) {Value =  info.Code},
					 new SqlParameter("@Name",SqlDbType.VarChar,64) {Value =  info.Name}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DictInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblDict SET ");
            builder.Append("DictType=@DictType,");
            builder.Append("Name=@Name ");
            builder.Append("WHERE Code=@Code ");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@DictType",SqlDbType.Int,4){Value =  info.DictType},
					 new SqlParameter("@Name",SqlDbType.VarChar,64){Value =  info.Name},
					 new SqlParameter("@Code",SqlDbType.VarChar,32){Value =  info.Code}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DictInfo GetInfo(string code)
        {
            string sql = string.Format("SELECT TOP 1 DictType,Code,Name FROM tblDict WHERE  Code= '{0}'", code);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<DictInfo>();
            }
        }

        public List<DictInfo> GetList()
        {
            string sql = "SELECT DictType,Code,Name FROM tblDict ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<DictInfo>();
            }
        }
    }
}

