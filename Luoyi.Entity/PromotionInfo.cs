
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 优惠活动
	/// </summary>
	[Serializable]
	public class PromotionInfo
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
		/// GUID
		/// </summary>
		public string GUID
		{
			set;
			get;
		}
		/// <summary>
		/// 运营点GUID
		/// </summary>
		public string SiteGUID
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
		/// 起始日期
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
		/// 订单需满N金额
		/// </summary>
		public decimal MinOrderAmt
		{
			set;
			get;
		}
		/// <summary>
		/// 允许最多购买数量
		/// </summary>
		public int MaxQty
		{
			set;
			get;
		}

	}
}

