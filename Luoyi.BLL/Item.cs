using System.Collections.Generic;
using System.Data;
using System.Linq;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Item
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.Item dal = new DAL.Item();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ItemInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(ItemInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ItemInfo GetInfo(int itemID,string language ="")
        {
            return dal.GetInfo(itemID,language);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ItemInfo GetInfo(string guid)
        {
            return dal.GetInfo(guid);
        }

        public static List<ItemInfo> GetList(string filter)
        {
            return dal.GetList(filter);
        }

        public static DataTable GetPage(ItemFilter filter,  out int recordCount)
        {
            return dal.GetPage(filter, out recordCount);
        }

        public static DataTable GetPage(ItemFilter filter, string language, out int recordCount)
        {
            language = language.Replace("_", "");
            DataTable dt =  dal.GetPage(filter, out recordCount);
            dt.Columns["itemname"].Expression = string.Format("iif(isnull(itemname{0},'')='',itemnamezhcn,itemname{0})", language);
            return dt;
        }

        public static Dictionary<int, string> GetSortDict()
        {
            var list = EnumHelper.GetTypeItemList<Enums.ItemSortEnum>();
            return list.ToDictionary(item => item.Value.ToType<int>(), item => item.Description);
        }
    }
}
