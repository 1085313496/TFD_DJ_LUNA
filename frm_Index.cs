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
        /// 窗体被释放时，取消钩子监听并停止打碟助手线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Disposed(object sender, EventArgs e)
        {
            UnHookSanner();

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
            ckb_ManualSPSKILL.Checked = tFD_LUNA.ManualSPSKILL == 1;

            #region 加载战地炮台设置
            if (tFD_LUNA != null)
            {
                tb_BFS_R_Interval.Text = tFD_LUNA.BFS_R_Interval.ToString();
                tb_BFS_CVZ_CX_time.Text = tFD_LUNA.CVZ_CX_time.ToString();
                tb_BFS_PowerfulTime.Text = tFD_LUNA.PowerfulTime.ToString();

                switch (tFD_LUNA.AutoPowerfulSkill)
                {
                    case 0: rb_BFS_APF_Disable.Checked = true; break;
                    case 1: rb_BFS_APF_Enable.Checked = true; break;
                    case 2: rb_BFS_APF_Enable_SP.Checked = true; break;
                }
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

            #region 图像处理格式
            switch (tFD_LUNA.RecognizeSetting_Rhombus.OpencvMode)
            {
                case 1: rg_R_Grey.Checked = true; break;
                case 2: rg_R_HSV.Checked = true; break;
                case 0:
                default: rg_R_bgr.Checked = true; break;
            }
            switch (tFD_LUNA.RecognizeSetting_MuseBar.OpencvMode)
            {
                case 1: rg_MB_Grey.Checked = true; break;
                case 2: rg_MB_HSV.Checked = true; break;
                case 0:
                default: rg_MB_bgr.Checked = true; break;
            }
            switch (tFD_LUNA.RS_BFS_L.OpencvMode)
            {
                case 1: rb_BFS_L_Grey.Checked = true; break;
                case 2: rb_BFS_L_HSV.Checked = true; break;
                case 0:
                default: rb_BFS_L_BGR.Checked = true; break;
            }
            switch (tFD_LUNA.RS_BFS_R.OpencvMode)
            {
                case 1: rb_BFS_R_GREY.Checked = true; break;
                case 2: rb_BFS_R_HSV.Checked = true; break;
                case 0:
                default: rb_BFS_R_BGR.Checked = true; break;
            }
            #endregion

            #region 识别区域赋值
            if (tFD_LUNA.RecognizeSetting_Rhombus != null)
            {
                tb_RX.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.X.ToString();
                tb_RY.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.Y.ToString();
                tb_RW.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.Width.ToString();
                tb_RH.Text = tFD_LUNA.RecognizeSetting_Rhombus.Area.Height.ToString();
                tkb_R.Value = (int)(tFD_LUNA.RecognizeSetting_Rhombus.Threshold * 100);
                ckb_Sharpen_R.Checked = tFD_LUNA.RecognizeSetting_Rhombus.Sharpen == 1;
            }

            if (tFD_LUNA.RecognizeSetting_MuseBar != null)
            {
                tb_MBX.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.X.ToString();
                tb_MBY.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.Y.ToString();
                tb_MBW.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.Width.ToString();
                tb_MBH.Text = tFD_LUNA.RecognizeSetting_MuseBar.Area.Height.ToString();
                tkb_MB.Value = (int)(tFD_LUNA.RecognizeSetting_MuseBar.Threshold * 100);
                ckb_Sharpen_MB.Checked = tFD_LUNA.RecognizeSetting_MuseBar.Sharpen == 1;
            }

            if (tFD_LUNA.RS_BFS_L != null)
            {
                tb_BFS_L_X.Text = tFD_LUNA.RS_BFS_L.Area.X.ToString();
                tb_BFS_L_Y.Text = tFD_LUNA.RS_BFS_L.Area.Y.ToString();
                tb_BFS_L_W.Text = tFD_LUNA.RS_BFS_L.Area.Width.ToString();
                tb_BFS_L_H.Text = tFD_LUNA.RS_BFS_L.Area.Height.ToString();
                tkb_BFS_L.Value = (int)(tFD_LUNA.RS_BFS_L.Threshold * 100);
                ckb_Sharpen_BFS_L.Checked = tFD_LUNA.RS_BFS_L.Sharpen == 1;
            }

            if (tFD_LUNA.RS_BFS_R != null)
            {
                tb_BFS_R_X.Text = tFD_LUNA.RS_BFS_R.Area.X.ToString();
                tb_BFS_R_Y.Text = tFD_LUNA.RS_BFS_R.Area.Y.ToString();
                tb_BFS_R_W.Text = tFD_LUNA.RS_BFS_R.Area.Width.ToString();
                tb_BFS_R_H.Text = tFD_LUNA.RS_BFS_R.Area.Height.ToString();
                tkb_BFS_R.Value = (int)(tFD_LUNA.RS_BFS_R.Threshold * 100);
                ckb_Sharpen_BFS_R.Checked = tFD_LUNA.RS_BFS_R.Sharpen == 1;
            }
            #endregion
        }

        /// <summary>
        /// 编辑页面状态改变事件，切换页面只读或可编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void swb_EditPage_StateChanged(object sender, EventArgs e)
        {
            PageEditable = !PageEditable;
            SetReadonly();
        }
        /// <summary>
        /// 设置控件为只读或可编辑状态
        /// </summary>
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

            tb_BFS_R_Interval.ReadOnly = !PageEditable;
            tb_BFS_CVZ_CX_time.ReadOnly = !PageEditable;
            tb_BFS_PowerfulTime.ReadOnly = !PageEditable;

            tb_RX.ReadOnly = !PageEditable;
            tb_RY.ReadOnly = !PageEditable;
            tb_RW.ReadOnly = !PageEditable;
            tb_RH.ReadOnly = !PageEditable;

            tb_MBX.ReadOnly = !PageEditable;
            tb_MBY.ReadOnly = !PageEditable;
            tb_MBW.ReadOnly = !PageEditable;
            tb_MBH.ReadOnly = !PageEditable;

            tb_BFS_L_X.ReadOnly = !PageEditable;
            tb_BFS_L_Y.ReadOnly = !PageEditable;
            tb_BFS_L_W.ReadOnly = !PageEditable;
            tb_BFS_L_H.ReadOnly = !PageEditable;

            tb_BFS_R_X.ReadOnly = !PageEditable;
            tb_BFS_R_Y.ReadOnly = !PageEditable;
            tb_BFS_R_W.ReadOnly = !PageEditable;
            tb_BFS_R_H.ReadOnly = !PageEditable;

            tkb_MB.Enabled = PageEditable;
            tkb_R.Enabled = PageEditable;
            tkb_BFS_L.Enabled = PageEditable;
            tkb_BFS_R.Enabled = PageEditable;
        }


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
                MessageShowList.SendEventMsg("灵感条满了需要手动使用强化技能", 1);
            }
            else
            {
                tFD_LUNA.ManualSPSKILL = 0;
                MessageShowList.SendEventMsg("灵感条满了无所谓", 1);
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

                    gb_Assist.Hide();
                    gb_BFS.Hide();
                    gb_Noise.Show();
                    gb_Noise.BringToFront();
                }
                else if (rb_Mode_Assist.Checked)
                {
                    tFD_LUNA.Mode = 2;
                    modeText = "辅助流";

                    gb_Noise.Hide();
                    gb_BFS.Hide();
                    gb_Assist.Show();
                    gb_Assist.BringToFront();
                }
                else if (rb_Mode_BFS.Checked)
                {
                    tFD_LUNA.Mode = 3;
                    modeText = "战地演唱会";

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

        private void tkb_BFS_L_ValueChanged(object sender, EventArgs e)
        {
            double thd = tkb_BFS_L.Value / 100.0;
            if (tFD_LUNA != null)
                tFD_LUNA.RS_BFS_L.Threshold = thd;
            lb_THD_BFS_L.Text = string.Format("{0:0.00}", thd);
            Common.SaveIniParamVal("战地演唱会识别设置_左", "Threshold", thd.ToString());
        }

        private void tkb_BFS_R_ValueChanged(object sender, EventArgs e)
        {
            double thd = tkb_BFS_R.Value / 100.0;
            if (tFD_LUNA != null)
                tFD_LUNA.RS_BFS_R.Threshold = thd;
            lb_THD_BFS_R.Text = string.Format("{0:0.00}", thd);
            Common.SaveIniParamVal("战地演唱会识别设置_右", "Threshold", thd.ToString());
        }

        #endregion

        #region 手动框选图案识别区域
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

        private void btn_PickArea_BFS_L_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_PickArea f = new frm_PickArea();
            f.TopLevel = true;
            f.Opacity = 0.35;
            f.PickedArea = tFD_LUNA.RS_BFS_L.Area;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                this.BringToFront();

                tb_BFS_L_X.Text = f.PickedArea.X.ToString();
                tb_BFS_L_Y.Text = f.PickedArea.Y.ToString();
                tb_BFS_L_W.Text = f.PickedArea.Width.ToString();
                tb_BFS_L_H.Text = f.PickedArea.Height.ToString();

                tFD_LUNA.RS_BFS_L.SetArea(f.PickedArea.X, f.PickedArea.Y, f.PickedArea.Width, f.PickedArea.Height);
                MessageShowList.SendEventMsg("已选择区域: " + f.PickedArea.ToString(), 1);
            }
            else
            {
                this.Show();
                this.BringToFront();
            }
        }

        private void btn_PickArea_BFS_R_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_PickArea f = new frm_PickArea();
            f.TopLevel = true;
            f.Opacity = 0.35;
            f.PickedArea = tFD_LUNA.RS_BFS_R.Area;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                this.BringToFront();

                tb_BFS_R_X.Text = f.PickedArea.X.ToString();
                tb_BFS_R_Y.Text = f.PickedArea.Y.ToString();
                tb_BFS_R_W.Text = f.PickedArea.Width.ToString();
                tb_BFS_R_H.Text = f.PickedArea.Height.ToString();

                tFD_LUNA.RS_BFS_R.SetArea(f.PickedArea.X, f.PickedArea.Y, f.PickedArea.Width, f.PickedArea.Height);
                MessageShowList.SendEventMsg("已选择区域: " + f.PickedArea.ToString(), 1);
            }
            else
            {
                this.Show();
                this.BringToFront();
            }
        }
        #endregion

        #region 手动输入数值设置图案识别区域
        /// <summary>
        /// 手动输入数值设置菱形图案识别区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    MessageShowList.SendEventMsg("已设置菱形图案识别区: " + tFD_LUNA.RecognizeSetting_Rhombus.Area.ToString(), 1);
                }
            }
            else
            {
                //MessageShowList.SendEventMsg("无效的菱形图案识别区域参数", 2);
            }
        }

        /// <summary>
        /// 手动输入数值设置灵感条图案识别区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    MessageShowList.SendEventMsg("已设置灵感条图案识别区: " + tFD_LUNA.RecognizeSetting_MuseBar.Area.ToString(), 1);
                }
            }
            else
            {
                //MessageShowList.SendEventMsg("无效的灵感条图案识别区域参数", 2);
            }
        }

        /// <summary>
        /// 手动输入数值设置 战地炮台左边 识别区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_PA_BFS_L_Changed(object sender, EventArgs e)
        {
            if (int.TryParse(tb_BFS_L_X.Text, out int x) &&
                int.TryParse(tb_BFS_L_Y.Text, out int y) &&
                int.TryParse(tb_BFS_L_W.Text, out int w) &&
                int.TryParse(tb_BFS_L_H.Text, out int h))
            {
                if (tFD_LUNA != null)
                {
                    tFD_LUNA.RS_BFS_L.SetArea(x, y, w, h);
                    MessageShowList.SendEventMsg("已设置战地炮台左边识别区: " + tFD_LUNA.RS_BFS_L.Area.ToString(), 1);
                }
            }
            else
            {
                //MessageShowList.SendEventMsg("无效的战地炮台左边识别区域参数", 2);
            }
        }
        /// <summary>
        /// 手动输入数值设置 战地炮台右边 识别区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_PA_BFS_R_Changed(object sender, EventArgs e)
        {
            if (int.TryParse(tb_BFS_R_X.Text, out int x) &&
                int.TryParse(tb_BFS_R_Y.Text, out int y) &&
                int.TryParse(tb_BFS_R_W.Text, out int w) &&
                int.TryParse(tb_BFS_R_H.Text, out int h))
            {
                if (tFD_LUNA != null)
                {
                    tFD_LUNA.RS_BFS_R.SetArea(x, y, w, h);
                    MessageShowList.SendEventMsg("已设置战地炮台右边识别区: " + tFD_LUNA.RS_BFS_R.Area.ToString(), 1);
                }
            }
            else
            {
                //MessageShowList.SendEventMsg("无效的战地炮台右边识别区域参数", 2);
            }
        }
        #endregion

        #region 图像处理格式切换
        /// <summary>
        /// 菱形图案识别图像处理模式切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rg_R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
                return;

            int opencvImgMode = 0;
            string _modename = rb.Text;

            if (rg_R_bgr.Checked)
            {
                opencvImgMode = 0;
            }
            else if (rg_R_Grey.Checked)
            {
                opencvImgMode = 1;
            }
            else if (rg_R_HSV.Checked)
            {
                opencvImgMode = 2;
            }

            if (tFD_LUNA != null)
            {
                tFD_LUNA.RecognizeSetting_Rhombus.OpencvMode = opencvImgMode;
                Common.SaveIniParamVal("菱形图案识别设置", "OpencvMode", opencvImgMode.ToString());
            }

            MessageShowList.SendEventMsg("已切换OpenCV图像处理模式: " + _modename, 1);
        }

        /// <summary>
        /// 战地炮台左边 识别图像处理模式切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rg_BFS_L_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
                return;

            int opencvImgMode = 0;
            string _modename = rb.Text;

            if (rb_BFS_L_BGR.Checked)
            {
                opencvImgMode = 0;
            }
            else if (rb_BFS_L_Grey.Checked)
            {
                opencvImgMode = 1;
            }
            else if (rb_BFS_L_HSV.Checked)
            {
                opencvImgMode = 2;
            }

            if (tFD_LUNA != null)
            {
                tFD_LUNA.RS_BFS_L.OpencvMode = opencvImgMode;
                Common.SaveIniParamVal("战地演唱会识别设置_左", "OpencvMode", opencvImgMode.ToString());
            }

            MessageShowList.SendEventMsg("已切换战地炮台左边图像处理模式: " + _modename, 1);
        }
        /// <summary>
        /// 战地炮台右边 识别图像处理模式切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rg_BFS_R_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
                return;

            int opencvImgMode = 0;
            string _modename = rb.Text;

            if (rb_BFS_R_BGR.Checked)
            {
                opencvImgMode = 0;
            }
            else if (rb_BFS_R_GREY.Checked)
            {
                opencvImgMode = 1;
            }
            else if (rb_BFS_R_HSV.Checked)
            {
                opencvImgMode = 2;
            }

            if (tFD_LUNA != null)
            {
                tFD_LUNA.RS_BFS_R.OpencvMode = opencvImgMode;
                Common.SaveIniParamVal("战地演唱会识别设置_右", "OpencvMode", opencvImgMode.ToString());
            }

            MessageShowList.SendEventMsg("已切换战地炮台右边图像处理模式: " + _modename, 1);
        }

        /// <summary>
        /// 灵感条图像处理模式切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rg_MB_Changed(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!rb.Checked)
                return;
            int opencvImgMode = 0;
            string _modename = rb.Text;

            if (rg_MB_bgr.Checked)
            {
                opencvImgMode = 0;
            }
            else if (rg_MB_Grey.Checked)
            {
                opencvImgMode = 1;
            }
            else if (rg_MB_HSV.Checked)
            {
                opencvImgMode = 2;
            }

            if (tFD_LUNA != null)
            {
                tFD_LUNA.RecognizeSetting_MuseBar.OpencvMode = opencvImgMode;
                Common.SaveIniParamVal("灵感条图案识别设置", "OpencvMode", opencvImgMode.ToString());
            }
            MessageShowList.SendEventMsg("已切换灵感条图像处理模式: " + _modename, 1);
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

        #region 是否锐化截图切换
        private void ckb_Sharpen_BFS_L_CheckedChanged(object sender, EventArgs e)
        {
            int _Sharpen = ckb_Sharpen_BFS_L.Checked ? 1 : 0;
            Common.SaveIniParamVal("战地演唱会识别设置_左", "Sharpen", _Sharpen.ToString());
        }

        private void ckb_Sharpen_BFS_R_CheckedChanged(object sender, EventArgs e)
        {
            int _Sharpen = ckb_Sharpen_BFS_R.Checked ? 1 : 0;
            Common.SaveIniParamVal("战地演唱会识别设置_右", "Sharpen", _Sharpen.ToString());
        }

        private void ckb_Sharpen_R_CheckedChanged(object sender, EventArgs e)
        {
            int _Sharpen = ckb_Sharpen_R.Checked ? 1 : 0;
            Common.SaveIniParamVal("菱形图案识别设置", "Sharpen", _Sharpen.ToString());
        }

        private void ckb_Sharpen_MB_CheckedChanged(object sender, EventArgs e)
        {
            int _Sharpen = ckb_Sharpen_MB.Checked ? 1 : 0;
            Common.SaveIniParamVal("灵感条图案识别设置", "Sharpen", _Sharpen.ToString());
        }
        #endregion

        #region 战地炮台设置相关
        private void tb_BFS_R_Interval_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_BFS_R_Interval.Text, out int interval) && interval > 0)
            {
                tFD_LUNA.BFS_R_Interval = interval;
                Common.SaveIniParamVal("战地演唱会设置", "BFS_R_Interval", interval.ToString());
                MessageShowList.SendEventMsg("已设置战地演唱会截图间隔: " + interval + "毫秒", 1);
            }
            else
            {
                MessageShowList.SendEventMsg("无效的时间: " + tb_BFS_R_Interval.Text, 2);
            }
        }

        private void tb_BFS_CVZ_CX_time_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tb_BFS_CVZ_CX_time.Text, out int interval) && interval > 0)
            {
                tFD_LUNA.CVZ_CX_time = interval;
                Common.SaveIniParamVal("战地演唱会设置", "CVZ_CX_time", interval.ToString());
                MessageShowList.SendEventMsg("已设置普通技能持续时间: " + interval + "毫秒", 1);
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
                MessageShowList.SendEventMsg("已设置强化技能持续时间: " + interval + "毫秒", 1);
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
                else if (rb_BFS_APF_Enable_SP.Checked)
                {
                    _apf = 2;
                    _modename = "启用[CVZ一起按]";
                }

                if (tFD_LUNA != null)
                {
                    tFD_LUNA.AutoPowerfulSkill = _apf;
                    Common.SaveIniParamVal("战地演唱会设置", "AutoPowerfulSkill", _apf.ToString());
                }

                MessageShowList.SendEventMsg("已切换自动使用强化技能: " + _modename, 1);
            }
            catch { }
        }
        #endregion


    }
}
