using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;
using Luoyi.Common;
using Luoyi.Cache;

namespace Luoyi.BLL
{
    public class SiteAdmin
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.SiteAdmin dal = new DAL.SiteAdmin();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SiteAdminInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SiteAdminInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SiteAdminInfo GetInfo(int id)
        {
            return dal.GetInfo(id);
        }

        public static List<SiteAdminInfo> GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="userName">登录名称</param>
        /// <param name="userPwd">登录密码</param>
        /// <returns>登录成功返回管理员ID，失败返回 0 </returns>
        public static int Signin(string userName, string userPwd)
        {
            return dal.Signin(userName, userPwd);
        }
    }
}
