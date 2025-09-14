using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    /// <summary>
    /// 截取屏幕并检测指定图案是否存在
    /// </summary>
    public class ScreenPatternDetector
    {
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
                scales = new double[] { 1.0, 1.1, 1.2, 1.3 };

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


                        Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);


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
                // 新增：预先计算模板的边缘特征（用于所有模式）
                Mat templateEdges = new Mat();
                Mat templateGray = new Mat();
                Cv2.CvtColor(templateMat, templateGray, ColorConversionCodes.BGR2GRAY);
                Cv2.Canny(templateGray, templateEdges, 50, 150);

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
                        string Dir = string.Format("{0}\\截图\\{1}", TFD_LUNA.rootPath, "锐化截图");
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
                        Cv2.CvtColor(templateMat, templateGray, ColorConversionCodes.BGR2GRAY);
                        Cv2.MatchTemplate(screenGray, templateGray, result, TemplateMatchModes.CCoeffNormed);

                        if (saveImg)
                        {
                            #region 保存结果图像
                            //保存结果图像
                            string DirGray = string.Format("{0}\\截图\\{1}", TFD_LUNA.rootPath, "屏幕截图_灰度" + ImgMode.ToString());
                            if (!System.IO.Directory.Exists(DirGray))
                                System.IO.Directory.CreateDirectory(DirGray);
                            templateGray.SaveImage(string.Format("{0}\\{1}_GRAY_MB.png", DirGray, DateTime.Now.ToString("mm_ss_ffff")));
                            screenGray.SaveImage(string.Format("{0}\\{1}_GRAY_JT.png", DirGray, DateTime.Now.ToString("mm_ss_ffff")));
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
                            //保存结果图像
                            string DirHSV = string.Format("{0}\\截图\\{1}", TFD_LUNA.rootPath, "屏幕截图_HSV_H" + ImgMode.ToString());
                            if (!System.IO.Directory.Exists(DirHSV))
                                System.IO.Directory.CreateDirectory(DirHSV);

                            screenHSV.SaveImage(string.Format("{0}\\{1}_HSV_JT.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            templateHSV.SaveImage(string.Format("{0}\\{1}_HSV_MB.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            screenH.SaveImage(string.Format("{0}\\{1}_HSV_JT_H.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            templateH.SaveImage(string.Format("{0}\\{1}_HSV_MB_H.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
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
                            //保存结果图像
                            string DirHSV = string.Format("{0}\\截图\\{1}", TFD_LUNA.rootPath, "屏幕截图_HSV_S" + ImgMode.ToString());
                            if (!System.IO.Directory.Exists(DirHSV))
                                System.IO.Directory.CreateDirectory(DirHSV);

                            JT_HSV.SaveImage(string.Format("{0}\\{1}_HSV_JT.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            MB_HSV.SaveImage(string.Format("{0}\\{1}_HSV_MB.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            JT_S.SaveImage(string.Format("{0}\\{1}_HSV_JT_S.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            MB_S.SaveImage(string.Format("{0}\\{1}_HSV_MB_S.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
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
                            //保存结果图像
                            string DirHSV = string.Format("{0}\\截图\\{1}", TFD_LUNA.rootPath, "屏幕截图_HSV_HS" + ImgMode.ToString());
                            if (!System.IO.Directory.Exists(DirHSV))
                                System.IO.Directory.CreateDirectory(DirHSV);

                            JT_HSV_HS.SaveImage(string.Format("{0}\\{1}_HSV_JT.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            MB_HSV_HS.SaveImage(string.Format("{0}\\{1}_HSV_MB.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            JT_HS.SaveImage(string.Format("{0}\\{1}_HSV_JT_HS.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
                            MB_HS.SaveImage(string.Format("{0}\\{1}_HSV_MB_HS.png", DirHSV, DateTime.Now.ToString("mm_ss_ffff")));
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
                            //保存结果图像
                            string Dir = string.Format("{0}\\截图\\{1}", TFD_LUNA.rootPath, "屏幕截图_BGR" + ImgMode.ToString());
                            if (!System.IO.Directory.Exists(Dir))
                                System.IO.Directory.CreateDirectory(Dir);
                            screenMat.SaveImage(string.Format("{0}\\{1}_bgr.png", Dir, DateTime.Now.ToString("mm_ss_ffff")));
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
        /// 截全屏与1080P分辨率对比计算出模板的缩放比例
        /// </summary>
        /// <returns></returns>
        public double GetScaleFactor()
        {
            double scaleFactor = 1;

            try
            {
                // 1. 定义制作模板时的基准分辨率（1080P）
                System.Drawing.Size templateResolution = new System.Drawing.Size(1920, 1080);

                // 2. 获取当前帧
                Bitmap screenshot = CaptureScreen();
                System.Drawing.Size currentFrameSize = screenshot.Size;

                // 3. 【关键】选择一个策略来计算【统一】的缩放因子
                // 这里使用最常用的【基于宽度】的策略
                scaleFactor = (double)currentFrameSize.Width / templateResolution.Width;
            }
            catch (Exception e) { }

            return scaleFactor;
        }

        /// <summary>
        /// 检测某张图片是否有特定的某个图案
        /// </summary>
        /// <param name="templateImg">模板图片</param>
        /// <param name="templateMask">模板图掩码 灰度图，黑色部分为忽略部分，白色为保留检测部分</param>
        /// <param name="screenBitmap">截图</param>
        /// <param name="MaxVal">返回的 匹配度</param>
        /// <param name="Pt">返回的 匹配位置</param>
        /// <param name="OutImg">返回的带位置标记的图片</param>
        /// <param name="Threshold">阈值 大于等于此值视为成功匹配</param>
        /// <param name="ScaleFactor">模板缩放比例</param>
        /// <param name="ImgMode">图片处理模式 【-1：纯色块； 0：BGR； 1：灰度图； 2：HSV仅H通道； 3：HSV仅S通道；4：HSV_H+S通道】</param>
        /// <param name="UseMask">是否使用掩码 【1：自动生成掩码； 2：使用手动生成的掩码； 其他值：不使用掩码】</param>
        /// <param name="SaveImg">是否保存图片</param>
        /// <returns></returns>
        public static bool IsPatternPresent_New(Bitmap templateImg, Bitmap templateMask, Bitmap screenBitmap
            , out double MaxVal, out System.Drawing.Point Pt, out Bitmap OutImg
            , double Threshold = 0.8, double ScaleFactor = 1.0
            , int ImgMode = 0, int UseMask = 0, bool SaveImg = false)
        {
            MaxVal = 0;
            Pt = new System.Drawing.Point(0, 0);
            OutImg = null;


            using (Mat mat_template_Ori = BitmapToMat(templateImg))
            using (Mat mat_screen = BitmapToMat(screenBitmap))  //截图
            {
                double minVal = 0;
                Mat result = new Mat();         //检测结果
                Mat mat_template = new Mat();   //模板图案
                OpenCvSharp.Point minLoc, maxLoc;//最小/最大匹配度
                Mat mask = new Mat();//掩码

                try
                {
                    #region 若分辨率不是1080P,则对模板【mat_template】缩放
                    if (ScaleFactor != 1)
                    {
                        //使用【同一个】scaleFactor来计算模板的新尺寸（保持宽高比！）
                        OpenCvSharp.Size newTemplateSize = new OpenCvSharp.Size(
                            (int)Math.Round(mat_template_Ori.Width * ScaleFactor), // 宽用同一个因子
                            (int)Math.Round(mat_template_Ori.Height * ScaleFactor) // 高用同一个因子
                        );

                        // 按模板自身长宽比缩放模板 
                        Cv2.Resize(mat_template_Ori, mat_template, newTemplateSize);
                    }
                    else
                    {
                        mat_template = BitmapToMat(templateImg);
                    }
                    #endregion

                    #region 加载或生成掩码
                    if (UseMask == 2 && templateMask == null)
                        UseMask = 1;

                    switch (UseMask)
                    {
                        case 1:
                            #region 自动掩码
                            mask = new Mat(mat_template.Height, mat_template.Width, MatType.CV_8UC1);
                            // 将模板转换为灰度图
                            Mat grayTemplate = new Mat();
                            Cv2.CvtColor(mat_template, grayTemplate, ColorConversionCodes.BGR2GRAY);
                            // 对灰度图进行阈值处理，得到二值掩码
                            Cv2.Threshold(grayTemplate, mask, 10, 255, ThresholdTypes.Binary); // 此时mask是单通道，值与templateImage尺寸相同
                            #endregion
                            break;
                        case 2:
                            #region 手动掩码
                            Mat mask0 = BitmapToMat(templateMask);
                            Cv2.CvtColor(mask0, mask, ColorConversionCodes.BGR2GRAY);
                            #endregion
                            break;
                        default: break;
                    }
                    #endregion

                    switch (ImgMode)
                    {
                        case -1:
                            #region 检查模板是否为纯色 使用直接颜色比较方法
                            if (IsUniformColor(mat_template))
                                return CheckUniformColorMatch(mat_screen, mat_template, out Pt, out MaxVal, Threshold);
                            #endregion
                            break;
                        case 1:
                            #region 灰度图
                            Mat screen_Gray = new Mat();
                            Mat template_Gray = new Mat();
                            Cv2.CvtColor(mat_screen, screen_Gray, ColorConversionCodes.BGR2GRAY);
                            Cv2.CvtColor(mat_template, template_Gray, ColorConversionCodes.BGR2GRAY);

                            Cv2.MatchTemplate(screen_Gray, template_Gray, result, TemplateMatchModes.CCoeffNormed, mask);
                            #endregion
                            break;
                        case 2:
                            #region HSV_H通道
                            // 转换到HSV空间
                            Mat screenHSV = new Mat();
                            Mat templateHSV = new Mat();
                            Cv2.CvtColor(mat_screen, screenHSV, ColorConversionCodes.BGR2HSV);
                            Cv2.CvtColor(mat_template, templateHSV, ColorConversionCodes.BGR2HSV);

                            // H通道
                            Mat screenH = new Mat();
                            Mat templateH = new Mat();
                            Cv2.ExtractChannel(screenHSV, screenH, 0);
                            Cv2.ExtractChannel(templateHSV, templateH, 0);

                            Cv2.MatchTemplate(screenH, templateH, result, TemplateMatchModes.CCoeffNormed, mask);
                            #endregion
                            break;
                        case 3:
                            #region HSV_S通道
                            // 转换到HSV空间
                            Mat JT_HSV = new Mat();
                            Mat MB_HSV = new Mat();
                            Cv2.CvtColor(mat_screen, JT_HSV, ColorConversionCodes.BGR2HSV);
                            Cv2.CvtColor(mat_template, MB_HSV, ColorConversionCodes.BGR2HSV);

                            //S通道
                            Mat JT_S = new Mat();
                            Cv2.ExtractChannel(JT_HSV, JT_S, 1);
                            Mat MB_S = new Mat();
                            Cv2.ExtractChannel(MB_HSV, MB_S, 1);

                            Cv2.MatchTemplate(JT_S, MB_S, result, TemplateMatchModes.CCoeffNormed, mask);
                            #endregion
                            break;
                        case 4:
                            #region HSV_H和S通道加权合并
                            // 转换到HSV空间
                            Mat JT_HSV_HS = new Mat();
                            Mat MB_HSV_HS = new Mat();
                            Cv2.CvtColor(mat_screen, JT_HSV_HS, ColorConversionCodes.BGR2HSV);
                            Cv2.CvtColor(mat_template, MB_HSV_HS, ColorConversionCodes.BGR2HSV);

                            // H通道
                            Mat JT_H_HS = new Mat();
                            Mat MB_H_HS = new Mat();
                            Cv2.ExtractChannel(JT_HSV_HS, JT_H_HS, 0);
                            Cv2.ExtractChannel(MB_HSV_HS, MB_H_HS, 0);

                            //S通道
                            Mat JT_S_HS = new Mat();
                            Cv2.ExtractChannel(JT_HSV_HS, JT_S_HS, 1);
                            Mat MB_S_HS = new Mat();
                            Cv2.ExtractChannel(MB_HSV_HS, MB_S_HS, 1);

                            // 将H和S通道加权合并
                            Mat JT_HS = new Mat();
                            Cv2.AddWeighted(JT_H_HS, 0.5, JT_S_HS, 0.5, 0, JT_HS);
                            Mat MB_HS = new Mat();
                            Cv2.AddWeighted(MB_H_HS, 0.5, MB_S_HS, 0.5, 0, MB_HS);

                            Cv2.MatchTemplate(JT_HS, MB_HS, result, TemplateMatchModes.CCoeffNormed, mask);
                            #endregion
                            break;
                        case 0:
                        default:
                            #region BGR
                            Cv2.MatchTemplate(image: mat_screen,
                                templ: mat_template,
                                result: result,
                                method: TemplateMatchModes.CCoeffNormed,
                                mask: mask
                            );
                            #endregion
                            break;
                    }

                    //查找最佳匹配位置
                    Cv2.MinMaxLoc(result, out minVal, out MaxVal, out minLoc, out maxLoc);

                    #region  绘制矩形框标记匹配位置（假设我们使用CCoeffNormed，最大值位置是最佳匹配）
                    OpenCvSharp.Point matchLoc = maxLoc;
                    Cv2.Rectangle(
                        img: mat_screen,
                        pt1: matchLoc,
                        pt2: new OpenCvSharp.Point(matchLoc.X + mat_template.Cols, matchLoc.Y + mat_template.Rows),
                        color: new Scalar(0, 255, 0),
                        thickness: 2
                    );
                    OutImg = BitmapConverter.ToBitmap(mat_screen);
                    #endregion

                    //判断结果
                    if (MaxVal >= Threshold)
                    {
                        Pt = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                        return true;
                    }
                    return false;
                }
                catch { return false; }
                finally
                {
                    result.Dispose();
                    mat_template.Dispose();
                    mask.Dispose();
                }
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
        /// Gamma校正
        /// </summary>
        /// <param name="src"></param>
        /// <param name="gamma">Gamma值 (>1降低亮度，<1增加亮度)</param>
        /// <returns></returns>
        public static Mat ImgGamm(Mat src, double gamma = 1.5)
        {
            Mat dst = new Mat();

            using (Mat lookupTable = new Mat(1, 256, MatType.CV_8U))
            {

                // 逐个设置查找表的值
                for (int i = 0; i < 256; i++)
                {
                    byte value = (byte)(Math.Pow(i / 255.0, gamma) * 255.0);
                    lookupTable.Set<byte>(0, i, value);
                }

                Cv2.LUT(src, lookupTable, dst);
            }
            return dst;

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
                Mat mat = Mat.FromPixelData(bitmap.Height, bitmap.Width, MatType.CV_8UC4, bitmapData.Scan0);
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
