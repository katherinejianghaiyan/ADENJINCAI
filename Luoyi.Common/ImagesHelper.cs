using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Luoyi.Common
{
    /// <summary>
    /// 图片操作：生成缩略图、添加水印、截取图片等
    /// </summary>
    public class ImagesHelper
    {

       

        /// <summary>
        /// 根据文件流获得图片宽度
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static int getImgWidth(Stream stream)
        {           
            Image img = Image.FromStream(stream);
            int result = img.Width;
            img.Dispose();
            stream.Dispose();
            return result;
        }

        /// <summary>
        /// 根据图片路径获得图片宽度
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static int getImgWidth(string filePath)
        {            
            Image img = Image.FromFile(filePath);
            int result = img.Width;
            img.Dispose();
            return result;
        }
       

        #region 从文件流生成缩略图
        /// <summary>
        /// 从文件流生成缩略图
        /// </summary>
        /// <param name="stream">数据IO流</param>
        /// <param name="savePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static bool GetThumbNail(Stream stream, string savePath, int width, int height, ThumbNailScale scale)
        {
            //缩略图
            Image img = Image.FromStream(stream);
            stream.Dispose();
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = img.Width;
            int oh = img.Height;
            //如果图片小于指定宽度
            if (ow < width)
                width = ow;

            if (oh < height)
                height = oh;


            switch (scale)
            {
                case ThumbNailScale.Appointed:
                    break;
                case ThumbNailScale.ScaleWidth:
                    toheight = img.Height * width / img.Width;
                    break;
                case ThumbNailScale.ScaleHeight:
                    towidth = img.Width * height / img.Height;
                    break;
                case ThumbNailScale.Cut:
                    if ((double)img.Width / (double)img.Height > (double)towidth / (double)toheight)
                    {
                        oh = img.Height;
                        ow = img.Height * towidth / toheight;
                        y = 0;
                        x = (img.Width - ow) / 2;
                    }
                    else
                    {
                        ow = img.Width;
                        oh = img.Width * height / towidth;
                        x = 0;
                        y = (img.Height - oh) / 2;
                    }
                    break;
                case ThumbNailScale.ScaleDown:
                    double Tw, Th;
                    Tw = width;
                    Th = height * (Convert.ToDouble(oh) / Convert.ToDouble(ow));
                    if (Th > height)
                    {
                        Th = height;
                        Tw = width * (Convert.ToDouble(ow) / Convert.ToDouble(oh));
                    }
                    towidth = Convert.ToInt32(Tw);
                    toheight = Convert.ToInt32(Th);
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);


            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(img, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(savePath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("从文件流生成缩略图：{0}", ex.Message));
                return false;
            }
            finally
            {
                img.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

            return true;
        }
        #endregion

        #region 从文件路径生成缩略图
        /// <summary>
        /// 从图片路径生成缩略图
        /// </summary>
        /// <param name="originalImagePath">图片路径</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">HW:指定高宽缩放（可能变形） W://指定宽，高按比例 H://指定高，宽按比例 Cut://指定高宽裁减（不变形） </param>
        /// <returns></returns>
        public static bool GetThumbNail(string originalImagePath, string savePath, int width, int height, ThumbNailScale scale)
        {
            //缩略图
            Image img = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = img.Width;
            int oh = img.Height;

            //如果图片小于指定宽度
            if (ow < width)
                width = ow;

            switch (scale)
            {
                case ThumbNailScale.Appointed:
                    break;
                case ThumbNailScale.ScaleWidth:
                    toheight = img.Height * width / img.Width;
                    break;
                case ThumbNailScale.ScaleHeight:
                    towidth = img.Width * height / img.Height;
                    break;
                case ThumbNailScale.Cut:
                    if ((double)img.Width / (double)img.Height > (double)towidth / (double)toheight)
                    {
                        oh = img.Height;
                        ow = img.Height * towidth / toheight;
                        y = 0;
                        x = (img.Width - ow) / 2;
                    }
                    else
                    {
                        ow = img.Width;
                        oh = img.Width * height / towidth;
                        x = 0;
                        y = (img.Height - oh) / 2;
                    }
                    break;
                case ThumbNailScale.ScaleDown:
                    double Tw, Th;
                    Tw = width;
                    Th = height * (Convert.ToDouble(oh) / Convert.ToDouble(ow));
                    if (Th > height)
                    {
                        Th = height;
                        Tw = width * (Convert.ToDouble(ow) / Convert.ToDouble(oh));
                    }
                    towidth = Convert.ToInt32(Tw);
                    toheight = Convert.ToInt32(Th);
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(img, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(savePath, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                img.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

            return true;

        }
        #endregion

        #region 获取图片格式
        /// <summary>
        /// 获取图片格式
        /// </summary>
        /// <param name="strContentType"></param>
        /// <returns>返回图片格式</returns>
        public static ImageFormat GetImageType(object strContentType)
        {
            if ((strContentType.ToString().ToLower()) == "image/pjpeg")
            {
                return ImageFormat.Jpeg;
            }
            else if ((strContentType.ToString().ToLower()) == "image/gif")
            {
                return ImageFormat.Gif;
            }
            else if ((strContentType.ToString().ToLower()) == "image/bmp")
            {
                return ImageFormat.Bmp;
            }
            else if ((strContentType.ToString().ToLower()) == "image/tiff")
            {
                return ImageFormat.Tiff;
            }
            else if ((strContentType.ToString().ToLower()) == "image/x-icon")
            {
                return ImageFormat.Icon;
            }
            else if ((strContentType.ToString().ToLower()) == "image/x-png")
            {
                return ImageFormat.Png;
            }
            else if ((strContentType.ToString().ToLower()) == "image/x-emf")
            {
                return ImageFormat.Emf;
            }
            else if ((strContentType.ToString().ToLower()) == "image/x-exif")
            {
                return ImageFormat.Exif;
            }
            else if ((strContentType.ToString().ToLower()) == "image/x-wmf")
            {
                return ImageFormat.Wmf;
            }
            else
            {
                return ImageFormat.MemoryBmp;
            }
        }
        #endregion

        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="saveFile">保存文件路径</param>
        /// <returns></returns>
        public static bool MakeWaterImage(Stream sourceFile, string saveFile)
        {
            bool result = false;
            //水印图片
            try
            {
                Image imgPhoto = Image.FromStream(sourceFile);
                sourceFile.Close();
                sourceFile.Dispose();

                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Image imgWatermark = new Bitmap(System.Web.HttpContext.Current.Server.MapPath("/images/watermark.png"));
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;

                if (phWidth > (wmWidth + 100) && phHeight > (wmHeight + 100))
                {
                    Graphics grPhoto = Graphics.FromImage(bmPhoto);
                    grPhoto.Clear(Color.White);
                    grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

                    grPhoto.Dispose();

                    //添加水印图片

                    using (Bitmap bmWatermark = new Bitmap(bmPhoto))
                    {
                        bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                        Graphics grWatermark = Graphics.FromImage(bmWatermark);
                        using (ImageAttributes imageAttributes = new ImageAttributes())
                        {
                            //ColorMap colorMap = new ColorMap();
                            //colorMap.OldColor = Color.FromArgb(255, 255, 255,255);
                            //colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            //ColorMap[] remapTable = { colorMap };
                            //imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                            float[][] colorMatrixElements = { new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f }, new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f }, new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f }, new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f }, new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f } };
                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            int xPosOfWm = ((phWidth - wmWidth) - 2);
                            int yPosOfWm = ((phHeight - wmHeight) - 2);
                            grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);
                        }
                        imgPhoto = bmWatermark;
                        grWatermark.Dispose();
                        imgPhoto.Save(saveFile, ImageFormat.Jpeg);
                    }

                    result = true;
                }
                else
                {
                    result = false;
                }
                imgWatermark.Dispose();
                bmPhoto.Dispose();
                imgPhoto.Dispose();
            }
            catch(Exception ex)
            {
                Logger.Error(string.Format("生成水印图片错误:{0}", ex.Message));

                try
                {
                    Image imgPhoto2 = Image.FromStream(sourceFile);
                    imgPhoto2.Save(saveFile, ImageFormat.Jpeg);
                    imgPhoto2.Dispose();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }

            return result;

        }

        /// <summary>
        /// 生成水印图片
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="saveFile">保存文件路径</param>
        /// <param name="Location">位置 0 - 右下角 1 -  居中 2 - 右上角 3 - 左下角</param>
        /// <returns></returns>
        public static bool MakeWaterImage(Stream sourceFile, string saveFile, ImagePosition Position)
        {
            bool result = false;
            //水印图片
            try
            {
                Image imgPhoto = Image.FromStream(sourceFile);


                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Image imgWatermark = new Bitmap(System.Web.HttpContext.Current.Server.MapPath("/images/watermark.png"));
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;

                if (phWidth > (wmWidth + 100) && phHeight > (wmHeight + 100))
                {
                    Graphics grPhoto = Graphics.FromImage(bmPhoto);
                    grPhoto.Clear(Color.White);
                    grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);

                    grPhoto.Dispose();

                    //添加水印图片

                    using (Bitmap bmWatermark = new Bitmap(bmPhoto))
                    {
                        bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                        Graphics grWatermark = Graphics.FromImage(bmWatermark);
                        using (ImageAttributes imageAttributes = new ImageAttributes())
                        {
                            //ColorMap colorMap = new ColorMap();
                            //colorMap.OldColor = Color.FromArgb(255, 255, 255,255);
                            //colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                            //ColorMap[] remapTable = { colorMap };
                            //imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                            float[][] colorMatrixElements = { new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f }, new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f }, new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f }, new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f }, new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f } };
                            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            int xPosOfWm = 0;
                            int yPosOfWm = 0;
                            switch (Position)
                            {
                                case ImagePosition.BottomRight:
                                    xPosOfWm = ((phWidth - wmWidth) - 2);
                                    yPosOfWm = ((phHeight - wmHeight) - 2);
                                    break;
                                case ImagePosition.TopLeft:
                                    xPosOfWm = 2;
                                    yPosOfWm = 2;
                                    break;
                                case ImagePosition.TopRigth:
                                    xPosOfWm = ((phWidth - wmWidth) - 2);
                                    yPosOfWm = 2;
                                    break;
                                case ImagePosition.BottomLeft:
                                    xPosOfWm = 2;
                                    yPosOfWm = ((phHeight - wmHeight) - 2);
                                    break;
                                case ImagePosition.Center:
                                    xPosOfWm = ((phWidth / 2) - (wmWidth / 2));
                                    yPosOfWm = ((phHeight / 2) - (wmHeight / 2));
                                    break;
                            }
                            grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);
                        }
                        imgPhoto = bmWatermark;
                        grWatermark.Dispose();
                        imgPhoto.Save(saveFile, ImageFormat.Jpeg);
                    }

                    result = true;
                }
                else
                {
                    Image imgPhoto2 = Image.FromStream(sourceFile);
                    imgPhoto2.Save(saveFile, ImageFormat.Jpeg);
                    imgPhoto2.Dispose();
                    result = true;
                }
                imgWatermark.Dispose();
                bmPhoto.Dispose();
                imgPhoto.Dispose();
            }
            catch
            {

                try
                {
                    Image imgPhoto2 = Image.FromStream(sourceFile);
                    imgPhoto2.Save(saveFile, ImageFormat.Jpeg);
                    imgPhoto2.Dispose();
                    result = true;
                }
                catch
                {
                    result = false;
                }
            }

            sourceFile.Close();
            sourceFile.Dispose();

            return result;

        }

        #region 从图片中截取一张指定大小的图片
        /// <summary>
        /// 从图片中截取部分生成新图
        /// </summary>
        /// <param name="sFromFilePath">原始图片</param>
        /// <param name="saveFilePath">生成新图</param>
        /// <param name="width">截取图片宽度</param>
        /// <param name="height">截取图片高度</param>
        /// <param name="spaceX">截图图片X坐标</param>
        /// <param name="spaceY">截取图片Y坐标</param>
        public static void CaptureImage(string sFromFilePath, string saveFilePath, int width, int height, int spaceX, int spaceY)
        {
            //载入底图
            Image fromImage = Image.FromFile(sFromFilePath);
            int x = 0; //截取X坐标
            int y = 0; //截取Y坐标
            //原图宽与生成图片宽 之差
            //当小于0(即原图宽小于要生成的图)时，新图宽度为较小者 即原图宽度 X坐标则为0
            //当大于0(即原图宽大于要生成的图)时，新图宽度为设置值 即width X坐标则为 sX与spaceX之间较小者
            //Y方向同理
            int sX = fromImage.Width - width;
            int sY = fromImage.Height - height;
            if (sX > 0)
            {
                x = sX > spaceX ? spaceX : sX;
            }
            else
            {
                width = fromImage.Width;
            }
            if (sY > 0)
            {
                y = sY > spaceY ? spaceY : sY;
            }
            else
            {
                height = fromImage.Height;
            }

            //创建新图位图
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            //从作图区生成新图
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            //保存图象
            saveImage.Save(saveFilePath, ImageFormat.Jpeg);
            //释放资源
            saveImage.Dispose();
            bitmap.Dispose();
            graphic.Dispose();
        }
        #endregion

        public enum ImagePosition
        {
            /// <summary>
            /// 居中
            /// </summary>
            Center,
            /// <summary>
            /// 左上角
            /// </summary>
            TopLeft,
            /// <summary>
            /// 左下角
            /// </summary>
            BottomLeft,
            /// <summary>
            /// 右下角
            /// </summary>
            BottomRight,
            /// <summary>
            /// 右上角
            /// </summary>
            TopRigth
        }

        /// <summary>
        /// 图片
        /// </summary>
        public enum ThumbNailScale
        {
            /// <summary>
            /// 指定高宽缩放，图片长宽不一致会变形
            /// </summary>
            Appointed,
            /// <summary>
            /// 指定宽，高按比例    
            /// </summary>
            ScaleWidth,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            ScaleHeight,
            /// <summary>
            /// 指定高宽裁减，可能只显示部分图片
            /// </summary>
            Cut,
            /// <summary>
            /// 按图片比例缩放，不变形，显示全部图片（推荐）
            /// </summary>
            ScaleDown
        }
    }
}
