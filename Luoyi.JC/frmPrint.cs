using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Luoyi.Common;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Luoyi.JC
{
    public partial class frmPrint : Form
    {
        const int pageWidth = 100;//340;
        const int pageHeight = 50;// 170;

        const int barcodeHeight = 45;
        private Font textFont = new Font(new FontFamily("宋体"), 10);


        public frmPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前打印信息
        /// </summary>
        private PrintInfo currentPrintInfo = null;


        private void frmPrint_Load(object sender, EventArgs e)
        {
            //遍历集合，将所有打印机加载到下拉列表comboBox1中
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                cmbPrinter.Items.Add(item);
            }
            cmbPrinter.Text = ConfigurationManager.AppSettings["Printer"].ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPrinter.Text))
            {
                MessageBox.Show("请选择打印机！");
                return;
            }


            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", (int)(pageWidth / 25.4 * 100), (int)(pageHeight / 25.4 * 100));// 340, 225);
            printDocument1.OriginAtMargins = true;
            printDocument1.DefaultPageSettings.Margins.Top = (int)(2 / 25.4 * 100);
            printDocument1.DefaultPageSettings.Margins.Left = (int)(4 / 25.4 * 100);

            printDialog1.Document = printDocument1;
            printDialog1.PrinterSettings.PrinterName = cmbPrinter.Text;


            foreach (DataGridViewRow dgvr in dgvPrint.Rows)
            {
                if (dgvr.Cells["IsPrint"].Value.ToType<bool>())
                {
                    //TODO: 遍历表格控件，把选中要打印的项目取出赋值给实体
                    currentPrintInfo = new PrintInfo()
                    {
                        ItemCode = dgvr.Cells["ItemCode"].Value.ToString(),
                        ItemName = dgvr.Cells["ItemName"].Value.ToString(),
                        ItemBom = dgvr.Cells["ItemBom"].Value.ToString().JSONDeserialize<Dictionary<string, string>>(),
                        Tips = dgvr.Cells["Tips"].Value.ToString(),
                        ProductionDate = dgvr.Cells["RequiredDate"].Value.ToString(),
                        ExpirationDate = dgvr.Cells["RequiredDate"].Value.ToString(),
                        PrintQty = dgvr.Cells["PrintQty"].Value.ToType<short>()
                    };
                    try
                    {
                        printDialog1.PrinterSettings.Copies = currentPrintInfo.PrintQty;
                        printDocument1.Print();
                    }
                    catch (Exception ex)
                    {
                        //停止打印
                        printDocument1.PrintController.OnEndPrint(printDocument1, new PrintEventArgs());
                        MessageBox.Show("打印失败：" + ex.Message);
                    }
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (currentPrintInfo == null) return;


            #region 测试
            /*
            string s = "";
            for (int i = 0; i < 100; i++) s += string.Format("{0}\n", i);
            e.Graphics.DrawString(s, new Font(new FontFamily("宋体"), fontSize), Brushes.Black, 20,0);
            return;
             */
            #endregion

            var code = new BarCodeHelper.Code128();
            code.Height = barcodeHeight;//64;
            var barCodeImage = code.GetCodeImage(currentPrintInfo.ItemCode, BarCodeHelper.Code128.Encode.Code128B);

            int pointX = 0;
            int pointY = 0;
            int printWidth = e.PageSettings.Bounds.Width - e.PageSettings.Margins.Left * 2;
            int fontHeight = (int)e.Graphics.MeasureString("A", textFont).Height + 2;
            // 打印条码
            e.Graphics.DrawImage(barCodeImage, new Rectangle()
            {
                X = pointX,//10,
                Y = pointY,//10,
                Width = barCodeImage.Width,
                Height = barCodeImage.Height
            });


            e.Graphics.DrawString(string.Format("{0}\n{1}", currentPrintInfo.ItemCode, currentPrintInfo.ItemName), new Font(new FontFamily("黑体"), 15),
                Brushes.Black, pointX + barCodeImage.Width + 10, pointY);
            // e.Graphics.DrawString(currentPrintInfo.ItemName, new Font(new FontFamily("黑体"), 16), Brushes.Black, pointX + barCodeImage.Width + 10, 30);
            string stext = "原料产地：江苏\n      调料自备";
            e.Graphics.DrawString(stext, textFont /*new Font(new FontFamily("宋体"), fontSize)*/, Brushes.Black, //240, pointY);
                printWidth - e.Graphics.MeasureString(stext, textFont).Width, pointY);

            pointY += barcodeHeight + 5;
            e.Graphics.DrawString(string.Format("生产日期:{0}", currentPrintInfo.ProductionDate), textFont, Brushes.Black, pointX, pointY);

            stext = string.Format("保质日期:{0}", currentPrintInfo.ExpirationDate);
            e.Graphics.DrawString(stext, textFont, Brushes.Black, printWidth - e.Graphics.MeasureString(stext, textFont).Width, pointY);

            /*e.Graphics.DrawString(sdate1 + "".PadLeft(lineLen - Encoding.Default.GetBytes(sdate1 + sdate2).Length) + sdate2,// new Font(new FontFamily("宋体"), fontSize), 
                textFont,Brushes.Black, pointX, pointY);// 80);*/

            #region 测试
            /*
            string s = "";
            for (int i = 0; i < 6; i++) s += "0123456789";
            pointY += fontHeight;
            e.Graphics.DrawString(s, new Font(new FontFamily("宋体"), fontSize), Brushes.Black, pointX, pointY);
            */
            #endregion
            var list = GetBomItemPrintText(e.Graphics, currentPrintInfo.ItemBom, printWidth);

            // 物料组成，需要计算行数 100
            /*
            foreach (var item in list)
            {
                pointY += fontHeight;
                e.Graphics.DrawString(item, textFont , Brushes.Black, pointX, pointY);
            }
             * for (int i = 0; i < 3 - list.Count; i++) pointY += fontHeight;
            */
            stext = string.Join("\n", list.ToArray());
            pointY += fontHeight;
            e.Graphics.DrawString(stext, textFont, Brushes.Black, pointX, pointY);
            pointY += 3 * fontHeight;//(int)e.Graphics.MeasureString(stext, textFont).Height;          

            // 打印原料自备
            //pointY += fontHeight;
            //e.Graphics.DrawString("调料自备                原料产地：江苏", new Font(new FontFamily("宋体"), fontSize), Brushes.Black, pointX, pointY);

            var tips = "温馨提示：" + currentPrintInfo.Tips;
            list = GetSplitPrintText(e.Graphics, tips, printWidth);
            /*
            foreach (var item in list)
            {                
                e.Graphics.DrawString(item,textFont , Brushes.Black, pointX, pointY);
                pointY += fontHeight;//18;
            }
            */
            stext = string.Join("\n", list.ToArray());
            e.Graphics.DrawString(stext, textFont, Brushes.Black, pointX, pointY);
            pointY += (int)e.Graphics.MeasureString(stext, textFont).Height;

            e.Graphics.DrawString("参照NY/T 1529-2007标准执行 埃顿服务", textFont/*new Font(new FontFamily("宋体"), fontSize)*/, Brushes.Black, pointX, pointY);
        }

        /// <summary>
        ///  将一个长文本拆分成多个分别打印
        /// </summary>
        /// <param name="printString"></param>
        /// <param name="lineTexts">每行打印多少个</param>
        /// <returns></returns>
        private List<string> GetSplitPrintText(Graphics g, string printString, int lineLen)//int lineTexts)
        {
            var result = new List<string>();
            var item = "";
            for (int i = 0; i < printString.Length; i++)
            {
                //if (Encoding.Default.GetBytes(item + printString[i].ToString()).Length > lineLen)
                if (g.MeasureString(item + printString[i].ToString(), textFont).Width > lineLen)
                {
                    result.Add(item);
                    item = "".PadRight(10);
                }
                item += printString[i].ToString();
            }
            /*
            for (int i = 0; i < printString.Length; i++)
            {
                item += printString[i].ToString();
                if (item.GetStringLength() >= lineTexts)
                {
                    result.Add(item);
                    item = "";
                }
            }
             */
            if (!string.IsNullOrEmpty(item)) result.Add(item);

            return result;
        }

        private List<string> GetBomItemPrintText(Graphics g, Dictionary<string, string> dict, int lineLen)
        {
            var result = new List<string>();
            var lineText = "";
            int i = 0;

            foreach (var item in dict)
            {
                if (i++ > 8) break;
                string sitem = string.Format("{0} {1}", item.Key, item.Value);

                //if(Encoding.Default.GetBytes(sitem+lineText).Length > lineLen)
                if (g.MeasureString(sitem + lineText, textFont).Width > lineLen)
                {
                    result.Add(lineText);
                    lineText = "";
                }

                lineText += sitem + ' ';

                /*
                if (i < 10)
                {
                    //lineText += item.Key.PadRight(4, ' ') + item.Value;
                
                
                    if (i % 3 == 0)
                    {
                        result.Add(lineText);
                        lineText = "";
                    }
                    else
                    {
                        lineText += " ";
                    }
                }*/
            }

            if (!string.IsNullOrEmpty(lineText)) result.Add(lineText);
            return result;
        }

        /// <summary>
        /// 打印内容实体
        /// </summary>
        internal class PrintInfo
        {
            /// <summary>
            /// 物料代号
            /// </summary>
            public string ItemCode { get; set; }
            /// <summary>
            /// 菜品名称
            /// </summary>
            public string ItemName { get; set; }
            /// <summary>
            /// 物料清单
            /// </summary>
            public Dictionary<string, string> ItemBom { get; set; }
            /// <summary>
            /// 温馨提示
            /// </summary>
            public string Tips { get; set; }
            public string ProductionDate { get; set; }
            public string ExpirationDate { get; set; }
            /// <summary>
            /// 打印数量
            /// </summary>
            public short PrintQty { set; get; }

        }

        internal class PrintItemInfo
        {
            public string ItemGUID { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string CompNameCn { get; set; }
            public string RequiredDate { get; set; }
            public string ItemBom { get; set; }
            public string Tips { get; set; }
            public int Qty { get; set; }
            public int PrintQty { get; set; }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            dgvPrint.AutoGenerateColumns = false;

            string filter = string.Format(" AND o.IsPaid = 1 AND s.IsPrint =0 AND o.SiteGUID = '{0}'", UserLogin._AdminInfo.SiteGUID);

            filter += string.Format(" AND DATEDIFF(day,'{0}',o.RequiredDate) = 0", dtpPrintDate.Value);

            DataTable dt = BLL.SaleOrderItem.GetTable(filter);

            List<PrintItemInfo> list = dt.AsEnumerable().GroupBy(x => x.Field<string>("ItemGUID")).Select(x => new PrintItemInfo
            {
                ItemGUID = x.Key,
                ItemCode = x.FirstOrDefault().Field<string>("ItemCode"),
                ItemName = x.FirstOrDefault().Field<string>("ItemName"),
                CompNameCn = x.FirstOrDefault().Field<string>("CompNameCn"),
                RequiredDate = x.FirstOrDefault().Field<DateTime>("RequiredDate").ToString("yyyy-MM-dd"),
                ItemBom = BLL.ItemBom.GetTable(x.Key).AsEnumerable().ToDictionary(key => key["ItemName"], value => string.Concat(value["StdQty"].ToDecimal().ToString("G0"), value["NameEn"])).JSONSerialize(),
                Tips = x.FirstOrDefault().Field<string>("Tips"),
                Qty = x.Sum(n => n.Field<int>("Qty")),
                PrintQty = x.Sum(n => n.Field<int>("Qty"))
            }).ToList();

            if (list != null && list.Count > 0)
            {
                dgvPrint.RowTemplate = new DataGridViewRow();
                dgvPrint.DataSource = list;
            }
            else
            {
                dgvPrint.RowTemplate = new EmptyDataRow();
                list.Add(new PrintItemInfo() { });
                dgvPrint.DataSource = list;
            }
        }

        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAll.Checked)
            {
                foreach (DataGridViewRow dgvr in dgvPrint.Rows)
                {
                    dgvr.Cells["IsPrint"].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow dgvr in dgvPrint.Rows)
                {
                    dgvr.Cells["IsPrint"].Value = false;
                }
            }
        }

        private void cmbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //修改配置文件  
            config.AppSettings.Settings["Printer"].Value = cmbPrinter.Text;
            //保存  
            config.Save();
        }
    }
}
