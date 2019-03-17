using System.Collections.Generic;
using System.Linq;
using Luoyi.Entity;

namespace Luoyi.BLL
{
    public class SUZHYC
    {
        // <summary>
        /// 从工厂里面创建产品类的数据访问对象
        /// </summary>
        private readonly static DAL.SUZHYC dal = new DAL.SUZHYC();

        /// <summary>
        /// 读取SUZHYC的海报图片
        /// </summary>
        /// <param name="siteGuid"></param>
        /// <param name="businessType"></param>
        /// <returns></returns>
        public static List<PostPic> GetPost(string siteGuid,string businessType)
        {
            return dal.GetPost(siteGuid, businessType);
        }

    }
}