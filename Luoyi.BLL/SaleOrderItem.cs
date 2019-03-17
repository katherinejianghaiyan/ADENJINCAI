using System.Collections.Generic;
using System.Data;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class SaleOrderItem
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.SaleOrderItem dal = new DAL.SaleOrderItem();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(SaleOrderItemInfo info)
        {
            return dal.Add(info, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SaleOrderItemInfo info)
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
        public static SaleOrderItemInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<SaleOrderItemInfo> GetList(string filter)
        {
            return dal.GetList(filter);
        }

        public static DataTable GetTable(string filter, string language="")
        {
            return dal.GetTable(filter,language);
        }

        /// <summary>
        /// 更新删除状态
        /// </summary>
        public static bool UpdateIsComment(int id, bool isComment)
        {
            return dal.UpdateIsComment(id, isComment);
        }

        public static bool UpdateIsPrint(int id, bool isPrint)
        {
            return dal.UpdateIsPrint(id, isPrint);
        }
    }
}
