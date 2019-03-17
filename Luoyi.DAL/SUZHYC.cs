using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Luoyi.Common;
using Luoyi.Entity;



namespace Luoyi.DAL
{
    public class SUZHYC
    {
        //读取SUZHYC的海报图片
        public List<PostPic> GetPost(string siteGuid,string businessType)
        {
            string sql = string.Format("select top 1 val1 as picUrl,startdate "
                + "from tblDatas (nolock) where siteguid='{0}' and BusinessType='{1}' order by startdate desc", 
                siteGuid,businessType);

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<PostPic>();
            }
        }

    }
}