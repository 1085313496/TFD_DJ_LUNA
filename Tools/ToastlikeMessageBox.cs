using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    /// <summary>
    /// 仿Toast样式消息提示窗体
    /// </summary>
    public partial class ToastlikeMessageBox : Form
    {
        #region  参数
        /// <summary>
        /// 消息内容
        /// </summary>
        private string _messageContent = "";
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent
        {
            get { return _messageContent; }
            set { _messageContent = value; this.Invalidate(); }
        }

        /// <summary>
        /// 到时是否自动关闭窗体
        /// </summary>
        private bool _AutoClose = true;
        /// <summary>
        /// 到时是否自动关闭窗体
        /// </summary>
        public bool AutoClose
        {
            set { _AutoClose = value; }
            get { return _AutoClose; }
        }
        /// <summary>
        /// 窗体显示时间，到时自动关闭
        /// </summary>
        public int ShowInterval
        {
            get { return timer_AutoHide.Interval; }
            set { timer_AutoHide.Interval = value; }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _borderColor = Color.FromArgb(194, 196, 203);
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor
        {
            set { _borderColor = value; }
            get { return _borderColor; }
        }

        /// <summary>
        /// 边框粗细度
        /// </summary>
        int _BorderWidth = 1;
        /// <summary>
        /// 边框粗细度
        /// </summary>
        public int BorderWidth { get { return _BorderWidth; } set { _BorderWidth = value; } }
        /// <summary>
        /// 圆角度数
        /// </summary>
        int _BorderRadius = 2;
        /// <summary>
        /// 圆角半径
        /// </summary>
        public int BorderRadius { get { return _BorderRadius; } set { _BorderRadius = value; } }

        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color _backColorCustom = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackColorCustom
        {
            set { _backColorCustom = value; }
            get { return _backColorCustom; }
        }


        /// <summary>
        /// 左上起始点坐标
        /// </summary>
        Point _OriginPoint = new Point(0, 0);
        /// <summary>
        /// 左上起始点坐标
        /// </summary>
        public Point OriginPoint { get { return _OriginPoint; } set { _OriginPoint = value; } }

        /// <summary>
        /// 文字区域外间距
        /// </summary>
        public int textPadding = 5;

        /// <summary>
        /// 是否启用鼠标穿透
        /// </summary>
        public bool EnableMouseTransClick = true;
        /// <summary>
        /// 是否绘制边框阴影
        /// </summary>
        public bool ShowBorderShadow = false;

        /// <summary>
        /// X坐标调整值
        /// </summary>
        public int AdjustX = 1;
        /// <summary>
        /// Y坐标调整值
        /// </summary>
        public int AdjustY = 1;
        /// <summary>
        /// 半径调整值
        /// </summary>
        public int AdjustR = 1;
        #endregion

        public ToastlikeMessageBox() { InitializeComponent(); }
        public ToastlikeMessageBox(string msgstr) { InitializeComponent(); _messageContent = msgstr; }
        private void ToastlikeMessageBox_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            if (EnableMouseTransClick)
            {
                MouseTransClick mtc = new MouseTransClick(this.Handle);
                mtc.SetPenetrate();
            }

            if (ShowBorderShadow)
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //API函数加载，实现窗体边框阴影效果
            }

            if (_AutoClose && !timer_AutoHide.Enabled)
                timer_AutoHide.Start();
        }

        /// <summary>
        /// 将消息内容绘制出来,并调整窗体大小和位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToastlikeMessageBox_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                #region 边框
                SolidBrush sb_Border = new SolidBrush(_borderColor);
                SolidBrush sb_BorderPie = new SolidBrush(_borderColor);

                g.FillRectangle(sb_Border, OriginPoint.X + BorderRadius - AdjustX, OriginPoint.Y - AdjustY, this.Width - BorderRadius * 2 + AdjustX, this.Height + AdjustY);
                g.FillRectangle(sb_Border, OriginPoint.X - AdjustX, OriginPoint.Y + BorderRadius - AdjustY, this.Width + AdjustX, this.Height - BorderRadius * 2 + AdjustY);

                g.FillPie(sb_BorderPie, OriginPoint.X, OriginPoint.Y
                    , BorderRadius * 2, BorderRadius * 2
                    , -180, 90);
                g.FillPie(sb_BorderPie, this.Width - BorderRadius * 2 - 1, OriginPoint.Y
                    , BorderRadius * 2, BorderRadius * 2 + 1
                    , 0, -90);
                g.FillPie(sb_BorderPie, this.Width - BorderRadius * 2 - 1, this.Height - BorderRadius * 2 - 1
                    , BorderRadius * 2, BorderRadius * 2
                    , 0, 90);
                g.FillPie(sb_BorderPie, OriginPoint.X, this.Height - BorderRadius * 2 - 1
                    , BorderRadius * 2 + 2, BorderRadius * 2
                    , -180, -90);
                #endregion

                #region 背景填充
                SolidBrush sb_Main = new SolidBrush(_backColorCustom);
                SolidBrush sb_MainPie = new SolidBrush(_backColorCustom);

                g.FillRectangle(sb_Main, OriginPoint.X + BorderRadius + BorderWidth - AdjustX, OriginPoint.Y + BorderWidth - AdjustY
                    , this.Width - BorderRadius * 2 - BorderWidth * 2 + AdjustX, this.Height - BorderWidth * 2 + AdjustY);
                g.FillRectangle(sb_Main, OriginPoint.X + BorderWidth - AdjustX, OriginPoint.Y + BorderRadius + BorderWidth - AdjustX
                    , this.Width - BorderWidth * 2 + AdjustX, this.Height - BorderRadius * 2 - BorderWidth * 2 + AdjustX);

                //左上扇形
                g.FillPie(sb_MainPie
                    , OriginPoint.X + BorderWidth - AdjustX, OriginPoint.Y + BorderWidth - AdjustY
                    , BorderRadius * 2 + AdjustR, BorderRadius * 2 + AdjustR
                    , -180, 90);
                //右上
                g.FillPie(sb_MainPie
                    , this.Width - BorderRadius * 2 - 1 - BorderWidth - AdjustX, OriginPoint.Y + BorderWidth - 1
                    , BorderRadius * 2 + AdjustR * 2, BorderRadius * 2 + AdjustR * 3
                    , 0, -90);

                //右下
                g.FillPie(sb_MainPie
                    , this.Width - BorderRadius * 2 - 1 - BorderWidth, this.Height - BorderRadius * 2 - 1 - BorderWidth
                    , BorderRadius * 2 + AdjustR, BorderRadius * 2 + AdjustR
                    , 0, 90);

                //左下
                g.FillPie(sb_MainPie
                    , OriginPoint.X + BorderWidth - AdjustX, this.Height - BorderRadius * 2 - 1 - BorderWidth
                    , BorderRadius * 2 + AdjustR * 2, BorderRadius * 2 + AdjustR
                    , -180, -90);
                #endregion


                g.DrawString(_messageContent, this.Font, new SolidBrush(this.ForeColor), new Point(12, 12));

                Size preferredSize = g.MeasureString(_messageContent, this.Font).ToSize();

                this.ClientSize = new Size(
                   preferredSize.Width + (textPadding * 2) + 12,
                   preferredSize.Height + (textPadding * 2) + 12);

                int x = (Screen.PrimaryScreen.WorkingArea.Width - this.ClientSize.Width) / 2;
                int y = (Screen.PrimaryScreen.WorkingArea.Height - this.ClientSize.Height) / 2;
                this.Location = new Point(x, y);

                g.Dispose();
            }
        }

        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="msg">消息内容</param>
        public void ShowTips(string msg = "")
        {
            if (!string.IsNullOrWhiteSpace(msg))
                MessageContent = msg;
            else
                return;

            this.Show();
            this.BringToFront();
            timer_AutoHide.Start();
        }
        /// <summary>
        /// 到时自动关闭/隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_AutoHide_Tick(object sender, EventArgs e)
        {
            timer_AutoHide.Stop();
            this.Dispose();
        }

        #region 窗体边框阴影效果变量申明

        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        #endregion
    }

    /// <summary>
    /// 仿Toast样式消息提示框
    /// </summary>
    public static class ToastMessageBox
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void QuicklyShow(string msg)
        {
            ToastlikeMessageBox tmb = new ToastlikeMessageBox(msg);
            tmb.Show();
        }
        /// <summary>
        /// 显示toast式弹窗
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="StyleType">0 红底白字，类似警告信息;1蓝底白字，一般信息;其他 黑底白字</param>
        public static void Show(string msg, int StyleType = 1)
        {
            switch (StyleType)
            {
                case 0:
                    ToastMessageBox.Show(
                        msg: msg
                        , _fontColor: Color.FromArgb(255, 255, 255, 255)
                        , _font: new System.Drawing.Font("微软雅黑", 14, FontStyle.Regular, GraphicsUnit.Pixel)
                        , _BackColor: Color.FromArgb(255, 192, 192, 192)
                        , _TransparencyKey: Color.FromArgb(255, 192, 192, 192)
                        , _Opacity: 0.85
                        , _BorderColor: Color.FromArgb(255, 255, 255, 255)
                        , _BackColorCustom: Color.FromArgb(255, 255, 0, 0)
                        , _EnableMouseTransClick: true
                        , _ShowBorderShadow: false
                        , _BorderRadius: 3
                        , _BorderWidth: 1
                        , _showInterval: 2000
                        , _autoClose: true);
                    break;
                case 1:
                    ToastMessageBox.Show(
                       msg: msg
                       , _fontColor: Color.FromArgb(255, 255, 255, 255)
                       , _font: new System.Drawing.Font("微软雅黑", 14, FontStyle.Regular, GraphicsUnit.Pixel)
                       , _BackColor: Color.FromArgb(255, 112, 112, 112)
                       , _TransparencyKey: Color.FromArgb(255, 112, 112, 112)
                       , _Opacity: 0.85
                       , _BorderColor: Color.FromArgb(255, 57, 120, 172)
                       , _BackColorCustom: Color.FromArgb(255, 89, 154, 218)
                       , _EnableMouseTransClick: true
                       , _ShowBorderShadow: false
                       , _BorderRadius: 4
                       , _BorderWidth: 0
                       , _showInterval: 2000
                       , _autoClose: true);
                    break;
                default:
                    ToastMessageBox.Show(
                         msg: msg
                         , _fontColor: Color.FromArgb(255, 255, 255, 255)
                         , _font: new System.Drawing.Font("微软雅黑", 14, FontStyle.Regular, GraphicsUnit.Pixel)
                        , _BackColor: Color.FromArgb(255, 192, 192, 192)
                        , _TransparencyKey: Color.FromArgb(255, 192, 192, 192)
                        , _Opacity: 0.75
                        , _BorderColor: Color.FromArgb(255, 54, 54, 54)
                        , _BackColorCustom: Color.FromArgb(255, 64, 64, 64)
                        , _EnableMouseTransClick: true
                        , _ShowBorderShadow: false
                        , _BorderRadius: 3
                        , _BorderWidth: 1
                        , _showInterval: 2000
                        , _autoClose: true);
                    break;
            }
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="backColor">背景颜色</param>
        /// <param name="fontColor">字体颜色</param>
        public static void Show(string msg, Color backColor, Color fontColor)
        {
            ToastlikeMessageBox tmb = new ToastlikeMessageBox(msg);
            tmb.BackColorCustom = backColor;
            tmb.ForeColor = fontColor;
            tmb.Show();
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="backColor">背景颜色</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="font">字体</param>
        /// <param name="showInterval">消息显示时间 单位ms</param>
        public static void Show(string msg, Color backColor, Color fontColor, Font font, int showInterval)
        {
            ToastlikeMessageBox tmb = new ToastlikeMessageBox(msg);
            tmb.BackColorCustom = backColor;
            tmb.ForeColor = fontColor;
            tmb.Font = font;
            tmb.ShowInterval = showInterval <= 0 ? 2000 : showInterval;
            tmb.Show();
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="_fontColor">字体颜色</param>
        /// <param name="_font">字体</param>
        /// <param name="_BackColor">窗体背景颜色</param>
        /// <param name="_TransparencyKey">窗体透明颜色</param>
        /// <param name="_Opacity">窗体透明度</param>
        /// <param name="_BorderColor">自绘边框颜色</param>
        /// <param name="_BackColorCustom">自绘背景色</param>
        /// <param name="_EnableMouseTransClick">是否启用鼠标穿透 【若启用，鼠标在上窗体点击，将会透过此窗体，作用到窗体下面覆盖的窗体】</param>
        /// <param name="_ShowBorderShadow">是否绘制边框阴影</param>
        /// <param name="_BorderRadius">四角圆角半径</param>
        /// <param name="_BorderWidth">边框粗细度</param>
        /// <param name="_showInterval">窗体显示时间</param>
        /// <param name="_autoClose">窗体显示时间到时，是否自动销毁窗体</param>
        public static void Show(string msg, Color _fontColor, Font _font
            , Color _BackColor, Color _TransparencyKey, double _Opacity, Color _BorderColor, Color _BackColorCustom
            , bool _EnableMouseTransClick, bool _ShowBorderShadow, int _BorderRadius, int _BorderWidth, int _showInterval, bool _autoClose = true)
        {
            ToastlikeMessageBox tmb = new ToastlikeMessageBox(msg);

            tmb.ForeColor = _fontColor;
            tmb.Font = _font;
            tmb.BackColor = _BackColor;
            if (_TransparencyKey != Color.Transparent)
                tmb.TransparencyKey = _TransparencyKey;

            tmb.Opacity = _Opacity >= 1 ? 0.99 : _Opacity;
            tmb.BorderColor = _BorderColor;
            tmb.BackColorCustom = _BackColorCustom;
            tmb.EnableMouseTransClick = _EnableMouseTransClick;
            tmb.ShowBorderShadow = _ShowBorderShadow;
            tmb.BorderRadius = _BorderRadius;
            tmb.BorderWidth = _BorderWidth;
            tmb.ShowInterval = _showInterval <= 0 ? 2000 : _showInterval;
            tmb.AutoClose = _autoClose;

            tmb.Show();
        }


    }
}
