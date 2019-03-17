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
    public class CouponRule
    {

        public CouponRuleInfo GetInfo(string buguid, decimal orderAmt)
        {
            string sql = string.Format("SELECT TOP 1 RuleID,BUGUID,OrderAmt,DeductAmt,UseBeginTime,UseEndTime FROM tblCouponRule WHERE BUGUID='{0}' AND UseBeginTime <= '{1}' AND UseEndTime >='{1}' AND OrderAmt <= {2} ORDER BY OrderAmt DESC", buguid, DateTime.Now.ToString(), orderAmt);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<CouponRuleInfo>();
            }
        }

        public List<CouponRuleInfo> GetList(string buguid)
        {
            string sql = string.Format("SELECT RuleID,BUGUID,OrderAmt,DeductAmt,UseBeginTime,UseEndTime FROM tblCouponRule WHERE BUGUID='{0}' AND UseBeginTime <= '{1}' AND UseEndTime >='{1}'", buguid, DateTime.Now.ToString());
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CouponRuleInfo>();
            }
        }

        public List<CouponRuleInfo> GetList(string buguid, decimal orderAmt)
        {
            string sql = string.Format("SELECT RuleID,BUGUID,OrderAmt,DeductAmt,UseBeginTime,UseEndTime FROM tblCouponRule WHERE BUGUID='{0}' AND UseBeginTime <= '{1}' AND UseEndTime >='{1}' AND OrderAmt <= {2}", buguid, DateTime.Now.ToString(), orderAmt);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CouponRuleInfo>();
            }
        }
    }
}

