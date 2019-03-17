using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 供应商-营运点
	/// </summary>
	[Serializable]
	public class SupplierSiteInfo
	{
		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set;
			get;
		}
		/// <summary>
		/// 供应商ID
		/// </summary>
		public string SupplierGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 公司ID(与营运点ID二选一)
		/// </summary>
		public string BUGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 营运点ID
		/// </summary>
		public string SiteGUID
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

	}
}

