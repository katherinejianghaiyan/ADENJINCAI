﻿using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class UOM
    {
        /// <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.UOM dal = new DAL.UOM();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(UOMInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(UOMInfo info)
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
        public static UOMInfo GetInfo(int iD)
        {
            return dal.GetInfo(iD);
        }


        public static List<UOMInfo> GetList()
        {
            return dal.GetList();
        }
    }
}
