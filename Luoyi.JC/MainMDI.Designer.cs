namespace Luoyi.JC
{
    partial class MainMDI
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.SaleOrderMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SOMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SyncMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaleOrderMenu,
            this.PrintMenu,
            this.SOMenu,
            this.SyncMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(936, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // SaleOrderMenu
            // 
            this.SaleOrderMenu.Name = "SaleOrderMenu";
            this.SaleOrderMenu.Size = new System.Drawing.Size(86, 21);
            this.SaleOrderMenu.Text = "订单查询(&O)";
            this.SaleOrderMenu.Click += new System.EventHandler(this.SaleOrderMenu_Click);
            // 
            // PrintMenu
            // 
            this.PrintMenu.Name = "PrintMenu";
            this.PrintMenu.Size = new System.Drawing.Size(83, 21);
            this.PrintMenu.Text = "打印标签(&P)";
            this.PrintMenu.Click += new System.EventHandler(this.PrintMenu_Click);
            // 
            // SOMenu
            // 
            this.SOMenu.Name = "SOMenu";
            this.SOMenu.Size = new System.Drawing.Size(44, 21);
            this.SOMenu.Text = "发货";
            this.SOMenu.Click += new System.EventHandler(this.SOMenu_Click);
            // 
            // SyncMenu
            // 
            this.SyncMenu.Name = "SyncMenu";
            this.SyncMenu.Size = new System.Drawing.Size(68, 21);
            this.SyncMenu.Text = "数据同步";
            this.SyncMenu.Click += new System.EventHandler(this.SyncMenu_Click);
            // 
            // MainMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(936, 418);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainMDI";
            this.Text = "净菜";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainMDI_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem SaleOrderMenu;
        private System.Windows.Forms.ToolStripMenuItem PrintMenu;
        private System.Windows.Forms.ToolStripMenuItem SOMenu;
        private System.Windows.Forms.ToolStripMenuItem SyncMenu;
    }
}



