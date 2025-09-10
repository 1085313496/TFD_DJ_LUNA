using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    /// <summary>
    /// 截取屏幕并检测指定图案是否存在
    /// </summary>
    public class ScreenPatternDetector
    {
        /// <summary>
        /// 屏幕上是否有指定的图案
        /// </summary>
        /// <param name="templateBitmap">指定的图</param>
        /// <param name="threshold">阈值[识别精度,0-1],通常设置在0.8~0.95</param>
        /// <returns></returns>
        public static bool IsPatternPresent(Bitmap templateBitmap, out System.Drawing.Point pt, double threshold = 0.8, bool useGray = false)
        {
            pt = new System.Drawing.Point(0, 0);

            // 1. 截取屏幕图像
            using (var screenBitmap = CaptureScreen())
            {
                // 2. 将Bitmap转换为OpenCV的Mat格式（并转换为RGB）
                using (Mat screenMat = BitmapToMat(screenBitmap))
                using (Mat templateMat = BitmapToMat(templateBitmap))
                {
                    Mat result = new Mat();
                    if (useGray)
                    {
                        // 3. 转换为灰度图（可选，但通常能提高性能）
                        Mat screenGray = new Mat();
                        Cv2.CvtColor(screenMat, screenGray, ColorConversionCodes.BGR2GRAY);

                        Mat templateGray = new Mat();
                        Cv2.CvtColor(templateMat, templateGray, ColorConversionCodes.BGR2GRAY);

                        // 4. 模板匹配
                        Cv2.MatchTemplate(screenGray, templateGray, result, TemplateMatchModes.CCoeffNormed);
                    }
                    else
                    {
                        // 4. 模板匹配
                        // 不做灰度转换，直接用BGR三通道做模板匹配
                        Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);
                    }

                    // 5. 获取最大匹配值
                    Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                    // 6. 判断是否超过阈值
                    if (maxVal >= threshold)
                    {
                        // 将 OpenCvSharp.Point 转换为 System.Drawing.Point
                        pt = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                        return true;
                    }
                    return false;
                }
            }
        }
        /// <summary>
        /// 屏幕上是否有指定的图案
        /// </summary>
        /// <param name="templatePath">图案路径</param>
        /// <param name="dtcf">识别配置</param>
        /// <returns></returns>
        public static bool IsPatternPresent(string templatePath, out System.Drawing.Point pt, int SA_X = 0, int SA_Y = 0, int SA_W = 0, int SA_H = 0, bool useGray = false, double threshold = 0.8)
        {
            Rectangle screenBounds = SystemInformation.VirtualScreen;
            Rectangle captureArea = new Rectangle(screenBounds.X, screenBounds.Y, screenBounds.Width, screenBounds.Height);
            if (SA_W > 0 && SA_H > 0)
                captureArea = new Rectangle(SA_X, SA_Y, SA_W, SA_H);

            pt = new System.Drawing.Point(0, 0);

            // 1. 截取屏幕图像
            using (var screenBitmap = CaptureScreen(captureArea))
            {
                //screenBitmap.Save("screen.png", System.Drawing.Imaging.ImageFormat.Png); // 保存截屏图像以供调试

                // 2. 将Bitmap转换为OpenCV的Mat格式（并转换为RGB）
                using (Mat screenMat = BitmapToMat(screenBitmap))
                using (Mat templateMat = new Mat(templatePath, ImreadModes.Color))
                {
                    Mat result = new Mat();
                    if (useGray)
                    {
                        // 3. 转换为灰度图（可选，但通常能提高性能）
                        Mat screenGray = new Mat();
                        Cv2.CvtColor(screenMat, screenGray, ColorConversionCodes.BGR2GRAY);
                        Mat templateGray = new Mat();
                        Cv2.CvtColor(templateMat, templateGray, ColorConversionCodes.BGR2GRAY);

                        // 4.模板匹配
                        Cv2.MatchTemplate(screenGray, templateGray, result, TemplateMatchModes.CCoeffNormed);
                    }
                    else
                    {
                        // 4. 模板匹配
                        // 不做灰度转换，直接用BGR三通道做模板匹配
                        Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);
                    }

                    // 5. 获取最大匹配值
                    Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                    // 6. 判断是否超过阈值
                    if (maxVal >= threshold)
                    {
                        // 将 OpenCvSharp.Point 转换为 System.Drawing.Point
                        pt = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                        return true;
                    }
                    return false;
                }
            }
        }

        /// <summary>
        /// 指定图案是否存在于指定的屏幕图像中
        /// </summary>
        /// <param name="targetImg"></param>
        /// <param name="screenBitmap"></param>
        /// <param name="pt"></param>
        /// <param name="useGray"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool IsPatternPresent(Bitmap targetImg, Bitmap screenBitmap, out System.Drawing.Point pt, bool useGray = false, double threshold = 0.8, bool useHSV = false)
        {
            pt = new System.Drawing.Point(0, 0);

            using (Mat screenMat = BitmapToMat(screenBitmap))
            using (Mat templateMat = BitmapToMat(targetImg))
            {
                Mat result = new Mat();

                if (useHSV)
                {
                    // 转换到HSV空间
                    Mat screenHSV = new Mat();
                    Mat templateHSV = new Mat();
                    Cv2.CvtColor(screenMat, screenHSV, ColorConversionCodes.BGR2HSV);
                    Cv2.CvtColor(templateMat, templateHSV, ColorConversionCodes.BGR2HSV);

                    // 只用H通道
                    Mat screenH = new Mat();
                    Mat templateH = new Mat();
                    Cv2.ExtractChannel(screenHSV, screenH, 0); // H通道
                    Cv2.ExtractChannel(templateHSV, templateH, 0);

                    Cv2.MatchTemplate(screenH, templateH, result, TemplateMatchModes.CCoeffNormed);
                }
                else if (useGray)
                {
                    Mat screenGray = new Mat();
                    Cv2.CvtColor(screenMat, screenGray, ColorConversionCodes.BGR2GRAY);
                    Mat templateGray = new Mat();
                    Cv2.CvtColor(templateMat, templateGray, ColorConversionCodes.BGR2GRAY);

                    Cv2.MatchTemplate(screenGray, templateGray, result, TemplateMatchModes.CCoeffNormed);
                }
                else
                {
                    Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);
                }

                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal >= threshold)
                {
                    pt = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 将模板图片按不同缩放比例（如0.8~1.2倍）多次匹配，找到最佳匹配结果。这样可以检测到不同大小的目标。
        /// </summary>
        /// <param name="targetImg"></param>
        /// <param name="screenBitmap"></param>
        /// <param name="pt"></param>
        /// <param name="threshold"></param>
        /// <param name="useHSV"></param>
        /// <param name="scales"></param>
        /// <returns></returns>
        public static bool IsPatternPresent_MultiScale(Bitmap targetImg, Bitmap screenBitmap, out System.Drawing.Point pt, double threshold = 0.8, bool useHSV = false, double[] scales = null)
        {
            pt = new System.Drawing.Point(0, 0);
            if (scales == null)
                scales = new double[] { 0.7, 0.8, 0.9, 1.0, 1.1, 1.2, 1.3 };

            using (Mat screenMat = BitmapToMat(screenBitmap))
            {
                double bestVal = double.MinValue;
                OpenCvSharp.Point bestLoc = new OpenCvSharp.Point(0, 0);

                foreach (var scale in scales)
                {
                    // 缩放模板
                    Bitmap scaledTemplate = new Bitmap(
                        targetImg,
                        (int)(targetImg.Width * scale),
                        (int)(targetImg.Height * scale)
                    );
                    using (Mat templateMat = BitmapToMat(scaledTemplate))
                    {
                        Mat result = new Mat();

                        if (useHSV)
                        {
                            Mat screenHSV = new Mat();
                            Mat templateHSV = new Mat();
                            Cv2.CvtColor(screenMat, screenHSV, ColorConversionCodes.BGR2HSV);
                            Cv2.CvtColor(templateMat, templateHSV, ColorConversionCodes.BGR2HSV);

                            Mat screenH = new Mat();
                            Mat templateH = new Mat();
                            Cv2.ExtractChannel(screenHSV, screenH, 0);
                            Cv2.ExtractChannel(templateHSV, templateH, 0);

                            Cv2.MatchTemplate(screenH, templateH, result, TemplateMatchModes.CCoeffNormed);
                        }
                        else
                        {
                            Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);
                        }

                        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                        if (maxVal > bestVal)
                        {
                            bestVal = maxVal;
                            bestLoc = maxLoc;
                        }
                    }
                    scaledTemplate.Dispose();
                }

                if (bestVal >= threshold)
                {
                    pt = new System.Drawing.Point(bestLoc.X, bestLoc.Y);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 指定图案是否存在于指定的屏幕图像中
        /// </summary>
        /// <param name="targetImg"></param>
        /// <param name="screenBitmap"></param>
        /// <param name="pt"></param>
        /// <param name="ImgMode"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool IsPatternPresent(Bitmap targetImg, Bitmap screenBitmap, out System.Drawing.Point pt, out double _MaxVal, int ImgMode = 0, double threshold = 0.8, int ScreenshotSharpen = 0, bool saveImg = false)
        {
            pt = new System.Drawing.Point(0, 0);
            _MaxVal = 0;

            using (Mat screenMat = BitmapToMat(screenBitmap))
            using (Mat templateMat = BitmapToMat(targetImg))
            {
                if (ScreenshotSharpen == 1)
                {
                    #region 将截图USM锐化处理
                    Mat blur = new Mat();
                    Mat usm = new Mat();

                    ////相当吃CPU
                    //Cv2.GaussianBlur(screenMat, blur, new OpenCvSharp.Size(0, 0), 25);
                    //Cv2.AddWeighted(screenMat, 1.5, blur, -0.5, 0, usm);

                    // 最佳性价比方案
                    Cv2.GaussianBlur(screenMat, blur, new OpenCvSharp.Size(5, 5), 2.0);
                    Cv2.AddWeighted(screenMat, 1.5, blur, -0.5, 0, usm);

                    if (saveImg)
                    {
                        #region 保存结果图像
                        //保存结果图像
                        string Dir = string.Format("{0}\\{1}", TFD_LUNA.rootPath, "锐化截图");
                        if (!System.IO.Directory.Exists(Dir))
                            System.IO.Directory.CreateDirectory(Dir);
                        screenMat.SaveImage(string.Format("{0}\\{1}_0.png", Dir, DateTime.Now.ToString("mm_ss_fff")));
                        #endregion
                    }
                    #endregion
                }

                if (ImgMode == -1)
                {
                    // 检查模板是否为纯色
                    if (IsUniformColor(templateMat))
                    {
                        // 使用直接颜色比较方法
                        return CheckUniformColorMatch(screenMat, templateMat, out pt, out _MaxVal, threshold);
                    }
                }

                // 否则使用模板匹配
                Mat result = new Mat();

                switch (ImgMode)
                {
                    case 1:
                        #region 灰度图
                        Mat screenGray = new Mat();
                        Cv2.CvtColor(screenMat, screenGray, ColorConversionCodes.BGR2GRAY);

                        Mat templateGray = new Mat();
                        Cv2.CvtColor(templateMat, templateGray, ColorConversionCodes.BGR2GRAY);

                        Cv2.MatchTemplate(screenGray, templateGray, result, TemplateMatchModes.CCoeffNormed);

                        if (saveImg)
                        {
                            #region 保存结果图像
                            ////保存结果图像
                            //string DirGray = string.Format("{0}\\{1}", TFD_LUNA.rootPath, "屏幕截图_灰度" + ImgMode.ToString());
                            //if (!System.IO.Directory.Exists(DirGray))
                            //    System.IO.Directory.CreateDirectory(DirGray);
                            //templateGray.SaveImage(string.Format("{0}\\{1}_GRAY_MB.png", DirGray, DateTime.Now.ToString("mm_ss_ffff")));
                            //screenGray.SaveImage(string.Format("{0}\\{1}_GRAY_JT.png", DirGray, DateTime.Now.ToString("mm_ss_ffff")));
                            #endregion
                        }
                        #endregion
                        break;
                    case 2:
                        #region HSV_H通道
                        // 转换到HSV空间
                        Mat screenHSV = new Mat();
                        Mat templateHSV = new Mat();
                        Cv2.CvtColor(screenMat, screenHSV, ColorConversionCodes.BGR2HSV);
                        Cv2.CvtColor(templateMat, templateHSV, ColorConversionCodes.BGR2HSV);

                        // H通道
                        Mat screenH = new Mat();
                        Mat templateH = new Mat();
                        Cv2.ExtractChannel(screenHSV, screenH, 0);
                        Cv2.ExtractChannel(templateHSV, templateH, 0);

                        Cv2.MatchTemplate(screenH, templateH, result, TemplateMatchModes.CCoeffNormed);

                        if (saveImg)
                        {
                            #region 保存结果图像
                            ////保存结果图像
                            //string DirHSV = string.Format("{0}\\{1}", TFD_LUNA.rootPath, "屏幕截图_HSV_H" + ImgMode.ToString());
                            //if (!System.IO.Directory.Exists(DirHSV))
                            //    System.IO.Directory.CreateDirectory(DirHSV);

                            //screenHSV.SaveImage(string.Format("{0}\\{1}_HSV_JT.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //templateHSV.SaveImage(string.Format("{0}\\{1}_HSV_MB.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //screenH.SaveImage(string.Format("{0}\\{1}_HSV_JT_H.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //templateH.SaveImage(string.Format("{0}\\{1}_HSV_MB_H.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            #endregion
                        }
                        #endregion
                        break;
                    case 3:
                        #region HSV_S通道
                        // 转换到HSV空间
                        Mat JT_HSV = new Mat();
                        Mat MB_HSV = new Mat();
                        Cv2.CvtColor(screenMat, JT_HSV, ColorConversionCodes.BGR2HSV);
                        Cv2.CvtColor(templateMat, MB_HSV, ColorConversionCodes.BGR2HSV);

                        //S通道
                        Mat JT_S = new Mat();
                        Cv2.ExtractChannel(JT_HSV, JT_S, 1);
                        Mat MB_S = new Mat();
                        Cv2.ExtractChannel(MB_HSV, MB_S, 1);

                        Cv2.MatchTemplate(JT_S, MB_S, result, TemplateMatchModes.CCoeffNormed);

                        if (saveImg)
                        {
                            #region 保存结果图像
                            ////保存结果图像
                            //string DirHSV = string.Format("{0}\\{1}", TFD_LUNA.rootPath, "屏幕截图_HSV_S" + ImgMode.ToString());
                            //if (!System.IO.Directory.Exists(DirHSV))
                            //    System.IO.Directory.CreateDirectory(DirHSV);

                            //JT_HSV.SaveImage(string.Format("{0}\\{1}_HSV_JT.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //MB_HSV.SaveImage(string.Format("{0}\\{1}_HSV_MB.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //JT_S.SaveImage(string.Format("{0}\\{1}_HSV_JT_S.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //MB_S.SaveImage(string.Format("{0}\\{1}_HSV_MB_S.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            #endregion
                        }
                        #endregion
                        break;
                    case 4:
                        #region HSV_H和S通道加权合并
                        // 转换到HSV空间
                        Mat JT_HSV_HS = new Mat();
                        Mat MB_HSV_HS = new Mat();
                        Cv2.CvtColor(screenMat, JT_HSV_HS, ColorConversionCodes.BGR2HSV);
                        Cv2.CvtColor(templateMat, MB_HSV_HS, ColorConversionCodes.BGR2HSV);

                        // H通道
                        Mat JT_H_HS = new Mat();
                        Mat MB_H_HS = new Mat();
                        Cv2.ExtractChannel(JT_HSV_HS, JT_H_HS, 0);
                        Cv2.ExtractChannel(MB_HSV_HS, MB_H_HS, 0);

                        // Cv2.MatchTemplate(screenH, templateH, result, TemplateMatchModes.CCoeffNormed);

                        //S通道
                        Mat JT_S_HS = new Mat();
                        Cv2.ExtractChannel(JT_HSV_HS, JT_S_HS, 1);
                        Mat MB_S_HS = new Mat();
                        Cv2.ExtractChannel(MB_HSV_HS, MB_S_HS, 1);

                        // 可以尝试将H和S通道加权合并
                        Mat JT_HS = new Mat();
                        Cv2.AddWeighted(JT_H_HS, 0.5, JT_S_HS, 0.5, 0, JT_HS);
                        Mat MB_HS = new Mat();
                        Cv2.AddWeighted(MB_H_HS, 0.5, MB_S_HS, 0.5, 0, MB_HS);

                        Cv2.MatchTemplate(JT_HS, MB_HS, result, TemplateMatchModes.CCoeffNormed);

                        if (saveImg)
                        {
                            #region 保存结果图像
                            ////保存结果图像
                            //string DirHSV = string.Format("{0}\\{1}", TFD_LUNA.rootPath, "屏幕截图_HSV_HS" + ImgMode.ToString());
                            //if (!System.IO.Directory.Exists(DirHSV))
                            //    System.IO.Directory.CreateDirectory(DirHSV);

                            //JT_HSV_HS.SaveImage(string.Format("{0}\\{1}_HSV_JT.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //MB_HSV_HS.SaveImage(string.Format("{0}\\{1}_HSV_MB.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //JT_HS.SaveImage(string.Format("{0}\\{1}_HSV_JT_HS.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            //MB_HS.SaveImage(string.Format("{0}\\{1}_HSV_MB_HS.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            #endregion
                        }
                        #endregion
                        break;
                    case 0:
                    default:
                        #region BGR
                        Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);

                        if (saveImg)
                        {
                            #region 保存结果图像
                            ////保存结果图像
                            //string Dir = string.Format("{0}\\{1}", TFD_LUNA.rootPath, "屏幕截图_BGR" + ImgMode.ToString());
                            //if (!System.IO.Directory.Exists(Dir))
                            //    System.IO.Directory.CreateDirectory(Dir);
                            //screenMat.SaveImage(string.Format("{0}\\{1}_bgr.png", Dir, DateTime.Now.ToString("mm_ss_ffff")));
                            #endregion
                        }
                        #endregion
                        break;
                }

                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);
                _MaxVal = maxVal;

                if (maxVal >= threshold)
                {
                    pt = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 检查Mat是否为纯色
        /// </summary>
        /// <param name="mat">输入图像</param>
        /// <param name="maxStdDev">最大标准差阈值，默认5</param>
        /// <returns>如果是纯色返回true，否则返回false</returns>
        private static bool IsUniformColor(Mat mat, double maxStdDev = 5)
        {
            Scalar mean, stdDev;
            Cv2.MeanStdDev(mat, out mean, out stdDev);
            // 检查所有通道的标准差是否都小于阈值
            for (int i = 0; i < 4; i++) // 通常有4个通道(B,G,R,A)
            {
                if (stdDev[i] > maxStdDev) // 使用索引器而不是Val属性
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 对于纯色模板，使用直接颜色比较进行匹配
        /// </summary>
        /// <param name="screenMat">屏幕截图</param>
        /// <param name="templateMat">模板图像</param>
        /// <param name="pt">匹配位置</param>
        /// <param name="threshold">颜色相似度阈值（0-1），默认0.8</param>
        /// <returns>如果找到匹配返回true，否则返回false</returns>
        private static bool CheckUniformColorMatch(Mat screenMat, Mat templateMat, out System.Drawing.Point pt, out double _MaxVal, double threshold = 0.8)
        {
            pt = new System.Drawing.Point(0, 0);
            _MaxVal = 0;

            // 计算模板的平均颜色
            Scalar templateMean = Cv2.Mean(templateMat);

            // 模板尺寸
            int tWidth = templateMat.Width;
            int tHeight = templateMat.Height;

            // 屏幕尺寸
            int sWidth = screenMat.Width;
            int sHeight = screenMat.Height;

            // 如果屏幕尺寸小于模板尺寸，直接返回false
            if (sWidth < tWidth || sHeight < tHeight)
                return false;

            // 滑动窗口遍历屏幕图像
            for (int y = 0; y <= sHeight - tHeight; y++)
            {
                for (int x = 0; x <= sWidth - tWidth; x++)
                {
                    // 获取当前窗口区域
                    Mat roi = new Mat(screenMat, new OpenCvSharp.Rect(x, y, tWidth, tHeight));
                    // 计算当前区域的平均颜色
                    Scalar roiMean = Cv2.Mean(roi);
                    roi.Dispose();

                    // 计算颜色差异（使用欧氏距离或绝对值）
                    double diff = 0;
                    for (int i = 0; i < 3; i++) // 只比较前3个通道（BGR）
                    {
                        diff += Math.Abs(roiMean[i] - templateMean[i]); // 使用索引器而不是Val属性
                    }
                    diff /= 3.0; // 平均差异

                    // 归一化差异（因为颜色值范围0-255，所以最大差异为255）
                    double similarity = 1 - (diff / 255.0);
                    _MaxVal = similarity;

                    if (similarity >= threshold)
                    {
                        pt = new System.Drawing.Point(x, y);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 截取整个屏幕
        /// </summary>
        /// <returns></returns>
        public static Bitmap CaptureScreen()
        {
            Rectangle screenBounds = SystemInformation.VirtualScreen;
            Bitmap bitmap = new Bitmap(screenBounds.Width, screenBounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, screenBounds.Size);
            }
            return bitmap;
        }
        /// <summary>
        /// 截取屏幕的指定区域
        /// </summary>
        /// <param name="captureArea">需要截取的屏幕区域</param>
        /// <returns>截取的屏幕图像</returns>
        public static Bitmap CaptureScreen(Rectangle captureArea)
        {
            Bitmap bitmap = new Bitmap(captureArea.Width, captureArea.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(captureArea.X, captureArea.Y, 0, 0, captureArea.Size);
            }
            return bitmap;
        }


        /// <summary>
        /// 将Bitmap转换为OpenCV的Mat（并处理通道顺序）
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private static Mat BitmapToMat(Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );
            try
            {
                // 创建一个与Bitmap相同大小的Mat
                Mat mat = new Mat(bitmap.Height, bitmap.Width, MatType.CV_8UC4, bitmapData.Scan0);
                // 转换颜色通道顺序：BGRA -> BGR
                Cv2.CvtColor(mat, mat, ColorConversionCodes.BGRA2BGR);
                return mat.Clone(); // 避免内存被释放
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
