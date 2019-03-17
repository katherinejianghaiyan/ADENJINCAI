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
    public class CouponRelease
    {

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CouponReleaseInfo GetInfo(string buguid)
        {
            string sql = string.Format("SELECT TOP 1 * FROM tblCouponRelease WHERE BUGUID = '{0}' AND BeginTime <= '{1}' AND EndTime >='{1}' ORDER BY ID DESC", buguid, DateTime.Now.ToString());
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<CouponReleaseInfo>();
            }
        }

        public List<CouponReleaseInfo> GetList(string buguid)
        {
            string sql = string.Format("SELECT * FROM tblCouponRelease WHERE BUGUID = '{0}' AND BeginTime <= '{1}' AND EndTime >='{1}'", buguid, DateTime.Now.ToString());

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<CouponReleaseInfo>();
            }
        }
    }
}

