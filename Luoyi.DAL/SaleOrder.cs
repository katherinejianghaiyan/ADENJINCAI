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
    public class SaleOrder
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SaleOrderInfo info, SqlTransaction trans)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblSaleOrder(");
            builder.Append("GUID,OrderCode,UserID,SiteGUID,OrderTime,OrderDate,RequiredDate,requireddinnertype,TotalAmount,CouponCode,CouponAmount,PaymentAmount,PaymentID,IsPaid,PaidTime,PayTransCode,Status,IsDel,Comments,ShipToAddr)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@OrderCode,@UserID,@SiteGUID,@OrderTime,@OrderDate,@RequiredDate,@RequiredDinnerType,@TotalAmount,@CouponCode,@CouponAmount,@PaymentAmount,@PaymentID,@IsPaid,@PaidTime,@PayTransCode,@Status,@IsDel,@Comments,@ShipToAddr)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@OrderCode",SqlDbType.VarChar,64) {Value =  info.OrderCode ?? ""},
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,36) {Value =  info.SiteGUID ?? ""},
					 new SqlParameter("@OrderTime",SqlDbType.DateTime) {Value =  info.OrderTime},
					 new SqlParameter("@OrderDate",SqlDbType.Int,4) {Value =  info.OrderDate},
					 new SqlParameter("@RequiredDate",SqlDbType.DateTime) {Value =  info.RequiredDate},
                     new SqlParameter("@RequiredDinnerType",SqlDbType.VarChar) {Value =  info.RequiredDinnerType},
                     new SqlParameter("@TotalAmount",SqlDbType.Decimal,9) {Value =  info.TotalAmount},
					 new SqlParameter("@CouponCode",SqlDbType.VarChar,36) {Value =  info.CouponCode ?? ""},
					 new SqlParameter("@CouponAmount",SqlDbType.Decimal,9) {Value =  info.CouponAmount},
					 new SqlParameter("@PaymentAmount",SqlDbType.Decimal,9) {Value =  info.PaymentAmount},
					 new SqlParameter("@PaymentID",SqlDbType.Int,4) {Value =  info.PaymentID},
					 new SqlParameter("@IsPaid",SqlDbType.Bit,1) {Value =  info.IsPaid},
					 new SqlParameter("@PaidTime",SqlDbType.DateTime) {Value = DBNull.Value},
					 new SqlParameter("@PayTransCode",SqlDbType.VarChar,64) {Value =  info.PayTransCode ?? ""},
					 new SqlParameter("@Status",SqlDbType.Int,4) {Value =  info.Status},
					 new SqlParameter("@IsDel",SqlDbType.Bit,1) {Value =  info.IsDel},
                     new SqlParameter("@Comments",SqlDbType.VarChar,30) {Value = info.Comments?? ""},
                     new SqlParameter("@ShipToAddr",SqlDbType.VarChar, 100) {Value = info.ShipToAddr?? ""  }
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

        public int Add(SaleOrderInfo info, List<SaleOrderItemInfo> listItem)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.dbAden))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    int orderID = Add(info, trans);

                    if (orderID == 0)
                    {
                        trans.Rollback();
                        return 0;
                    }
                    SaleOrderItem dalItem = new SaleOrderItem();

                    foreach (var item in listItem)
                    {
                        if (dalItem.Add(item, trans) == 0)
                        {
                            trans.Rollback();
                            return 0;
                        }
                    }

                    trans.Commit();
                    return orderID;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SaleOrderInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblSaleOrder SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("OrderCode=@OrderCode,");
            builder.Append("UserID=@UserID,");
            builder.Append("SiteGUID=@SiteGUID,");
            builder.Append("OrderTime=@OrderTime,");
            builder.Append("OrderDate=@OrderDate,");
            builder.Append("RequiredDate=@RequiredDate,");
            builder.Append("TotalAmount=@TotalAmount,");
            builder.Append("CouponCode=@CouponCode,");
            builder.Append("CouponAmount=@CouponAmount,");
            builder.Append("PaymentAmount=@PaymentAmount,");
            builder.Append("PaymentID=@PaymentID,");
            builder.Append("IsPaid=@IsPaid,");
            builder.Append("PaidTime=@PaidTime,");
            builder.Append("PayTransCode=@PayTransCode,");
            builder.Append("Status=@Status ");
            builder.Append("WHERE OrderID=@OrderID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@OrderCode",SqlDbType.VarChar,64){Value =  info.OrderCode},
					 new SqlParameter("@UserID",SqlDbType.Int,4){Value =  info.UserID},
					 new SqlParameter("@SiteGUID",SqlDbType.VarChar,32){Value =  info.SiteGUID},
					 new SqlParameter("@OrderTime",SqlDbType.DateTime){Value =  info.OrderTime},
					 new SqlParameter("@OrderDate",SqlDbType.Int,4){Value =  info.OrderDate},
					 new SqlParameter("@RequiredDate",SqlDbType.DateTime){Value =  info.RequiredDate},
					 new SqlParameter("@TotalAmount",SqlDbType.Decimal,9){Value =  info.TotalAmount},
					 new SqlParameter("@CouponCode",SqlDbType.VarChar,32){Value =  info.CouponCode},
					 new SqlParameter("@CouponAmount",SqlDbType.Decimal,9){Value =  info.CouponAmount},
					 new SqlParameter("@PaymentAmount",SqlDbType.Decimal,9){Value =  info.PaymentAmount},
					 new SqlParameter("@PaymentID",SqlDbType.Int,4){Value =  info.PaymentID},
					 new SqlParameter("@IsPaid",SqlDbType.Bit,1){Value =  info.IsPaid},
					 new SqlParameter("@PaidTime",SqlDbType.DateTime){Value =  info.PaidTime},
					 new SqlParameter("@PayTransCode",SqlDbType.VarChar,64){Value =  info.PayTransCode},
					 new SqlParameter("@Status",SqlDbType.Int,4){Value =  info.Status},
					 new SqlParameter("@OrderID",SqlDbType.Int,4){Value =  info.OrderID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int orderID)
        {
            string sql = string.Format("DELETE FROM tblSaleOrder WHERE OrderID = {0}", orderID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool UpdateIsDel(string orderGUID, bool isDel)
        {
            string sql = string.Format("UPDATE tblSaleOrder SET IsDel={1} WHERE GUID = '{0}'", orderGUID, isDel ? 1 : 0);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool UpdateStatus(int orderID, int status, int adminID)
        {
            return UpdateStatus(orderID, status, adminID, null);
        }

        public bool UpdateStatus(int orderID, int status, int adminID, SqlTransaction trans)
        {
            string sql = string.Format("UPDATE tblSaleOrder SET Status={1},ShippedDate='{2}',ShippedUser={3} WHERE OrderID = {0}", orderID, status, status == (int)SaleOrderInfo.StatusEnum.YFH ? DateTime.Now : DateTime.MinValue, adminID);
            if (trans != null)
            {
                return SqlHelper.ExecuteSql(trans, CommandType.Text, sql) > 0;
            }
            else
            {
                return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool RemoveCoupon(int orderID)
        {
            string sql = string.Format("UPDATE tblSaleOrder SET CouponCode='' WHERE OrderID = {0}", orderID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SaleOrderInfo GetInfo(int orderID)
        {
            string sql = string.Format("SELECT TOP 1 OrderID,GUID,OrderCode,UserID,SiteGUID,OrderTime,OrderDate,RequiredDate,TotalAmount,CouponCode,CouponAmount,PaymentAmount,PaymentID,IsPaid,PaidTime,PayTransCode,Status FROM tblSaleOrder WHERE OrderID = {0}", orderID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SaleOrderInfo>();
            }
        }

        public SaleOrderInfo GetInfo(int orderID, int userID)
        {
            string sql = string.Format("SELECT TOP 1 o.*,s.Code FROM tblSaleOrder AS o LEFT JOIN tblSite AS s ON o.SiteGUID=s.GUID WHERE o.OrderID = {0} AND o.UserID = {1}", orderID, userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SaleOrderInfo>();
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SaleOrderInfo GetInfo(string orderCode)
        {
            string sql = string.Format("SELECT TOP 1 OrderID,GUID,OrderCode,UserID,SiteGUID,OrderTime,OrderDate,RequiredDate,RequiredDinnerType,TotalAmount,CouponCode,CouponAmount,PaymentAmount,PaymentID,IsPaid,PaidTime,PayTransCode,Status,Comments FROM tblSaleOrder WHERE OrderCode = '{0}'", orderCode);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SaleOrderInfo>();
            }
        }

        public SaleOrderInfo GetInfoByOutTradeNo(string outTradeNo)
        {
            string sql = string.Format("SELECT TOP 1 o.* FROM tblSaleOrder o LEFT JOIN tblSite s ON s.GUID=o.SiteGUID WHERE s.Code+o.OrderCode = '{0}'", outTradeNo);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<SaleOrderInfo>();
            }
        }


        public List<SaleOrderInfo> GetList(string filter)
        {
            string sql = string.Format("SELECT OrderID,GUID,OrderCode,UserID,SiteGUID,OrderTime,OrderDate,RequiredDate,RequiredDinnerType,TotalAmount,CouponCode,CouponAmount,PaymentAmount,PaymentID,IsPaid,PaidTime,PayTransCode,Status,Comments FROM tblSaleOrder WHERE IsDel = 0 {0}", filter);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SaleOrderInfo>();
            }
        }

        public List<SaleOrderInfo> GetList(int userID,string language)
        {
            string sql = string.Format("SELECT OrderID,GUID,OrderCode,UserID,SiteGUID,OrderTime,OrderDate,RequiredDate,TotalAmount,CouponCode,CouponAmount,PaymentAmount,PaymentID,IsPaid,PaidTime,PayTransCode,Status,Comments FROM tblSaleOrder WHERE IsDel = 0 AND UserID = {0}", userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SaleOrderInfo>();
            }
        }
        public List<SaleOrderUserInfo> GetPaiedUserList()
        {
            string sql = string.Format("select a2.WechatID from tblSaleOrder a1 join tblUser a2 on a1.UserID=a2.UserID where a1.IsPaid=1 "
                + "and a1.Status=20 and Convert(varchar(10),a1.RequiredDate,120)='{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SaleOrderUserInfo>();
            }
        }


        public List<SaleOrderInfo> GetNotPaidList(int userID)
        {
            string sql = string.Format("SELECT OrderID,GUID,OrderCode,UserID,SiteGUID,OrderTime,OrderDate,RequiredDate,TotalAmount,CouponCode,CouponAmount,PaymentAmount,PaymentID,IsPaid,PaidTime,PayTransCode,Status,Comments FROM tblSaleOrder WHERE IsDel = 0 AND IsPaid = 0 AND CouponCode != '' AND UserID = {0}", userID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<SaleOrderInfo>();
            }
        }

        /// <summary>
        /// 返回当前页码的数据集表
        /// </summary> 
        /// <returns>当前页码的数据集</returns>
        public DataTable GetPage(SaleOrderFilter filter, out int recordCount)
        {
            var fileds = "s.OrderID,s.GUID,s.OrderCode,s.UserID,s.SiteGUID,s.OrderTime,s.OrderDate,s.RequiredDate,s.TotalAmount,s.CouponCode,s.CouponAmount,s.PaymentAmount,s.PaymentID,s.IsPaid,s.PaidTime,s.PayTransCode,CASE s.Status WHEN 10 THEN '已发货' WHEN 20 THEN '未发货' ELSE '' END AS Status,s.ShippedDate,u.FirstName+u.LastName AS UserName,u.WechatID,u.Mobile";
            var table = "tblSaleOrder AS s LEFT JOIN tblUser AS u ON s.UserID=u.UserID";
            var order = "s.OrderID DESC";
            var builder = new StringBuilder(" ( s.IsDel = 0 ) ");

            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                builder.Append(filter.Keyword);
            }

            return SqlHelper.GetPage(SqlHelper.dbAden, table, filter.PageSize, filter.PageIndex, fileds, builder.ToString(), order, out recordCount).Tables[0];
        }

        public bool PaymentSuccess(string orderCode, string payTransCode)
        {
            string sql = string.Format("UPDATE tblSaleOrder SET IsPaid= 1,PaidTime='{0}',PayTransCode='{1}' WHERE OrderCode = '{2}'", DateTime.Now.ToString(), payTransCode, orderCode);

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        public bool Shipped(SaleOrderInfo info, List<StockTransactionInfo> stock, int adminID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.dbAden))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    StockTransaction dalStock = new StockTransaction();

                    //新增
                    foreach (var item in stock)
                    {
                        if (dalStock.Add(item, trans) == 0)
                        {
                            trans.Rollback();
                            return false;
                        }
                    }

                    if (!UpdateStatus(info.OrderID, (int)SaleOrderInfo.StatusEnum.YFH, adminID, trans))
                    {
                        trans.Rollback();
                        return false;
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
    }
}

