using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
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
