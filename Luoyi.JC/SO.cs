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
    public partial class SO : Form
    {
        public SaleOrderInfo info { get; set; }

        public SO()
        {
            InitializeComponent();
        }

        private void SO_Load(object sender, EventArgs e)
        {
            if (info == null)
            {
                txtSOGUID.Select();
            }
            else
            {
                txtSOGUID.Text = info.OrderCode;
                Bind();
            }
        }

        protected void Bind()
        {
            if (info != null)
            {
                if (info.SiteGUID == UserLogin._AdminInfo.SiteGUID)
                {

                    var userInfo = BLL.User.GetInfo(info.UserID);

                    lbUserName.Text = string.Format("客户姓名：{0} 手机：{1}", userInfo.FirstName + userInfo.LastName, userInfo.Mobile);

                    lbRequestDate.Text = string.Format("需求日期：{0}", info.RequiredDate.ToString("yyyy-MM-dd"));

                    lbPayTime.Text = string.Format("订单金额：{0} 付款时间：{1}", info.PaymentAmount.ToString(), info.PaidTime.ToString("yyyy-MM-dd HH:mm"));

                    btnQH.Visible = btnReset.Visible = info.Status == (int)SaleOrderInfo.StatusEnum.WFH;

                    string filter = string.Empty;

                    if (txtSOGUID.Text.Trim() != string.Empty)
                    {
                        filter += string.Format(" AND s.SOGUID = '{0}'", info.GUID);
                    }

                    dgvSaleOrderItem.AutoGenerateColumns = false;

                    dgvSaleOrderItem.DataSource = BLL.SaleOrderItem.GetTable(filter);
                    dgvSaleOrderItem.ClearSelection();

                    if (info.Status == (int)SaleOrderInfo.StatusEnum.WFH)
                    {
                        txtItemCode.Enabled = true;
                        txtItemCode.Focus();

                        if (DateTime.Now.Date > info.RequiredDate.Date)
                        {
                            lbUserName.ForeColor = lbRequestDate.ForeColor = lbPayTime.ForeColor = Color.Red;
                            MsgBox.Show("不是今日订单,请确认", "提示", MsgBox.MyButtons.OK);
                        }
                        else if (DateTime.Now.Date < info.RequiredDate.Date)
                        {
                            MsgBox.Show("该订单为明天订单,今天无法取货", "提示", MsgBox.MyButtons.OK);

                            this.txtSOGUID.Text = string.Empty;
                            this.txtItemCode.Text = string.Empty;
                            this.lbPayTime.Text = string.Empty;
                            this.lbRequestDate.Text = string.Empty;
                            this.lbUserName.Text = string.Empty;
                            this.dgvSaleOrderItem.DataSource = null;
                            this.txtSOGUID.Select();

                        }
                    }
                    else
                    {
                        txtItemCode.Enabled = false;
                        MsgBox.Show("此订单已发货，不可继续发货", "提示", MsgBox.MyButtons.OK);
                    }
                }
                else
                {
                    MsgBox.Show("此订单非本运营点，请到相关运营点取货", "提示", MsgBox.MyButtons.OK);
                    txtSOGUID.Text = string.Empty;
                    txtSOGUID.Select();
                }
            }
            else
            {
                MsgBox.Show(string.Format("单号：{0}\n错误，请检查之", txtSOGUID.Text.Trim()), "提示", MsgBox.MyButtons.OK);
                txtSOGUID.Text = string.Empty;
                txtSOGUID.Select();
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

        private void btnQH_Click(object sender, EventArgs e)
        {
            if (info != null && info.Status == (int)SaleOrderInfo.StatusEnum.WFH)
            {

                List<StockTransactionInfo> list = new List<StockTransactionInfo>();

                int count = 0;

                foreach (DataGridViewRow dgvr in dgvSaleOrderItem.Rows)
                {
                    var stInfo = new StockTransactionInfo();
                    var itemInfo = BLL.SaleOrderItem.GetInfo(dgvr.Cells["ID"].Value.ToString().ToInt32());
                    stInfo.UserID = itemInfo.UserID;
                    stInfo.SiteGUID = UserLogin._AdminInfo.SiteGUID;
                    stInfo.SODetailGUID = itemInfo.GUID;
                    stInfo.PODetailGUID = string.Empty;
                    stInfo.ItemGUID = itemInfo.ItemGUID;
                    stInfo.Cost = itemInfo.Price;
                    stInfo.UOMGUID = itemInfo.UOMGUID;
                    stInfo.Qty = dgvr.Cells["StockQty"].Value == null ? 0 : dgvr.Cells["StockQty"].Value.ToString().ToInt32();
                    stInfo.CreateTime = DateTime.Now;
                    stInfo.CreateUser = UserLogin._AdminInfo.ID;
                    list.Add(stInfo);

                    if (stInfo.Qty != itemInfo.Qty)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    MsgBox.Show(string.Format("发货数量错误！", txtSOGUID.Text.Trim()), "提示", MsgBox.MyButtons.OK);
                }
                else
                {
                    if (BLL.SaleOrder.Shipped(info, list, UserLogin._AdminInfo.ID))
                    {
                        MsgBox.Show("发货成功！", "提示", MsgBox.MyButtons.OK);

                        this.txtSOGUID.Text = string.Empty;
                        this.txtItemCode.Text = string.Empty;
                        this.lbPayTime.Text = string.Empty;
                        this.lbRequestDate.Text = string.Empty;
                        this.lbUserName.Text = string.Empty;

                        this.dgvSaleOrderItem.DataSource = null;
                        this.txtSOGUID.Select();

                    }
                    else
                    {
                        MsgBox.Show("发货失败！", "提示", MsgBox.MyButtons.OK);
                    }
                }
            }
        }

        private void SO_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaleOrder frmSaleOrder = new SaleOrder();
            frmSaleOrder.SetOrderCodeFocus(true);
        }

        private void SO_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    btnQH_Click(this, EventArgs.Empty);
                    break;
                case Keys.F1:
                    this.txtSOGUID.Text = string.Empty;
                    this.txtItemCode.Text = string.Empty;
                    this.lbPayTime.Text = string.Empty;
                    this.lbRequestDate.Text = string.Empty;
                    this.lbUserName.Text = string.Empty;
                    this.dgvSaleOrderItem.DataSource = null;
                    this.txtSOGUID.Select();
                    break;
                case Keys.F2:
                    txtItemCode.Select();
                    break;
                case Keys.Escape:
                    if (info.Status == (int)SaleOrderInfo.StatusEnum.WFH)
                    {
                        foreach (DataGridViewRow dgvr in this.dgvSaleOrderItem.Rows)
                        {
                            dgvr.Cells["StockQty"].Value = 0;
                        }
                    }
                    break;
            }
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            if (info.Status == (int)SaleOrderInfo.StatusEnum.WFH)
            {
                foreach (DataGridViewRow dgvr in this.dgvSaleOrderItem.Rows)
                {
                    dgvr.Cells["StockQty"].Value = 0;
                }

                txtItemCode.Select();
            }
        }

        private void txtSOGUID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                info = BLL.SaleOrder.GetInfo(txtSOGUID.Text.Trim());
                Bind();
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var itemCode = this.txtItemCode.Text;

                int count = 0;
                bool hasItemCode = false;
                foreach (DataGridViewRow dgvr in dgvSaleOrderItem.Rows)
                {
                    int qty = dgvr.Cells["Qty"].Value.ToString().ToInt32();
                    int stockqty = dgvr.Cells["StockQty"].Value == null ? 0 : dgvr.Cells["StockQty"].Value.ToString().ToInt32();

                    if (itemCode.Equals(dgvr.Cells["ItemCode"].Value.ToString()))
                    {
                        hasItemCode = true;

                        if (dgvr.Cells["Qty"].Value != null)
                        {
                            if (stockqty < qty)
                            {
                                stockqty++;
                                dgvr.Cells["StockQty"].Value = stockqty;
                            }
                            else
                            {
                                MsgBox.Show("该菜品数量已足够", "提示", MsgBox.MyButtons.OK);
                            }
                        }
                    }

                    if (qty > stockqty)
                    {
                        count++;
                    }
                }

                if (!hasItemCode)
                {
                    MsgBox.Show("无此商品条码", "提示", MsgBox.MyButtons.OK);
                }

                this.txtItemCode.Text = string.Empty;
                this.txtItemCode.Select();

                if (dgvSaleOrderItem.Rows.Count > 0 && count == 0)
                {
                    DialogResult result = MsgBox.Show("该取货单已全部完成", "提示", MsgBox.MyButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        btnQH_Click(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
