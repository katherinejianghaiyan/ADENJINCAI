using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 计量单位表
	/// </summary>
	[Serializable]
	public class UOMInfo
	{
		/// <summary>
		/// 自动编号
		/// </summary>
		public int ID
		{
			set;
			get;
		}
		/// <summary>
		/// 单位GUID
		/// </summary>
		public string GUID
		{
			set;
			get;
		}
		/// <summary>
		/// 单位名称-中文
		/// </summary>
		public string NameCn
		{
			set;
			get;
		}
		/// <summary>
		/// 单位名称-英文
		/// </summary>
		public string NameEn
		{
			set;
			get;
		}
		/// <summary>
		/// 转换单位
		/// </summary>
		public string ToUOMGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 转换系数
		/// </summary>
		public int ToQty
		{
			set;
			get;
		}

	}
}

