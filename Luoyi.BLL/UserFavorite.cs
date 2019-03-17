using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class UserFavorite
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.UserFavorite dal = new DAL.UserFavorite();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(UserFavoriteInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(UserFavoriteInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int userID, string itemGUID)
        {
            return dal.Del(userID, itemGUID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static UserFavoriteInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<UserFavoriteInfo> GetList(int userID)
        {
            return dal.GetList(userID);
        }

        public static bool Exists(string itemGUID, int userID)
        {
            return dal.Exists(itemGUID, userID);
        }
    }
}
