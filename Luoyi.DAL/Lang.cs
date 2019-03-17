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
    public class Lang
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LangInfo GetInfo(string pageName, string controlID)
        {
            string sql = string.Format("SELECT TOP 1 ID,PageName,ControlID,ZHCNText,ENUSText,Remark FROM tblLang WHERE PageName= '{0}' AND ControlID='{1}'", pageName, controlID);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<LangInfo>();
            }
        }

        public List<LangInfo> GetInfo()
        {
            string sql = "SELECT ID,isnull(PageName,'') pagename,isnull(ControlID,'') controlid,ZHCNText,ENUSText,Remark FROM tblLang";
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntityList<LangInfo>();
            }
        }
    }
}

