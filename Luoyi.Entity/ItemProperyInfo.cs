
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 物料性质
	/// </summary>
	[Serializable]
	public class ItemProperyInfo
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
		/// 所属物料
		/// </summary>
		public string ItemGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 类型字典代号
		/// </summary>
		public string DictCode
		{
			set;
			get;
		}
		/// <summary>
		/// 属性名称
		/// </summary>
		public string PropName
		{
			set;
			get;
		}
		/// <summary>
		/// 属性值
		/// </summary>
		public string PropValue
		{
			set;
			get;
		}

	}
}

