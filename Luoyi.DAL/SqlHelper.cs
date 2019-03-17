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
    /// ���ݷ��ʳ��������
    /// Copyright (C) 2009 Luoyi.com 
    /// </summary>
    public sealed class SqlHelper
    {
        /// <summary>
        /// Ĭ�����ݿ������ַ���(web.config������)
        /// </summary>
        public readonly static string dbAden = ConfigurationManager.ConnectionStrings["AdenDB"].ToString();
        public readonly static string dbAden2 = ConfigurationManager.ConnectionStrings["AdenDB2"].ToString();

        #region ��ҳ���ݰ�

        /// <summary>
        /// �������ݱ�ҳ��֧�ֶ�������
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="tableName">����</param>
        /// <param name="pageSize">ÿҳ��ʾ��������</param>
        /// <param name="pageIndex">��ǰҳ���</param>
        /// <param name="fields">ѡ���ֶ�</param>
        /// <param name="condition">��ѯ����</param>
        /// <param name="order">����</param>
        /// <param name="recordCount">�������������¼����</param>
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

        #region ��������

        /// <summary>
        /// ����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
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
                        Logger.Error(string.Format("���ݲ����ExecuteSql��\r\n {0} \r\n {1}", commandText, ex));
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// ����Ӱ��ļ�¼��
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
        /// <returns></returns>
        public static int ExecuteSql(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Ԥ����
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);

            // ִ��
            int retval = cmd.ExecuteNonQuery();

            // ���������,�Ա��ٴ�ʹ��.
            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// ���ص�һ�е�һ�в�ѯ�����object����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
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
                        Logger.Error(string.Format("���ݲ����ExecuteScalar��\r\n {0} \r\n {1}", commandText, ex));
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// ���ص�һ�е�һ�в�ѯ�����object����
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // ����SqlCommand����,������Ԥ����
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);

            // ִ��SqlCommand����,�����ؽ��.
            object obj = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                return null;
            else
                return obj;
        }

        /// <summary>
        /// ����SqlDataReader
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
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
                Logger.Error(string.Format("���ݲ����ExecuteReader��\r\n {0} \r\n {1}", commandText, ex));
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ����SqlDataReader
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // ��������
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return dataReader;
        }

        /// <summary>
        /// ����DataSet
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
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
                        Logger.Error(string.Format("���ݲ����ExecuteDataSet��\r\n {0} \r\n {1}", commandText, ex));
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        /// <summary>
        /// ����DataSet
        /// </summary>
        /// <param name="transaction">���ݿ�����</param>
        /// <param name="commandType">�������� (�洢����,�����ı�������)</param>
        /// <param name="commandText">�洢��������sql���</param>
        /// <param name="parameters">SqlParamter��������</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            if(transaction == null)
                throw new ArgumentNullException("transaction");
            if(transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            // Ԥ����
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, parameters);

            // ���� DataAdapter & DataSet
            using(SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// ִ�ж���sql��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="lstSql">/���ݵĲ�����һ�����飬���Ҫִ��sql���</param>		
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
                    Logger.Error(string.Format("���ݲ����ExecuteSqlTran��{0}", ex));
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// ִ�ж���sql��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="dicSql">keyΪsql��䣬value�Ǹ�����SqlParameter���飩</param>
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
                        //ѭ��
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
                        Logger.Error(string.Format("���ݲ����ExecuteSqlTran��{0}", ex));
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region  ��ȡ�洢���̣����ߺ���֮��Ĳ�������ֵ

        /// <summary>
        /// ��ȡ�洢���̣����ߺ���֮��Ĳ�������ֵ
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
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
        /// ���� SqlCommand ����ʵ��(��������һ������ֵ)	
        /// </summary>
        /// <param name="connection">���ݿ�����</param>
        /// <param name="storedProcName">�洢������</param>
        /// <param name="parameters">�洢���̲���</param>
        /// <returns>SqlCommand ����ʵ��</returns>
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

        #region �ڲ�����

        /// <summary>
        /// �ڲ�����
        /// </summary>
        /// <param name="cmd">Ҫ�����SqlCommand</param>
        /// <param name="conn">���ݿ�����</param>
        /// <param name="trans">һ����Ч�����������nullֵ</param>
        /// <param name="commandType">��������(�洢����,�����ı�, ����.)</param>
        /// <param name="cmdText">��������</param>
        /// <param name="cmdParms">�������������SqlParameter��������,���û�в���Ϊ'null'</param>
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
