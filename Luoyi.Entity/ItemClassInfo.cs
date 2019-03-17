
using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 物料分类
	/// </summary>
	[Serializable]
	public class ItemClassInfo
	{
		/// <summary>
		/// 分类ID
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
		/// 分类名称
		/// </summary>
		public string ClassName
		{
			set;
			get;
		}
		/// <summary>
		/// 显示排序
		/// </summary>
		public int Sort
		{
			set;
			get;
		}

        public string PagePath
        {
            set;
            get;
        }

        public string BUGUID
        {
            set;
            get;
        }
        public string SiteGUID
        {
            set;
            get;
        }
    }
}

