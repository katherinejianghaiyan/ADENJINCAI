
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 数据字典
	/// </summary>
	[Serializable]
	public class DictInfo
	{
		/// <summary>
		/// 类型
		/// </summary>
		public int DictType
		{
			set;
			get;
		}
		/// <summary>
		/// 字典代号
		/// </summary>
		public string Code
		{
			set;
			get;
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string Name
		{
			set;
			get;
		}

	}
}

