using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    public class TFD_LUNA
    {
        public static readonly string rootPath = Application.StartupPath;
        /// <summary>
        /// 菱形图案检测阈值，默认0.8
        /// </summary>
        public double Threshold_Rhombus = 0.8;
        /// <summary>
        /// 屏幕截图间隔，单位毫秒
        /// </summary>
        public int ScreenShotInterval = 123;

        /// <summary>
        /// 识别菱形方块的矩形范围
        /// </summary>
        public Rectangle Rec_RhombusArea { get; set; }

        Bitmap d_0;
        Bitmap d_1;
        Bitmap d_2;
        Bitmap d_3;

        private SendKeyboardMouse SendKBM = new SendKeyboardMouse();
        /// <summary>
        /// 主体线程
        /// </summary>
        private Thread th = null;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;

        public void init()
        {
            Rectangle screenBounds = SystemInformation.VirtualScreen;
            int SearchAreaW = 80;
            int SearchAreaH = 75;
            int SearchAreaX = (screenBounds.Width - SearchAreaW) / 2;
            int SearchAreaY = screenBounds.Height / 2 + 5;
            Rec_RhombusArea = new Rectangle(SearchAreaX, SearchAreaY, SearchAreaW, SearchAreaH);

            d_0 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj0.png", rootPath));
            d_1 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj1.png", rootPath));
            d_2 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj2.png", rootPath));
            d_3 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj3.png", rootPath));
        }

        public void Start()
        {
            try
            {
                init();

                th = new Thread(new ThreadStart(() =>
                {
                    IsRunning = true;
                    while (IsRunning)
                    {
                        Noise();
                        Thread.Sleep(ScreenShotInterval);
                    }
                }));
                th.IsBackground = true;
                th.Start();
            }
            catch { }
        }
        public void Stop()
        {
            try
            {
                IsRunning = false;

                if (th != null && th.IsAlive)
                {
                    th.Join();
                    th = null;
                }

                if (d_0 != null)
                    d_0.Dispose();
                if (d_1 != null)
                    d_1.Dispose();
                if (d_2 != null)
                    d_2.Dispose();
                if (d_3 != null)
                    d_3.Dispose();
            }
            catch { }
        }

        #region 噪音涌动输出流
        /// <summary>
        /// 识别到菱形图案后按键的方式 [0 严格按照映射,如:图案0按下C 1按下V 2按下Z 3按下C] [1 依序交替按下CVZ]
        /// </summary>
        public int SwitchType_Noise = 0;
        /// <summary>
        /// 噪音涌动输出流上次按下的按键 0Q 1C 2V 3Z
        /// </summary>
        public int LaskKey_Noise = 0;
        /// <summary>
        /// 噪音涌动输出流
        /// </summary>
        /// <param name="threshold"></param>
        public void Noise()
        {
            try
            {
                #region 截取指定区域
                Bitmap scImg;
                if (Rec_RhombusArea.Width <= 0 || Rec_RhombusArea.Height <= 0)
                {
                    Rectangle screenBounds = SystemInformation.VirtualScreen;

                    int SearchAreaW = 80;
                    int SearchAreaH = 75;

                    int SearchAreaX = (screenBounds.Width - SearchAreaW) / 2;
                    int SearchAreaY = screenBounds.Height / 2 + 5;

                    Rectangle rt = new Rectangle(SearchAreaX, SearchAreaY, SearchAreaW, SearchAreaH);
                    scImg = ScreenPatternDetector.CaptureScreen(rt);
                }
                else
                    scImg = ScreenPatternDetector.CaptureScreen(Rec_RhombusArea);
                #endregion

                #region 识别截图是否有菱形图案，并按下对应按键
                Point pt = new Point(0, 0);
                if (ScreenPatternDetector.IsPatternPresent(d_0, scImg, out pt, false, Threshold_Rhombus, useHSV: true))
                {
                    Presskey("C", pt, "0");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_1, scImg, out pt, false, Threshold_Rhombus, useHSV: true))
                {
                    Presskey("V", pt, "1");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_2, scImg, out pt, false, Threshold_Rhombus, useHSV: true))
                {
                    Presskey("Z", pt, "2");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_3, scImg, out pt, false, Threshold_Rhombus, useHSV: true))
                {
                    Presskey("C", pt, "3");
                }
                else
                {
                    // MessageShowList.SendEventMsg("未检测到任何图案", 3);
                }
                #endregion
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(ex.Message, 1); }
        }
        /// <summary>
        /// 按下指定按键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pt"></param>
        /// <param name="imgNum"></param>
        private void Presskey(string key, Point pt, string imgNum)
        {
            try
            {
                if (SwitchType_Noise == 0)
                {
                    MessageShowList.SendEventMsg(string.Format("检测到图案{2},位置: {0}，将按下{1}", pt, key, imgNum), 1);
                    byte key1 = GlobalParams.GetKeyCode(key);
                    SendKBM.SendKeyPress(key1);
                }
                else
                {
                    string _keycode = "";
                    switch (LaskKey_Noise)
                    {
                        case 0: { _keycode = "C"; break; }
                        case 1: { _keycode = "V"; break; }
                        case 2: { _keycode = "Z"; break; }
                        case 3: { _keycode = "C"; break; }
                        default: { _keycode = "C"; break; }
                    }
                    MessageShowList.SendEventMsg(string.Format("检测到图案{2},位置: {0}，将按下{1}", pt, _keycode, imgNum), 1);
                    byte key1 = GlobalParams.GetKeyCode(_keycode);
                    SendKBM.SendKeyPress(key1);
                    LaskKey_Noise = LaskKey_Noise >= 3 ? 1 : LaskKey_Noise + 1;
                }
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg(ex.Message, 1);
            }
        }

        #endregion


        #region 上buff工具人
        public void Assist()
        {

        }
        #endregion
    }
}
