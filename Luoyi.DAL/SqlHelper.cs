using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using Luoyi.Common;


namespace Luoyi.DAL
{
    /// <summary>
    /// 数据访问抽象基础类
    /// Copyright (C) 2009 Luoyi.com 
    /// </summary>
    public sealed class SqlHelper
    {
        /// <summary>
        /// 默认数据库连接字符串(web.config来配置)
        /// </summary>
        public readonly static string dbAden = ConfigurationManager.ConnectionStrings["AdenDB"].ToString();
        public readonly static string dbAden2 = ConfigurationManager.ConnectionStrings["AdenDB2"].ToString();

        #region 翻页数据绑定

        /// <summary>
        /// 单个数据表翻页，支持多重排序
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="pageSize">每页显示数据条数</param>
        /// <param name="pageIndex">当前页序号</param>
        /// <param name="fields">选择字段</param>
        /// <param name="condition">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="recordCount">输出符合条件记录总数</param>
        public static DataSet GetPage(string connectionString, string tableName, int pageSize, int pageIndex, string fields, string condition, string order, out int recordCount)
        {
            DataSet ds;

            SqlParameter[] parameters = {
                new SqlParameter("@Tables",SqlDbType.NVarChar,1024),
                new SqlParameter("@Fields",SqlDbType.NVarChar,2048),
                new SqlParameter("@PageSize",SqlDbType.Int,4),
                new SqlParameter("@PageIndex",SqlDbType.Int,4),
                new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                new SqlParameter("@OrderBy",SqlDbType.NVarChar,256),
                new SqlParameter("@TotalRecord",SqlDbType.Int,4)
            };
            parameters[0].Value = tableName;
            parameters[1].Value = fields;
            parameters[2].Value = pageSize;
            parameters[3].Value = pageIndex;
            parameters[4].Value = condition;
            parameters[5].Value = order;
            parameters[6].Direction = ParameterDirection.Output;

            ds = ExecuteDataSet(connectionString,CommandType.StoredProcedure,"uspGetPage", parameters);
            recordCount = (int)parameters[6].Value;

            return ds;
        }

        public static DataSet GetPage(string connectionString, string tableName, int pageSize, int pageIndex, string fields, string condition, string order,string group ,out int recordCount)
        {
            DataSet ds;

            SqlParameter[] parameters = {
                new SqlParameter("@Tables",SqlDbType.NVarChar,1024),
                new SqlParameter("@Fields",SqlDbType.NVarChar,2048),
                new SqlParameter("@PageIndex",SqlDbType.Int,4),
                new SqlParameter("@PageSize",SqlDbType.Int,4),
                new SqlParameter("@Where",SqlDbType.NVarChar,4000),
                new SqlParameter("@OrderBy",SqlDbType.NVarChar,256),
                new SqlParameter("@GroupBy",SqlDbType.NVarChar,256),
                new SqlParameter("@TotalRecord",SqlDbType.Int,4)
            };
            parameters[0].Value = tableName;
            parameters[1].Value = fields;
            parameters[2].Value = pageIndex;
            parameters[3].Value = pageSize;
            parameters[4].Value = condition;
            parameters[5].Value = order;
            parameters[6].Value = group;
            parameters[7].Direction = ParameterDirection.Output;

            ds = ExecuteDataSet(connectionString, CommandType.StoredProcedure, "uspGetPage1", parameters);
            recordCount = (int)parameters[7].Value;

            return ds;
        }

        #endregion          

        #region 基本方法

        /// <summary>
        /// 返回影响的记录数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static int ExecuteSql(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, commandType, commandText, parameters);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch(SqlException ex)
                    {
                        Logger.Error(string.Format("数据层错误ExecuteSql：\r\n {0} \r\n {1}", commandText, ex));
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 返回影响的记录数
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static int ExecuteSql(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // 预处理
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);

            // 执行
            int retval = cmd.ExecuteNonQuery();

            // 清除参数集,以便再次使用.
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// 返回第一列第一行查询结果（object）。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, commandType, commandText, parameters);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                            return null;
                        else
                            return obj;
                    }
                    catch(System.Data.SqlClient.SqlException ex)
                    {
                        Logger.Error(string.Format("数据层错误ExecuteScalar：\r\n {0} \r\n {1}", commandText, ex));
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 返回第一列第一行查询结果（object）。
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // 创建SqlCommand命令,并进行预处理
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);

            // 执行SqlCommand命令,并返回结果.
            object obj = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                return null;
            else
                return obj;
        }

        /// <summary>
        /// 返回SqlDataReader
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, parameters);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                Logger.Error(string.Format("数据层错误ExecuteReader：\r\n {0} \r\n {1}", commandText, ex));
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 返回SqlDataReader
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // 创建命令
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return dataReader;
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, commandType, commandText, parameters);
                using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                    }
                    catch(System.Data.SqlClient.SqlException ex)
                    {
                        Logger.Error(string.Format("数据层错误ExecuteDataSet：\r\n {0} \r\n {1}", commandText, ex));
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param>
        /// <param name="commandText">存储过程名或sql语句</param>
        /// <param name="parameters">SqlParamter参数数组</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // 预处理
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);

            // 创建 DataAdapter & DataSet
            using(SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 执行多条sql语句，实现数据库事务。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="lstSql">/传递的参数是一个数组，存放要执行sql语句</param>		
        public static int ExecuteSqlTran(string connectionString, List<string> lstSql)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;

                try
                {
                    int rows = 0;
                    foreach(string strSql in lstSql)
                    {
                        if(strSql.Trim().Length > 1)
                        {
                            cmd.CommandText = strSql;
                            rows += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return rows;
                }
                catch(System.Data.SqlClient.SqlException ex)
                {
                    tx.Rollback(); 
                    Logger.Error(string.Format("数据层错误ExecuteSqlTran：{0}", ex));
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 执行多条sql语句，实现数据库事务。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dicSql">key为sql语句，value是该语句的SqlParameter数组）</param>
        public static int ExecuteSqlTran(string connectionString, Dictionary<string, SqlParameter[]> dicSql)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using(SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int rows = 0;
                        //循环
                        foreach(KeyValuePair<string, SqlParameter[]> myDt in dicSql)
                        {
                            PrepareCommand(cmd, conn, trans, CommandType.Text, myDt.Key, myDt.Value);
                            rows += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return rows;
                    }
                    catch(System.Data.SqlClient.SqlException ex)
                    {
                        trans.Rollback(); 
                        Logger.Error(string.Format("数据层错误ExecuteSqlTran：{0}", ex));
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region  获取存储过程，或者函数之类的操作返回值

        /// <summary>
        /// 获取存储过程，或者函数之类的操作返回值
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static int GetRunProcedureValue(string connectionString, string storedProcName, SqlParameter[] parameters)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                return result;
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, null, CommandType.StoredProcedure, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #endregion

        #region 内部构建

        /// <summary>
        /// 内部设置
        /// </summary>
        /// <param name="cmd">要处理的SqlCommand</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">一个有效的事务或者是null值</param>
        /// <param name="commandType">命令类型(存储过程,命令文本, 其它.)</param>
        /// <param name="cmdText">命令内容</param>
        /// <param name="cmdParms">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType commandType, string cmdText, SqlParameter[] cmdParms)
        {
            if(conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandTimeout = 360;
            cmd.CommandText = cmdText;
            if(trans != null)
            {
                if(trans.Connection == null)
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                cmd.Transaction = trans;
            }
            cmd.CommandType = commandType;
            if(cmdParms != null)
            {
                foreach(SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

    }

}
