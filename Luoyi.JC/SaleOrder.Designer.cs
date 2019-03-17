namespace Luoyi.JC
{
    partial class SaleOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpShippedEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpRequestEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpShippedBegin = new System.Windows.Forms.DateTimePicker();
            this.dtpRequestBegin = new System.Windows.Forms.DateTimePicker();
            this.txtUserKeyword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrderCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvSaleOrder = new System.Windows.Forms.DataGridView();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WechatID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequiredDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelPager = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtItemKeyword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(747, 8);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 20);
            this.cbStatus.TabIndex = 24;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(700, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(168, 28);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpShippedEnd
            // 
            this.dtpShippedEnd.Checked = false;
            this.dtpShippedEnd.Location = new System.Drawing.Point(574, 46);
            this.dtpShippedEnd.MaxDate = new System.DateTime(2025, 12, 31, 0, 0, 0, 0);
            this.dtpShippedEnd.MinDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
            this.dtpShippedEnd.Name = "dtpShippedEnd";
            this.dtpShippedEnd.ShowCheckBox = true;
            this.dtpShippedEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpShippedEnd.TabIndex = 17;
            this.dtpShippedEnd.ValueChanged += new System.EventHandler(this.dtpShippedEnd_ValueChanged);
            // 
            // dtpRequestEnd
            // 
            this.dtpRequestEnd.Checked = false;
            this.dtpRequestEnd.Location = new System.Drawing.Point(218, 46);
            this.dtpRequestEnd.Name = "dtpRequestEnd";
            this.dtpRequestEnd.ShowCheckBox = true;
            this.dtpRequestEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpRequestEnd.TabIndex = 18;
            this.dtpRequestEnd.ValueChanged += new System.EventHandler(this.dtpRequestEnd_ValueChanged);
            // 
            // dtpShippedBegin
            // 
            this.dtpShippedBegin.Checked = false;
            this.dtpShippedBegin.Location = new System.Drawing.Point(426, 46);
            this.dtpShippedBegin.Name = "dtpShippedBegin";
            this.dtpShippedBegin.ShowCheckBox = true;
            this.dtpShippedBegin.Size = new System.Drawing.Size(120, 21);
            this.dtpShippedBegin.TabIndex = 19;
            this.dtpShippedBegin.ValueChanged += new System.EventHandler(this.dtpShippedBegin_ValueChanged);
            // 
            // dtpRequestBegin
            // 
            this.dtpRequestBegin.Checked = false;
            this.dtpRequestBegin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtpRequestBegin.Location = new System.Drawing.Point(69, 46);
            this.dtpRequestBegin.Name = "dtpRequestBegin";
            this.dtpRequestBegin.ShowCheckBox = true;
            this.dtpRequestBegin.Size = new System.Drawing.Size(120, 21);
            this.dtpRequestBegin.TabIndex = 20;
            this.dtpRequestBegin.ValueChanged += new System.EventHandler(this.dtpRequestBegin_ValueChanged);
            // 
            // txtUserKeyword
            // 
            this.txtUserKeyword.Location = new System.Drawing.Point(325, 8);
            this.txtUserKeyword.Name = "txtUserKeyword";
            this.txtUserKeyword.Size = new System.Drawing.Size(150, 21);
            this.txtUserKeyword.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "发货日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "用户姓名/微信/手机号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "需求日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "单号";
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.Location = new System.Drawing.Point(45, 8);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Size = new System.Drawing.Size(143, 21);
            this.txtOrderCode.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(698, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "状态";
            // 
            // dgvSaleOrder
            // 
            this.dgvSaleOrder.AllowUserToAddRows = false;
            this.dgvSaleOrder.AllowUserToDeleteRows = false;
            this.dgvSaleOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSaleOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GUID,
            this.OrderCode,
            this.UserName,
            this.WechatID,
            this.Mobile,
            this.RequiredDate,
            this.ShippedDate,
            this.TotalAmount,
            this.PaidTime,
            this.PaymentAmount,
            this.Status});
            this.dgvSaleOrder.Location = new System.Drawing.Point(12, 73);
            this.dgvSaleOrder.MultiSelect = false;
            this.dgvSaleOrder.Name = "dgvSaleOrder";
            this.dgvSaleOrder.ReadOnly = true;
            this.dgvSaleOrder.RowTemplate.Height = 23;
            this.dgvSaleOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSaleOrder.Size = new System.Drawing.Size(856, 256);
            this.dgvSaleOrder.TabIndex = 25;
            this.dgvSaleOrder.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSaleOrder_CellDoubleClick);
            // 
            // GUID
            // 
            this.GUID.DataPropertyName = "GUID";
            this.GUID.HeaderText = "GUID";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            this.GUID.Visible = false;
            // 
            // OrderCode
            // 
            this.OrderCode.DataPropertyName = "OrderCode";
            this.OrderCode.HeaderText = "单号";
            this.OrderCode.Name = "OrderCode";
            this.OrderCode.ReadOnly = true;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "客户名";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // WechatID
            // 
            this.WechatID.DataPropertyName = "WechatID";
            this.WechatID.HeaderText = "微信号";
            this.WechatID.Name = "WechatID";
            this.WechatID.ReadOnly = true;
            // 
            // Mobile
            // 
            this.Mobile.DataPropertyName = "Mobile";
            this.Mobile.HeaderText = "手机号";
            this.Mobile.Name = "Mobile";
            this.Mobile.ReadOnly = true;
            // 
            // RequiredDate
            // 
            this.RequiredDate.DataPropertyName = "RequiredDate";
            dataGridViewCellStyle1.Format = "yyyy-M-d";
            dataGridViewCellStyle1.NullValue = null;
            this.RequiredDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.RequiredDate.HeaderText = "需求日期";
            this.RequiredDate.Name = "RequiredDate";
            this.RequiredDate.ReadOnly = true;
            // 
            // ShippedDate
            // 
            this.ShippedDate.DataPropertyName = "ShippedDate";
            dataGridViewCellStyle2.Format = "yyyy-M-d HH:mm";
            dataGridViewCellStyle2.NullValue = null;
            this.ShippedDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.ShippedDate.HeaderText = "发货时间";
            this.ShippedDate.Name = "ShippedDate";
            this.ShippedDate.ReadOnly = true;
            this.ShippedDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TotalAmount
            // 
            this.TotalAmount.DataPropertyName = "TotalAmount";
            this.TotalAmount.HeaderText = "订单金额";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // PaidTime
            // 
            this.PaidTime.DataPropertyName = "PaidTime";
            dataGridViewCellStyle3.Format = "yyyy-M-d HH:mm";
            dataGridViewCellStyle3.NullValue = null;
            this.PaidTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.PaidTime.HeaderText = "付款时间";
            this.PaidTime.Name = "PaidTime";
            this.PaidTime.ReadOnly = true;
            // 
            // PaymentAmount
            // 
            this.PaymentAmount.DataPropertyName = "PaymentAmount";
            this.PaymentAmount.HeaderText = "付款金额";
            this.PaymentAmount.Name = "PaymentAmount";
            this.PaymentAmount.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "发货状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // panelPager
            // 
            this.panelPager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPager.Location = new System.Drawing.Point(12, 335);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(856, 33);
            this.panelPager.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(491, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "菜品";
            // 
            // txtItemKeyword
            // 
            this.txtItemKeyword.Location = new System.Drawing.Point(526, 8);
            this.txtItemKeyword.Name = "txtItemKeyword";
            this.txtItemKeyword.Size = new System.Drawing.Size(151, 21);
            this.txtItemKeyword.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 29;
            this.label7.Text = "至";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(551, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 29;
            this.label8.Text = "至";
            // 
            // SaleOrder
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 370);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtItemKeyword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panelPager);
            this.Controls.Add(this.dgvSaleOrder);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpShippedEnd);
            this.Controls.Add(this.dtpRequestEnd);
            this.Controls.Add(this.dtpShippedBegin);
            this.Controls.Add(this.dtpRequestBegin);
            this.Controls.Add(this.txtUserKeyword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOrderCode);
            this.Name = "SaleOrder";
            this.Text = "订单查询";
            this.Load += new System.EventHandler(this.SaleOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpShippedEnd;
        private System.Windows.Forms.DateTimePicker dtpRequestEnd;
        private System.Windows.Forms.DateTimePicker dtpShippedBegin;
        private System.Windows.Forms.DateTimePicker dtpRequestBegin;
        private System.Windows.Forms.TextBox txtUserKeyword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvSaleOrder;
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WechatID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequiredDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtItemKeyword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}