using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Luoyi.JC
{
    public partial class MainMDI : Form
    {
        public MainMDI()
        {
            InitializeComponent();
        }

        private void SaleOrderMenu_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("SaleOrder"))
            {
                SaleOrder frmSaleOrder = new SaleOrder();
                frmSaleOrder.MdiParent = this;
                frmSaleOrder.WindowState = FormWindowState.Maximized;
                frmSaleOrder.Show();
            }
        }

        //防止打开多个窗体
        private bool ShowChildrenForm(string p_ChildrenFormText)
        {
            //依次检测当前窗体的子窗体
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                //判断当前子窗体的Text属性值是否与传入的字符串值相同
                if (this.MdiChildren[i].Name == p_ChildrenFormText)
                {
                    //如果值相同则表示此子窗体为想要调用的子窗体，激活此子窗体并返回true值
                    //this.MdiChildren[i].Activate();
                    //this.MdiChildren[i].WindowState = FormWindowState.Maximized;
                    this.MdiChildren[i].Close();
                    return false;
                }
            }
            //如果没有相同的值则表示要调用的子窗体还没有被打开，返回false值
            return false;
        }

        private void MainMDI_Load(object sender, EventArgs e)
        {
            SaleOrder frmSaleOrder = new SaleOrder();
            frmSaleOrder.MdiParent = this;
            frmSaleOrder.WindowState = FormWindowState.Maximized;
            frmSaleOrder.Show();

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SynchData"].ToString()))
            {
                SyncMenu.Visible = false;
            }
            else
            {
                if (!File.Exists(Application.StartupPath + ConfigurationManager.AppSettings["SynchData"].ToString()))
                {
                    SyncMenu.Visible = false;
                }
            }
        }

        private void SOMenu_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("SO"))
            {
                SO frmSO = new SO();
                frmSO.MdiParent = this;
                frmSO.WindowState = FormWindowState.Maximized;
                frmSO.Show();
            }
        }

        private void PrintMenu_Click(object sender, EventArgs e)
        {
            if (!ShowChildrenForm("frmPrint"))
            {
                frmPrint frmPrint = new frmPrint();
                frmPrint.MdiParent = this;
                frmPrint.WindowState = FormWindowState.Maximized;
                frmPrint.Show();
            }
        }

        private void SyncMenu_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + ConfigurationManager.AppSettings["SynchData"].ToString());
        }
    }
}
