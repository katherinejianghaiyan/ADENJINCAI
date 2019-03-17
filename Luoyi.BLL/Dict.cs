using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class Dict
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.Dict dal = new DAL.Dict();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DictInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DictInfo info)
        {
            return dal.Update(info);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DictInfo GetInfo(string code)
        {
            return dal.GetInfo(code);
        }

        public static List<DictInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
