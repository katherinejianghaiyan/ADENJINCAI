using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class BU
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.BU dal = new DAL.BU();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(BUInfo info)
        {
            return dal.Add(info);

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(BUInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int id)
        {
            return dal.Del(id);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static BUInfo GetInfo(int id)
        {
            return dal.GetInfo(id);
        }

        public static BUInfo GetInfo(string buguid)
        {
            return dal.GetInfo(buguid);
        }

        public static List<BUInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
