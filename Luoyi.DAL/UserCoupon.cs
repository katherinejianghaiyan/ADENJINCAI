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
    public class UserCoupon
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(UserCouponInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblUserCoupon(");
            builder.Append("UserID,CouponGUID,StartTime,StopTime,Qty,CouponCode)");
            builder.Append("VALUES (");
            builder.Append("@UserID,@CouponGUID,@StartTime,@StopTime,@Qty,@CouponCode)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@UserID",SqlDbType.Int,4) {Value =  info.UserID},
					 new SqlParameter("@CouponGUID",SqlDbType.Char,36) {Value =  info.CouponGUID},
					 new SqlParameter("@StartTime",SqlDbType.DateTime) {Value =  info.StartTime},
					 new SqlParameter("@StopTime",SqlDbType.DateTime) {Value =  info.StopTime},
					 new SqlParameter("@Qty",SqlDbType.Int,4) {Value =  info.Qty},
					 new SqlParameter("@CouponCode",SqlDbType.Char,16) {Value =  info.CouponCode ?? ""}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool UpdateQty(int id, int qty)
        {
            string sql = string.Format("UPDATE tblUserCoupon SET Qty=Qty+({0}) WHERE ID = {1}", qty, id);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 购物车默认获取Coupon
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="buGuid"></param>
        /// <param name="orderAmt"></param>
        /// <returns></returns>
        public UserCouponInfo GetDefaultUseCouponInfo(int userID, string buGuid, decimal orderAmt)
        {

            string sql = string.Format("SELECT TOP 1 u.ID,u.UserID,u.CouponGUID,u.StartTime,u.StopTime,u.Qty,c.CouponID,c.GUID,c.Image,c.Amount,c.UseBeginTime,c.UseEndTime,c.IsUseDown FROM tblUserCoupon u LEFT JOIN tblCoupon c ON c.GUID = u.CouponGUID LEFT JOIN tblCouponRule r ON (r.DeductAmt = c.Amount OR (r.DeductAmt < c.Amount AND c.IsUseDown = 1)) WHERE u.UserID = {0} AND r.OrderAmt <= {1} AND r.UseBeginTime <= '{2}' AND r.UseEndTime >= '{2}' AND c.UseBeginTime <= '{2}' AND c.UseEndTime >= '{2}' AND u.Qty > 0 AND u.StartTime <= '{2}' AND u.StopTime >= '{2}' and r.buguid='{3}' ORDER BY c.Amount DESC,u.StopTime ASC", userID, orderAmt, DateTime.Now.ToString(), buGuid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserCouponInfo>();
            }
        }

        public UserCouponInfo GetInfo(int id)
        {
            string sql = string.Format("SELECT TOP 1 u.ID,u.UserID,u.CouponGUID,u.StartTime,u.StopTime,u.Qty,c.CouponID,c.GUID,c.Image,c.Amount,c.UseBeginTime,c.UseEndTime,c.IsUseDown FROM tblUserCoupon u LEFT JOIN tblCoupon c ON c.GUID = u.CouponGUID WHERE u.ID = {0}", id);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserCouponInfo>();
            }
        }

        public UserCouponInfo GetInfo(int userID, string couponGUID)
        {
            string sql = string.Format("SELECT TOP 1 u.ID,u.UserID,u.CouponGUID,u.StartTime,u.StopTime,u.Qty,c.CouponID,c.GUID,c.Image,c.Amount,c.UseBeginTime,c.UseEndTime,c.IsUseDown FROM tblUserCoupon u LEFT JOIN tblCoupon c ON c.GUID = u.CouponGUID WHERE u.UserID = {0} AND u.CouponGUID = '{1}'", userID, couponGUID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserCouponInfo>();
            }
        }

        public UserCouponInfo GetInfo(int userID, string couponGUID, string couponCode, DateTime StartTime, DateTime StopTime)
        {
            string sql = string.Format("SELECT TOP 1 u.ID,u.UserID,u.CouponGUID,u.StartTime,u.StopTime,u.Qty,c.CouponID,c.GUID,c.Image,c.Amount,c.UseBeginTime,c.UseEndTime,c.IsUseDown FROM tblUserCoupon u LEFT JOIN tblCoupon c ON c.GUID = u.CouponGUID WHERE u.UserID = {0} AND u.CouponGUID = '{1}' AND u.CouponCode = '{2}' AND u.StartTime = '{3}' AND u.StopTime = '{4}'", userID, couponGUID, couponCode, StartTime.ToString(), StopTime.ToString());
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<UserCouponInfo>();
            }
        }

        /// <summary>
        /// 个人中心中所有的券
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<UserCouponInfo> GetList(int userID, string buGuid)
        {
            string sql = string.Format("SELECT u.ID,u.UserID,u.CouponGUID,u.StartTime,u.StopTime,u.Qty,c.CouponID,c.GUID,c.Image,c.Amount,c.UseBeginTime,c.UseEndTime,c.IsUseDown FROM tblUserCoupon u LEFT JOIN tblCoupon c ON c.GUID = u.CouponGUID LEFT JOIN tblCouponRule r ON (r.DeductAmt = c.Amount OR (r.DeductAmt < c.Amount AND c.IsUseDown = 1)) WHERE u.UserID = {0} AND r.UseBeginTime <= '{1}' AND r.UseEndTime >= '{1}' AND c.UseBeginTime <= '{1}' AND c.UseEndTime >= '{1}' AND u.Qty > 0 AND u.StartTime <= '{1}' AND u.StopTime >= '{1}' and r.buguid='{2}'", userID, DateTime.Now.ToString(), buGuid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UserCouponInfo>();
            }
        }

        /// <summary>
        ///  购物车中显示优惠券
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="buGuid"></param>
        /// <param name="orderAmt"></param>
        /// <returns></returns>
        public List<UserCouponInfo> GetList(int userID, string buGuid, decimal orderAmt)
        {
            string sql = string.Format("SELECT u.ID,u.UserID,u.CouponGUID,u.StartTime,u.StopTime,u.Qty,c.CouponID,c.GUID,c.Image,c.Amount,c.UseBeginTime,c.UseEndTime,c.IsUseDown FROM tblUserCoupon u LEFT JOIN tblCoupon c ON c.GUID = u.CouponGUID LEFT JOIN tblCouponRule r ON (r.DeductAmt = c.Amount OR (r.DeductAmt < c.Amount AND c.IsUseDown = 1)) WHERE u.UserID = {0} AND r.OrderAmt <= {1} AND r.UseBeginTime <= '{2}' AND r.UseEndTime >= '{2}' AND c.UseBeginTime <= '{2}' AND c.UseEndTime >= '{2}' AND u.Qty > 0 AND u.StartTime <= '{2}' AND u.StopTime >= '{2}' and r.buguid='{3}'", userID, orderAmt, DateTime.Now.ToString(), buGuid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UserCouponInfo>();
            }
        }
    }
}

