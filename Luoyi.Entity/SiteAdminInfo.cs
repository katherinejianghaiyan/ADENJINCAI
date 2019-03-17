using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 营运点管理员
	/// </summary>
	[Serializable]
	public class SiteAdminInfo
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
		/// 营运点GUID
		/// </summary>
		public string SiteGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 登录名
		/// </summary>
		public string UserName
		{
			set;
			get;
		}
		/// <summary>
		/// 登录密码
		/// </summary>
		public string Password
		{
			set;
			get;
		}

	}
}

