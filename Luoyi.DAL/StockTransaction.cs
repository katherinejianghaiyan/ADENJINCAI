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
    public class StockTransaction
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(StockTransactionInfo info, SqlTransaction trans)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblStockTransaction(");
            builder.Append("UserID,SiteGUID,SODetailGUID,PODetailGUID,ItemGUID,Cost,UOMGUID,Qty,CreateTime,CreateUser)");
            builder.Append("VALUES (");
            builder.Append("@UserID,@SiteGUID,@SODetailGUID,@PODetailGUID,@ItemGUID,@Cost,@UOMGUID,@Qty,@CreateTime,@CreateUser)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32) {Value =  info.SiteGUID ?? ""},
					 new SqlParameter("@SODetailGUID",SqlDbType.VarChar,32) {Value =  info.SODetailGUID ?? ""},
					 new SqlParameter("@PODetailGUID",SqlDbType.VarChar,32) {Value =  info.PODetailGUID ?? ""},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32) {Value =  info.ItemGUID ?? ""},
					 new SqlParameter("@Cost",SqlDbType.Decimal,9) {Value =  info.Cost},
					 new SqlParameter("@UOMGUID",SqlDbType.VarChar,32) {Value =  info.UOMGUID ?? ""},
					 new SqlParameter("@Qty",SqlDbType.Int,4) {Value =  info.Qty},
					 new SqlParameter("@CreateTime",SqlDbType.DateTime) {Value =  info.CreateTime},
					 new SqlParameter("@CreateUser",SqlDbType.Int,4) {Value =  info.CreateUser}
			};


            object obj = null;

            if (trans != null)
            {
                obj = SqlHelper.ExecuteScalar(trans, CommandType.Text, builder.ToString(), lstParameters.ToArray());

            }
            else
            {
                obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            }

            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        public bool Add(List<StockTransactionInfo> list)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.dbAden))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    //新增
                    foreach (var item in list)
                    {
                        if (Add(item, trans) == 0)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(StockTransactionInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblStockTransaction SET ");
            builder.Append("UserID=@UserID,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("SODetailGUID=@SODetailGUID,");
            builder.Append("PODetailGUID=@PODetailGUID,");
            builder.Append("ItemGUID=@ItemGUID,");
            builder.Append("Cost=@Cost,");
            builder.Append("UOMGUID=@UOMGUID,");
            builder.Append("Qty=@Qty,");
            builder.Append("CreateTime=@CreateTime ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32){Value =  info.SiteGUID},
					 new SqlParameter("@SODetailGUID",SqlDbType.VarChar,32){Value =  info.SODetailGUID},
					 new SqlParameter("@PODetailGUID",SqlDbType.VarChar,32){Value =  info.PODetailGUID},
					 new SqlParameter("@ItemGUID",SqlDbType.VarChar,32){Value =  info.ItemGUID},
					 new SqlParameter("@Cost",SqlDbType.Decimal,9){Value =  info.Cost},
					 new SqlParameter("@UOMGUID",SqlDbType.VarChar,32){Value =  info.UOMGUID},
					 new SqlParameter("@Qty",SqlDbType.Int,4){Value =  info.Qty},
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
            string sql = string.Format("DELETE FROM tblStockTransaction WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public StockTransactionInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,UserID,SiteGUID,SODetailGUID,PODetailGUID,ItemGUID,Cost,UOMGUID,Qty,CreateTime FROM tblStockTransaction WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<StockTransactionInfo>();
            }
        }


        public List<StockTransactionInfo> GetList()
        {
            string sql = "SELECT ID,UserID,SiteGUID,SODetailGUID,PODetailGUID,ItemGUID,Cost,UOMGUID,Qty,CreateTime FROM tblStockTransaction ";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<StockTransactionInfo>();
            }
        }
    }
}

