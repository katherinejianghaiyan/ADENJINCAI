using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 供应商
	/// </summary>
	[Serializable]
	public class SupplierInfo
	{
		/// <summary>
		/// 供应商编号
		/// </summary>
		public int ID
		{
			set;
			get;
		}
		/// <summary>
		/// 供应商GUID
		/// </summary>
		public string SupplierGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 公司名中文
		/// </summary>
		public string CompNameCn
		{
			set;
			get;
		}
		/// <summary>
		/// 公司名英文
		/// </summary>
		public string CompNameEn
		{
			set;
			get;
		}
		/// <summary>
		/// 公司地址
		/// </summary>
		public string Address
		{
			set;
			get;
		}
		/// <summary>
		/// 邮编
		/// </summary>
		public string PostCode
		{
			set;
			get;
		}
		/// <summary>
		/// 电话总机
		/// </summary>
		public string TelNbr
		{
			set;
			get;
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public string ContactName
		{
			set;
			get;
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string EmailAddress
		{
			set;
			get;
		}
		/// <summary>
		/// 手机号
		/// </summary>
		public string MobileNbr
		{
			set;
			get;
		}
		/// <summary>
		/// 是否停用
		/// </summary>
		public bool Active
		{
			set;
			get;
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set;
			get;
		}
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel
        {
            set;
            get;
        }
	}
}

