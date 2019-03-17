using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class ItemPrice
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.ItemPrice dal = new DAL.ItemPrice();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ItemPriceInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(ItemPriceInfo info)
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
        public static ItemPriceInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static ItemPriceInfo GetInfo(string siteGUID,string itemGUID, int startDate, int endDate, string priceType)
        {
            return dal.GetInfo(siteGUID,itemGUID, startDate, endDate, priceType);
        }

        public static List<ItemPriceInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
