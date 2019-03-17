namespace Luoyi.JC
{
    partial class SaleOrderDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSOGUID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSaleOrderItem = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合计金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbPayTime = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbRequestDate = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtRequestDate = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtPayAmount = new System.Windows.Forms.TextBox();
            this.txtPayTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrderItem)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSOGUID
            // 
            this.txtSOGUID.Location = new System.Drawing.Point(134, 15);
            this.txtSOGUID.Margin = new System.Windows.Forms.Padding(6);
            this.txtSOGUID.Name = "txtSOGUID";
            this.txtSOGUID.ReadOnly = true;
            this.txtSOGUID.Size = new System.Drawing.Size(305, 35);
            this.txtSOGUID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "单号";
            // 
            // dgvSaleOrderItem
            // 
            this.dgvSaleOrderItem.AllowUserToAddRows = false;
            this.dgvSaleOrderItem.AllowUserToDeleteRows = false;
            this.dgvSaleOrderItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSaleOrderItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleOrderItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleOrderItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ItemCode,
            this.ItemName,
            this.Qty,
            this.Price,
            this.合计金额,
            this.StockQty});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSaleOrderItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSaleOrderItem.Location = new System.Drawing.Point(17, 145);
            this.dgvSaleOrderItem.Margin = new System.Windows.Forms.Padding(6);
            this.dgvSaleOrderItem.Name = "dgvSaleOrderItem";
            this.dgvSaleOrderItem.ReadOnly = true;
            this.dgvSaleOrderItem.RowTemplate.Height = 28;
            this.dgvSaleOrderItem.Size = new System.Drawing.Size(960, 305);
            this.dgvSaleOrderItem.TabIndex = 5;
            this.dgvSaleOrderItem.TabStop = false;
            this.dgvSaleOrderItem.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSaleOrder_RowPostPaint);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.HeaderText = "菜品条码";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "菜品名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "数量";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "单价";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // 合计金额
            // 
            this.合计金额.DataPropertyName = "Amount";
            this.合计金额.HeaderText = "合计金额";
            this.合计金额.Name = "合计金额";
            this.合计金额.ReadOnly = true;
            // 
            // StockQty
            // 
            this.StockQty.DataPropertyName = "StockQty";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.StockQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.StockQty.HeaderText = "发货数量";
            this.StockQty.Name = "StockQty";
            this.StockQty.ReadOnly = true;
            // 
            // lbPayTime
            // 
            this.lbPayTime.AutoSize = true;
            this.lbPayTime.Location = new System.Drawing.Point(15, 115);
            this.lbPayTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbPayTime.Name = "lbPayTime";
            this.lbPayTime.Size = new System.Drawing.Size(106, 24);
            this.lbPayTime.TabIndex = 11;
            this.lbPayTime.Text = "订单金额";
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(475, 18);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(106, 24);
            this.lbUserName.TabIndex = 12;
            this.lbUserName.Text = "用户姓名";
            // 
            // lbRequestDate
            // 
            this.lbRequestDate.AutoSize = true;
            this.lbRequestDate.Location = new System.Drawing.Point(13, 70);
            this.lbRequestDate.Name = "lbRequestDate";
            this.lbRequestDate.Size = new System.Drawing.Size(106, 24);
            this.lbRequestDate.TabIndex = 13;
            this.lbRequestDate.Text = "需求日期";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(590, 15);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(305, 35);
            this.txtUserName.TabIndex = 1;
            // 
            // txtRequestDate
            // 
            this.txtRequestDate.Location = new System.Drawing.Point(134, 62);
            this.txtRequestDate.Margin = new System.Windows.Forms.Padding(6);
            this.txtRequestDate.Name = "txtRequestDate";
            this.txtRequestDate.ReadOnly = true;
            this.txtRequestDate.Size = new System.Drawing.Size(305, 35);
            this.txtRequestDate.TabIndex = 1;
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(590, 62);
            this.txtMobile.Margin = new System.Windows.Forms.Padding(6);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.ReadOnly = true;
            this.txtMobile.Size = new System.Drawing.Size(305, 35);
            this.txtMobile.TabIndex = 1;
            // 
            // txtPayAmount
            // 
            this.txtPayAmount.Location = new System.Drawing.Point(133, 104);
            this.txtPayAmount.Margin = new System.Windows.Forms.Padding(6);
            this.txtPayAmount.Name = "txtPayAmount";
            this.txtPayAmount.ReadOnly = true;
            this.txtPayAmount.Size = new System.Drawing.Size(305, 35);
            this.txtPayAmount.TabIndex = 1;
            // 
            // txtPayTime
            // 
            this.txtPayTime.Location = new System.Drawing.Point(590, 104);
            this.txtPayTime.Margin = new System.Windows.Forms.Padding(6);
            this.txtPayTime.Name = "txtPayTime";
            this.txtPayTime.ReadOnly = true;
            this.txtPayTime.Size = new System.Drawing.Size(305, 35);
            this.txtPayTime.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(475, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "手机";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(475, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "付款时间";
            // 
            // SaleOrderDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 465);
            this.Controls.Add(this.lbRequestDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbPayTime);
            this.Controls.Add(this.dgvSaleOrderItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtPayTime);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.txtPayAmount);
            this.Controls.Add(this.txtRequestDate);
            this.Controls.Add(this.txtSOGUID);
            this.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaleOrderDetail";
            this.Text = "订单发货";
            this.Load += new System.EventHandler(this.SO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrderItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSOGUID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSaleOrderItem;
        private System.Windows.Forms.Label lbPayTime;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbRequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合计金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockQty;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtRequestDate;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtPayAmount;
        private System.Windows.Forms.TextBox txtPayTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}