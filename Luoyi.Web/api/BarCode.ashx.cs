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
    public class BarCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/Jpeg";
            var orderCode = WebHelper.GetQueryString("OrderCode");
            string tmpBarcode = WebHelper.GetQueryString("BarcodeOfSO");
            
            BarCodeHelper.Code128 barCode = new BarCodeHelper.Code128();
            barCode.Height = 150;
            barCode.Magnify = 2;
            Bitmap bitmap = new Bitmap(567, (int)barCode.Height);
            if (string.IsNullOrWhiteSpace(tmpBarcode) || tmpBarcode.Trim().ToLower() == "barcode")
                //barCode.ValueFont = new Font(new FontFamily("宋体"), 12);
                bitmap = barCode.GetCodeImage(orderCode, BarCodeHelper.Code128.Encode.Code128B);
            else
                bitmap = (new QrCodeHelper()).ThoughtWorksQRCode(orderCode);

            //if(orderCode.EndsWith("10004049"))
            //    bitmap.Save(@"E:\git\jc\Luoyi.Web\img\" + Guid.NewGuid().ToString() + ".jpg");
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bitmap.Dispose();
                context.Response.ClearContent();
                context.Response.BinaryWrite(ms.ToArray());
                context.Response.End();
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