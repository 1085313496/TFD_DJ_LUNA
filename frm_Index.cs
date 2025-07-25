using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
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
        /// 当前页面状态 0查看 1编辑状态
        /// </summary>
        public bool PageEditable = false;

        TFD_LUNA tFD_LUNA = new TFD_LUNA();

        public frm_Index()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Disposed += Form1_Disposed;
            MessageShowList.SendNotice += MessageShowList_SendNotice;

            SetReadonly();

            LoadIni();
            LoadParams();

            SetScanner();
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadIni()
        {
            HotKey_Global = Common.GetIniParamVal("全局设置", "HotKey_Global");
            HotKey_Global = string.IsNullOrWhiteSpace(HotKey_Global) ? "F5" : HotKey_Global.ToUpper();

            HotKey_Switch = Common.GetIniParamVal("辅助流设置", "HotKey_Switch");
            HotKey_Switch = string.IsNullOrWhiteSpace(HotKey_Switch) ? "F6" : HotKey_Switch.ToUpper();
        }
        /// <summary>
        /// 加载参数到界面控件
        /// </summary>
        private void LoadParams()
        {
            tb_HotKey_Global.Text = HotKey_Global;
            tb_HotKey_Switch.Text = HotKey_Switch;

            tb_ScreenShotInterval.Text = tFD_LUNA.ScreenShotInterval.ToString();

            #region 运行类型 1噪音涌动输出流 2辅助流 缺省值1
            if (tFD_LUNA.Mode == 1)
            {
                rb_Mode_Noise.Checked = true;

                gb_Assist.Hide();
                gb_Noise.Show();
                gb_Noise.BringToFront();
            }
            else
            {
                rb_Mode_Assist.Checked = true;
                gb_Noise.Hide();
                gb_Assist.Show();
                gb_Assist.BringToFront();
            }
            #endregion

            #region 
            if (tFD_LUNA.SwitchType_Noise == 1)
            {
                rg_SWT_1.Checked = true;
            }
            else
            {
                rg_SWT_2.Checked = true;
            }
            #endregion

            #region
            if (tFD_LUNA.SwitchType_Assist == 1)
            {
                rb_CandV.Checked = true;
            }
            else
            {
                rb_CorV.Checked = true;
            }
            #endregion

            ckb_ManualSPSKILL.Checked = tFD_LUNA.ManualSPSKILL == 1;

            if (tFD_LUNA.RecognizeSetting_Rhombus != null)
            {
                tb_RX.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.X.ToString();
                tb_RY.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.Y.ToString();
                tb_RW.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.Width.ToString();
                tb_RH.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.Height.ToString();
                tkb_R.Value = (int)(tFD_LUNA.RecognizeSetting_Rhombus.Threshold * 100);
            }

            if (tFD_LUNA.RecognizeSetting_MuseBar != null)
            {
                tb_MBX.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.X.ToString();
                tb_MBY.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.Y.ToString();
                tb_MBW.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.Width.ToString();
                tb_MBH.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.Height.ToString();
                tkb_MB.Value = (int)(tFD_LUNA.RecognizeSetting_MuseBar.Threshold * 100);
            }

        }
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

                frm_log flog = new frm_log();
                flog.StartPosition = FormStartPosition.Manual;
                flog.Location = new Point(200, 0);
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

                    rtb.AppendText(msg);
                    rtb.AppendText(Environment.NewLine); // 添加换行符
                    rtb.ScrollToCaret(); // 滚动到最新消息
                }));
            }
            catch { }

        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            UnHookSanner();

            if (tFD_LUNA != null)
            {
                tFD_LUNA.IsRunning = false;
                tFD_LUNA.Stop();
            }
        }

        private void swb_EditPage_StateChanged(object sender, EventArgs e)
        {
            PageEditable = !PageEditable;
            SetReadonly();
        }
        private void SetReadonly()
        {
            tb_HotKey_Global.ReadOnly = !PageEditable;
            tb_HotKey_Switch.ReadOnly = !PageEditable;
            tb_ScreenShotInterval.ReadOnly = !PageEditable;

            //rb_Mode_Noise.Enabled = PageEditable;
            //rb_Mode_Assist.Enabled = PageEditable;
            //rg_SWT_1.Enabled = PageEditable;
            //rg_SWT_2.Enabled = PageEditable;
            //rb_CandV.Enabled = PageEditable;
            //rb_CorV.Enabled = PageEditable;

            ckb_ManualSPSKILL.Enabled = PageEditable;

            tb_RX.ReadOnly = !PageEditable;
            tb_RY.ReadOnly = !PageEditable;
            tb_RW.ReadOnly = !PageEditable;
            tb_RH.ReadOnly = !PageEditable;

            tb_MBX.ReadOnly = !PageEditable;
            tb_MBY.ReadOnly = !PageEditable;
            tb_MBW.ReadOnly = !PageEditable;
            tb_MBH.ReadOnly = !PageEditable;

            tkb_MB.Enabled = PageEditable;
            tkb_R.Enabled = PageEditable;

        }

        private void ckb_ShowLogFrm_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_ShowLogFrm.Checked)
            {
                ShowLogFrm();
            }
            else
            {
                CloseLogFrm();
            }
        }

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

                string kc = e.KeyCode.ToString();
                if (string.IsNullOrWhiteSpace(kc))
                    return;

                if (HotKey_Global == kc.ToUpper())
                {
                    RunMianFunction();
                }
                else if (HotKey_Switch == kc.ToUpper())
                {
                    SwitchCurrentKey();
                }

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

        #region 切换运行模式
        private void Rb_Mode_CheckedChanged(object sender, EventArgs e) { SwitchMode(); }
        /// <summary>
        /// 切换运行模式
        /// </summary>
        private void SwitchMode()
        {
            try
            {
                if (rb_Mode_Noise.Checked)
                {
                    tFD_LUNA.Mode = 1;

                    gb_Assist.Hide();
                    gb_Noise.Show();
                    gb_Noise.BringToFront();
                }
                else
                {
                    tFD_LUNA.Mode = 2;

                    gb_Noise.Hide();
                    gb_Assist.Show();
                    gb_Assist.BringToFront();
                }

                Common.SaveIniParamVal("全局设置", "Mode", tFD_LUNA.Mode.ToString());
                MessageShowList.SendEventMsg("已切换运行模式: " + (tFD_LUNA.Mode == 1 ? "噪音涌动输出流" : "辅助流"), 1);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("切换运行模式失败: " + ex.Message, 2);
            }
        }
        #endregion

        #region 切换噪音涌动输出流设置
        private void Rb_SwitchType_Noise_CHKChanged(object sender, EventArgs e) { SwitchSwitchType_Noise(); }

        /// <summary>
        /// 切换噪音涌动输出流设置
        /// </summary>
        private void SwitchSwitchType_Noise()
        {
            try
            {
                if (rg_SWT_1.Checked)
                {
                    tFD_LUNA.SwitchType_Noise = 1;
                }
                else
                {
                    tFD_LUNA.SwitchType_Noise = 0;
                }

                Common.SaveIniParamVal("噪音涌动输出流设置", "SwitchType_Noise", tFD_LUNA.SwitchType_Noise.ToString());
                MessageShowList.SendEventMsg("噪音输出流按键已切换为: " + (tFD_LUNA.SwitchType_Noise == 1 ? "依序交替按下CVZ" : "严格按照映射"), 1);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("切换噪音涌动输出流设置失败: " + ex.Message, 2);
            }
        }
        #endregion

        #region 切换辅助流设置
        private void Rb_SwitchType_Assist_CHKChanged(object sender, EventArgs e) { SwitchSType_Assist(); }
        private void SwitchSType_Assist()
        {
            try
            {
                if (rb_CandV.Checked)
                {
                    tFD_LUNA.SwitchType_Assist = 1;
                }
                else
                {
                    tFD_LUNA.SwitchType_Assist = 0;
                }

                Common.SaveIniParamVal("辅助流设置", "SwitchType_Assist", tFD_LUNA.SwitchType_Assist.ToString());
                MessageShowList.SendEventMsg("辅助流按键已切换为: " + (tFD_LUNA.SwitchType_Assist == 1 ? "交替按下CV" : "手动切换只按C或V"), 1);
            }
            catch (Exception ex)
            {
                MessageShowList.SendEventMsg("切换辅助流设置失败: " + ex.Message, 2);
            }
        }
        #endregion

        #region 识别阈值设置
        private void tkb_R_ValueChanged(object sender, EventArgs e)
        {
            double thd = tkb_R.Value / 100.0;
            if (tFD_LUNA != null)
                tFD_LUNA.RecognizeSetting_Rhombus.Threshold = thd;
            lb_THD_R.Text = string.Format("{0:0.00}", thd);
            Common.SaveIniParamVal("菱形图案识别设置", "Threshold", thd.ToString());
        }

        private void tkb_MB_ValueChanged(object sender, EventArgs e)
        {
            double thd = tkb_MB.Value / 100.0;
            if (tFD_LUNA != null)
                tFD_LUNA.RecognizeSetting_MuseBar.Threshold = thd;
            lb_THD_MB.Text = string.Format("{0:0.00}", thd);
            Common.SaveIniParamVal("灵感条图案识别设置", "Threshold", thd.ToString());
        }
        #endregion

        #region 手动设置菱形图案识别区域
        private void btn_PickArea_R_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_PickArea f = new frm_PickArea();
            f.TopLevel = true;
            f.Opacity = 0.35;
            f.PickedArea = tFD_LUNA.RecognizeSetting_Rhombus.Area;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                this.BringToFront();

                tb_RX.Text = f.PickedArea.X.ToString();
                tb_RY.Text = f.PickedArea.Y.ToString();
                tb_RW.Text = f.PickedArea.Width.ToString();
                tb_RH.Text = f.PickedArea.Height.ToString();

                tFD_LUNA.RecognizeSetting_Rhombus.SetArea(f.PickedArea.X, f.PickedArea.Y, f.PickedArea.Width, f.PickedArea.Height);
                MessageShowList.SendEventMsg("已选择区域: " + f.PickedArea.ToString(), 1);
            }
            else
            {
                this.Show();
                this.BringToFront();
            }
        }
        #endregion

        #region 手动设置灵感条图案识别区域
        private void btn_PickArea_MB_Click(object sender, EventArgs e)
        {
            this.Hide();

            frm_PickArea f = new frm_PickArea();
            f.TopLevel = true;
            f.Opacity = 0.35;
            f.PickedArea = tFD_LUNA.RecognizeSetting_MuseBar.Area;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                this.BringToFront();

                tb_MBX.Text = f.PickedArea.X.ToString();
                tb_MBY.Text = f.PickedArea.Y.ToString();
                tb_MBW.Text = f.PickedArea.Width.ToString();
                tb_MBH.Text = f.PickedArea.Height.ToString();

                tFD_LUNA.RecognizeSetting_MuseBar.SetArea(f.PickedArea.X, f.PickedArea.Y, f.PickedArea.Width, f.PickedArea.Height);
                MessageShowList.SendEventMsg("已选择区域: " + f.PickedArea.ToString(), 1);
            }
            else
            {
                this.Show();
                this.BringToFront();
            }
        }
        #endregion

        private void btn_Recog_R_Click(object sender, EventArgs e)
        {

        }

        private void btn_Recog_MB_Click(object sender, EventArgs e)
        {

        }

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

        private void tb_HotKey_Global_TextChanged(object sender, EventArgs e)
        {
            Keys k;
            if (TryParseKey(tb_HotKey_Global.Text, out k))
            {
                HotKey_Global = k.ToString().ToUpper();
                Common.SaveIniParamVal("全局设置", "HotKey_Global", HotKey_Global);
                MessageShowList.SendEventMsg("已设置全局快捷键: " + HotKey_Global, 1);
            }
            else
            {
                MessageShowList.SendEventMsg("无效的全局快捷键: " + tb_HotKey_Global.Text, 2);
            }
        }

        private void tb_HotKey_Switch_TextChanged(object sender, EventArgs e)
        {
            Keys k;
            if (TryParseKey(tb_HotKey_Switch.Text, out k))
            {
                HotKey_Switch = k.ToString().ToUpper();
                Common.SaveIniParamVal("辅助流设置", "HotKey_Switch", HotKey_Switch);
                MessageShowList.SendEventMsg("已设置辅助流切换快捷键: " + HotKey_Switch, 1);
            }
            else
            {
                MessageShowList.SendEventMsg("无效的辅助流切换快捷键: " + tb_HotKey_Switch.Text, 2);
            }
        }

        private void btn_Run_Click(object sender, EventArgs e) { RunMianFunction(); }
        private void btn_Switch_Click(object sender, EventArgs e) { SwitchCurrentKey(); }

        #endregion

        private void ckb_ManualSPSKILL_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_ManualSPSKILL.Checked)
            {
                tFD_LUNA.ManualSPSKILL = 1;
                MessageShowList.SendEventMsg("灵感条满了需要手动使用强化技能", 1);
            }
            else
            {
                tFD_LUNA.ManualSPSKILL = 0;
                MessageShowList.SendEventMsg("灵感条满了无所谓", 1);
            }
            Common.SaveIniParamVal("辅助流设置", "ManualSPSKILL", tFD_LUNA.ManualSPSKILL.ToString());
        }

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

        private void Tb_PA_R_Changed(object sender, EventArgs e)
        {
            if (int.TryParse(tb_RX.Text, out int x) &&
                int.TryParse(tb_RY.Text, out int y) &&
                int.TryParse(tb_RW.Text, out int w) &&
                int.TryParse(tb_RH.Text, out int h))
            {
                if (tFD_LUNA != null)
                {
                    tFD_LUNA.RecognizeSetting_Rhombus.SetArea(x, y, w, h);
                    MessageShowList.SendEventMsg("已设置菱形图案识别区域: " + tFD_LUNA.RecognizeSetting_Rhombus.Area.ToString(), 1);
                }
            }
            else
            {
                //MessageShowList.SendEventMsg("无效的菱形图案识别区域参数", 2);
            }
        }

        private void Tb_PA_MB_Changed(object sender, EventArgs e)
        {
            if (int.TryParse(tb_MBX.Text, out int x) &&
                int.TryParse(tb_MBY.Text, out int y) &&
                int.TryParse(tb_MBW.Text, out int w) &&
                int.TryParse(tb_MBH.Text, out int h))
            {
                if (tFD_LUNA != null)
                {
                    tFD_LUNA.RecognizeSetting_MuseBar.SetArea(x, y, w, h);
                    MessageShowList.SendEventMsg("已设置灵感条图案识别区域: " + tFD_LUNA.RecognizeSetting_MuseBar.Area.ToString(), 1);
                }
            }
            else
            {
                //MessageShowList.SendEventMsg("无效的灵感条图案识别区域参数", 2);
            }
        }

        private void ckb_SaveScreenImg_CheckedChanged(object sender, EventArgs e)
        {
            if(ckb_SaveScreenImg.Checked)
            {
                tFD_LUNA.SaveScreenShot = true;
                MessageShowList.SendEventMsg("已启用截图保存功能", 1);
            }
            else
            {
                tFD_LUNA.SaveScreenShot=false;
                MessageShowList.SendEventMsg("已禁用截图保存功能", 1);
            }   


        }
    }
}
