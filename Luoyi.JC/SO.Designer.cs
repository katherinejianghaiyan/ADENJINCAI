namespace Luoyi.JC
{
    partial class SO
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
            this.btnQH = new System.Windows.Forms.Button();
            this.txtItemCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPayTime = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbRequestDate = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合计金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrderItem)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSOGUID
            // 
            this.txtSOGUID.Location = new System.Drawing.Point(160, 18);
            this.txtSOGUID.Margin = new System.Windows.Forms.Padding(6);
            this.txtSOGUID.Name = "txtSOGUID";
            this.txtSOGUID.Size = new System.Drawing.Size(389, 35);
            this.txtSOGUID.TabIndex = 1;
            this.txtSOGUID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSOGUID_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "单号(F1)";
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
            this.dgvSaleOrderItem.Size = new System.Drawing.Size(960, 260);
            this.dgvSaleOrderItem.TabIndex = 5;
            this.dgvSaleOrderItem.TabStop = false;
            this.dgvSaleOrderItem.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSaleOrder_RowPostPaint);
            // 
            // btnQH
            // 
            this.btnQH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQH.Location = new System.Drawing.Point(776, 416);
            this.btnQH.Margin = new System.Windows.Forms.Padding(6);
            this.btnQH.Name = "btnQH";
            this.btnQH.Size = new System.Drawing.Size(201, 45);
            this.btnQH.TabIndex = 7;
            this.btnQH.Text = "确认取货(空格)";
            this.btnQH.UseVisualStyleBackColor = true;
            this.btnQH.Click += new System.EventHandler(this.btnQH_Click);
            // 
            // txtItemCode
            // 
            this.txtItemCode.Location = new System.Drawing.Point(160, 95);
            this.txtItemCode.Margin = new System.Windows.Forms.Padding(6);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(389, 35);
            this.txtItemCode.TabIndex = 1;
            this.txtItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemCode_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "商品条码(F2)";
            // 
            // lbPayTime
            // 
            this.lbPayTime.AutoSize = true;
            this.lbPayTime.Location = new System.Drawing.Point(549, 98);
            this.lbPayTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbPayTime.Name = "lbPayTime";
            this.lbPayTime.Size = new System.Drawing.Size(106, 24);
            this.lbPayTime.TabIndex = 11;
            this.lbPayTime.Text = "付款时间";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReset.Location = new System.Drawing.Point(614, 417);
            this.btnReset.Margin = new System.Windows.Forms.Padding(6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(150, 45);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "重置(ESC)";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(549, 18);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(106, 24);
            this.lbUserName.TabIndex = 12;
            this.lbUserName.Text = "用户姓名";
            // 
            // lbRequestDate
            // 
            this.lbRequestDate.AutoSize = true;
            this.lbRequestDate.Location = new System.Drawing.Point(549, 55);
            this.lbRequestDate.Name = "lbRequestDate";
            this.lbRequestDate.Size = new System.Drawing.Size(106, 24);
            this.lbRequestDate.TabIndex = 13;
            this.lbRequestDate.Text = "需求日期";
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
            // SO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 465);
            this.Controls.Add(this.lbRequestDate);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbPayTime);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnQH);
            this.Controls.Add(this.dgvSaleOrderItem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSOGUID);
            this.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SO";
            this.Text = "订单发货";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SO_FormClosed);
            this.Load += new System.EventHandler(this.SO_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SO_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrderItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSOGUID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSaleOrderItem;
        private System.Windows.Forms.Button btnQH;
        private System.Windows.Forms.TextBox txtItemCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbPayTime;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbRequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合计金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockQty;
    }
}