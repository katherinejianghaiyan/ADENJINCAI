using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Luoyi.Common;
using ThoughtWorks.QRCode.Codec;

namespace Luoyi.Web.api
{
    /// <summary>
    /// BarCode 的摘要说明
    /// </summary>
    public class WxBarCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/Jpeg";
            var orderCode = WebHelper.GetQueryString("OrderCode");
            string tmpBarcode =  WebHelper.GetQueryString("BarcodeOfSO");
            string isSendWX = WebHelper.GetQueryString("ShowType"); //发送微信消息

            BarCodeHelper.Code128 barCode = new BarCodeHelper.Code128();
            barCode.Height = 315;
            if (isSendWX.ToLower() != "sendwxmsg") barCode.Height = 150;
            barCode.Magnify = 2;
            Bitmap bitmap;
            if (string.IsNullOrWhiteSpace(tmpBarcode) || tmpBarcode.Trim().ToLower() == "barcode")
                //barCode.ValueFont = new Font(new FontFamily("宋体"), 12);
                bitmap = barCode.GetCodeImage(orderCode, BarCodeHelper.Code128.Encode.Code128B);
            else if (isSendWX.ToLower() != "sendwxmsg") //发货单中查询 QR code
            {
                bitmap = (new QrCodeHelper()).ThoughtWorksQRCode(orderCode);
            }
            else //发微信消息
            {
                using (Bitmap map = (new QrCodeHelper()).ThoughtWorksQRCode(orderCode))
                {
                    int x = 10;
                    bitmap = new Bitmap(Math.Ceiling((map.Width + 2 * x) * 2.35).ToInt(),
                        Math.Ceiling((map.Width + 2 * x) * 2.35 / 16 * 9).ToInt());
                    using (Graphics gra = Graphics.FromImage(bitmap))
                    {
                        gra.Clear(Color.White);

                        //gra.FillRectangle(new SolidBrush(Color.Red), 0, 0, bitmap.Width, bitmap.Height);

                        gra.DrawImage(map, (bitmap.Width - map.Width) / 2, (bitmap.Height - map.Height) / 2,
                             map.Width, map.Height);
                    }
                }
            }
            //bitmap.Save(@"D:\jc\img\" + Guid.NewGuid().ToString() + ".jpg");
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmap.Dispose();
                context.Response.ClearContent();
                context.Response.BinaryWrite(ms.ToArray());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}