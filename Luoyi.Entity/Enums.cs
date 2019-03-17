using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Luoyi.Entity
{
    public class Enums
    {
        public enum ItemSortEnum
        {
            /// <summary>
            /// 菜品销售量
            /// </summary>
            [Description("好评榜")]
            好评榜 = 100,
            /// <summary>
            /// 短期有促销价格
            /// </summary>
            [Description("促销价")]
            促销价 = 200
        }
    }
}
