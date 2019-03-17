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
    public class UOM
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UOMInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblUOM(");
            builder.Append("GUID,NameCn,NameEn,ToUOMGUID,ToQty)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@NameCn,@NameEn,@ToUOMGUID,@ToQty)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@NameCn",SqlDbType.VarChar,8) {Value =  info.NameCn},
					 new SqlParameter("@NameEn",SqlDbType.VarChar,16) {Value =  info.NameEn},
					 new SqlParameter("@ToUOMGUID",SqlDbType.VarChar,32) {Value =  info.ToUOMGUID},
					 new SqlParameter("@ToQty",SqlDbType.Int,4) {Value =  info.ToQty}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UOMInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblUOM SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("NameCn=@NameCn,");
            builder.Append("NameEn=@NameEn,");
            builder.Append("ToUOMGUID=@ToUOMGUID,");
            builder.Append("ToQty=@ToQty ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@NameCn",SqlDbType.VarChar,8){Value =  info.NameCn},
					 new SqlParameter("@NameEn",SqlDbType.VarChar,16){Value =  info.NameEn},
					 new SqlParameter("@ToUOMGUID",SqlDbType.VarChar,32){Value =  info.ToUOMGUID},
					 new SqlParameter("@ToQty",SqlDbType.Int,4){Value =  info.ToQty},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblUOM WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UOMInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,NameCn,NameEn,ToUOMGUID,ToQty FROM tblUOM WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UOMInfo>();
            }
        }


        public List<UOMInfo> GetList()
        {
            string sql = "SELECT ID,GUID,NameCn,NameEn,ToUOMGUID,ToQty FROM tblUOM ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UOMInfo>();
            }
        }
    }
}

