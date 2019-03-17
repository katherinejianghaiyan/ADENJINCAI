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
    public partial class SaleOrder : Form
    {
        public SaleOrder()
        {
            InitializeComponent();
        }

        private delegate void SetPos(int ipos);

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Bind(1);
        }

        public void Bind(int currentPage)
        {
            string builder = string.Format(" AND s.IsPaid = 1 AND s.SiteGUID = '{0}'", UserLogin._AdminInfo.SiteGUID);

            var orderCode = txtOrderCode.Text;

            var userKeyword = txtUserKeyword.Text;

            var status = cbStatus.SelectedValue.ToString().ToInt32();

            var itemKeyword = txtItemKeyword.Text;

            if (!string.IsNullOrEmpty(orderCode))
            {
                builder += string.Format(" AND s.OrderCode LIKE '%{0}%'", orderCode);
            }
            if (!string.IsNullOrEmpty(userKeyword))
            {
                builder += string.Format(" AND (u.FirstName+u.LastName LIKE '%{0}%' OR u.WechatID LIKE '%{0}%' OR u.Mobile LIKE '%{0}%')", userKeyword);
            }
            if (!string.IsNullOrEmpty(itemKeyword))
            {
                builder += string.Format(" AND s.GUID IN(SELECT SOGUID FROM tblSaleOrderItem WHERE ItemGUID IN(SELECT GUID FROM tblItem WHERE ItemName+ItemCode LIKE '%{0}%'))", itemKeyword);
            }
            if (status > 0)
            {
                builder += string.Format(" AND s.Status = '{0}'", status);
            }
            if (dtpRequestBegin.Checked)
            {
                builder += string.Format(" AND s.RequiredDate >= '{0}'", dtpRequestBegin.Value.ToString("yyyy-MM-dd"));
            }
            if (dtpRequestEnd.Checked)
            {
                builder += string.Format(" AND s.RequiredDate <= '{0}'", dtpRequestEnd.Value.ToString("yyyy-MM-dd 23:59:59.997"));
            }
            if (dtpShippedBegin.Checked)
            {
                builder += string.Format(" AND s.ShippedDate >= '{0}'", dtpShippedBegin.Value.ToString("yyyy-MM-dd"));
            }
            if (dtpShippedEnd.Checked)
            {
                builder += string.Format(" AND s.ShippedDate <= '{0}'", dtpShippedEnd.Value.ToString("yyyy-MM-dd 23:59:59.997"));
            }

            dgvSaleOrder.AutoGenerateColumns = false;

            SaleOrderFilter filter = new SaleOrderFilter();

            filter.PageSize = 20;
            filter.PageIndex = currentPage;
            filter.Keyword = builder;

            int recordCount = 0;

            DataTable dt = BLL.SaleOrder.GetPage(filter, out recordCount);

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvSaleOrder.RowTemplate = new DataGridViewRow();
                dgvSaleOrder.DataSource = dt;

                dgvSaleOrder.ClearSelection();

                PagerControl pager = new PagerControl(filter.PageIndex, filter.PageSize, recordCount, "跳转");
                pager.currentPageChanged += new EventHandler(pager_currentPageChanged);//页码变化 触发的事件
                panelPager.Controls.Add(pager);
            }
            else
            {
                dgvSaleOrder.RowTemplate = new EmptyDataRow();
                dt.Rows.Add(dt.NewRow());
                dgvSaleOrder.DataSource = dt;
            }
        }

        private void pager_currentPageChanged(object sender, EventArgs e)
        {
            PagerControl pager = sender as PagerControl;
            if (pager == null)
            {
                return;
            }
            Bind(pager.CurrentPage);//查询数据并分页绑定
        }

        private void SaleOrder_Load(object sender, EventArgs e)
        {
            var dictStatus = BLL.SaleOrder.GetStatusDict();
            dictStatus.Add(0, "全部");
            BindingSource bsStatus = new BindingSource();
            bsStatus.DataSource = dictStatus;
            cbStatus.DataSource = bsStatus;
            cbStatus.ValueMember = "Key";
            cbStatus.DisplayMember = "Value";
            cbStatus.SelectedValue = 0;

            dtpRequestBegin.Format = DateTimePickerFormat.Custom;
            dtpRequestBegin.CustomFormat = " ";

            dtpRequestEnd.Format = DateTimePickerFormat.Custom;
            dtpRequestEnd.CustomFormat = " ";

            dtpShippedBegin.Format = DateTimePickerFormat.Custom;
            dtpShippedBegin.CustomFormat = " ";

            dtpShippedEnd.Format = DateTimePickerFormat.Custom;
            dtpShippedEnd.CustomFormat = " ";

            Bind(1);
        }

        private void dgvSaleOrderItem_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgvSaleOrder.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvSaleOrder.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgvSaleOrder.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvSaleOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var info = BLL.SaleOrder.GetInfo(dgvSaleOrder.CurrentRow.Cells["OrderCode"].Value.ToString());

                SaleOrderDetail frmSOD = new SaleOrderDetail();
                frmSOD.info = info;
                frmSOD.WindowState = FormWindowState.Maximized;
                frmSOD.MdiParent = this.MdiParent;
                frmSOD.Show();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (dgvSaleOrder.Focused && dgvSaleOrder.SelectedRows.Count > 0)
                {
                    var info = BLL.SaleOrder.GetInfo(dgvSaleOrder.CurrentRow.Cells["OrderCode"].Value.ToString());
                    SaleOrderDetail frmSOD = new SaleOrderDetail();
                    frmSOD.info = info;
                    frmSOD.WindowState = FormWindowState.Maximized;
                    frmSOD.MdiParent = this.MdiParent;
                    frmSOD.Show();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);

        }

        public void SetOrderCodeFocus(bool focused)
        {
            if (focused)
            {
                txtOrderCode.Focus();
            }
        }

        private void dtpRequestBegin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRequestBegin.Checked)
            {
                dtpRequestEnd.MinDate = dtpRequestBegin.Value;
                dtpRequestBegin.CustomFormat = "yyyy-MM-dd";
            }
            else
            {
                dtpRequestEnd.MinDate = new DateTime(1753, 1, 1);
                dtpRequestBegin.CustomFormat = " ";
            }
        }

        private void dtpRequestEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRequestEnd.Checked)
            {
                dtpRequestBegin.MaxDate = dtpRequestEnd.Value;
                dtpRequestEnd.CustomFormat = "yyyy-MM-dd";
            }
            else
            {
                dtpRequestBegin.MaxDate = new DateTime(9998, 12, 31);
                dtpRequestEnd.CustomFormat = " ";
            }
        }

        private void dtpShippedBegin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpShippedBegin.Checked)
            {
                dtpShippedEnd.MinDate = dtpShippedBegin.Value;
                dtpShippedBegin.CustomFormat = "yyyy-MM-dd";
            }
            else
            {
                dtpShippedEnd.MinDate = new DateTime(1753, 1, 1);
                dtpShippedBegin.CustomFormat = " ";
            }
        }

        private void dtpShippedEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpShippedEnd.Checked)
            {
                dtpShippedBegin.MaxDate = dtpShippedEnd.Value;
                dtpShippedEnd.CustomFormat = "yyyy-MM-dd";
            }
            else
            {
                dtpShippedBegin.MaxDate = new DateTime(9998, 12, 31);
                dtpShippedEnd.CustomFormat = " ";
            }
        }
    }
}
