using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class User
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.User dal = new DAL.User();

        /// <summary>
        /// 增加一条数据 
        /// 返回-1表示不符合注册条件的用户
        /// </summary>
        public static int Add(UserInfo info)
        {
            // 检查用户是否可注册
            if (dal.checkRegister(info))  return dal.Add(info);
            return -1;
        }

        public static void UserPageLog(UserInfo info, string pageName)
        {
            dal.UserPageLog(info, pageName);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static int Update(UserInfo info)
        {
            if (dal.checkRegister(info)) return dal.Update(info);
            return -1;
        }

        public static void UpdateLanguage(int userID, string langCode)
        {
            (new System.Threading.Thread(() =>
            {
                dal.UpdateLanguage(userID, langCode);
            })).Start();
            
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int userID)
        {
            return dal.Del(userID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static UserInfo GetInfo(int userID)
        {
            return dal.GetInfo(userID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetRealName(int userID)
        {
            return dal.GetRealName(userID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wechatID"></param>
        /// <returns></returns>
        public static UserInfo GetInfo(string wechatID)
        {
            return dal.GetInfo(wechatID);
        }

        public static List<UserInfo> GetList()
        {
            return dal.GetList();
        }

        public static bool UserCostCenter(string wechatID)
        {
            return dal.UserCostCenter(wechatID);
        }

        public static WechatConfig WeChatConfig(string appName)
        {
            return dal.WeChatConfig(appName);
        }

    }
}
