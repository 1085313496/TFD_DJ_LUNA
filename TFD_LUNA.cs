
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        public int ScreenShotInterval { get; set; } = 123;
        /// <summary>
        /// 运行类型 1噪音涌动输出流 2辅助流 3战地演唱会 缺省值1
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 是否保存屏幕截图，缺省值false
        /// </summary>
        public bool SaveScreenShot { get; set; } = false;
        /// <summary>
        /// 检测到节拍后的按键形式 【0[C\V\Z 交替按] 1[左键与 同时按CVZ 交替] 2[只按左键] 3[CVZ同时按] 4[左键与C\V\Z之一轮流按] 5[左键\C\V\Z轮流按] 6[C\V 交替按] 7[切换只按C或V] 8[按照映射]】
        /// </summary>
        public int BeatMode { get; set; } = 0;

        /// <summary>
        /// 菱形图案识别区域设置
        /// </summary>
        public RecognizeSetting RecognizeSetting_Rhombus { get; set; }
        /// <summary>
        /// 灵感条识别区域设置
        /// </summary>
        public RecognizeSetting RecognizeSetting_MuseBar { get; set; }
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
        /// 键码 Q
        /// </summary>
        byte btKey_Q = GlobalParams.GetKeyCode("Q");
        /// <summary>
        /// 键码 C
        /// </summary>
        byte btKey_C = GlobalParams.GetKeyCode("C");
        /// <summary>
        /// 键码 V
        /// </summary>
        byte btKey_V = GlobalParams.GetKeyCode("V");
        /// <summary>
        /// 键码 Z
        /// </summary>
        byte btKey_Z = GlobalParams.GetKeyCode("Z");

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning { get; set; } = false;

        #region  各模板图片
        #region  普通露娜打点图案
        /// <summary>
        /// 普通露娜打点图案 Q
        /// </summary>
        public Bitmap Normal_R_Q { get; set; }
        /// <summary>
        /// 普通露娜打点图案掩码 Q
        /// </summary>
        public Bitmap Normal_R_Q_Mask { get; set; }
        /// <summary>
        /// 普通露娜打点图案 C
        /// </summary>
        public Bitmap Normal_R_C { get; set; }
        /// <summary>
        /// 普通露娜打点图案掩码 C
        /// </summary>
        public Bitmap Normal_R_C_Mask { get; set; }
        /// <summary>
        /// 普通露娜打点图案 V
        /// </summary>
        public Bitmap Normal_R_V { get; set; }
        /// <summary>
        /// 普通露娜打点图案掩码 V
        /// </summary>
        public Bitmap Normal_R_V_Mask { get; set; }
        /// <summary>
        /// 普通露娜打点图案 Z
        /// </summary>
        public Bitmap Normal_R_Z { get; set; }
        /// <summary>
        /// 普通露娜打点图案掩码 Z
        /// </summary>
        public Bitmap Normal_R_Z_Mask { get; set; }
        #endregion

        #region 终极露娜打点图案
        /// <summary>
        /// 战地演唱会 右箭头  Q
        /// </summary>
        public Bitmap BFL_R_Q { get; set; }
        /// <summary>
        /// 战地演唱会 右箭头掩码  Q
        /// </summary>
        public Bitmap BFL_R_Q_Mask { get; set; }

        /// <summary>
        /// 战地演唱会 右箭头  C
        /// </summary>
        public Bitmap BFL_R_C { get; set; }
        /// <summary>
        /// 战地演唱会 右箭头掩码  C
        /// </summary>
        public Bitmap BFL_R_C_Mask { get; set; }

        /// <summary>
        /// 战地演唱会 右箭头  V
        /// </summary>
        public Bitmap BFL_R_V { get; set; }
        /// <summary>
        /// 战地演唱会 右箭头掩码  V
        /// </summary>
        public Bitmap BFL_R_V_Mask { get; set; }

        /// <summary>
        /// 战地演唱会 右箭头 Z
        /// </summary>
        public Bitmap BFL_R_Z { get; set; }
        /// <summary>
        /// 战地演唱会 右箭头掩码  Z
        /// </summary>
        public Bitmap BFL_R_Z_Mask { get; set; }
        #endregion

        /// <summary>
        /// 满灵感条图片组
        /// </summary>
        public List<Bitmap> lsMusebarImgs = new List<Bitmap>();
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
        /// 开启识别功能
        /// </summary>
        public void Start()
        {
            try
            {
                GC.Collect();

                loadIni();
                init();

                th = new Thread(new ThreadStart(() =>
                                   {
                                       IsRunning = true;
                                       while (IsRunning)
                                       {
                                           BeatCheck();
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

                #region 释放图片模板资源
                if (BFL_R_Q != null) BFL_R_Q.Dispose();
                if (BFL_R_C != null) BFL_R_C.Dispose();
                if (BFL_R_V != null) BFL_R_V.Dispose();
                if (BFL_R_Z != null) BFL_R_Z.Dispose();
                if (BFL_R_Q_Mask != null) BFL_R_Q_Mask.Dispose();
                if (BFL_R_C_Mask != null) BFL_R_C_Mask.Dispose();
                if (BFL_R_V_Mask != null) BFL_R_V_Mask.Dispose();
                if (BFL_R_Z_Mask != null) BFL_R_Z_Mask.Dispose();

                if (Normal_R_Q != null) Normal_R_Q.Dispose();
                if (Normal_R_Q_Mask != null) Normal_R_Q_Mask.Dispose();
                if (Normal_R_C != null) Normal_R_C.Dispose();
                if (Normal_R_C_Mask != null) Normal_R_C_Mask.Dispose();
                if (Normal_R_V != null) Normal_R_V.Dispose();
                if (Normal_R_V_Mask != null) Normal_R_V_Mask.Dispose();
                if (Normal_R_Z != null) Normal_R_Z.Dispose();
                if (Normal_R_Z_Mask != null) Normal_R_Z_Mask.Dispose();

                if (lsMusebarImgs != null && lsMusebarImgs.Count > 0)
                {
                    foreach (Bitmap bmp in lsMusebarImgs)
                    {
                        bmp.Dispose();
                    }
                }
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
            BeatMode = int.TryParse(Common.GetIniParamVal("全局设置", "BeatMode"), out int _bMode) ? _bMode : 0;
            CurrentBeatMode = BeatMode;
            ScreenShotInterval = int.TryParse(Common.GetIniParamVal("全局设置", "ScreenShotInterval"), out int interval) ? interval : 120;

            ManualSPSKILL = int.TryParse(Common.GetIniParamVal("辅助流设置", "ManualSPSKILL"), out int manualSPSKILL) ? manualSPSKILL : 1;

            CVZ_CX_time = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "CVZ_CX_time"), out int i2) ? i2 : 15000;
            PowerfulTime = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "PowerfulTime"), out int i3) ? i3 : 14100;
            AutoPowerfulSkill = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "AutoPowerfulSkill"), out int apf) ? apf : 0;
            int _bfl_ast = int.TryParse(Common.GetIniParamVal("战地演唱会设置", "BFL_AutoShot"), out int _ast) ? _ast : 0;
            BFL_AutoShot = _bfl_ast == 1;

            RecognizeSetting_Rhombus = new RecognizeSetting("菱形图案识别设置");
            RecognizeSetting_MuseBar = new RecognizeSetting("灵感条图案识别设置");

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
                BFL_R_Q = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_Q.png", rootPath));
                BFL_R_Q_Mask = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_Q_mask.png", rootPath));
                BFL_R_C = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_C.png", rootPath));
                BFL_R_C_Mask = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_C_mask.png", rootPath));
                BFL_R_V = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_V.png", rootPath));
                BFL_R_V_Mask = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_V_mask.png", rootPath));
                BFL_R_Z = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_Z.png", rootPath));
                BFL_R_Z_Mask = new Bitmap(string.Format("{0}\\imgs\\BFL\\BFL_R_Z_mask.png", rootPath));

                Normal_R_Q = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_Q.png", rootPath));
                Normal_R_Q_Mask = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_Q_Mask.png", rootPath));
                Normal_R_C = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_C.png", rootPath));
                Normal_R_C_Mask = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_C_Mask.png", rootPath));
                Normal_R_V = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_V.png", rootPath));
                Normal_R_V_Mask = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_V_Mask.png", rootPath));
                Normal_R_Z = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_Z.png", rootPath));
                Normal_R_Z_Mask = new Bitmap(string.Format("{0}\\imgs\\Noise\\Noise_B_Z_Mask.png", rootPath));

                lsMusebarImgs.Clear();
                string folderPath = string.Format("{0}\\imgs\\Musebar", rootPath);
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                try
                {
                    var imagePaths = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                        .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                        .ToList();

                    foreach (string path in imagePaths)
                    {
                        Bitmap bmp = new Bitmap(path);
                        lsMusebarImgs.Add(bmp);
                    }
                }
                catch (Exception ex)
                {
                    MessageShowList.SendEventMsg("加载灵感条模板图案异常！" + ex.Message);
                }

                string actualKey = dicSkillKey.ContainsKey("Q") ? dicSkillKey["Q"] : "Q";
                btKey_Q = GlobalParams.GetKeyCode(actualKey);
                actualKey = dicSkillKey.ContainsKey("C") ? dicSkillKey["C"] : "C";
                btKey_C = GlobalParams.GetKeyCode(actualKey);
                actualKey = dicSkillKey.ContainsKey("V") ? dicSkillKey["V"] : "V";
                btKey_V = GlobalParams.GetKeyCode(actualKey);
                actualKey = dicSkillKey.ContainsKey("Z") ? dicSkillKey["Z"] : "Z";
                btKey_Z = GlobalParams.GetKeyCode(actualKey);
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

                string Dir = string.Format("{0}\\截图\\{1}", rootPath, Foldername);
                if (!System.IO.Directory.Exists(Dir))
                    System.IO.Directory.CreateDirectory(Dir);

                img.Save(string.Format("{0}\\{1}_0.png", Dir, DateInfo), ImageFormat.Png);
            }
            catch { }
        }

        #region 新版
        /// <summary>
        /// 上次抓到到节拍后按下的按键 [0左键 1C  2V  3Z]
        /// </summary>
        public int LastBeatKey { get; set; } = -1;
        /// <summary>
        /// 上一次按下的技能键[CVZ之一] 1C 2V 3Z
        /// </summary>
        private int LastBeatKey_CVZ { get; set; } = -1;
        /// <summary>
        /// 上次按下的按键类型 【0未按键 1鼠标左键 2CVZ一起按】
        /// </summary>
        private int LastBeatKeyType { get; set; } = 0;

        /// <summary>
        /// 检测灵感条是否已充满
        /// </summary>
        /// <returns></returns>
        private bool FullMuseBarCheck()
        {
            try
            {
                Bitmap MuseBar_Screenshot = ScreenPatternDetector.CaptureScreen(RecognizeSetting_MuseBar.Area);// 截取 灵感条区域 截图
                string dtestrMB = DateTime.Now.ToString("HH_mm_ss_ffff");

                #region 检测指定区域是否有几张灵感条图片之一
                System.Drawing.Point pt_R = new System.Drawing.Point(0, 0);
                double MaxVal = 0;
                Bitmap bmp_out = null;
                bool saveImg = SaveScreenShot;
                bool FullMusebarChecked = false;
                foreach (Bitmap bmp in lsMusebarImgs)
                {
                    if (ScreenPatternDetector.IsPatternPresent_New(bmp, null, MuseBar_Screenshot, out MaxVal, out pt_R, out bmp_out
                        , RecognizeSetting_MuseBar.Threshold, 1, RecognizeSetting_MuseBar.OpencvMode, RecognizeSetting_MuseBar.MaskMode, saveImg))
                    {
                        FullMusebarChecked = true;
                        LastFullMBTime = DateTime.Now;
                        MessageShowList.SendEventMsg(string.Format("检测到满灵感条,匹配度={0},位置=[{1},{2}],{3}", MaxVal.ToString("f3"), pt_R.X, pt_R.Y, dtestrMB));
                        break;
                    }
                }
                #endregion

                return FullMusebarChecked;
            }
            catch { return false; }
        }
        /// <summary>
        /// 节拍检测
        /// </summary>
        private void BeatCheck()
        {
            try
            {
                #region 非噪音涌动，先检查灵感条是否充满
                if (Mode != 1)
                {
                    if (FullMuseBarCheck())
                    {
                        if (!MuseBarFullController())
                            return;
                    }
                }
                #endregion

                #region 要截屏的区域设置  普通版本和终极版新红卡分开
                string imgSaveFoldername = "";
                RecognizeSetting RS_OBJ = null;

                Bitmap bmp_t_q = null;
                Bitmap bmp_t_q_mask = null;
                Bitmap bmp_t_c = null;
                Bitmap bmp_t_c_mask = null;
                Bitmap bmp_t_v = null;
                Bitmap bmp_t_v_mask = null;
                Bitmap bmp_t_z = null;
                Bitmap bmp_t_z_mask = null;

                switch (Mode)
                {
                    case 3:
                        //新红卡版本
                        RS_OBJ = RS_BFS_R;
                        imgSaveFoldername = "右箭头";

                        bmp_t_q = BFL_R_Q;
                        bmp_t_q_mask = BFL_R_Q_Mask;
                        bmp_t_c = BFL_R_C;
                        bmp_t_c_mask = BFL_R_C_Mask;
                        bmp_t_v = BFL_R_V;
                        bmp_t_v_mask = BFL_R_V_Mask;
                        bmp_t_z = BFL_R_Z;
                        bmp_t_z_mask = BFL_R_Z_Mask;

                        break;
                    case 1:
                    case 2:
                    default:
                        //普通版
                        RS_OBJ = RecognizeSetting_Rhombus;
                        imgSaveFoldername = "菱形方块";

                        bmp_t_q = Normal_R_Q;
                        bmp_t_q_mask = Normal_R_Q_Mask;
                        bmp_t_c = Normal_R_C;
                        bmp_t_c_mask = Normal_R_C_Mask;
                        bmp_t_v = Normal_R_V;
                        bmp_t_v_mask = Normal_R_V_Mask;
                        bmp_t_z = Normal_R_Z;
                        bmp_t_z_mask = Normal_R_Z_Mask;

                        break;
                }
                #endregion

                double ScaleFactor = RS_OBJ.ScaleMode == 1 ? (RS_OBJ.RealScreenWidth / 1920.0) : 1;
                Point pt_R = new Point(0, 0);
                double MaxVal = 0;
                Bitmap bmp_out = null;
                bool saveImg = SaveScreenShot;
                int opencvImgMode_C = RS_OBJ.OpencvMode == 2 ? 3 : RS_OBJ.OpencvMode; //HSV_H通道时使用  C技能图片颜色为橙色，不能使用H通道，需更换为S通道

                Bitmap bmp_ScreenShot = ScreenPatternDetector.CaptureScreen(RS_OBJ.Area);//指定区域的截图
                string dteStrMB = DateTime.Now.ToString("HH_mm_ss_ffff");

                #region 图片检测
                if (ScreenPatternDetector.IsPatternPresent_New(bmp_t_c, bmp_t_c_mask, bmp_ScreenShot, out MaxVal, out pt_R, out bmp_out, RS_OBJ.Threshold, ScaleFactor, opencvImgMode_C, RS_OBJ.MaskMode, saveImg))
                {
                    MessageShowList.SendEventMsg(string.Format("检测到C节拍,匹配度:{0},位置:[{1},{2}]\t{3}.", MaxVal.ToString("f3"), pt_R.X, pt_R.Y, dteStrMB), 1);
                    KeyPressController("C");
                    SaveScreenImg(bmp_ScreenShot, dteStrMB, imgSaveFoldername + "_C");
                }
                else if (ScreenPatternDetector.IsPatternPresent_New(bmp_t_v, bmp_t_v_mask, bmp_ScreenShot, out MaxVal, out pt_R, out bmp_out, RS_OBJ.Threshold, ScaleFactor, RS_OBJ.OpencvMode, RS_OBJ.MaskMode, saveImg))
                {
                    MessageShowList.SendEventMsg(string.Format("检测到V节拍,匹配度:{0},位置:[{1},{2}]\t{3}.", MaxVal.ToString("f3"), pt_R.X, pt_R.Y, dteStrMB), 1);
                    KeyPressController("V");
                    SaveScreenImg(bmp_ScreenShot, dteStrMB, imgSaveFoldername + "_V");
                }
                else if (ScreenPatternDetector.IsPatternPresent_New(bmp_t_z, bmp_t_z_mask, bmp_ScreenShot, out MaxVal, out pt_R, out bmp_out, RS_OBJ.Threshold, ScaleFactor, RS_OBJ.OpencvMode, RS_OBJ.MaskMode, saveImg))
                {
                    MessageShowList.SendEventMsg(string.Format("检测到Z节拍,匹配度:{0},位置:[{1},{2}]\t{3}.", MaxVal.ToString("f3"), pt_R.X, pt_R.Y, dteStrMB), 1);
                    KeyPressController("Z");
                    SaveScreenImg(bmp_ScreenShot, dteStrMB, imgSaveFoldername + "_Z");
                }
                else if (ScreenPatternDetector.IsPatternPresent_New(bmp_t_q, bmp_t_q_mask, bmp_ScreenShot, out MaxVal, out pt_R, out bmp_out, RS_OBJ.Threshold, ScaleFactor, RS_OBJ.OpencvMode, RS_OBJ.MaskMode, saveImg))
                {
                    MessageShowList.SendEventMsg(string.Format("检测到Q节拍,匹配度:{0},位置:[{1},{2}]\t{3}.", MaxVal.ToString("f3"), pt_R.X, pt_R.Y, dteStrMB), 1);
                    KeyPressController("Q");
                    SaveScreenImg(bmp_ScreenShot, dteStrMB, imgSaveFoldername + "_Q");
                }
                else
                {
                    //MessageShowList.SendEventMsg("未检测到技能状态。");
                }
                #endregion

                //if(Mode==3)
                //    Thread.Sleep(80);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("检测节拍图案异常！" + ex.Message);
            }
        }


        /// <summary>
        /// 按下按键
        /// </summary>
        /// <param name="_key"></param>
        private void PressKey_Normal(string _key, int _interval = 50)
        {
            try
            {
                switch (_key)
                {
                    case "LBUTTON": SendKBM.MouseLeftClick(10); break;
                    case "RBUTTON": SendKBM.MouseRightClick(10); break;
                    case "MBUTTON": SendKBM.MouseMiddleClick(); break;
                    case "Q": SendKBM.SendKeyPress(btKey_Q, _interval); break;
                    case "C": SendKBM.SendKeyPress(btKey_C, _interval); break;
                    case "V": SendKBM.SendKeyPress(btKey_V, _interval); break;
                    case "Z": SendKBM.SendKeyPress(btKey_Z, _interval); break;
                }
                MessageShowList.SendEventMsg(string.Format("按下按键{0},{1}", _key, DateTime.Now.ToString("mm:ss:fff")));
            }
            catch (Exception ex) { MessageShowList.SendEventMsg(string.Format("按下按键{0}时出现异常！{1}", _key, ex.Message)); }
        }
        /// <summary>
        /// 检测到节拍后的操作
        /// </summary>
        /// <param name="beatKey">检测到的节拍类型 Q/C/V/Z</param>
        private void KeyPressController(string beatKey)
        {
            try
            {
                byte[] keybytes = { btKey_V, btKey_Z, btKey_C };

                switch (BeatMode)
                {
                    case 0:
                        #region [C\V\Z 交替按]
                        string _key_0 = "C";
                        switch (LastBeatKey)
                        {
                            case 0: _key_0 = "C"; break;
                            case 1: _key_0 = "V"; break;
                            case 2: _key_0 = "Z"; break;
                            case 3: _key_0 = "C"; break;
                            default: _key_0 = "C"; break;
                        }

                        LastBeatKey++;
                        if (LastBeatKey > 3)
                            LastBeatKey = 1;

                        PressKey_Normal(_key_0);
                        #endregion
                        break;
                    case 1:
                        #region [左键与 同时按CVZ 交替]
                        switch (LastBeatKeyType)
                        {
                            case 1:
                                SendKBM.SendMultiKeysPress(keybytes, 100);
                                MessageShowList.SendEventMsg("VZC同时按");
                                LastBeatKeyType = 2;
                                break;
                            case 2:
                            case 0:
                            default:
                                PressKey_Normal("LBUTTON");
                                LastBeatKeyType = 1;
                                break;
                        }
                        #endregion
                        break;
                    case 2:
                        #region [只按左键]
                        SendKBM.MouseLeftClick(20);
                        MessageShowList.SendEventMsg("仅按左键");
                        #endregion
                        break;
                    case 3:
                        #region [CVZ同时按]
                        SendKBM.SendMultiKeysPress(keybytes, 100);
                        MessageShowList.SendEventMsg("VZC同时按");
                        #endregion
                        break;
                    case 4:
                        #region [左键与C\V\Z之一轮流按]
                        string _key_4 = "LBUTTON";

                        if (LastBeatKey == 0)
                        {
                            //上一次按的左键，本次应该按CVZ其中之一
                            switch (LastBeatKey_CVZ)
                            {
                                case 1:
                                    _key_4 = "V";
                                    LastBeatKey = 2;
                                    LastBeatKey_CVZ = 2;
                                    break;
                                case 2:
                                    _key_4 = "Z";
                                    LastBeatKey = 3;
                                    LastBeatKey_CVZ = 3;
                                    break;
                                case 3:
                                default:
                                    _key_4 = "C";
                                    LastBeatKey = 1;
                                    LastBeatKey_CVZ = 1;
                                    break;
                            }
                        }
                        else
                        {
                            //上一次按的不是左键，此次按左键
                            _key_4 = "LBUTTON";
                            LastBeatKey = 0;
                        }

                        PressKey_Normal(_key_4);
                        #endregion
                        break;
                    case 5:
                        #region [左键\C\V\Z轮流按]
                        string _key_5 = "LBUTTON";
                        switch (LastBeatKey)
                        {
                            case 0: _key_5 = "C"; break;
                            case 1: _key_5 = "V"; break;
                            case 2: _key_5 = "Z"; break;
                            case 3: _key_5 = "LBUTTON"; break;
                            default: _key_5 = "LBUTTON"; break;
                        }

                        LastBeatKey++;
                        if (LastBeatKey > 3)
                            LastBeatKey = 0;

                        PressKey_Normal(_key_5);
                        #endregion
                        break;
                    case 6:
                        #region [C\V 交替按]
                        string _key_8 = "C";
                        switch (LastBeatKey)
                        {
                            case 1: _key_8 = "V"; LastBeatKey = 0; break;
                            case 2: //_key_8 = "Z"; break;
                            case 0:
                            case 3:
                            default: _key_8 = "C"; LastBeatKey = 1; break;
                        }

                        PressKey_Normal(_key_8);
                        #endregion
                        break;
                    case 7:
                        #region [切换只按C或V]
                        string _key7 = "C";
                        switch (CurrentKey_Assist)
                        {
                            case 1: { _key7 = "C"; break; }
                            case 2: { _key7 = "V"; break; }
                            default: { _key7 = "C"; break; }
                        }
                        PressKey_Normal(_key7);
                        #endregion
                        break;
                    case 8:
                        #region [按照映射]
                        string s_key = "C";
                        switch (beatKey)
                        {
                            case "C": s_key = "V"; break;
                            case "V": s_key = "Z"; break;
                            case "Z":
                            case "Q":
                            default: s_key = "C"; break;
                        }
                        PressKey_Normal(s_key);
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("模拟按键出现异常！" + ex.Message);
            }
        }
        /// <summary>
        /// 检测到灵感条充满后的操作 返回false将跳过当次循环
        /// </summary>
        private bool MuseBarFullController()
        {
            try
            {
                bool bl_R = true;
                byte[] keybytes = { btKey_V, btKey_Z, btKey_C };

                switch (Mode)
                {
                    case 2:
                        #region 普通红卡辅助流
                        if (ManualSPSKILL != 1)
                        {
                            //自动使用强化技能
                        }
                        else
                        {
                            MessageShowList.SendEventMsg("灵感条已满，等待手动使用强化技能...");
                            bl_R = false;
                        }
                        #endregion
                        break;
                    case 3:
                        #region 终极版  战地演唱会
                        int _diffTime = 250 + 200;
                        int _pcvzTime = PowerfulTime - _diffTime;

                        if (AutoPowerfulSkill == 1)
                        {
                            MessageShowList.SendEventMsg(string.Format("灵感条已满,将自动使用强化技能"), 1);

                            PressKey_Normal("V");
                            Thread.Sleep(150);
                            PressKey_Normal("Z");
                            Thread.Sleep(150);
                            PressKey_Normal("C");
                            Thread.Sleep(150);


                            if (BFL_AutoShot)
                            {
                                _pcvzTime = _pcvzTime - 150 - 150 - 150;

                                SendKBM.MouseLeftDown();
                                Thread.Sleep(_pcvzTime);
                                SendKBM.MouseLeftUp();
                            }
                        }
                        else
                        {
                            MessageShowList.SendEventMsg(string.Format("灵感条已满,请手动使用强化技能"), 1);
                        }

                        #endregion
                        break;
                }
                return bl_R;
            }
            catch { return true; }
        }
        #endregion


        #region 上buff工具人
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
        #endregion

        #region 战地演唱会 
        /// <summary>
        /// 上次检测到灵感条充满的时间
        /// </summary>
        private DateTime LastFullMBTime { get; set; } = DateTime.MinValue;
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
        /// 自动使用强化技能时是否自动开枪
        /// </summary>
        public bool BFL_AutoShot { get; set; } = false;

        /// <summary>
        /// 当前使用的打点方式
        /// </summary>
        public int CurrentBeatMode { get; set; } = 0;
        /// <summary>
        /// 切换打点方式
        /// </summary>
        public void SwitchBeatMode()
        {
            switch (BeatMode)
            {
                case 2:
                    BeatMode = CurrentBeatMode;
                    break;
                default:
                    CurrentBeatMode = BeatMode;
                    BeatMode = 2;
                    break;
            }

            string BeatModeTxt = BeatMode == 2 ? "只按左键" : "原打点方式";
            MessageShowList.SendEventMsg(string.Format("打点方式已切换:{0}", BeatModeTxt), 1);
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
        /// <summary>
        /// opencv识图对比模式 -1纯色块 0默认 1灰度图 2HSV
        /// </summary>
        public int OpencvMode { get; set; } = 0;
        /// <summary>
        /// 掩码设置 【1：自动生成掩码 2：使用手动生成的掩码 其他值：不使用掩码】
        /// </summary>
        public int MaskMode { get; set; } = 0;
        /// <summary>
        /// 缩放模板以便于在其他分辨率使用 【1：使用缩放  其他值：不使用缩放】
        /// </summary>
        public int ScaleMode { get; set; } = 0;
        /// <summary>
        /// 实际的游戏画面宽度 用于计算基于1080P的缩放倍率
        /// </summary>
        public int RealScreenWidth { get; set; } = 1920;


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

            this.MaskMode = int.TryParse(Common.GetIniParamVal(sectionName, "MaskMode"), out int _maskmode) ? _maskmode : 0;
            this.MaskMode = (this.MaskMode == 1 || this.MaskMode == 2) ? this.MaskMode : 0;

            this.ScaleMode = int.TryParse(Common.GetIniParamVal(sectionName, "ScaleMode"), out int _scmode) ? _scmode : 0;
            this.ScaleMode = this.ScaleMode == 1 ? this.ScaleMode : 0;

            this.RealScreenWidth = int.TryParse(Common.GetIniParamVal(sectionName, "RealScreenWidth"), out int _rsw) ? _rsw : 1920;
            this.RealScreenWidth = this.RealScreenWidth <= 0 ? 1920 : this.RealScreenWidth;
        }
    }
}
