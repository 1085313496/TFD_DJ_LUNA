using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        /// 运行类型 1噪音涌动输出流 2辅助流 3战地演唱会 缺省值1
        /// </summary>
        public int Mode { get; set; }

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
        /// 战地演唱会左箭头识别区
        /// </summary>
        public RecognizeSetting RS_BFS_L { get; set; }
        /// <summary>
        /// 战地演唱会右箭头识别区
        /// </summary>
        public RecognizeSetting RS_BFS_R { get; set; }

        /// <summary>
        /// 技能按键映射字典
        /// </summary>
        public static Dictionary<string, string> dicSkillKey = new Dictionary<string, string>()
        {
            { "Q", "Q" },
            { "C", "C" },
            { "V", "V" },
            { "Z", "Z" }
        };
        /// <summary>
        /// 加载技能按键映射
        /// </summary>
        public static void LoaddicSkillKey()
        {
            try
            {
                string _k = Common.GetIniParamVal("技能按键映射", "Q");
                string key_Q = string.IsNullOrWhiteSpace(_k) ? "Q" : _k;
                if (!dicSkillKey.ContainsKey("Q"))
                    dicSkillKey.Add("Q", key_Q);
                else
                    dicSkillKey["Q"] = key_Q;

                _k = Common.GetIniParamVal("技能按键映射", "C");
                string key_C = string.IsNullOrWhiteSpace(_k) ? "C" : _k;
                if (!dicSkillKey.ContainsKey("C"))
                    dicSkillKey.Add("C", key_C);
                else
                    dicSkillKey["C"] = key_C;

                _k = Common.GetIniParamVal("技能按键映射", "V");
                string key_V = string.IsNullOrWhiteSpace(_k) ? "V" : _k;
                if (!dicSkillKey.ContainsKey("V"))
                    dicSkillKey.Add("V", key_V);
                else
                    dicSkillKey["V"] = key_V;

                _k = Common.GetIniParamVal("技能按键映射", "Z");
                string key_Z = string.IsNullOrWhiteSpace(_k) ? "Z" : _k;
                if (!dicSkillKey.ContainsKey("Z"))
                    dicSkillKey.Add("Z", key_Z);
                else
                    dicSkillKey["Z"] = key_Z;

                MessageShowList.SendEventMsg(string.Format("已加载技能按键映射:Q={0},C={1},V={2},Z={3}."
                    , dicSkillKey["Q"], dicSkillKey["C"], dicSkillKey["V"], dicSkillKey["Z"]));
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("加载技能按键映射失败: " + ex.Message, 1);
            }
        }


        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;

        #region  各模板图片
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
        /// 战地演唱会左箭头_Q
        /// </summary>
        public Bitmap BFS_L_Q;
        /// <summary>
        /// 战地演唱会左箭头_C
        /// </summary>
        public Bitmap BFS_L_C;
        /// <summary>
        /// 战地演唱会左箭头_V
        /// </summary>
        public Bitmap BFS_L_V;
        /// <summary>
        /// 战地演唱会左箭头_Z
        /// </summary>
        public Bitmap BFS_L_Z;

        /// <summary>
        /// 战地演唱会右箭头_Q
        /// </summary>
        public Bitmap BFS_R_Q;
        /// <summary>
        /// 战地演唱会右箭头_C
        /// </summary>
        public Bitmap BFS_R_C;
        /// <summary>
        /// 战地演唱会右箭头_V
        /// </summary>
        public Bitmap BFS_R_V;
        /// <summary>
        /// 战地演唱会右箭头_Z
        /// </summary>
        public Bitmap BFS_R_Z;
        #endregion

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
            LoaddicSkillKey();
            loadIni();
            init();
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

                if (Mode == 3)
                {
                    //BFSC_Stop();
                    //Thread.Sleep(100);
                    //BFSC_Start();
                    BFS_R_Start();
                }
                else
                {
                    th = new Thread(new ThreadStart(() =>
                    {
                        IsRunning = true;
                        while (IsRunning)
                        {
                            switch (Mode)
                            {
                                case 2: Assist(); break;
                                case 3:
                                case 1:
                                default: Noise(); break;
                            }

                            Thread.Sleep(ScreenShotInterval);
                        }
                    }));
                    th.IsBackground = true;
                    th.Start();
                }
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

                if (Mode == 3)
                {
                    //BFSC_Stop();
                    BFS_R_Stop();
                }
                else
                {
                    if (th != null && th.IsAlive)
                    {
                        th.Join();
                        th = null;
                    }
                }

                #region 释放图片模板资源
                if (d_0 != null) d_0.Dispose();
                if (d_1 != null) d_1.Dispose();
                if (d_2 != null) d_2.Dispose();
                if (d_3 != null) d_3.Dispose();

                if (BFS_L_Q != null) BFS_L_Q.Dispose();
                if (BFS_L_C != null) BFS_L_C.Dispose();
                if (BFS_L_V != null) BFS_L_V.Dispose();
                if (BFS_L_Z != null) BFS_L_Z.Dispose();

                if (BFS_R_Q != null) BFS_R_Q.Dispose();
                if (BFS_R_C != null) BFS_R_C.Dispose();
                if (BFS_R_V != null) BFS_R_V.Dispose();
                if (BFS_R_Z != null) BFS_R_Z.Dispose();
                #endregion
            }
            catch { }
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

            BFS_R_Interval = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "BFS_R_Interval"), out int i1) ? i1 : 120;
            CVZ_CX_time = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "CVZ_CX_time"), out int i2) ? i2 : 15000;
            PowerfulTime = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "PowerfulTime"), out int i3) ? i3 : 14100;
            AutoPowerfulSkill = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "AutoPowerfulSkill"), out int apf) ? apf : 0;

            RecognizeSetting_Rhombus = new RecognizeSetting("菱形图案识别设置");
            RecognizeSetting_Crosshair = new RecognizeSetting("自瞄准星图案识别设置");
            RecognizeSetting_MuseBar = new RecognizeSetting("灵感条图案识别设置");

            RS_BFS_L = new RecognizeSetting("战地演唱会识别设置_左");
            RS_BFS_R = new RecognizeSetting("战地演唱会识别设置_右");

            if (RecognizeSetting_Rhombus.Area.Width <= 0 || RecognizeSetting_Rhombus.Area.Height <= 0)
            {
                int SearchAreaW = 80;
                int SearchAreaH = 70;
                int SearchAreaX = (Common.ScreenSize.Width - SearchAreaW) / 2;
                int SearchAreaY = Common.ScreenSize.Height / 2 + 5;
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

                BFS_L_Q = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_L_Q.png", rootPath));
                BFS_L_C = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_L_C.png", rootPath));
                BFS_L_V = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_L_V.png", rootPath));
                BFS_L_Z = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_L_Z.png", rootPath));

                BFS_R_Q = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_R_Q.png", rootPath));
                BFS_R_C = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_R_C.png", rootPath));
                BFS_R_V = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_R_V.png", rootPath));
                BFS_R_Z = new Bitmap(string.Format("{0}\\imgs\\lunaBFS\\BFS_R_Z.png", rootPath));
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("初始化识别图案失败: " + ex.Message, 1);
            }
        }
        /// <summary>
        /// 保存截图到文件
        /// </summary>
        /// <param name="img"></param>
        /// <param name="DateInfo"></param>
        /// <param name="Foldername"></param>
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
                    int SearchAreaW = 80;
                    int SearchAreaH = 70;

                    int SearchAreaX = (Common.ScreenSize.Width - SearchAreaW) / 2;
                    int SearchAreaY = Common.ScreenSize.Height / 2 + 5;

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
                double MaxVal = 0;
                #region
                if (ScreenPatternDetector.IsPatternPresent(d_0, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                {
                    Presskey("C", pt, "0", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_1, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                {
                    Presskey("V", pt, "1", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_2, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                {
                    Presskey("Z", pt, "2", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else if (ScreenPatternDetector.IsPatternPresent(d_3, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                {
                    Presskey("C", pt, "3", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                    SaveScreenImg(scImg, dtestr, "噪音涌动菱形识别区截屏");
                }
                else
                {
                    // MessageShowList.SendEventMsg("未检测到任何图案", 3);
                    // SaveScreenImg(scImg, dtestr, "不匹配的截图");
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
        private void Presskey(string key, Point pt, string imgNum, string dtestr, string extraInfo = "")
        {
            try
            {
                if (SwitchType_Noise == 0)
                {
                    string actualKey = dicSkillKey.ContainsKey(key) ? dicSkillKey[key] : key;//实际的按键
                    MessageShowList.SendEventMsg(string.Format("检测到图案{2}{4},位置: {0}，将按下{1}，{3}", pt, actualKey, imgNum, dtestr, extraInfo), 1);

                    switch (actualKey)
                    {
                        case "LBUTTON": SendKBM.MouseLeftClick(); break;
                        case "RBUTTON": SendKBM.MouseRightClick(); break;
                        case "MBUTTON": SendKBM.MouseMiddleClick(); break;
                        default:
                            byte key1 = GlobalParams.GetKeyCode(actualKey);
                            SendKBM.SendKeyPress(key1);
                            break;
                    }
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

                    string actualKey = dicSkillKey.ContainsKey(_keycode) ? dicSkillKey[_keycode] : _keycode;//实际的按键
                    MessageShowList.SendEventMsg(string.Format("检测到图案{2}{4},位置: {0}，将按下{1}，{3}", pt, actualKey, imgNum, dtestr, extraInfo), 1);

                    switch (actualKey)
                    {
                        case "LBUTTON": SendKBM.MouseLeftClick(); break;
                        case "RBUTTON": SendKBM.MouseRightClick(); break;
                        case "MBUTTON": SendKBM.MouseMiddleClick(); break;
                        default:
                            byte key1 = GlobalParams.GetKeyCode(actualKey);
                            SendKBM.SendKeyPress(key1);
                            break;
                    }

                    //byte key1 = GlobalParams.GetKeyCode(actualKey);
                    //SendKBM.SendKeyPress(key1);
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

            string cka = CurrentKey_Assist == 1 ? "C" : "V";
            string actualKey = dicSkillKey.ContainsKey(cka) ? dicSkillKey[cka] : cka;//实际的按键
            MessageShowList.SendEventMsg(string.Format("辅助流当前按下的按键已切换为: {0}", actualKey), 1);
        }
        /// <summary>
        /// 噪音涌动输出流上次按下的按键 1C 2V 
        /// </summary>
        private int LaskKey_Assist = 1;
        /// <summary>
        /// 灵感条是否已经充满
        /// </summary>
        bool MusebarFull = false;

        /// <summary>
        /// 上次按下的强化技能按键
        /// </summary>
        private string LastFullKey = "";
        /// <summary>
        /// 是否等待手动使用第二个强化技能
        /// </summary>
        private bool WaitFor2NDFull = false;

        public void Assist()
        {
            try
            {
                #region 截取菱形图案判定成功区域
                Bitmap scImg;
                if (RecognizeSetting_Rhombus.Area.Width <= 0 || RecognizeSetting_Rhombus.Area.Height <= 0)
                {
                    int SearchAreaW = 80;
                    int SearchAreaH = 70;

                    int SearchAreaX = (Common.ScreenSize.Width - SearchAreaW) / 2;
                    int SearchAreaY = Common.ScreenSize.Height / 2 + 5;

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

                //if (ManualSPSKILL != 0)
                //{
                #region 截取灵感条图案区域
                Bitmap MuseBarImg;
                if (RecognizeSetting_MuseBar.Area.Width <= 0 || RecognizeSetting_MuseBar.Area.Height <= 0)
                {
                    int SearchAreaW = 80;
                    int SearchAreaX = Common.ScreenSize.Width / 2 + 120;
                    int SearchAreaY = Common.ScreenSize.Height - 90;
                    int SearchAreaH = Common.ScreenSize.Height - SearchAreaY;

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
                double MaxVal = 0;
                if (ScreenPatternDetector.IsPatternPresent(MuseBar, MuseBarImg, out pt0, out MaxVal, RecognizeSetting_MuseBar.OpencvMode, RecognizeSetting_MuseBar.Threshold))
                {
                    MusebarFull = true;

                    #region 灵感条充满 等待手动使用强化技能或者自动强化技能
                    if (ManualSPSKILL != 1)
                    {
                        #region 自动强化技能
                        SaveScreenImg(MuseBarImg, dtestrMB, "辅助流菱形识别区截屏");

                        string thisFullKey = string.IsNullOrWhiteSpace(LastFullKey) || LastFullKey == "C" ? "Z" : "C";
                        string actualFullKey = dicSkillKey.ContainsKey(thisFullKey) ? dicSkillKey[thisFullKey] : thisFullKey;//实际的按键
                        MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案,匹配度:{3},位置: {0}，将按下{1}，{2}", pt0, actualFullKey, dtestrMB, MaxVal.ToString("f3")), 1);

                        switch (actualFullKey)
                        {
                            case "LBUTTON": SendKBM.MouseLeftClick(); break;
                            case "RBUTTON": SendKBM.MouseRightClick(); break;
                            case "MBUTTON": SendKBM.MouseMiddleClick(); break;
                            default:
                                byte key1 = GlobalParams.GetKeyCode(actualFullKey);
                                SendKBM.SendKeyPress(key1);
                                break;
                        }

                        //byte key1 = GlobalParams.GetKeyCode(actualFullKey);
                        //SendKBM.SendKeyPress(key1);
                        if (thisFullKey == "Z")
                            WaitFor2NDFull = true;
                        else
                            WaitFor2NDFull = false;

                        MusebarFull = false;
                        #endregion
                    }
                    #endregion

                    MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案，匹配度:{2},位置: {0}，请手动使用强化技能，{1}", pt0, dtestrMB, MaxVal.ToString("f3")), 1);
                }
                else
                {
                    MusebarFull = false;
                    //MessageShowList.SendEventMsg("未检测到满灵感条图案", 3);
                }
                // }

                if (ManualSPSKILL == 0 || !MusebarFull)
                {
                    Point pt = new Point(0, 0);

                    #region 灵感条未充满或者不等待手动干预， 识别截图是否有菱形图案，并按下对应按键
                    if (ScreenPatternDetector.IsPatternPresent(d_0, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                    {
                        Presskey_Assist(pt, "0", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else if (ScreenPatternDetector.IsPatternPresent(d_1, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                    {
                        Presskey_Assist(pt, "1", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else if (ScreenPatternDetector.IsPatternPresent(d_2, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                    {
                        Presskey_Assist(pt, "2", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
                        SaveScreenImg(scImg, dtestr, "辅助流菱形识别区截屏");
                    }
                    else if (ScreenPatternDetector.IsPatternPresent(d_3, scImg, out pt, out MaxVal, RecognizeSetting_Rhombus.OpencvMode, RecognizeSetting_Rhombus.Threshold))
                    {
                        Presskey_Assist(pt, "3", dtestr, string.Format(",匹配度:{0}", MaxVal.ToString("f3")));
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
        private void Presskey_Assist(Point pt, string imgNum, string dtestr, string extraInfo = "")
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

                string actualKey = dicSkillKey.ContainsKey(_keycode) ? dicSkillKey[_keycode] : _keycode;//实际的按键
                MessageShowList.SendEventMsg(string.Format("检测到图案{2}{4},位置: {0}，将按下{1}\t{3}", pt, actualKey, imgNum, dtestr, extraInfo), 1);

                switch (actualKey)
                {
                    case "LBUTTON": SendKBM.MouseLeftClick(); break;
                    case "RBUTTON": SendKBM.MouseRightClick(); break;
                    case "MBUTTON": SendKBM.MouseMiddleClick(); break;
                    default:
                        byte key1 = GlobalParams.GetKeyCode(actualKey);
                        SendKBM.SendKeyPress(key1);
                        break;
                }

                //byte key1 = GlobalParams.GetKeyCode(actualKey);
                //SendKBM.SendKeyPress(key1);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg(ex.Message, 1);
            }
        }
        #endregion


        #region 战地演唱会
        /// <summary>
        /// 战地演唱会主体流程
        /// </summary>
        private Thread th_BFSinger_run = null;

        #region 战地演唱会循环体内容  按一轮cvz,然后按住左键
        /// <summary>
        /// BFS识图检测并按键线程是否正在运行
        /// </summary>
        public bool BFS_R_Running { get; set; } = false;
        /// <summary>
        /// BFS识图检测并按键线程 循环时间间隔 ms
        /// </summary>
        public int BFS_R_Interval { get; set; } = 120;
        /// <summary>
        /// 上次检测到灵感条充满的时间
        /// </summary>
        private DateTime LastFullMBTime = DateTime.MinValue;
        /// <summary>
        /// CVZ普通技能持续时间
        /// </summary>
        public int CVZ_CX_time { get; set; } = (int)(1000 * 15);
        /// <summary>
        /// 强化技能持续时间
        /// </summary>
        public int PowerfulTime { get; set; } = (int)(1000 * 14.1);
        /// <summary>
        /// 是否自动使用强化技能
        /// </summary>
        public int AutoPowerfulSkill { get; set; } = 0;
        /// <summary>
        /// 上一次按CVZ技能键的时间
        /// </summary>
        private DateTime LastSkillCVZTime { get; set; } = DateTime.MinValue;
        /// <summary>
        /// 上一次按cvz的次数，到3则重置为0
        /// </summary>
        private int LastSkillCVZCount { get; set; } = 0;
        /// <summary>
        /// 上一次按下的技能键 0鼠标左键 1C 2V 3Z
        /// </summary>
        private int LastBFSKey { get; set; } = -1;
        /// <summary>
        /// 上一次按下的技能键[CVZ之一] 1C 2V 3Z
        /// </summary>
        private int LastBFSKey_CVZ { get; set; } = -1;

        private void BFS_R_Start()
        {
            try
            {
                th_BFSinger_run = new Thread(new ThreadStart(() =>
                {
                    IsRunning = true;
                    BFS_R_Running = true;
                    LastFullMBTime=DateTime.MinValue;

                    while (BFS_R_Running)
                    {
                       
                        //Presskey("LBUTTON");
                        //Thread.Sleep(105);
                        //Presskey("C");
                        // Thread.Sleep(250);
                        //continue;

                        //灵感条满，跳出
                        if (CheckMBFullState())
                            continue;

                        Bitmap bmp_L = ScreenPatternDetector.CaptureScreen(RS_BFS_L.Area);
                        Bitmap bmp_R = ScreenPatternDetector.CaptureScreen(RS_BFS_R.Area);

                        Point pt_L = new Point(0, 0);
                        Point pt_R = new Point(0, 0);
                        double MaxVal = 0;
                        string dteStrMB = DateTime.Now.ToString("HH_mm_ss_ffff");

                        //SaveScreenImg(bmp_L, dteStrMB, "左箭头");
                        //SaveScreenImg(bmp_R, dteStrMB, "右箭头");

                        //C技能图片颜色为橙色，不能使用H通道，需更换为S通道
                        int opencvImgMode_C = RS_BFS_L.OpencvMode == 2 ? 3 : RS_BFS_L.OpencvMode;

                        #region 图片检测并按键
                        if (ScreenPatternDetector.IsPatternPresent(BFS_L_Q, bmp_L, out pt_L, out MaxVal, RS_BFS_L.OpencvMode, RS_BFS_L.Threshold, RS_BFS_L.Sharpen)
                            || ScreenPatternDetector.IsPatternPresent(BFS_R_Q, bmp_R, out pt_R, out MaxVal, RS_BFS_R.OpencvMode, RS_BFS_R.Threshold, RS_BFS_R.Sharpen))
                        {
                            MessageShowList.SendEventMsg(string.Format("检测到Q技能状态,匹配度:{0},位置:[{1},{2}],[{3},{4}]\t{5}."
                                , MaxVal.ToString("f3"), pt_L.X, pt_L.Y, pt_R.X, pt_R.Y, dteStrMB), 1);

                            PressKeyCVZ_BFS("Q");

                            SaveScreenImg(bmp_L, dteStrMB, "左箭头_Q");
                            SaveScreenImg(bmp_R, dteStrMB, "右箭头_Q");
                        }
                        else if (ScreenPatternDetector.IsPatternPresent(BFS_L_C, bmp_L, out pt_L, out MaxVal, opencvImgMode_C, RS_BFS_L.Threshold, RS_BFS_L.Sharpen)
                             || ScreenPatternDetector.IsPatternPresent(BFS_R_C, bmp_R, out pt_R, out MaxVal, opencvImgMode_C, RS_BFS_R.Threshold, RS_BFS_R.Sharpen))
                        {
                            MessageShowList.SendEventMsg(string.Format("检测到C技能状态,匹配度:{0},位置:[{1},{2}],[{3},{4}]\t{5}."
                                , MaxVal.ToString("f3"), pt_L.X, pt_L.Y, pt_R.X, pt_R.Y, dteStrMB), 1);

                            PressKeyCVZ_BFS("C");

                            SaveScreenImg(bmp_L, dteStrMB, "左箭头_C");
                            SaveScreenImg(bmp_R, dteStrMB, "右箭头_C");
                        }
                        else if (ScreenPatternDetector.IsPatternPresent(BFS_L_V, bmp_L, out pt_L, out MaxVal, RS_BFS_L.OpencvMode, RS_BFS_L.Threshold, RS_BFS_L.Sharpen)
                             || ScreenPatternDetector.IsPatternPresent(BFS_R_V, bmp_R, out pt_R, out MaxVal, RS_BFS_R.OpencvMode, RS_BFS_R.Threshold, RS_BFS_R.Sharpen))
                        {
                            MessageShowList.SendEventMsg(string.Format("检测到V技能状态,匹配度:{0},位置:[{1},{2}],[{3},{4}]\t{5}."
                                , MaxVal.ToString("f3"), pt_L.X, pt_L.Y, pt_R.X, pt_R.Y, dteStrMB), 1);

                            PressKeyCVZ_BFS("V");

                            SaveScreenImg(bmp_L, dteStrMB, "左箭头_V");
                            SaveScreenImg(bmp_R, dteStrMB, "右箭头_V");
                        }
                        else if (ScreenPatternDetector.IsPatternPresent(BFS_L_Z, bmp_L, out pt_L, out MaxVal, RS_BFS_L.OpencvMode, RS_BFS_L.Threshold, RS_BFS_L.Sharpen)
                             || ScreenPatternDetector.IsPatternPresent(BFS_R_Z, bmp_R, out pt_R, out MaxVal, RS_BFS_R.OpencvMode, RS_BFS_R.Threshold, RS_BFS_R.Sharpen))
                        {
                            MessageShowList.SendEventMsg(string.Format("检测到Z技能状态,匹配度:{0},位置:[{1},{2}],[{3},{4}]\t{5}."
                                , MaxVal.ToString("f3"), pt_L.X, pt_L.Y, pt_R.X, pt_R.Y, dteStrMB), 1);

                            PressKeyCVZ_BFS("Z");

                            SaveScreenImg(bmp_L, dteStrMB, "左箭头_Z");
                            SaveScreenImg(bmp_R, dteStrMB, "右箭头_Z");
                        }
                        else
                        {
                            //MessageShowList.SendEventMsg("未检测到技能状态。");
                        }
                        #endregion

                        Thread.Sleep(BFS_R_Interval);
                    }
                }));
                th_BFSinger_run.IsBackground = true;
                th_BFSinger_run.Start();
            }
            catch (Exception ex) { }
        }
        private void BFS_R_Stop()
        {
            try
            {
                IsRunning = false;
                if (BFS_R_Running)
                {
                    BFS_R_Running = false;
                    //SendKBM.MouseLeftUp();

                    if (th_BFSinger_run != null)
                    {
                        th_BFSinger_run.Abort();
                        th_BFSinger_run = null;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private bool IsEXCVZtime() {

            bool isPowerfulSkills = false;//是否处于强化技能下

            //距离上次检测到满灵感条的毫秒数 小于 强化技能持续时间，不按CVZ
            TimeSpan timeSpan = DateTime.Now - LastFullMBTime;
            if (timeSpan.TotalMilliseconds < PowerfulTime)
            {
                MessageShowList.SendEventMsg("距离上次检测到满灵感条的毫秒数 小于 强化技能持续时间，不操作");
                isPowerfulSkills = true;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 战地演唱会 识别到图片后按键
        /// </summary>
        /// <param name="skillState">识别到图片时的技能状态 QCVZ</param>
        /// <param name="ForcePowerSkill">强行使用强化技能</param>
        private void PressKeyCVZ_BFS(string skillState, bool ForcePowerSkill = false)
        {
            if (IsEXCVZtime())
                return;

            //Presskey("LBUTTON");
            //Thread.Sleep(30);
            //Presskey("C");
            //return;


            int BFSMODE = 1;

            switch (BFSMODE)
            {
                case 0:
                    #region 左键\C\V\Z 轮流按
                    string _key_0 = "LBUTTON";
                    switch (LastBFSKey)
                    {
                        case 0: _key_0 = "C"; break;
                        case 1: _key_0 = "V"; break;
                        case 2: _key_0 = "Z"; break;
                        case 3: _key_0 = "LBUTTON"; break;
                        default: _key_0 = "LBUTTON"; break;
                    }
                    //MessageShowList.SendEventMsg(string.Format("LastBFSKey={0},CurKey={1}", LastBFSKey, _key_0));
                    LastBFSKey++;
                    if (LastBFSKey > 3)
                        LastBFSKey = 0;

                    Presskey(_key_0);
                    #endregion
                    break;
                case 1:
                    #region 左键与C\V\Z三者之一 轮流按
                    string _key_1 = "LBUTTON";

                    if (LastBFSKey == 0)
                    {
                        //上一次按的左键，本次应该按CVZ其中之一
                        switch (LastBFSKey_CVZ)
                        {
                            case 1:
                                _key_1 = "V";
                                LastBFSKey = 2;
                                LastBFSKey_CVZ = 2;
                                break;
                            case 2:
                                _key_1 = "Z";
                                LastBFSKey = 3;
                                LastBFSKey_CVZ = 3;
                                break;
                            case 3:
                            default:
                                _key_1 = "C";
                                LastBFSKey = 1;
                                LastBFSKey_CVZ = 1;
                                break;
                        }
                    }
                    else
                    {
                        //上一次按的不是左键，此次按左键
                        _key_1 = "LBUTTON";
                        LastBFSKey = 0;
                    }

                    //MessageShowList.SendEventMsg(string.Format("LastBFSKey={0},LastBFSKey_CVZ={2},CurKey={1}", LastBFSKey, _key_1, LastBFSKey_CVZ));
                    Presskey(_key_1);
                    #endregion
                    break;
            }

            return;

            bool isPowerfulSkills = false;//是否处于强化技能下

            //距离上次检测到满灵感条的毫秒数 小于 强化技能持续时间，不按CVZ
            TimeSpan timeSpan = DateTime.Now - LastFullMBTime;
            if (timeSpan.TotalMilliseconds < PowerfulTime)
            {
                MessageShowList.SendEventMsg("距离上次检测到满灵感条的毫秒数 小于 强化技能持续时间，不按CVZ");
                isPowerfulSkills = true;
                if (!ForcePowerSkill)
                    LastSkillCVZTime = DateTime.Now;
            }

            //上次按cvz距离现在的毫秒数大于普通CVZ技能持续时间，则重置计数器，重新按CVZ
            TimeSpan tsCVZ = DateTime.Now - LastSkillCVZTime;
            if (tsCVZ.TotalMilliseconds >= CVZ_CX_time)
            {
                LastSkillCVZCount = 0;
                MessageShowList.SendEventMsg("上次按cvz距离现在的毫秒数大于普通CVZ技能持续时间，则重置计数器，重新按CVZ");
            }

            //强行使用强化技能时，重置计数器，重新按CVZ
            if (ForcePowerSkill)
            {
                MessageShowList.SendEventMsg("强行使用强化技能时，重置计数器，重新按CVZ");
                LastSkillCVZCount = 0;
            }

            //按键
            if (LastSkillCVZCount < 3)
            {
                #region cvz三个键未按完，继续按CVZ其中之一

                LastSkillCVZCount++;

                string _key = "C";
                switch (LastBFSKey)
                {
                    case 0: _key = "C"; break;
                    case 1: _key = "V"; break;
                    case 2: _key = "Z"; break;
                    case 3: _key = "C"; break;
                    default: _key = "C"; break;
                }
                LastBFSKey++;
                Presskey(_key);
                #endregion
            }
            else
            {
                #region CVZ三个键已按完，按鼠标左键
                if (isPowerfulSkills)
                {
                    //强化技能下按住鼠标左键
                    MessageShowList.SendEventMsg("强化技能下,按住鼠标左键");
                    SendKBM.MouseLeftDown();
                    Thread.Sleep(PowerfulTime);
                    MessageShowList.SendEventMsg("强化技能结束,释放鼠标左键");
                    SendKBM.MouseLeftUp();
                }
                else
                {
                    //普通技能下，点按鼠标左键
                    Presskey("LBUTTON");
                }
                #endregion
            }
        }

        /// <summary>
        /// 检查灵感条是否充满
        /// </summary>
        /// <returns></returns>
        private bool CheckMBFullState()
        {
            try
            {
                // 截取灵感条图案区域
                Bitmap MuseBarImg = ScreenPatternDetector.CaptureScreen(RecognizeSetting_MuseBar.Area);
                string dtestrMB = DateTime.Now.ToString("HH_mm_ss_ffff");

                Point pt0 = new Point(0, 0);
                double MaxVal = 0;
                if (ScreenPatternDetector.IsPatternPresent(MuseBar, MuseBarImg, out pt0, out MaxVal, RecognizeSetting_MuseBar.OpencvMode, RecognizeSetting_MuseBar.Threshold))
                {
                    MusebarFull = true;
                    LastFullMBTime = DateTime.Now;

                    if (AutoPowerfulSkill == 1)
                    {
                        MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案,匹配度:{2},位置: {0}，将自动使用强化技能，{1}", pt0, dtestrMB, MaxVal.ToString("f2")), 1);
                        PressKeyCVZ_BFS("", true);
                    }
                    else if (AutoPowerfulSkill == 2)
                    {
                        MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案,匹配度:{2},位置: {0}，将自动使用强化技能CVZ，{1}", pt0, dtestrMB, MaxVal.ToString("f2")), 1);
                        Presskey("C");
                        Presskey("V");
                        Presskey("Z");
                    }
                    else
                    {
                        MessageShowList.SendEventMsg(string.Format("检测到满灵感条图案,匹配度:{2},位置: {0}，请手动使用强化技能，{1}", pt0, dtestrMB, MaxVal.ToString("f2")), 1);
                    }

                    return true;
                }
                else
                {
                    MusebarFull = false;
                    return false;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 按下按键
        /// </summary>
        /// <param name="key"></param>
        private void Presskey(string key)
        {
            try
            {
                string dtestr = DateTime.Now.ToString("HH_mm_ss_ffff");
                string actualKey = dicSkillKey.ContainsKey(key) ? dicSkillKey[key] : key;//实际的按键
                MessageShowList.SendEventMsg(string.Format("将按下按键{0}，{1}", actualKey, dtestr), 1);

                if (new string[] { "C", "V", "Z" }.Contains(key))
                {
                    LastSkillCVZTime = DateTime.Now;
                }

                switch (actualKey)
                {
                    case "LBUTTON": SendKBM.MouseLeftClick(); break;
                    case "RBUTTON": SendKBM.MouseRightClick(); break;
                    case "MBUTTON": SendKBM.MouseMiddleClick(); break;
                    default:
                        byte key1 = GlobalParams.GetKeyCode(actualKey);
                        SendKBM.SendKeyPress(key1);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg(ex.Message, 1);
            }
        }

        #endregion

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
        /// <summary>
        /// opencv识图对比模式 -1纯色块 0默认 1灰度图 2HSV
        /// </summary>
        public int OpencvMode { get; set; } = 0;
        /// <summary>
        /// 锐化截图 【0不做处理 1USM锐化】
        /// </summary>
        public int Sharpen { get; set; } = 0;

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
            string OpencvModeStr = Common.GetIniParamVal(sectionName, "OpencvMode");
            string SharpeningStr = Common.GetIniParamVal(sectionName, "Sharpen");

            this.Area = new Rectangle(
                int.TryParse(X, out int x) ? x : 0,
                int.TryParse(Y, out int y) ? y : 0,
                int.TryParse(W, out int w) ? w : Common.ScreenSize.Width,
                int.TryParse(H, out int h) ? h : Common.ScreenSize.Height
            );

            if (double.TryParse(threshold, out double th) && th >= 0 && th <= 1)
            {
                this.Threshold = th;
            }
            else
            {
                this.Threshold = 0.55; // 默认值
            }

            if (int.TryParse(OpencvModeStr, out int opencvMode) && opencvMode >= -1 && opencvMode <= 2)
            {
                this.OpencvMode = opencvMode;
            }
            else
            {
                this.OpencvMode = 0; // 默认值
            }

            if (int.TryParse(SharpeningStr, out int _Sharpening))
            {
                this.Sharpen = _Sharpening;
            }
            else
            {
                this.Sharpen = 0;
            }
        }
    }
}
