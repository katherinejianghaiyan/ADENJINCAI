
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// BOM
	/// </summary>
	[Serializable]
	public class ItemBomInfo
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
		/// 成品GUID
		/// </summary>
		public string ProductGUID
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
		/// 标准数量
		/// </summary>
		public int StdQty
		{
			set;
			get;
		}
		/// <summary>
		/// 实际数量
		/// </summary>
		public int ActQty
		{
			set;
			get;
		}
		/// <summary>
		/// 数量单位
		/// </summary>
		public string UOMID
		{
			set;
			get;
		}

	}
}

