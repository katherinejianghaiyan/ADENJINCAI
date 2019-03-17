using System;

namespace Luoyi.Entity
{
	/// <summary>
	/// 用户产品评价
	/// </summary>
	[Serializable]
    public class UserCommentInfo
	{
		/// <summary>
		/// 评价ID
		/// </summary>
		public int ID
		{
			set;
			get;
		}
		/// <summary>
		/// 评价订单ID
		/// </summary>
		public int OrderID
		{
			set;
			get;
		}

        public string SiteGUID
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
		/// 评价物料ID
		/// </summary>
		public string ItemGUID
		{
			set;
			get;
		}
		/// <summary>
		/// 总体评价
		/// </summary>
		public int Score
		{
			set;
			get;
		}
		/// <summary>
		/// 口味
		/// </summary>
		public int ScoreTaste
		{
			set;
			get;
		}
		/// <summary>
		/// 价格
		/// </summary>
		public int ScorePrice
		{
			set;
			get;
		}
		/// <summary>
		/// 服务
		/// </summary>
		public int ScoreService
		{
			set;
			get;
		}
		/// <summary>
		/// 评价内容
		/// </summary>
		public string Content
		{
			set;
			get;
		}
		/// <summary>
		/// 评价图片
		/// </summary>
		public string Images
		{
			set;
			get;
		}
		/// <summary>
		/// 评价时间
		/// </summary>
		public DateTime CommentTime
		{
			set;
			get;
		}

	}
}

