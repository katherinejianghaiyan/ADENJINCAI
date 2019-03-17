using System;

namespace Luoyi.Entity
{
    /// <summary>
    /// 注册用户
    /// </summary>
    [Serializable]
    public class UserInfo : SiteInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set;
            get;
        }
        /// <summary>
        /// 微信ID
        /// </summary>
        public string WechatID
        {
            set;
            get;
        }

        public string UnionID
        {
            set;
            get;
        }
        /// <summary>
        /// 姓
        /// </summary>
        public string FirstName
        {
            set;
            get;
        }
        /// <summary>
        /// 名
        /// </summary>
        public string LastName
        {
            set;
            get;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set;
            get;
        }
        /// <summary>
        /// 营运点GUID
        /// </summary>
        public string SiteGUID
        {
            set;
            get;
        }
        /// <summary>
        /// 出生年月
        /// </summary>
        public int BirthDay
        {
            set;
            get;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            set;
            get;
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            set ;
            get;
        }

        /// <summary>
        /// 送货地址
        /// </summary>
        public string ShipToAddr
        {
            set;
            get;
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            set;
            get;
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set;
            get;
        }
        public string Section
        {
            set;
            get;
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position
        {
            set;
            get;
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime
        {
            set;
            get;
        }
        /// <summary>
        /// 注册日期
        /// </summary>
        public int CreateDate
        {
            set;
            get;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            set;
            get;
        }
        /// <summary>
        /// 扩展字段-微信头像
        /// </summary>
        public string HeaderImgUrl
        {
            set;
            get;
        }

    
    }
}

