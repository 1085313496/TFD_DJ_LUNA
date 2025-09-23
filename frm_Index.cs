using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using TFD_DJ_LUNA.BaseClass;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    public partial class frm_Index : Form
    {
        /// <summary>
        /// 启用/暂停 自动打碟功能快捷键 缺省值F5
        /// </summary>
        public string HotKey_Global { get; set; }
        /// <summary>
        /// 辅助流切换按键快捷键 
        /// </summary>
        public string HotKey_Switch { get; set; }
        /// <summary>
        /// 切换打点模式 【在 当前打点模式 和 只按左键 两种之间切换】
        /// </summary>
        public string HotKey_SwitchShootMode { get; set; }


        TFD_LUNA tFD_LUNA = new TFD_LUNA();

        public frm_Index()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Disposed += Form1_Disposed;
            MessageShowList.SendNotice += MessageShowList_SendNotice;

            LoadIni();
            LoadParams();

            SetScanner();
            SetMouseHook();
        }

        /// <summary>
        /// 窗体被释放时，取消钩子监听并停止打碟助手线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Disposed(object sender, EventArgs e)
        {
            UnHookSanner();
            UnHookMouseHook();

            if (tFD_LUNA != null)
            {
                tFD_LUNA.IsRunning = false;
                tFD_LUNA.Stop();
            }
        }
        /// <summary>
        /// 日志消息显示事件处理函数
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="noticeLevel"></param>
        private void MessageShowList_SendNotice(string msg, int noticeLevel)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (rtb.Text.Length > rtb.MaxLength * 0.75)
                    {
                        rtb.Clear();
                    }

                    rtb.AppendText(DateTime.Now.ToString("mm:ss:fff") + "\t");
                    rtb.AppendText(msg);
                    rtb.AppendText(Environment.NewLine); // 添加换行符
                    rtb.ScrollToCaret(); // 滚动到最新消息
                }));
            }
            catch { }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadIni()
        {
            string _k0 = Common.GetIniParamVal("全局设置", "HotKey_Global");
            HotKey_Global = string.IsNullOrWhiteSpace(_k0) ? "F5" : _k0.ToUpper();
            tb_HotKey_Global.Text = HotKey_Global;

            string _k1 = Common.GetIniParamVal("辅助流设置", "HotKey_Switch");
            HotKey_Switch = string.IsNullOrWhiteSpace(_k1) ? "F6" : _k1.ToUpper();
            tb_HotKey_Switch.Text = HotKey_Switch;

            string _k = Common.GetIniParamVal("战地演唱会设置", "HotKey_SwitchShootMode");
            HotKey_SwitchShootMode = string.IsNullOrWhiteSpace(_k) ? "F6" : _k.ToUpper();
            tb_HotKey_SwitchShootMode.Text = HotKey_SwitchShootMode;
        }
        /// <summary>
        /// 加载参数到界面控件
        /// </summary>
        private void LoadParams()
        {
            tb_ScreenShotInterval.Text = tFD_LUNA.ScreenShotInterval.ToString();
            ckb_ManualSPSKILL.Checked = tFD_LUNA.ManualSPSKILL == 1;
            ckb_BFL_AutoShot.Checked = tFD_LUNA.BFL_AutoShot;

            #region 加载战地炮台设置
            if (tFD_LUNA != null)
            {
                tb_BFS_CVZ_CX_time.Text = tFD_LUNA.CVZ_CX_time.ToString();
                tb_BFS_PowerfulTime.Text = tFD_LUNA.PowerfulTime.ToString();

                switch (tFD_LUNA.AutoPowerfulSkill)
                {
                    case 0: rb_BFS_APF_Disable.Checked = true; break;
                    case 1: rb_BFS_APF_Enable.Checked = true; break;
                }

                #region 打点方式
                switch (tFD_LUNA.BeatMode)
                {
                    case 0: rb_BeatMode_0.Checked = true; break;
                    case 1: rb_BeatMode_1.Checked = true; break;
                    case 2: rb_BeatMode_2.Checked = true; break;
                    case 3: rb_BeatMode_3.Checked = true; break;
                    case 4: rb_BeatMode_4.Checked = true; break;
                    case 5: rb_BeatMode_5.Checked = true; break;
                    case 6: rb_BeatMode_6.Checked = true; break;
                    case 7: rb_BeatMode_7.Checked = true; break;
                    case 8: rb_BeatMode_8.Checked = true; break;
                }
                #endregion
            }
            #endregion

            #region 运行类型 1噪音涌动输出流 2辅助流 缺省值1
            if (tFD_LUNA.Mode == 1)
            {
                rb_Mode_Noise.Checked = true;

                gb_Assist.Hide();
                gb_BFS.Hide();
                gb_Noise.Show();
                gb_Noise.BringToFront();
            }
            else if (tFD_LUNA.Mode == 2)
            {
                rb_Mode_Assist.Checked = true;
                gb_Noise.Hide();
                gb_BFS.Hide();
                gb_Assist.Show();
                gb_Assist.BringToFront();
            }
            else if (tFD_LUNA.Mode == 3)
            {
                rb_Mode_BFS.Checked = true;
                gb_Noise.Hide();
                gb_Assist.Hide();
                gb_BFS.Show();
                gb_BFS.BringToFront();
            }
            #endregion

            #region 识别区域赋值
            if (tFD_LUNA.RecognizeSetting_Rhombus != null)
                RAS_Normal_Block.Init(tFD_LUNA.RecognizeSetting_Rhombus);

            if (tFD_LUNA.RecognizeSetting_MuseBar != null)
                RAS_MuseBar.Init(tFD_LUNA.RecognizeSetting_MuseBar);

            if (tFD_LUNA.RS_BFS_R != null)
                RAS_BFL_R.Init(tFD_LUNA.RS_BFS_R);
            #endregion
        }

        #region 其他参数调整
        /// <summary>
        /// 手动设置灵感条满了需要手动使用强化技能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_ManualSPSKILL_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_ManualSPSKILL.Checked)
            {
                tFD_LUNA.ManualSPSKILL = 1;
                MessageShowList.SendEventMsg("[辅助流]灵感条满了需要手动使用强化技能", 1);
            }
            else
            {
                tFD_LUNA.ManualSPSKILL = 0;
                MessageShowList.SendEventMsg("[辅助流]灵感条满了无所谓", 1);
            }
            Common.SaveIniParamVal("辅助流设置", "ManualSPSKILL", tFD_LUNA.ManualSPSKILL.ToString());
        }
        /// <summary>
        /// 手动输入数值设置截图间隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_ScreenShotInterval_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_ScreenShotInterval.Text, out int interval) && interval > 0)
            {
                tFD_LUNA.ScreenShotInterval = interval;
                Common.SaveIniParamVal("全局设置", "ScreenShotInterval", interval.ToString());
                MessageShowList.SendEventMsg("已设置截图间隔: " + interval + "毫秒", 1);
            }
            else
            {
                MessageShowList.SendEventMsg("无效的截图间隔: " + tb_ScreenShotInterval.Text, 2);
            }
        }
        /// <summary>
        /// 启用/禁用截图保存功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_SaveScreenImg_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_SaveScreenImg.Checked)
            {
                tFD_LUNA.SaveScreenShot = true;
                MessageShowList.SendEventMsg("已启用截图保存功能", 1);
            }
            else
            {
                tFD_LUNA.SaveScreenShot = false;
                MessageShowList.SendEventMsg("已禁用截图保存功能", 1);
            }
        }
        /// <summary>
        /// 打点方式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_BeatMode_Changed(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
                return;

            string txt = rb.Text;
            int tagVal = Convert.ToInt32(Common.GetObj(rb.Tag, true));
            if (tFD_LUNA != null)
            {
                tFD_LUNA.BeatMode = tagVal;
                Common.SaveIniParamVal("全局设置", "BeatMode", tagVal.ToString());
            }
            MessageShowList.SendEventMsg("已切换打点方式: " + txt, 1);
        }
        /// <summary>
        /// 使用强化技能后自动开枪切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_BFL_AutoShot_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox ckb = sender as CheckBox;
                int _bflas = ckb.Checked ? 1 : 0;
                if (tFD_LUNA != null)
                    tFD_LUNA.BFL_AutoShot = (_bflas == 1);
                Common.SaveIniParamVal("战地演唱会设置", "BFL_AutoShot", _bflas.ToString());
            }
            catch { }
        }
        #endregion

        #region 日志弹窗
        /// <summary>
        /// 显示日志窗口
        /// </summary>
        private void ShowLogFrm()
        {
            if (!ckb_ShowLogFrm.Checked)
                return;

            try
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frm_log)
                    {
                        frm.Show();
                        frm.BringToFront();
                        return;
                    }
                }

                int _fl_x = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "X"), out int i_x) ? i_x : 200;
                int _fl_y = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "Y"), out int i_y) ? i_y : 0;
                int _fl_w = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "W"), out int i_w) ? i_w : 360;
                int _fl_h = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "H"), out int i_h) ? i_h : 135;

                frm_log flog = new frm_log();
                flog.StartPosition = FormStartPosition.Manual;
                flog.Location = new Point(_fl_x, _fl_y);
                flog.Size = new Size(_fl_w, _fl_h);
                flog.Show();
            }
            catch { }
        }
        /// <summary>
        /// 关闭日志窗口
        /// </summary>
        private void CloseLogFrm()
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frm_log)
                    {
                        frm.Close();
                        break;
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 复选框状态改变事件，显示或隐藏日志窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckb_ShowLogFrm_CheckedChanged(object sender, EventArgs e)
        {
            int _showLogForm = 0;
            if (ckb_ShowLogFrm.Checked)
            {
                _showLogForm = 1;
                ShowLogFrm();
            }
            else
            {
                _showLogForm = 0;
                CloseLogFrm();
            }
            Common.SaveIniParamVal("日志弹窗设置", "ShowLogForm", _showLogForm.ToString());
        }
        private void btn_SetLogForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_PickArea f = new frm_PickArea();
            f.TopLevel = true;
            f.Opacity = 0.35;

            int _fl_x = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "X"), out int i_x) ? i_x : 200;
            int _fl_y = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "Y"), out int i_y) ? i_y : 0;
            int _fl_w = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "W"), out int i_w) ? i_w : 360;
            int _fl_h = int.TryParse(Common.GetIniParamVal("日志弹窗设置", "H"), out int i_h) ? i_h : 135;

            Rectangle pickArea = new Rectangle(_fl_x, _fl_y, _fl_w, _fl_h);

            f.PickedArea = pickArea;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                this.BringToFront();

                Common.SaveIniParamVal("日志弹窗设置", "X", f.PickedArea.X.ToString());
                Common.SaveIniParamVal("日志弹窗设置", "Y", f.PickedArea.Y.ToString());
                Common.SaveIniParamVal("日志弹窗设置", "W", f.PickedArea.Width.ToString());
                Common.SaveIniParamVal("日志弹窗设置", "H", f.PickedArea.Height.ToString());

                MessageShowList.SendEventMsg("已选择区域: " + f.PickedArea.ToString(), 1);
            }
            else
            {
                this.Show();
                this.BringToFront();
            }
        }
        #endregion

        #region 自定义按键映射
        private void btn_SetKeyMatch_Click(object sender, EventArgs e) { OpenKeyMatchForm(); }
        /// <summary>
        /// 打开按键映射设置窗口
        /// </summary>
        private void OpenKeyMatchForm()
        {

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "frm_SetKeyMatch")
                {
                    f.Show();
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }

            frm_SetKeyMatch fskm = new frm_SetKeyMatch();
            fskm.Show();
            fskm.BringToFront();

        }
        #endregion

        #region 键盘鼠标钩子 快捷键
        #region 键盘钩子
        /// <summary>
        /// 键盘钩子
        /// </summary>
        private KeyboardHook KBH = null;
        /// <summary>
        /// 挂载钩子，注册键盘监听
        /// </summary>
        private void SetScanner()
        {
            try
            {
                KBH = new KeyboardHook();
                KBH.SetHook();
                KBH.OnKeyDownEvent += KBH_OnKeyDownEvent;
                KBH.OnKeyUpEvent += KBH_OnKeyUpEvent;
            }
            catch (Exception ex) { }
        }
        private void KBH_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            bool holdCtrl = (e.KeyData == (Keys.LControlKey | Keys.Control) || e.KeyData == (Keys.RControlKey | Keys.Control)) || e.Control;
            bool holdShift = (e.KeyData == (Keys.LShiftKey | Keys.Shift) || e.KeyData == (Keys.RShiftKey | Keys.Shift)) || e.Shift;
            bool holdAlt = (e.KeyData == (Keys.LMenu | Keys.Alt) || e.KeyData == (Keys.RMenu | Keys.Alt)) || e.Alt;
        }
        /// <summary>
        /// 监听快捷键是否按下并释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void KBH_OnKeyUpEvent(object sender, KeyEventArgs e)
        {
            try
            {
                bool holdCtrl = e.Control;
                bool holdShift = e.Shift;
                bool holdAlt = e.Alt;

                KeyMouseCaptrued(e.KeyCode.ToString(), 1, e);
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// 取消钩子监听
        /// </summary>
        private void UnHookSanner()
        {
            try
            {
                if (KBH != null)
                {
                    KBH.UnHook();
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        #region 鼠标钩子
        /// <summary>
        /// 鼠标钩子
        /// </summary>
        private GlobalMouseHook MSH = null;
        /// <summary>
        /// 安装鼠标钩子
        /// </summary>
        private void SetMouseHook()
        {
            try
            {
                MSH = new GlobalMouseHook();

                MSH.CaptureMoveEvents = false;
                MSH.CaptureClickEvents = true;
                MSH.CaptureWheelEvents = true;

                // MSH.MouseAction += MouseHook_MouseAction;
                MSH.MouseUp += MouseHook_MouseUp;

                MSH.StartHook();
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg(ex.Message);
            }
        }
        /// <summary>
        /// 卸载鼠标钩子
        /// </summary>
        private void UnHookMouseHook()
        {
            try
            {
                MSH.Dispose();
            }
            catch { }
        }
        private void MouseHook_MouseAction(object sender, MouseEventArgs e)
        {
            // 处理所有鼠标动作
            MessageShowList.SendEventMsg($"动作: {e.Button} at ({e.X}, {e.Y})");
        }
        private void MouseHook_MouseUp(object sender, GlobalMouseHook.MouseClickEventArgs e)
        {
            KeyMouseCaptrued(e.Button.ToString(), 2, e);
            // 处理鼠标释放
            // MessageShowList.SendEventMsg($"释放: {e.Button} ({e.ButtonId}) at ({e.X}, {e.Y})");
        }
        #endregion

        #region  对监听到的按键进行判断，执行快捷键对应的功能
        /// <summary>
        /// 钩子捕获到键之后的处理  判断是否是快捷键
        /// </summary>
        /// <param name="KeyOrMouse"></param>
        /// <param name="KMType"></param>
        /// <param name="ExtraData"></param>
        private void KeyMouseCaptrued(string KeyOrMouse, int KMType, object ExtraData = null)
        {
            //MessageShowList.SendEventMsg(string.Format("释放按键：{0},type={1}",KeyOrMouse,KMType));

            string kc = KeyOrMouse.ToUpper();
            if (string.IsNullOrWhiteSpace(kc))
                return;

            if (KMType == 2)
            {
                switch (KeyOrMouse)
                {
                    case "LEFT": kc = "LBUTTON"; break;
                    case "RIGHT": kc = "RBUTTON"; break;
                    case "MIDDLE": kc = "MBUTTON"; break;
                    case "XBUTTON1": kc = "XBUTTON1"; break;
                    case "XBUTTON2": kc = "XBUTTON2"; break;
                }
            }

            kc = kc.ToUpper();
            if (HotKey_Global.ToUpper() == kc)
            {
                RunMianFunction();
            }
            else if (HotKey_Switch.ToUpper() == kc)
            {
                SwitchCurrentKey();
            }
            else if (HotKey_SwitchShootMode.ToUpper() == kc)
            {
                SwitchBeatMode();
            }

        }
        /// <summary>
        /// 运行主功能函数，启动或停止打碟助手
        /// </summary>
        private void RunMianFunction()
        {
            bool HasPlayedNoticeSound = false;//是否已播放提示音，播放一次后就置为true，防止多个脚本共用一个快捷键时重复播放

            if (!HasPlayedNoticeSound)
                SystemSounds.Beep.Play();
            HasPlayedNoticeSound = true;

            if (!tFD_LUNA.IsRunning)
            {
                SetBtnRunStatus(true);
                ShowLogFrm();

                if (tFD_LUNA != null)
                {
                    tFD_LUNA.Start();
                    MessageShowList.SendEventMsg("打碟助手已启动", 1);
                }
                else
                {
                    MessageShowList.SendEventMsg("打碟助手初始化失败，请检查配置或重启程序", 2);
                    return;
                }
            }
            else
            {
                SetBtnRunStatus(false);
                tFD_LUNA.Stop();
                MessageShowList.SendEventMsg("打碟助手已停止", 1);
                Thread.Sleep(300);
                CloseLogFrm();
            }
        }
        /// <summary>
        /// 切换当前按键
        /// </summary>
        private void SwitchCurrentKey()
        {
            SystemSounds.Beep.Play();
            if (tFD_LUNA != null)
                tFD_LUNA.ChangeCurrentKey_Assist();
        }
        /// <summary>
        /// 切换打点方式
        /// </summary>
        private void SwitchBeatMode()
        {
            SystemSounds.Beep.Play();
            if (tFD_LUNA != null)
                tFD_LUNA.SwitchBeatMode();
        }
        /// <summary>
        /// 设置运行按钮状态
        /// </summary>
        /// <param name="running"></param>
        private void SetBtnRunStatus(bool running)
        {
            if (running)
            {
                btn_Run.Text = "停止";
                btn_Run.BackColor = Color.Red;
                btn_Run.ForeColor = Color.White;
                btn_Run.FlatAppearance.MouseDownBackColor = Color.OrangeRed;
                btn_Run.FlatAppearance.MouseOverBackColor = Color.Firebrick;
            }
            else
            {
                btn_Run.Text = "启动";
                btn_Run.BackColor = Color.SteelBlue;
                btn_Run.ForeColor = Color.White;
                btn_Run.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
                btn_Run.FlatAppearance.MouseOverBackColor = Color.SkyBlue;
            }

        }
        #endregion
        #endregion

        #region 切换运行模式
        private void Rb_Mode_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
                SwitchMode();
        }
        /// <summary>
        /// 切换运行模式
        /// </summary>
        private void SwitchMode()
        {
            try
            {
                string modeText = "-";
                if (rb_Mode_Noise.Checked)
                {
                    tFD_LUNA.Mode = 1;
                    modeText = "噪音涌动输出流";
                    SetRbtextColor(1);

                    gb_Assist.Hide();
                    gb_BFS.Hide();
                    gb_Noise.Show();
                    gb_Noise.BringToFront();
                }
                else if (rb_Mode_Assist.Checked)
                {
                    tFD_LUNA.Mode = 2;
                    modeText = "辅助流";
                    SetRbtextColor(2);

                    gb_Noise.Hide();
                    gb_BFS.Hide();
                    gb_Assist.Show();
                    gb_Assist.BringToFront();
                }
                else if (rb_Mode_BFS.Checked)
                {
                    tFD_LUNA.Mode = 3;
                    modeText = "战地演唱会";
                    SetRbtextColor(3);

                    gb_Assist.Hide();
                    gb_Noise.Hide();
                    gb_BFS.Show();
                    gb_BFS.BringToFront();
                }
                Common.SaveIniParamVal("全局设置", "Mode", tFD_LUNA.Mode.ToString());
                MessageShowList.SendEventMsg("已切换运行模式: " + modeText, 1);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("切换运行模式失败: " + ex.Message, 2);
            }
        }
        private void SetRbtextColor(int _mode)
        {
            //【0[C\V\Z 交替按] 1[左键与 同时按CVZ 交替] 2[只按左键] 3[CVZ同时按] 4[左键与C\V\Z之一轮流按] 5[左键\C\V\Z轮流按] 6[C\V 交替按] 7[切换只按C或V] 8[按照映射] 】
            rb_BeatMode_0.ForeColor = _mode == 1 ? Color.OrangeRed : Color.Black;
            rb_BeatMode_1.ForeColor = _mode == 3 ? Color.OrangeRed : Color.Black;
            rb_BeatMode_2.ForeColor = _mode == 3 ? Color.OrangeRed : Color.Black;
            rb_BeatMode_3.ForeColor = Color.Black;
            rb_BeatMode_4.ForeColor = _mode == 3 ? Color.OrangeRed : Color.Black;
            rb_BeatMode_5.ForeColor = Color.Black;
            rb_BeatMode_6.ForeColor = _mode == 2 ? Color.OrangeRed : Color.Black;
            rb_BeatMode_7.ForeColor = _mode == 2 ? Color.OrangeRed : Color.Black;
            rb_BeatMode_8.ForeColor = Color.Black;
        }
        #endregion

        #region 设置快捷键
        /// <summary>
        /// 输入的字符串是否为有效的键盘按键
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public bool TryParseKey(string keyStr, out Keys key)
        {
            key = Keys.None;
            if (string.IsNullOrWhiteSpace(keyStr))
                return false;

            // 特殊处理空格
            if (keyStr.Trim().Equals("空格", StringComparison.OrdinalIgnoreCase) ||
                keyStr.Trim().Equals("Space", StringComparison.OrdinalIgnoreCase))
            {
                key = Keys.Space;
                return true;
            }

            // 尝试转换为 Keys 枚举
            if (Enum.TryParse<Keys>(keyStr, true, out Keys parsedKey))
            {
                key = parsedKey;
                return true;
            }

            return false;
        }
        private void btn_Run_Click(object sender, EventArgs e) { RunMianFunction(); }
        private void btn_Switch_Click(object sender, EventArgs e) { SwitchCurrentKey(); }

        private void tb_HotKey_SwitchShootMode_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldKey = tb_HotKey_SwitchShootMode.Text;
                string _newkey = PickKey(_oldKey);

                if (_oldKey == _newkey)
                    return;

                tb_HotKey_SwitchShootMode.Text = _newkey;
                HotKey_SwitchShootMode = _newkey;
                Common.SaveIniParamVal("战地演唱会设置", "HotKey_SwitchShootMode", _newkey);
                MessageShowList.SendEventMsg(string.Format("临时切换打点模式快捷键已设置为:{0}", _newkey));
            }
            catch { }
        }
        private void tb_HotKey_Global_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldKey = tb_HotKey_Global.Text;
                string _newkey = PickKey(_oldKey);

                if (_oldKey == _newkey)
                    return;

                tb_HotKey_Global.Text = _newkey;
                HotKey_Global = _newkey;
                Common.SaveIniParamVal("全局设置", "HotKey_Global", _newkey);
                MessageShowList.SendEventMsg(string.Format("启用/停止快捷键已设置为:{0}", _newkey));
            }
            catch { }
        }
        private void tb_HotKey_Switch_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldKey = tb_HotKey_Switch.Text;
                string _newkey = PickKey(_oldKey);

                if (_oldKey == _newkey)
                    return;

                tb_HotKey_Switch.Text = _newkey;
                HotKey_Switch = _newkey;
                Common.SaveIniParamVal("辅助流设置", "HotKey_Switch", _newkey);
                MessageShowList.SendEventMsg(string.Format("辅助流C/V切换快捷键已设置为:{0}", _newkey));
            }
            catch { }
        }

        /// <summary>
        /// 打开快捷键选择窗体，选择快捷键
        /// </summary>
        /// <param name="_oldkey"></param>
        /// <returns></returns>
        private string PickKey(string _oldkey = "")
        {
            try
            {
                string _newkey = _oldkey;
                frm_Allkeys f = new frm_Allkeys(_oldkey);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _newkey = f.SelectedKey;
                    f.Dispose();
                }

                return _newkey;
            }
            catch { return _oldkey; }
        }
        #endregion

        #region 战地炮台设置相关
        private void tb_BFS_CVZ_CX_time_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_BFS_CVZ_CX_time.Text, out int interval) && interval > 0)
            {
                tFD_LUNA.CVZ_CX_time = interval;
                Common.SaveIniParamVal("战地演唱会设置", "CVZ_CX_time", interval.ToString());
                MessageShowList.SendEventMsg("[演唱会]已设置普通技能持续时间: " + interval + "毫秒", 1);
            }
            else
            {
                MessageShowList.SendEventMsg("无效的时间: " + tb_BFS_CVZ_CX_time.Text, 2);
            }
        }
        private void tb_BFS_PowerfulTime_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_BFS_PowerfulTime.Text, out int interval) && interval > 0)
            {
                tFD_LUNA.PowerfulTime = interval;
                Common.SaveIniParamVal("战地演唱会设置", "PowerfulTime", interval.ToString());
                MessageShowList.SendEventMsg("[演唱会]已设置强化技能持续时间: " + interval + "毫秒", 1);
            }
            else
            {
                MessageShowList.SendEventMsg("无效的时间: " + tb_BFS_PowerfulTime.Text, 2);
            }
        }
        private void rb_BFS_APF_Disable_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rb = sender as RadioButton;
                if (!rb.Checked)
                    return;

                string _modename = "禁用";
                int _apf = 0;

                if (rb_BFS_APF_Disable.Checked)
                {
                    _apf = 0;
                    _modename = "禁用";
                }
                else if (rb_BFS_APF_Enable.Checked)
                {
                    _apf = 1;
                    _modename = "启用";
                }

                if (tFD_LUNA != null)
                {
                    tFD_LUNA.AutoPowerfulSkill = _apf;
                    Common.SaveIniParamVal("战地演唱会设置", "AutoPowerfulSkill", _apf.ToString());
                }

                MessageShowList.SendEventMsg("[演唱会]已切换自动使用强化技能: " + _modename, 1);
            }
            catch { }
        }
        #endregion

        #region 战地演唱会识别设置_右
        private void RAS_BFL_R_P_ImgModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RS_BFS_R.OpencvMode = _val;
                Common.SaveIniParamVal("战地演唱会识别设置_右", "OpencvMode", _val.ToString());

                MessageShowList.SendEventMsg("已切换战地炮台图像处理模式: " + ExtraData, 1);
            }
            catch { }
            // MessageShowList.SendEventMsg(string.Format("1:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        private void RAS_BFL_R_P_MaskModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RS_BFS_R.MaskMode = _val;
                Common.SaveIniParamVal("战地演唱会识别设置_右", "MaskMode", _val.ToString());
            }
            catch { }
            // MessageShowList.SendEventMsg(string.Format("2:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        private void RAS_BFL_R_P_RealScreenWidthChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RS_BFS_R.RealScreenWidth = _val;
                Common.SaveIniParamVal("战地演唱会识别设置_右", "RealScreenWidth", _val.ToString());
            }
            catch { }

            //MessageShowList.SendEventMsg(string.Format("3:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        private void RAS_BFL_R_P_RecognizeAreaChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                Rectangle rec = (Rectangle)ParamValue;
                if (tFD_LUNA != null && tFD_LUNA.RS_BFS_R != null)
                    tFD_LUNA.RS_BFS_R.SetArea(rec.X, rec.Y, rec.Width, rec.Height);
                MessageShowList.SendEventMsg("已设置战地炮台右边识别区: " + rec.ToString(), 1);
            }
            catch { }
            // MessageShowList.SendEventMsg(string.Format("4:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        private void RAS_BFL_R_P_ScaleModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RS_BFS_R.ScaleMode = _val;
                Common.SaveIniParamVal("战地演唱会识别设置_右", "ScaleMode", _val.ToString());
            }
            catch { }

            // MessageShowList.SendEventMsg(string.Format("5:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        private void RAS_BFL_R_P_ThresholdChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                double thd = (double)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RS_BFS_R.Threshold = thd;
                Common.SaveIniParamVal("战地演唱会识别设置_右", "Threshold", thd.ToString());
            }
            catch { }

            // MessageShowList.SendEventMsg(string.Format("6:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        #endregion

        #region 菱形图案识别设置
        private void RAS_Normal_Block_P_ImgModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_Rhombus.OpencvMode = _val;
                Common.SaveIniParamVal("菱形图案识别设置", "OpencvMode", _val.ToString());

                MessageShowList.SendEventMsg("已切换菱形图案处理模式: " + ExtraData, 1);
            }
            catch { }
        }
        private void RAS_Normal_Block_P_MaskModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_Rhombus.MaskMode = _val;
                Common.SaveIniParamVal("菱形图案识别设置", "MaskMode", _val.ToString());
            }
            catch { }
        }
        private void RAS_Normal_Block_P_RealScreenWidthChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_Rhombus.RealScreenWidth = _val;
                Common.SaveIniParamVal("菱形图案识别设置", "RealScreenWidth", _val.ToString());
            }
            catch { }
        }
        private void RAS_Normal_Block_P_RecognizeAreaChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                Rectangle rec = (Rectangle)ParamValue;
                if (tFD_LUNA != null && tFD_LUNA.RecognizeSetting_Rhombus != null)
                    tFD_LUNA.RecognizeSetting_Rhombus.SetArea(rec.X, rec.Y, rec.Width, rec.Height);
                MessageShowList.SendEventMsg("已设置菱形图案识别区: " + rec.ToString(), 1);
            }
            catch { }
            //MessageShowList.SendEventMsg(string.Format("4:{0},{1},{2}", ParamName, ParamValue, ExtraData));
        }
        private void RAS_Normal_Block_P_ScaleModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_Rhombus.ScaleMode = _val;
                Common.SaveIniParamVal("菱形图案识别设置", "ScaleMode", _val.ToString());
            }
            catch { }
        }
        private void RAS_Normal_Block_P_ThresholdChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                double thd = (double)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_Rhombus.Threshold = thd;
                Common.SaveIniParamVal("菱形图案识别设置", "Threshold", thd.ToString());
            }
            catch { }
        }
        #endregion

        #region  灵感条图案识别设置
        private void RAS_MuseBar_P_ImgModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_MuseBar.OpencvMode = _val;
                Common.SaveIniParamVal("灵感条图案识别设置", "OpencvMode", _val.ToString());

                MessageShowList.SendEventMsg("已切换灵感条图案处理模式: " + ExtraData, 1);
            }
            catch { }
        }
        private void RAS_MuseBar_P_MaskModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_MuseBar.MaskMode = _val;
                Common.SaveIniParamVal("灵感条图案识别设置", "MaskMode", _val.ToString());
            }
            catch { }
        }
        private void RAS_MuseBar_P_RealScreenWidthChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_MuseBar.RealScreenWidth = _val;
                Common.SaveIniParamVal("灵感条图案识别设置", "RealScreenWidth", _val.ToString());
            }
            catch { }
        }
        private void RAS_MuseBar_P_RecognizeAreaChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                Rectangle rec = (Rectangle)ParamValue;
                if (tFD_LUNA != null && tFD_LUNA.RecognizeSetting_MuseBar != null)
                    tFD_LUNA.RecognizeSetting_MuseBar.SetArea(rec.X, rec.Y, rec.Width, rec.Height);
                MessageShowList.SendEventMsg("已设置灵感条图案识别区: " + rec.ToString(), 1);
            }
            catch { }
        }
        private void RAS_MuseBar_P_ScaleModeChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                int _val = (int)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_MuseBar.ScaleMode = _val;
                Common.SaveIniParamVal("灵感条图案识别设置", "ScaleMode", _val.ToString());
            }
            catch { }
        }
        private void RAS_MuseBar_P_ThresholdChanged(string ParamName, object ParamValue, object ExtraData)
        {
            try
            {
                double thd = (double)ParamValue;
                if (tFD_LUNA != null)
                    tFD_LUNA.RecognizeSetting_MuseBar.Threshold = thd;
                Common.SaveIniParamVal("灵感条图案识别设置", "Threshold", thd.ToString());
            }
            catch { }
        }
        #endregion
    }
}
