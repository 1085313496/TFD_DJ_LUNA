using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    public partial class SwitchBar : UserControl
    {
        public delegate void ClickedBarDelegate(object sender, EventArgs e);
        /// <summary>
        /// 状态切换事件
        /// </summary>
        public event ClickedBarDelegate StateChanged;

        /// <summary>
        /// 状态文字位置
        /// </summary>
        public enum StateTextPositon
        {
            /// <summary>
            /// 不显示状态文字
            /// </summary>
            None = 0,
            /// <summary>
            /// 文字位于左边
            /// </summary>
            Left = 1,
            /// <summary>
            /// 文字位于右边
            /// </summary>
            Right = 2
        }

        #region 属性
        /// <summary>
        /// 圆角半径
        /// </summary>
        private int _borderRadius = 0;
        /// <summary>
        /// 圆角半径
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("圆角半径")]
        public int swb_BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value; this.Invalidate(); }
        }

        /// <summary>
        /// 边框粗细
        /// </summary>
        private int _borderThickness = 1;
        /// <summary>
        /// 边框粗细
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("边框粗细")]
        public int swb_BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; this.Invalidate(); }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _borderCorlor = Color.FromArgb(185, 186, 195);
        /// <summary>
        /// 边框颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("边框颜色")]
        public Color swb_BorderCorlor
        {
            get { return _borderCorlor; }
            set { _borderCorlor = value; this.Invalidate(); }
        }

        /// <summary>
        /// 开关状态
        /// </summary>
        private bool _state = false;
        /// <summary>
        /// 开关状态
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("开关状态")]
        public bool SwitchState
        {
            get { return _state; }
            set { _state = value; this.Invalidate(); }
        }

        /// <summary>
        /// 打开状态文本
        /// </summary>
        private string _onText = "on";
        /// <summary>
        /// 打开状态文本
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("打开状态文本")]
        public string swb_OnText
        {
            get { return _onText; }
            set { _onText = value; this.Invalidate(); }
        }

        /// <summary>
        /// 关闭状态文本
        /// </summary>
        private string _offText = "off";
        /// <summary>
        /// 关闭状态文本
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("关闭状态文本")]
        public string swb_OffText
        {
            get { return _offText; }
            set { _offText = value; this.Invalidate(); }
        }

        /// <summary>
        /// 不同状态滑块间的间隔距离
        /// </summary>
        private float _barSpacing = 2;
        /// <summary>
        /// 不同状态滑块间的间隔距离
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("不同状态滑块间的间隔距离")]
        public float swb_BarSpacing
        {
            get { return _barSpacing; }
            set { _barSpacing = value; this.Invalidate(); }
        }

        /// <summary>
        /// 文本与开关的距离
        /// </summary>
        private float _textDistance = 5;
        /// <summary>
        /// 文本与开关的距离
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("文本与开关的距离")]
        public float swb_TextDistance
        {
            get { return _textDistance; }
            set { _textDistance = value; this.Invalidate(); }
        }

        /// <summary>
        /// 状态文字显示位置
        /// </summary>
        private StateTextPositon _textPosition = StateTextPositon.Right;
        /// <summary>
        /// 状态文字显示位置
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("状态文字显示位置")]
        public StateTextPositon swb_TextPosition
        {
            get { return _textPosition; }
            set { _textPosition = value; this.Invalidate(); }
        }

        /// <summary>
        /// 滑块颜色
        /// </summary>
        private Color _barColorTop = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 滑块颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("滑块颜色")]
        public Color swb_BarColorTop
        {
            get { return _barColorTop; }
            set { _barColorTop = value; this.Invalidate(); }
        }

        /// <summary>
        /// 滑块下部颜色
        /// </summary>
        private Color _barColorBottom = Color.FromArgb(221, 222, 227);
        /// <summary>
        /// 滑块下部颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("滑块下部颜色")]
        public Color swb_BarColorBottom
        {
            get { return _barColorBottom; }
            set { _barColorBottom = value; this.Invalidate(); }
        }

        /// <summary>
        /// 关闭状态背景色
        /// </summary>
        private Color _backColorOff = Color.FromArgb(243, 244, 245);
        /// <summary>
        /// 关闭状态背景色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("关闭状态背景色")]
        public Color swb_BackColorOff
        {
            get { return _backColorOff; }
            set { _backColorOff = value; this.Invalidate(); }
        }

        /// <summary>
        /// 打开状态背景色
        /// </summary>
        private Color _backColorOn = Color.FromArgb(235, 220, 244);
        /// <summary>
        /// 打开状态背景色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("SwitchBar"), Description("打开状态背景色")]
        public Color swb_BackColorOn
        {
            get { return _backColorOn; }
            set { _backColorOn = value; this.Invalidate(); }
        }
        #endregion

        public SwitchBar()
        {
            InitializeComponent();
            SetWMStyles();
        }

        private void switcherBar_Load(object sender, EventArgs e) { }

        private void SetWMStyles()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void switcherBar_Paint(object sender, PaintEventArgs e) { PaintBody(e); }

        private void PaintBody(PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                #region 计算坐标与大小
                //计算文本所占的区域大小
                SizeF TextSize = g.MeasureString(_onText, this.Font).Width > g.MeasureString(_offText, this.Font).Width ? g.MeasureString(_onText, this.Font) : g.MeasureString(_offText, this.Font);
                float tds = _textPosition == StateTextPositon.None ? 0 : _textDistance;

                float textWidth = _textPosition == StateTextPositon.None ? 0 : TextSize.Width;
                float containerWidth = this.Width - this.Padding.Left - this.Padding.Right - 1 - textWidth - tds;
                float containerHeight = this.Height - this.Padding.Top - this.Padding.Bottom - 1;

                float barWidth = (containerWidth - _borderThickness * 2 - 1 * 2) / 2 - _barSpacing;
                float barHeight = containerHeight - _borderThickness * 2 - 1 * 2;

                float containerX = _textPosition == StateTextPositon.Left ? this.Padding.Left + textWidth + tds : this.Padding.Left;
                float containerY = this.Padding.Top;

                float barOffX = containerX + _borderThickness + 1;
                float barOffY = containerY + _borderThickness + 1;

                float tw = _textPosition == StateTextPositon.Left ? TextSize.Width + tds : 0;
                float spw = 0;
                switch (_textPosition)
                {
                    case StateTextPositon.Left: spw = 0; break;
                    case StateTextPositon.Right: spw = 0.5f; break;
                    case StateTextPositon.None: spw = 1; break;
                }
                float barOnX = containerWidth / 2f + _barSpacing + tw + spw;
                float barOnY = containerY + _borderThickness + 1;

                #endregion

                if (_textPosition == StateTextPositon.Right)
                {
                    // g.FillRectangle(new SolidBrush(Color.LawnGreen), (int)(containerWidth + tds), (int)((this.Height - this.Padding.Bottom - TextSize.Height) / 2), TextSize.Width, TextSize.Height);
                    g.DrawString(_state ? _onText : _offText, this.Font, new SolidBrush(this.ForeColor)
                        , new Point((int)(containerWidth + tds), (int)((this.Height - this.Padding.Bottom - TextSize.Height) / 2)));
                }
                else if (_textPosition == StateTextPositon.Left)
                {
                    // g.FillRectangle(new SolidBrush(Color.LawnGreen), this.Padding.Left, (int)((this.Height - this.Padding.Bottom - TextSize.Height) / 2), TextSize.Width, TextSize.Height);

                    g.DrawString(_state ? _onText : _offText, this.Font, new SolidBrush(this.ForeColor)
                       , new Point(this.Padding.Left, (int)((this.Height - this.Padding.Bottom - TextSize.Height) / 2)));
                }

                using (Pen p = new Pen(_borderCorlor, _borderThickness))
                {
                    Rectangle recO = new Rectangle((int)containerX, (int)containerY, (int)containerWidth, (int)containerHeight);

                    Color cl = _state ? _backColorOn : _backColorOff;
                    using (SolidBrush sb = new SolidBrush(cl))
                    {
                        g.FillPath(sb, GetRoundRectangle(recO, _borderRadius));
                    }
                    g.DrawPath(p, GetRoundRectangle(recO, _borderRadius));

                    using (LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(100, 100, 100, 100), _barColorTop, _barColorBottom, 90F))
                    {
                        Rectangle recI = _state ? new Rectangle((int)barOnX, (int)barOnY, (int)barWidth, (int)barHeight) : new Rectangle((int)barOffX, (int)barOffY, (int)barWidth, (int)barHeight);
                        int nr = _borderRadius <= 2 ? 0 : _borderRadius - 2;

                        g.FillPath(lgb, GetRoundRectangle(recI, nr));
                        g.DrawPath(p, GetRoundRectangle(recI, nr));
                    }
                }
            }
            catch (Exception ex) { }
            finally {}
        }


        /// <summary>  
        /// 根据普通矩形得到圆角矩形的路径  
        /// </summary>  
        /// <param name="rectangle">原始矩形</param>  
        /// <param name="r">半径</param>  
        /// <returns>图形路径</returns>  
        private GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = r == 0 ? 1 : 2 * r;
            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中  
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);

            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);

            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            return gp;
        }

        private void barClicked(object sender, EventArgs e)
        {
            _state = !_state;
            this.Invalidate();

            if (StateChanged != null)
                StateChanged(this, e);
        }

        private void SwitchBar_MouseUp(object sender, MouseEventArgs e) { barClicked(sender, e); }
    }
}
