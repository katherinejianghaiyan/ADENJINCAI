using System.Collections.Generic;
using System.Data;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class ItemBom
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.ItemBom dal = new DAL.ItemBom();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ItemBomInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(ItemBomInfo info)
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
        public static ItemBomInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<ItemBomInfo> GetList()
        {
            return dal.GetList();
        }

        public static DataTable GetTable(string productGUID, string language = "")
        {
            return dal.GetTable(productGUID,language);
        }

        public static DataView GetRecipe(string productGUID, string language)
        {
            DataView dv = GetTable(productGUID, language).DefaultView;
            dv.RowFilter = string.Format("ItemType <> 'Expense'");
            return dv;
        }
    }
}
