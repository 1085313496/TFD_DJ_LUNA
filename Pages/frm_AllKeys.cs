using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    public partial class frm_Allkeys : Form
    {
        /// <summary>
        /// 选中的按键
        /// </summary>
        public string SelectedKey { get; set; }

        public frm_Allkeys()
        {
            InitializeComponent();
        }

        private void frm_Allkeys_Load(object sender, EventArgs e)
        {
            ShowAllkeys();
        }

        public void ShowAllkeys()
        {
            flp.Controls.Clear();
            flp.SuspendLayout();
            foreach (Dictionary<string, object> dic in GlobalParams.lsVKeys)
            {
                try
                {
                    string keyName = dic["VKey"].ToString();
                    string keyDescription = dic["Description"].ToString();
                    bool _enable = Convert.ToBoolean(dic["Enable"]);

                    if (!_enable)
                        continue;

                    DicItem di = new DicItem();
                    di.KeyContent = keyName;
                    di.KeyDescription = keyDescription;
                    toolTip1.SetToolTip(di, string.Format("按键代码：{0}，说明：{1}", keyName, keyDescription));
                    di.Size = new Size(120, 46);
                    di.DescriptionColor = Color.DimGray;
                    di.DicItemClick += Di_DicItemClick;

                    flp.Controls.Add(di);
                }
                catch { }
            }
            flp.ResumeLayout();

        }

        private void Di_DicItemClick(object sender, EventArgs e)
        {
            DicItem di = sender as DicItem;
            SelectedKey = di.KeyContent;
            this.DialogResult = DialogResult.OK;
        }
    }
}
