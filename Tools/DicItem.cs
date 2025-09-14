using System;
using System.Drawing;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    public partial class DicItem : UserControl
    {
        /// <summary>
        /// 按键名
        /// </summary>
        public string KeyContent
        {
            get { return lb_content.Text; }
            set { lb_content.Text = value; }
        }
        /// <summary>
        /// 按键名颜色
        /// </summary>
        public Color ContentColor
        {
            get { return lb_content.ForeColor; }
            set { lb_content.ForeColor = value; }
        }
        /// <summary>
        /// 按键说明
        /// </summary>
        public string KeyDescription
        {
            get { return lb_description.Text; }
            set { lb_description.Text = value; }
        }
        /// <summary>
        /// 说明文本颜色
        /// </summary>
        public Color DescriptionColor
        {
            get { return lb_description.ForeColor; }
            set { lb_description.ForeColor = value; }
        }

        public delegate void DicItemClickHandler(object sender, EventArgs e);
        public event DicItemClickHandler DicItemClick;

        public DicItem()
        {
            InitializeComponent();
        }

        private void DicItem_Load(object sender, EventArgs e)
        {

        }

        private void lb_content_Click(object sender, EventArgs e)
        {
            if (DicItemClick != null)
                DicItemClick(this, e);
        }

        private void lb_description_Click(object sender, EventArgs e)
        {
            if (DicItemClick != null)
                DicItemClick(this, e);
        }
    }
}
