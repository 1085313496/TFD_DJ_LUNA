using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    public partial class frm_PickArea : Form
    {
        // 添加DPI感知API
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        public enum ProcessDPIAwareness
        {
            DPI_Unaware = 0,
            System_DPI_Aware = 1,
            Per_Monitor_DPI_Aware = 2
        }

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
            set
            {
                ptLocation = new Point(value.X, value.Y);
                ptEnd = new Point(value.X + value.Width, value.Y + value.Height);
            }
        }

        public frm_PickArea()
        {
            InitializeComponent();

            // 启用DPI感知
            EnableDPIAwareness();

            this.DialogResult = DialogResult.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            SetDefaultValues();
        }

        /// <summary>
        /// 启用DPI感知
        /// </summary>
        private void EnableDPIAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version >= new Version(6, 3))
                {
                    // Windows 8.1及以上
                    SetProcessDpiAwareness(ProcessDPIAwareness.Per_Monitor_DPI_Aware);
                }
                else
                {
                    // Windows Vista及以上
                    SetProcessDPIAware();
                }
            }
            catch
            {
                // 如果API调用失败，忽略错误
            }
        }

        /// <summary>
        /// 获取修正后的鼠标位置（处理DPI缩放）
        /// </summary>
        private Point GetCorrectMousePosition(Point rawPosition)
        {
            // 通过屏幕坐标转换确保DPI缩放下的准确性
            Point screenPoint = this.PointToScreen(rawPosition);
            Point correctedPoint = this.PointToClient(screenPoint);
            return correctedPoint;
        }

        /// <summary>
        /// 获取当前DPI缩放比例
        /// </summary>
        private float GetDpiScale()
        {
            using (Graphics g = this.CreateGraphics())
            {
                return g.DpiX / 96f;
            }
        }

        private void frm_PickArea_Load(object sender, EventArgs e)
        {

        }

        private void SetDefaultValues()
        {
            LineColor = Color.Red;
            TextColor = Color.OrangeRed;
            LineWidth = 2;
            TextFont = new Font("微软雅黑", 18, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        private void frm_PickArea_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PaintBody(e.Graphics);
            }
            catch { }
        }

        private void PaintBody(Graphics g)
        {
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
                string text = string.Format("【x={0},y={1},Weight={2},Height={3}】\r\n按住左键拖到框选识别区，此时按住空格可移动选区，右键结束框选并退出"
                    , ptLocation.X, ptLocation.Y, ptEnd.X - ptLocation.X, ptEnd.Y - ptLocation.Y);
                SizeF size = g.MeasureString(text, TextFont);
                g.DrawString(text, TextFont, sb, 100, 100);
            }

            //GC.Collect();
        }

        /// <summary>
        /// 右键退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_PickArea_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        #region 按住左键拖动框选
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
                // 使用修正后的鼠标位置
                ptLocation = GetCorrectMousePosition(e.Location);
                ptEnd = ptLocation;
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
                // 使用修正后的鼠标位置
                Point correctedPosition = GetCorrectMousePosition(e.Location);

                if (!isSpaceDown)
                {
                    ptEnd = correctedPosition;
                }
                else
                {
                    int x_ex = correctedPosition.X - ptSpaceB.X;
                    int y_ex = correctedPosition.Y - ptSpaceB.Y;

                    ptLocation = new Point(ptLT.X + x_ex, ptLT.Y + y_ex);
                    ptEnd = new Point(ptRB.X + x_ex, ptRB.Y + y_ex);
                }
                this.Invalidate();
            }
        }
        #endregion

        #region 空格移动框选区
        /// <summary>
        /// 是否按住空格键
        /// </summary>
        private bool isSpaceDown = false;
        /// <summary>
        /// 空格键按下时的鼠标位置
        /// </summary>
        private Point ptSpaceB = new Point(0, 0);
        /// <summary>
        /// 用于记录框选区域左上角位置
        /// </summary>
        private Point ptLT = new Point(0, 0);
        /// <summary>
        /// 用于记录框选区域右下角位置
        /// </summary>
        private Point ptRB = new Point(0, 0);

        private void frm_PickArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!isSpaceDown && isMouseDown)
                {
                    isSpaceDown = true;
                    // 使用修正后的鼠标位置
                    Point currentPos = GetCorrectMousePosition(this.PointToClient(Cursor.Position));
                    ptSpaceB = currentPos;

                    ptLT = ptLocation;
                    ptRB = ptEnd;

                    this.Cursor = Cursors.SizeAll;
                }
            }
        }

        private void frm_PickArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isSpaceDown = false;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        private void frm_PickArea_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
