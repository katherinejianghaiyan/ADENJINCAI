using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class PurchaseOrder
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.PurchaseOrder dal = new DAL.PurchaseOrder();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(PurchaseOrderInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(PurchaseOrderInfo info)
        {
            return dal.Update(info);
        }

        public static List<PurchaseOrderInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
