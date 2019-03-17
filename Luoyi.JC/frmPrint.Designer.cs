namespace Luoyi.JC
{
    partial class frmPrint
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.dgvPrint = new System.Windows.Forms.DataGridView();
            this.dtpPrintDate = new System.Windows.Forms.DateTimePicker();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPrinter = new System.Windows.Forms.ComboBox();
            this.ckbAll = new System.Windows.Forms.CheckBox();
            this.IsPrint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequiredDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiteGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemBom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(630, 20);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 24);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "打  印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // dgvPrint
            // 
            this.dgvPrint.AllowUserToAddRows = false;
            this.dgvPrint.AllowUserToDeleteRows = false;
            this.dgvPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsPrint,
            this.ItemName,
            this.RequiredDate,
            this.ItemCode,
            this.SiteGUID,
            this.ItemBom,
            this.Tips,
            this.Qty,
            this.PrintQty});
            this.dgvPrint.Location = new System.Drawing.Point(17, 62);
            this.dgvPrint.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPrint.MultiSelect = false;
            this.dgvPrint.Name = "dgvPrint";
            this.dgvPrint.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrint.Size = new System.Drawing.Size(698, 335);
            this.dgvPrint.TabIndex = 5;
            // 
            // dtpPrintDate
            // 
            this.dtpPrintDate.Location = new System.Drawing.Point(81, 22);
            this.dtpPrintDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPrintDate.Name = "dtpPrintDate";
            this.dtpPrintDate.Size = new System.Drawing.Size(125, 21);
            this.dtpPrintDate.TabIndex = 6;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(216, 22);
            this.btnLoadData.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(99, 22);
            this.btnLoadData.TabIndex = 7;
            this.btnLoadData.Text = "查看待打印项目";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "打印日期：";
            // 
            // cmbPrinter
            // 
            this.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinter.Location = new System.Drawing.Point(442, 23);
            this.cmbPrinter.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPrinter.Name = "cmbPrinter";
            this.cmbPrinter.Size = new System.Drawing.Size(177, 20);
            this.cmbPrinter.TabIndex = 0;
            this.cmbPrinter.SelectedIndexChanged += new System.EventHandler(this.cmbPrinter_SelectedIndexChanged);
            // 
            // ckbAll
            // 
            this.ckbAll.AutoSize = true;
            this.ckbAll.Location = new System.Drawing.Point(331, 26);
            this.ckbAll.Name = "ckbAll";
            this.ckbAll.Size = new System.Drawing.Size(48, 16);
            this.ckbAll.TabIndex = 9;
            this.ckbAll.Text = "全选";
            this.ckbAll.UseVisualStyleBackColor = true;
            this.ckbAll.CheckedChanged += new System.EventHandler(this.ckbAll_CheckedChanged);
            // 
            // IsPrint
            // 
            this.IsPrint.FalseValue = "false";
            this.IsPrint.FillWeight = 50F;
            this.IsPrint.HeaderText = "打印";
            this.IsPrint.Name = "IsPrint";
            this.IsPrint.TrueValue = "true";
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.FillWeight = 71.94362F;
            this.ItemName.HeaderText = "物料名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // RequiredDate
            // 
            this.RequiredDate.DataPropertyName = "RequiredDate";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            dataGridViewCellStyle1.NullValue = null;
            this.RequiredDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.RequiredDate.HeaderText = "需求日期";
            this.RequiredDate.Name = "RequiredDate";
            this.RequiredDate.Visible = false;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.FillWeight = 71.94362F;
            this.ItemCode.HeaderText = "物料代码";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // SiteGUID
            // 
            this.SiteGUID.DataPropertyName = "CompNameCn";
            this.SiteGUID.FillWeight = 71.94362F;
            this.SiteGUID.HeaderText = "运营点";
            this.SiteGUID.Name = "SiteGUID";
            this.SiteGUID.ReadOnly = true;
            // 
            // ItemBom
            // 
            this.ItemBom.DataPropertyName = "ItemBom";
            this.ItemBom.HeaderText = "物料";
            this.ItemBom.Name = "ItemBom";
            this.ItemBom.ReadOnly = true;
            this.ItemBom.Visible = false;
            // 
            // Tips
            // 
            this.Tips.DataPropertyName = "Tips";
            this.Tips.HeaderText = "温馨提示";
            this.Tips.Name = "Tips";
            this.Tips.ReadOnly = true;
            this.Tips.Visible = false;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.FillWeight = 71.94362F;
            this.Qty.HeaderText = "需求数量";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // PrintQty
            // 
            this.PrintQty.DataPropertyName = "PrintQty";
            dataGridViewCellStyle2.NullValue = "0";
            this.PrintQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.PrintQty.FillWeight = 71.94362F;
            this.PrintQty.HeaderText = "需打印数量";
            this.PrintQty.MaxInputLength = 3;
            this.PrintQty.Name = "PrintQty";
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 414);
            this.Controls.Add(this.ckbAll);
            this.Controls.Add(this.cmbPrinter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.dtpPrintDate);
            this.Controls.Add(this.dgvPrint);
            this.Controls.Add(this.btnPrint);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrint";
            this.ShowIcon = false;
            this.Text = "条码打印";
            this.Load += new System.EventHandler(this.frmPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.DataGridView dgvPrint;
        private System.Windows.Forms.DateTimePicker dtpPrintDate;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPrinter;
        private System.Windows.Forms.CheckBox ckbAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequiredDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemBom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tips;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintQty;
    }
}