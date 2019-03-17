
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 物料的供应商
	/// </summary>
	[Serializable]
	public class ItemSupplierInfo
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
		/// 物料GUID
		/// </summary>
		public string ItemGUID
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
		/// 开始日期
		/// </summary>
		public int StartDate
		{
			set;
			get;
		}
		/// <summary>
		/// 起始日期
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

	}
}

