using System.Data;
using System;
 
namespace Luoyi.Common
{
    /// <summary>
    /// DataTable
    /// </summary>
    public  class DataTableHelper
    {
        /// <summary>
        /// DataTable翻页
        /// </summary>
        /// <param name="dt">要翻页浏览的DataTable</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetPage(DataTable dt,int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            if(dt == null)
                return null;

            recordCount = dt.Rows.Count;

            if(pageIndex == 0)
                pageIndex = 1;

            DataTable newdt = dt.Clone();

            int rowBegin = (pageIndex - 1) * pageSize;
            int rowEnd = pageIndex * pageSize;

            if(rowBegin >= dt.Rows.Count)
                return newdt;

            if(rowEnd > dt.Rows.Count)
                rowEnd = dt.Rows.Count;
            for(int i = rowBegin; i <= rowEnd - 1; i++)
            {
                newdt.ImportRow(dt.Rows[i]);
            }
            dt.Dispose();
            return newdt;
        }

    }
}
