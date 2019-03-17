using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Luoyi.JC
{
    public class EmptyDataRow : DataGridViewRow
    {
        //重写绘制的方法
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
        {
            var grid = this.DataGridView;
            int rowHeadersWidth = grid.RowHeadersVisible ? grid.RowHeadersWidth : 0;
            string empty = "暂无信息";
            Brush brush = SystemBrushes.Control;
            graphics.FillRectangle(brush, rowBounds);
            graphics.DrawString(empty, grid.Font, Brushes.Black, grid.Width / 2 - (empty.Length) * 5, rowBounds.Bottom - 18);
        }
    }

}
