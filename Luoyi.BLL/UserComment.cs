
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Luoyi.DAL;
using Luoyi.Entity;
using Luoyi.Common;

namespace Luoyi.BLL
{
    public class UserComment
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.UserComment dal = new DAL.UserComment();

        public static List<UserCommentInfo> GetData(string userId)
        {
            string sql = "select replace(comments,'\r\n','<br>') content, createtime commenttime "
                + "from tblcomments (nolock) where userid={0} order by id desc";
            sql = string.Format(sql, userId);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.dbAden, CommandType.Text, sql))
            {
                return reader.ToEntityList<UserCommentInfo>();
            }
        }
        public static bool SUZHYCAdd(UserCommentInfo info)
        {
            string sql = string.Format("insert into tblComments (SiteGuid,Comments,UserId) values "
                        + "('{0}','{1}',{2})",info.SiteGUID,info.Content,info.UserID);

            return SqlHelper.ExecuteSql(SqlHelper.dbAden, CommandType.Text, sql) > 0;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(UserCommentInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(UserCommentInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int iD)
        {
            return dal.Del(iD);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static UserCommentInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<UserCommentInfo> GetList(string filter)
        {
            return dal.GetList(filter) ?? new List<UserCommentInfo>();
        }

        public static DataTable GetTable(string filter)
        {
            return dal.GetTable(filter);
        }


    }
}
