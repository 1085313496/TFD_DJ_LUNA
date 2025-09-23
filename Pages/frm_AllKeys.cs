using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA
{
    public partial class frm_Allkeys : Form
    {
        /// <summary>
        /// 选中的按键
        /// </summary>
        public string SelectedKey { get; set; }
        /// <summary>
        /// 可选择的按键类型 1鼠标 2字母及数字 3常用键盘按键 4F1~F24
        /// </summary>
        public int[] EnableKeyType { get; set; } = new int[] { 1, 2, 3, 4 };

        public frm_Allkeys() { InitializeComponent(); }
        public frm_Allkeys(string _k) { InitializeComponent(); SelectedKey = _k; }

        private void frm_Allkeys_Load(object sender, EventArgs e)
        {
            ShowAllkeys();
        }

        public void ShowAllkeys(bool ShowAll = false)
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
                    int Ktype = Convert.ToInt32(Common.GetObj(dic["KType"], true));

                    if (!_enable)
                        continue;

                    if (!ShowAll && !EnableKeyType.Contains(Ktype))
                        continue;

                    DicItem di = new DicItem();
                    di.KeyContent = keyName;
                    di.KeyDescription = keyDescription;
                    toolTip1.SetToolTip(di, string.Format("按键代码：{0}，说明：{1}", keyName, keyDescription));
                    di.Size = new Size(120, 46);
                    di.DescriptionColor = Color.DimGray;
                    di.DicItemClick += Di_DicItemClick;

                    if (SelectedKey == keyName)
                    {
                        di.ContentColor = Color.OrangeRed;
                        di.DescriptionColor = Color.OrangeRed;
                    }

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
