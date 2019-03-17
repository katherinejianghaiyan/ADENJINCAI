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
    public class CouponTicket
    {

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CouponTicketInfo GetInfo(string verifyCode)
        {
            string sql = string.Format("SELECT TOP 1 t.TicketID,t.CouponGUID,t.VerifyCode,t.UseBeginTime,t.UseEndTime FROM tblCouponTicket t LEFT JOIN tblCoupon c ON c.GUID=t.CouponGUID WHERE t.VerifyCode = '{0}' AND t.UseBeginTime <='{1}' AND t.UseEndTime >= '{1}' AND t.Qty > 0", verifyCode, DateTime.Now.ToString());
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<CouponTicketInfo>();
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool UpdateQty(int ticketID, int qty)
        {
            string sql = string.Format("UPDATE tblCouponTicket SET Qty=Qty+({0}) WHERE TicketID = {1}", qty, ticketID);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }
    }
}

