using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class StockTransaction
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.StockTransaction dal = new DAL.StockTransaction();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(StockTransactionInfo info)
        {
            return dal.Add(info, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(List<StockTransactionInfo> list)
        {
            return dal.Add(list);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(StockTransactionInfo info)
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
        public static StockTransactionInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<StockTransactionInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
