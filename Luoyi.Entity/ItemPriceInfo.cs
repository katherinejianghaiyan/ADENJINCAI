
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 物料价格
	/// </summary>
	[Serializable]
	public class ItemPriceInfo
	{
		/// <summary>
		/// 编号
		/// </summary>
		public int ID
		{
			set;
			get;
		}
		/// <summary>
		/// 物料ID
		/// </summary>
		public string ItemGUID
		{
			set;
			get;
		}
		/// <summary>
		/// BUGUID
		/// </summary>
		public string BUGUID
		{
			set;
			get;
		}
        /// <summary>
        /// SupplierGUID
        /// </summary>
        public string SupplierGUID
        {
            set;
            get;
        }
		/// <summary>
		/// 开始日期
		/// </summary>
		public int StartDate
		{
			set;
			get;
		}
		/// <summary>
		/// 结束日期
		/// </summary>
		public int EndDate
		{
			set;
			get;
		}
		/// <summary>
		/// 价格
		/// </summary>
		public decimal Price
		{
			set;
			get;
		}
		/// <summary>
		/// 价格类型
		/// </summary>
		public string PriceType
		{
			set;
			get;
		}

	}
}

