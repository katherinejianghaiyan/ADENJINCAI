using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class PromotedItem
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.PromotedItem dal = new DAL.PromotedItem();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PromotedItemInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(PromotedItemInfo info)
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
        public static PromotedItemInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }


        public static List<PromotedItemInfo> GetList(string buGUID)
        {
            return dal.GetList(buGUID) ?? new List<PromotedItemInfo>();
        }
    }
}
