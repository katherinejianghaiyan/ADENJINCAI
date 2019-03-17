using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class SaleOrderCart
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.SaleOrderCart dal = new DAL.SaleOrderCart();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(SaleOrderCartInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(SaleOrderCartInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 更新购物车数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="qty"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool UpdateQty(int id, int qty, int userID)
        {
            return dal.UpdateQty(id, qty, userID);
        }

        public static bool UpdateIsBuy(int id, bool isBuy, int userID)
        {
            return dal.UpdateIsBuy(id, isBuy, userID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Del(int iD)
        {
            return dal.Del(iD);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static bool EmptyCart(int userID)
        {
            return dal.EmptyCart(userID);
        }

        /// <summary>
        /// 清空优惠商品
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EmptyPromotion(int userID)
        {
            return dal.EmptyPromotion(userID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static SaleOrderCartInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<SaleOrderCartInfo> GetList(int userID, string language)
        {
            return dal.GetList(userID,language) ?? new List<SaleOrderCartInfo>();
        }
    }
}
