using System.Collections.Generic;
using System.Data;
using System.Linq;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class ItemCustomClass
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.ItemCustomClass dal = new DAL.ItemCustomClass();

        public static ItemCustomClassInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }

        public static List<ItemCustomClassInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
