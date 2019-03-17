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
    public class Coupon
    {

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CouponInfo GetInfo(string guid)
        {
            string sql = string.Format("SELECT TOP 1 * FROM tblCoupon WHERE GUID = '{0}'", guid);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<CouponInfo>();
            }
        }

        public List<CouponInfo> GetList(string filter)
        {
            string sql = "SELECT * FROM tblCoupon ";
            if (!string.IsNullOrEmpty(filter))
            {
                sql += string.Format(" WHERE {0}", filter);
            }
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CouponInfo>();
            }
        }
        public List<CouponInfo> GetBUList(string BUGUID)
        {
            //有抵扣规则，则送优惠券
            string sql = "select a1.* from tblcoupon a1, tblcouponrule a2 where a1.amount=a2.DeductAmt and a2.BUGUID='{0}' "
                + "and a1. UseBeginTime <= getdate() and a1.UseEndTime >= getdate() and a2.UseBeginTime <= getdate() and a2.UseEndTime >= getdate()";
            sql = string.Format(sql, BUGUID);
                // "select * from tblCoupon where UseBeginTime<=getdate() and UseEndTime>=getdate()";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CouponInfo>();
            }
        }
    }
}

