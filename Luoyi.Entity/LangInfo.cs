
using System;

namespace Luoyi.Entity
{
	/// <summary>
    /// LangInfo
	/// </summary>
	[Serializable]
	public class LangInfo
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
		/// BUGUID
		/// </summary>
		public string PageName
		{
			set;
			get;
		}
		/// <summary>
		/// 控件ID
		/// </summary>
		public string ControlID
		{
			set;
			get;
		}
		/// <summary>
		/// 中文
		/// </summary>
        public string ZHCNText
		{
			set;
			get;
		}
		/// <summary>
		/// English
		/// </summary>
		public string ENUSText
		{
			set;
			get;
		}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set;
            get;
        }
	}
}

