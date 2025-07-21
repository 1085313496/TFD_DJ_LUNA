using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TFD_DJ_LUNA
{
    public partial class Form1 : Form
    {

        TFD_LUNA tFD_LUNA = new TFD_LUNA();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Disposed += Form1_Disposed;

            MessageShowList.SendNotice += MessageShowList_SendNotice;
            tkb.Value = 57;

            SetScanner();
        }

        /// <summary>
        /// 显示日志窗口
        /// </summary>
        private void ShowLogFrm()
        {
            try
            {
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

                if ("F5" == kc.ToUpper())
                {
                    bool HasPlayedNoticeSound = false;//是否已播放提示音，播放一次后就置为true，防止多个脚本共用一个快捷键时重复播放

                    if (!HasPlayedNoticeSound)
                        SystemSounds.Beep.Play();
                    HasPlayedNoticeSound = true;

                    if (!tFD_LUNA.IsRunning)
                    {
                        ShowLogFrm();

                        double thd = tkb.Value / 100.0;
                        if (tFD_LUNA != null)
                        {
                            tFD_LUNA.Threshold_Rhombus = thd;
                            tFD_LUNA.SwitchType_Noise = rg_SWT_1.Checked ? 1 : 0;

                            MessageShowList.SendEventMsg(string.Format("菱形图案检测阈值已设置为: {0:0.00}", thd), 1);
                            MessageShowList.SendEventMsg(string.Format("噪音涌动输出流按键方式已设置为: {0}", rg_SWT_1.Checked ? "依序交替按下CVZ" : "严格按照映射"), 1);
                        }

                        tFD_LUNA.Start();
                        MessageShowList.SendEventMsg("打碟助手已启动", 1);
                    }
                    else
                    {
                        tFD_LUNA.Stop();
                        MessageShowList.SendEventMsg("打碟助手已停止", 1);
                        CloseLogFrm();
                    }
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
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            double thd = tkb.Value / 100.0;
            tFD_LUNA.Noise();
        }

        private void tkb_ValueChanged(object sender, EventArgs e)
        {
            double thd = tkb.Value / 100.0;
            if (tFD_LUNA != null)
                tFD_LUNA.Threshold_Rhombus = thd;
            lb_threshold.Text = string.Format("{0:0.00}", thd);
            MessageShowList.SendEventMsg(string.Format("菱形图案检测阈值已设置为: {0:0.00}", thd), 1);
        }

        private void rg_SWT_CheckedChanged(object sender, EventArgs e)
        {
            if (tFD_LUNA != null)
                tFD_LUNA.SwitchType_Noise = rg_SWT_1.Checked ? 1 : 0;
            MessageShowList.SendEventMsg(string.Format("噪音涌动输出流按键方式已设置为: {0}", rg_SWT_1.Checked ? "依序交替按下CVZ" : "严格按照映射"), 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_PickArea f = new frm_PickArea();
            f.TopLevel = true;
            f.Opacity = 0.35;
            f.Show();
        }
    }
}
