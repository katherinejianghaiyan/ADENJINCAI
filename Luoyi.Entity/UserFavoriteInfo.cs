using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 用户收藏
	/// </summary>
	[Serializable]
	public class UserFavoriteInfo : ItemInfo
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
		/// 用户ID
		/// </summary>
		public int UserID
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

	}
}

