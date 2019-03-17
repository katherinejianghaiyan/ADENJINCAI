using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Luoyi.Common;
using Luoyi.Entity;

namespace Luoyi.JC
{
    public partial class SaleOrderDetail : Form
    {
        public SaleOrderInfo info { get; set; }

        public SaleOrderDetail()
        {
            InitializeComponent();
        }

        private void SO_Load(object sender, EventArgs e)
        {
            Bind();
        }

        protected void Bind()
        {
            if (info != null)
            {
                var userInfo = BLL.User.GetInfo(info.UserID);
                txtSOGUID.Text = info.OrderCode;
                txtUserName.Text = userInfo.FirstName + userInfo.LastName;
                txtMobile.Text = userInfo.Mobile;
                txtRequestDate.Text = info.RequiredDate.ToString("yyyy-MM-dd");
                txtPayAmount.Text = info.PaymentAmount.ToString("G0");
                txtPayTime.Text = info.PaidTime.ToString("yyyy-MM-dd HH:mm");

                string filter = string.Format(" AND s.SOGUID = '{0}'", info.GUID);
                dgvSaleOrderItem.AutoGenerateColumns = false;
                dgvSaleOrderItem.DataSource = BLL.SaleOrderItem.GetTable(filter);
                dgvSaleOrderItem.ClearSelection();
            }
        }

        private void dgvSaleOrder_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgvSaleOrderItem.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvSaleOrderItem.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvSaleOrderItem.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

            if (dgvSaleOrderItem.Rows[e.RowIndex].Cells["Qty"].Value.ToType<int>() == dgvSaleOrderItem.Rows[e.RowIndex].Cells["StockQty"].Value.ToType<int>())
            {
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Gray;
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
            else
            {
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.White;
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                dgvSaleOrderItem.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }
    }
}
