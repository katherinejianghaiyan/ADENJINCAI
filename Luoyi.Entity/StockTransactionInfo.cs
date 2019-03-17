using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 物料流水
	/// </summary>
	[Serializable]
	public class StockTransactionInfo
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
		/// 用户ID
		/// </summary>
		public int UserID
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
		/// SO订单明细行ID
		/// </summary>
		public string SODetailGUID
		{
			set;
			get;
		}
		/// <summary>
		/// PO单明细行ID
		/// </summary>
		public string PODetailGUID
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
		/// 单价
		/// </summary>
		public decimal Cost
		{
			set;
			get;
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string UOMGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 数量
		/// </summary>
		public int Qty
		{
			set;
			get;
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime CreateTime
		{
			set;
			get;
		}

        public int CreateUser { get; set; }

	}
}

