using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    public partial class frm_PickArea : Form
    {
        /// <summary>
        /// 框选线的颜色
        /// </summary>
        public Color LineColor { get; set; } = Color.Red;
        /// <summary>
        /// 提示文字的颜色
        /// </summary>
        public Color TextColor { get; set; } = Color.OrangeRed;
        /// <summary>
        /// 框选线的宽度
        /// </summary>
        public int LineWidth { get; set; } = 2;
        /// <summary>
        /// 提示文字的字体
        /// </summary>
        public Font TextFont { get; set; } = new Font("微软雅黑", 18, FontStyle.Bold, GraphicsUnit.Pixel);
        /// <summary>
        /// 绘图缓存
        /// </summary>
        private BufferedGraphics bfg;

        /// <summary>
        /// 获取框选区域
        /// </summary>
        public Rectangle PickedArea
        {
            get
            {
                return new Rectangle(ptLocation.X, ptLocation.Y, ptEnd.X - ptLocation.X, ptEnd.Y - ptLocation.Y);
            }
        }

        public frm_PickArea()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            bfg = new BufferedGraphicsContext().Allocate(this.CreateGraphics(), this.DisplayRectangle);
        }

        private void frm_PickArea_Load(object sender, EventArgs e)
        {

        }

        private void frm_PickArea_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PaintBody();
            }
            catch { }
        }
               


        private void PaintBody()
        {
            Bitmap btm = new Bitmap(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
            Graphics g = Graphics.FromImage((Image)btm);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Point ptLeftTop = ptLocation;
            Point ptRightTop = new Point(ptEnd.X, ptLocation.Y);
            Point ptLeftBottom = new Point(ptLocation.X, ptEnd.Y);
            Point ptRightBottom = ptEnd;

            using (Pen pen = new Pen(Color.OrangeRed, 1))
            {
                g.DrawLine(pen, new Point(0, this.Height / 2), new Point(this.Width, this.Height / 2));
                g.DrawLine(pen, new Point(this.Width / 2, 0), new Point(this.Width / 2, this.Height));
            }

            using (Pen pen = new Pen(LineColor, LineWidth))
            {
                g.DrawLine(pen, ptLeftTop, ptRightTop);
                g.DrawLine(pen, ptRightTop, ptRightBottom);
                g.DrawLine(pen, ptRightBottom, ptLeftBottom);
                g.DrawLine(pen, ptLeftBottom, ptLeftTop);
            }

            using (SolidBrush sb = new SolidBrush(TextColor))
            {
                string text = string.Format("【x={0},y={1},Weight={2},Height={3}】\r\n按住左键框选识别区，右键结束框选并退出", ptLocation.X, ptLocation.Y, ptEnd.X - ptLocation.X, ptEnd.Y - ptLocation.Y);
                SizeF size = g.MeasureString(text, TextFont);
                g.DrawString(text, TextFont, sb, 100, 100);
            }

            Graphics displayG = this.bfg.Graphics;
            displayG.Clear(this.BackColor);
            displayG.DrawImage(btm, 0, 0);
            bfg.Render();

            //GC.Collect();
        }

        private void frm_PickArea_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        /// <summary>
        /// 鼠标按下状态
        /// </summary>
        bool isMouseDown = false;
        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        Point ptLocation = new Point(0, 0);
        /// <summary>
        /// 鼠标拖动结束位置
        /// </summary>
        Point ptEnd = new Point(0, 0);
        private void frm_PickArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (!isMouseDown)
            {
                isMouseDown = true;
                ptLocation = e.Location;
                ptEnd = e.Location;
                this.Invalidate();
            }
        }

        private void frm_PickArea_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            this.Invalidate();
        }

        private void frm_PickArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                ptEnd = e.Location;
                this.Invalidate();
            }
        }

        private void frm_PickArea_Resize(object sender, EventArgs e)
        {
            bfg = new BufferedGraphicsContext().Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.Invalidate();
        }
    }
}
