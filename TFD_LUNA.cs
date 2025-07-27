using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    public class TFD_LUNA
    {
        public static readonly string rootPath = Application.StartupPath;

        /// <summary>
        /// 屏幕截图间隔，单位毫秒
        /// </summary>
        public int ScreenShotInterval = 123;
        /// <summary>
        /// 运行类型 1噪音涌动输出流 2辅助流 缺省值1
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// opencv识图对比模式 0默认 1灰度图 2HSV
        /// </summary>
        public int OpencvMode { get; set; } = 2;

        /// <summary>
        /// 是否保存屏幕截图，缺省值false
        /// </summary>
        public bool SaveScreenShot { get; set; } = false;

        /// <summary>
        /// 菱形图案识别区域设置
        /// </summary>
        public RecognizeSetting RecognizeSetting_Rhombus { get; set; }
        /// <summary>
        /// 准星识别区域设置
        /// </summary>
        public RecognizeSetting RecognizeSetting_Crosshair { get; set; }
        /// <summary>
        /// 灵感条识别区域设置
        /// </summary>
        public RecognizeSetting RecognizeSetting_MuseBar { get; set; }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// 按Q之后的黄色菱形图案
        /// </summary>
        public Bitmap d_0;
        /// <summary>
        /// 按C之后的淡橙红色菱形图案
        /// </summary>
        public Bitmap d_1;
        /// <summary>
        /// 按V之后的浅蓝色菱形图案
        /// </summary>
        public Bitmap d_2;
        /// <summary>
        /// 按Z之后的淡紫色菱形图案
        /// </summary>
        public Bitmap d_3;
        /// <summary>
        /// 满灵感条图案
        /// </summary>
        public Bitmap MuseBar;
        /// <summary>
        /// 准星图案
        /// </summary>
        public Bitmap Crosshair;

        /// <summary>
        /// 灵感条满之后是否等待手动使用强化技能 [0不等待 1等待 2自动使用双强化 缺省值1]
        /// </summary>
        public int ManualSPSKILL { get; set; }

        /// <summary>
        /// 发送键盘鼠标操作的工具类实例
        /// </summary>
        private SendKeyboardMouse SendKBM = new SendKeyboardMouse();
        /// <summary>
        /// 主体线程
        /// </summary>
        private Thread th = null;

        public TFD_LUNA()
        {
            loadIni();
            init();
        }

        /// <summary>
        /// 从INI文件加载配置
        /// </summary>
        private void loadIni()
        {
            Mode = int.TryParse(Common.GetIniParamVal("全局设置", "Mode"), out int mode) ? mode : 1;
            ScreenShotInterval = int.TryParse(Common.GetIniParamVal("全局设置", "ScreenShotInterval"), out int interval) ? interval : 120;

            SwitchType_Noise = int.TryParse(Common.GetIniParamVal("噪音涌动输出流设置", "SwitchType_Noise"), out int switchTypeNoise) ? switchTypeNoise : 1;

            ManualSPSKILL = int.TryParse(Common.GetIniParamVal("辅助流设置", "ManualSPSKILL"), out int manualSPSKILL) ? manualSPSKILL : 1;
            SwitchType_Assist = int.TryParse(Common.GetIniParamVal("辅助流设置", "SwitchType_Assist"), out int switchTypeAssist) ? switchTypeAssist : 1;

            RecognizeSetting_Rhombus = new RecognizeSetting("菱形图案识别设置");
            RecognizeSetting_Crosshair = new RecognizeSetting("自瞄准星图案识别设置");
            RecognizeSetting_MuseBar = new RecognizeSetting("灵感条图案识别设置");

            OpencvMode = int.TryParse(Common.GetIniParamVal("菱形图案识别设置", "OpencvMode"), out int _opencvMode) ? _opencvMode : 2;

            Rectangle screenBounds = SystemInformation.VirtualScreen;
            if (RecognizeSetting_Rhombus.Area.Width <= 0 || RecognizeSetting_Rhombus.Area.Height <= 0)
            {
                int SearchAreaW = 80;
                int SearchAreaH = 75;
                int SearchAreaX = (screenBounds.Width - SearchAreaW) / 2;
                int SearchAreaY = screenBounds.Height / 2 + 5;
                RecognizeSetting_Rhombus.Area = new Rectangle(SearchAreaX, SearchAreaY, SearchAreaW, SearchAreaH);

                Common.SaveIniParamVal("菱形图案识别设置", "X", SearchAreaX.ToString());
                Common.SaveIniParamVal("菱形图案识别设置", "Y", SearchAreaY.ToString());
                Common.SaveIniParamVal("菱形图案识别设置", "W", SearchAreaW.ToString());
                Common.SaveIniParamVal("菱形图案识别设置", "H", SearchAreaH.ToString());
            }
        }

        /// <summary>
        /// 初始化识别图案
        /// </summary>
        public void init()
        {
            try
            {
                d_0 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj0.png", rootPath));
                d_1 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj1.png", rootPath));
                d_2 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj2.png", rootPath));
                d_3 = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\lunadj3.png", rootPath));
                MuseBar = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\ln_lgt.png", rootPath));
                Crosshair = new Bitmap(string.Format("{0}\\imgs\\tfdlunadj\\ln_zx.png", rootPath));
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("初始化识别图案失败: " + ex.Message, 1);
            }
        }

        /// <summary>
        /// 开启识别功能
        /// </summary>
        public void Start()
        {
            try
            {
                loadIni();
                init();

                th = new Thread(new ThreadStart(() =>
                {
                    IsRunning = true;
                    while (IsRunning)
                    {
                        if (Mode == 1)
                            Noise();
                        else
                            Assist();

                        Thread.Sleep(ScreenShotInterval);
                    }
                }));
                th.IsBackground = true;
                th.Start();
            }
            catch { }
        }
        /// <summary>
        /// 停止识别功能
        /// </summary>
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
        private int LaskKey_Noise = 0;
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
                if (RecognizeSetting_Rhombus.Area.Width <= 0 || RecognizeSetting_Rhombus.Area.Height <= 0)
                {
                    Rectangle screenBounds = SystemInformation.VirtualScreen;

                    int SearchAreaW = 80;
                    int SearchAreaH = 75;

                    int SearchAreaX = (screenBounds.Width - SearchAreaW) / 2;
                    int SearchAreaY = screenBounds.Height / 2 + 5;

                    Rectangle rt = new Rectangle(SearchAreaX, SearchAreaY, SearchAreaW, SearchAreaH);
                    scImg = ScreenPatternDetector.CaptureScreen(rt);

                    Common.SaveIniParamVal("菱形图案识别设置", "X", SearchAreaX.ToString());
                    Common.SaveIniParamVal("菱形图案识别设置", "Y", SearchAreaY.ToString());
                    Common.SaveIniParamVal("菱形图案识别设置", "W", SearchAreaW.ToString());
                    Common.SaveIniParamVal("菱形图案识别设置", "H", SearchAreaH.ToString());
                }
                else
                    scImg = ScreenPatternDetector.CaptureScreen(RecognizeSetting_Rhombus.Area);
                string dtestr = DateTime.Now.ToString("HH_mm_ss_ffff");
                #endregion

                #region 识别截图是否有菱形图案，并按下对应按键
                Point pt = new Point(0, 0);
                bool _userGrey = false;
                bool _useHSV = false;
                switch (OpencvMode)
                {
                    case 0:
                        _userGrey = false;
                        _useHSV = false;
                        break;
                    case 1:
                        _userGrey = true;
                        _useHSV = false;
                        break;
                    case 2:
                        _userGrey = false;
                        _useHSV = true;
                        break;
                }

                #region
                if (ScreenPatternDetector.IsPatternPresent(d_0, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                {
                    Presskey("C", pt, "0", dtestr);
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_1, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                {
                    Presskey("V", pt, "1", dtestr);
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_2, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                {
                    Presskey("Z", pt, "2", dtestr);
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_3, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                {
                    Presskey("C", pt, "3", dtestr);
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else
                {
                    // MessageShowList.SendEventMsg("未检测到任何图案", 3);
                    SaveScreenImg(scImg, dtestr, "不匹配的截图");
                }
                #endregion

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
        private void Presskey(string key, Point pt, string imgNum, string dtestr)
        {
            try
            {
                if (SwitchType_Noise == 0)
                {
                    MessageShowList.SendEventMsg(string.Format("检测到图案{2},位置: {0}，将按下{1}，{3}", pt, key, imgNum, dtestr), 1);
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

                    MessageShowList.SendEventMsg(string.Format("检测到图案{2},位置: {0}，将按下{1}，{3}", pt, _keycode, imgNum, dtestr), 1);
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

        private void SaveScreenImg(Bitmap img, string DateInfo, string Foldername = "截屏")
        {
            try
            {
                if (!SaveScreenShot)
                    return;

                string Dir = string.Format("{0}\\{1}", rootPath, Foldername);
                if (!System.IO.Directory.Exists(Dir))
                    System.IO.Directory.CreateDirectory(Dir);

                img.Save(string.Format("{0}\\{1}_0.png", Dir, DateInfo), ImageFormat.Png);
            }
            catch { }
        }

        #region 上buff工具人
        /// <summary>
        /// 识别到菱形图案后按键形式 【0 手动切换只按C或V；1交替自动按CV两个技能按键】
        /// </summary>
        public int SwitchType_Assist { get; set; }
        /// <summary>
        /// 辅助流当前按下的按键 1C 2V
        /// </summary>
        public int CurrentKey_Assist { get; set; } = 1;
        /// <summary>
        /// 切换辅助流当前自动按下的按键
        /// </summary>
        public void ChangeCurrentKey_Assist()
        {
            switch (CurrentKey_Assist)
            {
                case 1:
                    CurrentKey_Assist = 2;
                    break;
                case 2:
                    CurrentKey_Assist = 1;
                    break;
                default:
                    CurrentKey_Assist = 1;
                    break;
            }
            MessageShowList.SendEventMsg(string.Format("辅助流当前按下的按键已切换为: {0}", CurrentKey_Assist == 1 ? "C" : "V"), 1);
        }
        /// <summary>
        /// 噪音涌动输出流上次按下的按键 1C 2V 
        /// </summary>
        private int LaskKey_Assist = 1;
        /// <summary>
        /// 灵感条是否已经充满
        /// </summary>
        bool MusebarFull = false;
        public void Assist()
        {
            try
            {
                Rectangle screenBounds = SystemInformation.VirtualScreen;

                #region 截取菱形图案判定成功区域
                Bitmap scImg;
                if (RecognizeSetting_Rhombus.Area.Width <= 0 || RecognizeSetting_Rhombus.Area.Height <= 0)
                {
                    int SearchAreaW = 80;
                    int SearchAreaH = 75;

                    int SearchAreaX = (screenBounds.Width - SearchAreaW) / 2;
                    int SearchAreaY = screenBounds.Height / 2 + 5;

                    Rectangle rt = new Rectangle(SearchAreaX, SearchAreaY, SearchAreaW, SearchAreaH);
                    scImg = ScreenPatternDetector.CaptureScreen(rt);

                    Common.SaveIniParamVal("菱形图案识别设置", "X", SearchAreaX.ToString());
                    Common.SaveIniParamVal("菱形图案识别设置", "Y", SearchAreaY.ToString());
                    Common.SaveIniParamVal("菱形图案识别设置", "W", SearchAreaW.ToString());
                    Common.SaveIniParamVal("菱形图案识别设置", "H", SearchAreaH.ToString());
                }
                else
                    scImg = ScreenPatternDetector.CaptureScreen(RecognizeSetting_Rhombus.Area);
                string dtestr = DateTime.Now.ToString("HH_mm_ss_ffff");
                #endregion

                //MessageShowList.SendEventMsg(string.Format("ManualSPSKILL={0},MusebarFull={1}", ManualSPSKILL, MusebarFull), 1);

                if (ManualSPSKILL != 0)
                {
                    #region 截取灵感条图案区域
                    Bitmap MuseBarImg;
                    if (RecognizeSetting_MuseBar.Area.Width <= 0 || RecognizeSetting_MuseBar.Area.Height <= 0)
                    {
                        int SearchAreaW = 510;
                        int SearchAreaX = (screenBounds.Width - SearchAreaW) / 2;
                        int SearchAreaY = screenBounds.Height - 20;
                        int SearchAreaH = screenBounds.Height - SearchAreaY;

                        Rectangle rt = new Rectangle(SearchAreaX, SearchAreaY, SearchAreaW, SearchAreaH);
                        MuseBarImg = ScreenPatternDetector.CaptureScreen(rt);

                        Common.SaveIniParamVal("灵感条图案识别设置", "X", SearchAreaX.ToString());
                        Common.SaveIniParamVal("灵感条图案识别设置", "Y", SearchAreaY.ToString());
                        Common.SaveIniParamVal("灵感条图案识别设置", "W", SearchAreaW.ToString());
                        Common.SaveIniParamVal("灵感条图案识别设置", "H", SearchAreaH.ToString());
                    }
                    else
                        MuseBarImg = ScreenPatternDetector.CaptureScreen(RecognizeSetting_MuseBar.Area);
                    string dtestrMB = DateTime.Now.ToString("HH_mm_ss_ffff");
                    #endregion

                    Point pt0 = new Point(0, 0);
                    //if (ScreenPatternDetector.IsPatternPresent(MuseBar, MuseBarImg, out pt0, false, RecognizeSetting_MuseBar.Threshold, useHSV: true))
                      // if (ScreenPatternDetector.IsPatternPresent(MuseBar, MuseBarImg, out pt0, true, RecognizeSetting_MuseBar.Threshold, useHSV: false))
                  if (ScreenPatternDetector.IsPatternPresent(MuseBar, MuseBarImg, out pt0, false, RecognizeSetting_MuseBar.Threshold, useHSV: false))
                    {
                        MusebarFull = true;

                        #region 灵感条充满 等待手动使用强化技能或者自动强化技能
                        if (ManualSPSKILL != 1)
                        {
                            SaveScreenImg(MuseBarImg, dtestrMB, "辅助流菱形识别区截屏");
                            #region 自动强化技能
                            MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案,位置: {0}，将按下{1}，{2}", pt0, "Z", dtestrMB), 1);
                            byte key1 = GlobalParams.GetKeyCode("Z");
                            SendKBM.SendKeyPress(key1);

                            MusebarFull = false;
                            #endregion
                        }
                        #endregion

                        MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案,位置: {0}，请手动使用强化技能，{1}", pt0, dtestrMB), 1);
                        //string imgname = string.Format("{2}\\满灵感条图案区域\\{0}_{1}.png", DateTime.Now.ToString("yy_MM_ddTHH_mm_ss_ffff"),new Random().Next(), rootPath);
                        //MuseBarImg.Save(imgname,ImageFormat.Png);
                    }
                    else
                    {
                        MusebarFull = false;
                        //MessageShowList.SendEventMsg("未检测到满灵感条图案", 3);
                    }
                }

                if (ManualSPSKILL == 0 || !MusebarFull)
                {
                    Point pt = new Point(0, 0);

                    bool _userGrey = false;
                    bool _useHSV = false;
                    switch (OpencvMode)
                    {
                        case 0:
                            _userGrey = false;
                            _useHSV = false;
                            break;
                        case 1:
                            _userGrey = true;
                            _useHSV = false;
                            break;
                        case 2:
                            _userGrey = false;
                            _useHSV = true;
                            break;
                    }

                    #region 灵感条未充满或者不等待手动干预， 识别截图是否有菱形图案，并按下对应按键
                    if (ScreenPatternDetector.IsPatternPresent(d_0, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                    {
                        Presskey_Assist(pt, "0", dtestr);
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else if (ScreenPatternDetector.IsPatternPresent(d_1, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                    {
                        Presskey_Assist(pt, "1", dtestr);
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else if (ScreenPatternDetector.IsPatternPresent(d_2, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                    {
                        Presskey_Assist(pt, "2", dtestr);
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else if (ScreenPatternDetector.IsPatternPresent(d_3, scImg, out pt, _userGrey, RecognizeSetting_Rhombus.Threshold, useHSV: _useHSV))
                    {
                        Presskey_Assist(pt, "3", dtestr);
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else
                    {
                        //MessageShowList.SendEventMsg("未检测到任何图案", 3);
                    }
                    MusebarFull = false;
                    #endregion
                }
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(ex.Message, 1); }
        }

        /// <summary>
        /// 按下指定按键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pt"></param>
        /// <param name="imgNum"></param>
        private void Presskey_Assist(Point pt, string imgNum, string dtestr)
        {
            try
            {
                string _keycode = "";

                if (SwitchType_Assist == 1)
                {
                    switch (LaskKey_Assist)
                    {
                        case 1: { _keycode = "V"; break; }
                        case 2: { _keycode = "C"; break; }
                        default: { _keycode = "C"; break; }
                    }
                    LaskKey_Assist = LaskKey_Assist >= 3 || LaskKey_Assist < 1 ? 1 : LaskKey_Assist + 1;
                }
                else
                {
                    switch (CurrentKey_Assist)
                    {
                        case 1: { _keycode = "C"; break; }
                        case 2: { _keycode = "V"; break; }
                        default: { _keycode = "C"; break; }
                    }
                }

                MessageShowList.SendEventMsg(string.Format("检测到图案{2},位置: {0}，将按下{1}\t{3}", pt, _keycode, imgNum, dtestr), 1);
                byte key1 = GlobalParams.GetKeyCode(_keycode);
                SendKBM.SendKeyPress(key1);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg(ex.Message, 1);
            }
        }
        #endregion
    }
    /// <summary>
    /// 识别设置类
    /// </summary>
    public class RecognizeSetting
    {
        /// <summary>
        /// 识别区域
        /// </summary>
        public Rectangle Area { get; set; } = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// 识别阈值，范围0-1，默认0.5
        /// </summary>
        public double Threshold { get; set; } = 0.5;

        public string IniSectionName = "";
        public RecognizeSetting() { }
        public RecognizeSetting(string sectionname) { IniSectionName = sectionname; LoadFromIni(sectionname); }

        /// <summary>
        /// 设置识别区域
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void SetArea(int x, int y, int w, int h)
        {
            Rectangle newArea = new Rectangle(x, y, w, h);
            this.Area = newArea;
            Common.SaveIniParamVal(IniSectionName, "X", x.ToString());
            Common.SaveIniParamVal(IniSectionName, "Y", y.ToString());
            Common.SaveIniParamVal(IniSectionName, "W", w.ToString());
            Common.SaveIniParamVal(IniSectionName, "H", h.ToString());
        }
        /// <summary>
        /// 从INI文件加载设置
        /// </summary>
        /// <param name="sectionName"> 配置章节名 </param>
        public void LoadFromIni(string sectionName)
        {
            string X = Common.GetIniParamVal(sectionName, "X");
            string Y = Common.GetIniParamVal(sectionName, "Y");
            string W = Common.GetIniParamVal(sectionName, "W");
            string H = Common.GetIniParamVal(sectionName, "H");
            string threshold = Common.GetIniParamVal(sectionName, "Threshold");

            this.Area = new Rectangle(
                int.TryParse(X, out int x) ? x : 0,
                int.TryParse(Y, out int y) ? y : 0,
                int.TryParse(W, out int w) ? w : 0,
                int.TryParse(H, out int h) ? h : 0
            );

            if (double.TryParse(threshold, out double th) && th >= 0 && th <= 1)
            {
                this.Threshold = th;
            }
            else
            {
                this.Threshold = 0.5; // 默认值
            }
        }
    }
}
