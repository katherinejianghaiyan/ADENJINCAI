
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// PO订单
	/// </summary>
	[Serializable]
	public class PurchaseOrderInfo
	{
		/// <summary>
		/// 订单编号
		/// </summary>
		public int OrderID
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
		/// 运营点ID
		/// </summary>
		public string SiteGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 订单日期
		/// </summary>
		public int OrderDate
		{
			set;
			get;
		}
		/// <summary>
		/// PO采购单号
		/// </summary>
		public string OrderCode
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
		/// 要求交货日期
		/// </summary>
		public int RequiredDate
		{
			set;
			get;
		}
		/// <summary>
		/// 是否关闭
		/// </summary>
		public bool IsClosed
		{
			set;
			get;
		}

	}
}

