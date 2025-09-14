using System;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    public partial class frm_log : Form
    {
        public frm_log()
        {
            InitializeComponent();
            SetWindowStyle();
        }

        private void frm_log_Load(object sender, EventArgs e)
        {
            MessageShowList.SendNotice += MessageShowList_SendNotice;

            MouseTransClick mtc = new MouseTransClick(this.Handle);
            mtc.SetPenetrate();
        }

        private void SetWindowStyle()
        {
            this.TopMost = true;
            this.TopLevel = true;
        }

        private void MessageShowList_SendNotice(string msg, int noticeLevel)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (!this.IsHandleCreated)
                        return;
                    if (!this.rtb.IsHandleCreated)
                        return;

                    if (rtb.Text.Length > rtb.MaxLength * 0.75)
                    {
                        rtb.Clear();
                    }

                    rtb.AppendText(msg);
                    rtb.AppendText(Environment.NewLine);
                    rtb.ScrollToCaret();
                }));
            }
            catch { }
        }
    }
}
