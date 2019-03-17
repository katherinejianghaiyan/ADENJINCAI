using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.DAL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class ItemClass
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ItemClassInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("INSERT INTO tblItemClass(");
            builder.Append("GUID,ClassName,Sort)");
            builder.Append("VALUES (");
            builder.Append("@GUID,@ClassName,@Sort)");
            builder.Append(";SELECT @@IDENTITY");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32) {Value =  info.GUID},
					 new SqlParameter("@ClassName",SqlDbType.VarChar,32) {Value =  info.ClassName},
					 new SqlParameter("@Sort",SqlDbType.Int,4) {Value =  info.Sort}
			};

            var obj = SqlHelper.ExecuteScalar(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray());
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ItemClassInfo info)
        {
            var builder = new StringBuilder();
            builder.Append("UPDATE tblItemClass SET ");
            builder.Append("GUID=@GUID,");
            builder.Append("ClassName=@ClassName,");
            builder.Append("Sort=@Sort ");
            builder.Append("WHERE ID=@ID");

            var lstParameters = new List<SqlParameter>
			{
					 new SqlParameter("@GUID",SqlDbType.VarChar,32){Value =  info.GUID},
					 new SqlParameter("@ClassName",SqlDbType.VarChar,32){Value =  info.ClassName},
					 new SqlParameter("@Sort",SqlDbType.Int,4){Value =  info.Sort},
					 new SqlParameter("@ID",SqlDbType.Int,4){Value =  info.ID}
			};

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, builder.ToString(), lstParameters.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Del(int iD)
        {
            string sql = string.Format("DELETE FROM tblItemClass WHERE ID = {0}", iD);
            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ItemClassInfo GetInfo(int iD)
        {
            string sql = string.Format("SELECT TOP 1 ID,GUID,ClassName,Sort FROM tblItemClass WHERE ID = {0}", iD);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                if (!reader.HasRows) return null;
                return reader.ToEntity<ItemClassInfo>();
            }
        }


        public List<ItemClassInfo> GetList(string buGUID, string siteGUID,string language)
        {
            language = language.Replace("_", "");
            //string sql = "SELECT ID,GUID,ClassName,Sort FROM tblItemClass where display=1 ORDER BY Sort DESC";
            string sql = "select distinct a3.guid,case isnull(classname{2},'') when '' then classname else classname{2} end classname," +
                "a3.sort,a2.siteguid,a2.BUGUID,'' pagepath from tblitem a1,tblitemprice a2,tblItemClass a3 "
                + "where a1.guid = a2.ItemGUID and a1.ClassGUID = a3.GUID and a1.isdel=0 and "
                + "isnull(a2.startdate, '20010101') <= convert(varchar(8), getdate(), 112) and "
                + "isnull(a2.enddate,'22221231') >= convert(varchar(8), getdate(), 112) and "
                + "(isnull(a2.buguid, '') = '{0}' or isnull(a2.siteguid, '') = '{1}') "
                + "union " +
                "select distinct  guid,case isnull(classname{2},'') when '' then classname else classname{2} end classname," +
                "sort,siteguid, null BUGUID," +
                "isnull(pagepath,isnull(img{2},imgzhcn)) pagepath " +   
                //"isnull(img{2},imgzhcn) pagepath " +
                "from tblitemclass " +
                "where isnull(siteguid, '') ='{1}' "
                + "order by sort";

            sql = string.Format(sql, buGUID, siteGUID,language);
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                List<ItemClassInfo> list = reader.ToEntityList<ItemClassInfo>();
                if (list == null) return null;
                if (list.Where(q => q.SiteGUID == siteGUID).Any()) return list.Where(q => q.SiteGUID == siteGUID).ToList();
                return list;
            }
        }
    }
}

