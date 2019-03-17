using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Luoyi.Common
{
    /// <summary>
    /// ͼƬ��������������ͼ�����ˮӡ����ȡͼƬ��
    /// </summary>
    public class ImagesHelper
    {

       

        /// <summary>
        /// �����ļ������ͼƬ���
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
        /// ����ͼƬ·�����ͼƬ���
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
       

        #region ���ļ�����������ͼ
        /// <summary>
        /// ���ļ�����������ͼ
        /// </summary>
        /// <param name="stream">����IO��</param>
        /// <param name="savePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static bool GetThumbNail(Stream stream, string savePath, int width, int height, ThumbNailScale scale)
        {
            //����ͼ
            Image img = Image.FromStream(stream);
            stream.Dispose();
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = img.Width;
            int oh = img.Height;
            //���ͼƬС��ָ�����
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

            //�½�һ��bmpͼƬ
            Image bitmap = new Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(Color.Transparent);


            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(img, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
                bitmap.Save(savePath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("���ļ�����������ͼ��{0}", ex.Message));
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

        #region ���ļ�·����������ͼ
        /// <summary>
        /// ��ͼƬ·����������ͼ
        /// </summary>
        /// <param name="originalImagePath">ͼƬ·��</param>
        /// <param name="savePath">����·��</param>
        /// <param name="width">����ͼ���</param>
        /// <param name="height">����ͼ�߶�</param>
        /// <param name="mode">HW:ָ���߿����ţ����ܱ��Σ� W://ָ�����߰����� H://ָ���ߣ������� Cut://ָ���߿�ü��������Σ� </param>
        /// <returns></returns>
        public static bool GetThumbNail(string originalImagePath, string savePath, int width, int height, ThumbNailScale scale)
        {
            //����ͼ
            Image img = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = img.Width;
            int oh = img.Height;

            //���ͼƬС��ָ�����
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

            //�½�һ��bmpͼƬ
            Image bitmap = new Bitmap(towidth, toheight);

            //�½�һ������
            Graphics g = Graphics.FromImage(bitmap);

            //���ø�������ֵ��
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //���ø�����,���ٶȳ���ƽ���̶�
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ���
            g.Clear(Color.White);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
            g.DrawImage(img, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //��jpg��ʽ��������ͼ
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

        #region ��ȡͼƬ��ʽ
        /// <summary>
        /// ��ȡͼƬ��ʽ
        /// </summary>
        /// <param name="strContentType"></param>
        /// <returns>����ͼƬ��ʽ</returns>
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
        /// ����ˮӡͼƬ
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="saveFile">�����ļ�·��</param>
        /// <returns></returns>
        public static bool MakeWaterImage(Stream sourceFile, string saveFile)
        {
            bool result = false;
            //ˮӡͼƬ
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

                    //���ˮӡͼƬ

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
                Logger.Error(string.Format("����ˮӡͼƬ����:{0}", ex.Message));

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
        /// ����ˮӡͼƬ
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="saveFile">�����ļ�·��</param>
        /// <param name="Location">λ�� 0 - ���½� 1 -  ���� 2 - ���Ͻ� 3 - ���½�</param>
        /// <returns></returns>
        public static bool MakeWaterImage(Stream sourceFile, string saveFile, ImagePosition Position)
        {
            bool result = false;
            //ˮӡͼƬ
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

                    //���ˮӡͼƬ

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

        #region ��ͼƬ�н�ȡһ��ָ����С��ͼƬ
        /// <summary>
        /// ��ͼƬ�н�ȡ����������ͼ
        /// </summary>
        /// <param name="sFromFilePath">ԭʼͼƬ</param>
        /// <param name="saveFilePath">������ͼ</param>
        /// <param name="width">��ȡͼƬ���</param>
        /// <param name="height">��ȡͼƬ�߶�</param>
        /// <param name="spaceX">��ͼͼƬX����</param>
        /// <param name="spaceY">��ȡͼƬY����</param>
        public static void CaptureImage(string sFromFilePath, string saveFilePath, int width, int height, int spaceX, int spaceY)
        {
            //�����ͼ
            Image fromImage = Image.FromFile(sFromFilePath);
            int x = 0; //��ȡX����
            int y = 0; //��ȡY����
            //ԭͼ��������ͼƬ�� ֮��
            //��С��0(��ԭͼ��С��Ҫ���ɵ�ͼ)ʱ����ͼ���Ϊ��С�� ��ԭͼ��� X������Ϊ0
            //������0(��ԭͼ�����Ҫ���ɵ�ͼ)ʱ����ͼ���Ϊ����ֵ ��width X������Ϊ sX��spaceX֮���С��
            //Y����ͬ��
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

            //������ͼλͼ
            Bitmap bitmap = new Bitmap(width, height);
            //������ͼ����
            Graphics graphic = Graphics.FromImage(bitmap);
            //��ȡԭͼ��Ӧ����д����ͼ��
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            //����ͼ��������ͼ
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            //����ͼ��
            saveImage.Save(saveFilePath, ImageFormat.Jpeg);
            //�ͷ���Դ
            saveImage.Dispose();
            bitmap.Dispose();
            graphic.Dispose();
        }
        #endregion

        public enum ImagePosition
        {
            /// <summary>
            /// ����
            /// </summary>
            Center,
            /// <summary>
            /// ���Ͻ�
            /// </summary>
            TopLeft,
            /// <summary>
            /// ���½�
            /// </summary>
            BottomLeft,
            /// <summary>
            /// ���½�
            /// </summary>
            BottomRight,
            /// <summary>
            /// ���Ͻ�
            /// </summary>
            TopRigth
        }

        /// <summary>
        /// ͼƬ
        /// </summary>
        public enum ThumbNailScale
        {
            /// <summary>
            /// ָ���߿����ţ�ͼƬ����һ�»����
            /// </summary>
            Appointed,
            /// <summary>
            /// ָ�����߰�����    
            /// </summary>
            ScaleWidth,
            /// <summary>
            /// ָ���ߣ�������
            /// </summary>
            ScaleHeight,
            /// <summary>
            /// ָ���߿�ü�������ֻ��ʾ����ͼƬ
            /// </summary>
            Cut,
            /// <summary>
            /// ��ͼƬ�������ţ������Σ���ʾȫ��ͼƬ���Ƽ���
            /// </summary>
            ScaleDown
        }
    }
}
