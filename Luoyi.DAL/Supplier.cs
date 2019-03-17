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
    public class Supplier
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SupplierInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSupplier(");
            builder.Append("SupplierGUID,CompNameCn,CompNameEn,Address,PostCode,TelNbr,ContactName,EmailAddress,MobileNbr,Active,CreateTime,IsDel)");
            builder.Append("VALUES (");
            builder.Append("@SupplierGUID,@CompNameCn,@CompNameEn,@Address,@PostCode,@TelNbr,@ContactName,@EmailAddress,@MobileNbr,@Active,@CreateTime,@IsDel)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32) {Value =  info.SupplierGUID},
					 new SqlParameter("@CompNameCn",SqlDbType.VarChar,32) {Value =  info.CompNameCn},
					 new SqlParameter("@CompNameEn",SqlDbType.VarChar,64) {Value =  info.CompNameEn},
					 new SqlParameter("@Address",SqlDbType.VarChar,64) {Value =  info.Address},
					 new SqlParameter("@PostCode",SqlDbType.VarChar,8) {Value =  info.PostCode},
					 new SqlParameter("@TelNbr",SqlDbType.VarChar,32) {Value =  info.TelNbr},
					 new SqlParameter("@ContactName",SqlDbType.VarChar,32) {Value =  info.ContactName},
					 new SqlParameter("@EmailAddress",SqlDbType.VarChar,64) {Value =  info.EmailAddress},
					 new SqlParameter("@MobileNbr",SqlDbType.VarChar,16) {Value =  info.MobileNbr},
					 new SqlParameter("@Active",SqlDbType.Bit,1) {Value =  info.Active},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime},
					 new SqlParameter("@IsDel",SqlDbType.Bit,1) {Value =  info.IsDel}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SupplierInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSupplier SET ");
            builder.Append("SupplierGUID=@SupplierGUID,");
            builder.Append("CompNameCn=@CompNameCn,");
            builder.Append("CompNameEn=@CompNameEn,");
            builder.Append("Address=@Address,");
            builder.Append("PostCode=@PostCode,");
            builder.Append("TelNbr=@TelNbr,");
            builder.Append("ContactName=@ContactName,");
            builder.Append("EmailAddress=@EmailAddress,");
            builder.Append("MobileNbr=@MobileNbr,");
            builder.Append("Active=@Active,");
            builder.Append("CreateTime=@CreateTime ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@SupplierGUID",SqlDbType.VarChar,32){Value =  info.SupplierGUID},
					 new SqlParameter("@CompNameCn",SqlDbType.VarChar,32){Value =  info.CompNameCn},
					 new SqlParameter("@CompNameEn",SqlDbType.VarChar,64){Value =  info.CompNameEn},
					 new SqlParameter("@Address",SqlDbType.VarChar,64){Value =  info.Address},
					 new SqlParameter("@PostCode",SqlDbType.VarChar,8){Value =  info.PostCode},
					 new SqlParameter("@TelNbr",SqlDbType.VarChar,32){Value =  info.TelNbr},
					 new SqlParameter("@ContactName",SqlDbType.VarChar,32){Value =  info.ContactName},
					 new SqlParameter("@EmailAddress",SqlDbType.VarChar,64){Value =  info.EmailAddress},
					 new SqlParameter("@MobileNbr",SqlDbType.VarChar,16){Value =  info.MobileNbr},
					 new SqlParameter("@Active",SqlDbType.Bit,1){Value =  info.Active},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime){Value =  info.CreateTime},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblSupplier WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SupplierInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,SupplierGUID,CompNameCn,CompNameEn,Address,PostCode,TelNbr,ContactName,EmailAddress,MobileNbr,Active,CreateTime FROM tblSupplier WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SupplierInfo>();
            }
        }


        public List<SupplierInfo> GetList()
        {
            string sql = "SELECT ID,SupplierGUID,CompNameCn,CompNameEn,Address,PostCode,TelNbr,ContactName,EmailAddress,MobileNbr,Active,CreateTime FROM tblSupplier WHERE IsDel = 0 ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SupplierInfo>();
            }
        }

    }
}

