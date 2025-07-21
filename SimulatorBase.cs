using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    internal class SimulatorBase
    {
    }

    #region window消息及其他常量
    public class WindowsConstant
    {
        #region 窗口样式和消息常量  这些常量用于窗口管理和消息处理
        /// <summary>
        /// 用于获取或设置窗口的扩展样式
        /// </summary>
        public const int GWL_EXSTYLE = -20;
        /// <summary>
        /// 表示窗口被禁用（不可交互）
        /// </summary>
        public const int WS_DISABLED = 0X8000000;
        /// <summary>
        /// 表示窗口获得焦点的消息
        /// </summary>
        public const int WM_SETFOCUS = 0X0007;
        #endregion

        #region 鼠标事件  用于通知窗口鼠标按键被按下的事件,用于处理鼠标输入。例如在 WndProc 函数中处理鼠标事件。
        /// <summary>
        /// 鼠标移动
        /// </summary>
        public const int WM_MOUSEMOVE = 0x200;
        /// <summary>
        /// 左键按下
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x201;
        /// <summary>
        /// 左键释放
        /// </summary>
        public const int WM_LBUTTONUP = 0x202;
        /// <summary>
        /// 左键双击
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x203;
        /// <summary>
        /// 右键按下
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x204;
        /// <summary>
        /// 右键释放
        /// </summary>
        public const int WM_RBUTTONUP = 0x205;
        /// <summary>
        /// 右键双击
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x206;
        /// <summary>
        /// 中键按下
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x207;
        /// <summary>
        /// 中键释放
        /// </summary>
        public const int WM_MBUTTONUP = 0x208;
        /// <summary>
        /// 鼠标滚轮滚动
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x020A;
        #endregion

        #region 键盘事件 用于通知窗口键盘按键被按下的事件, 这些常量表示键盘按键的状态变化，例如普通键或系统键（如 Alt、Ctrl）的按下和释放
        /// <summary>
        /// 普通键按下
        /// </summary>
        public const int WM_KEYDOWN = 0x100;
        /// <summary>
        /// 普通键释放
        /// </summary>
        public const int WM_KEYUP = 0x101;
        /// <summary>
        /// 系统键按下（如 Alt 键）
        /// </summary>
        public const int WM_SYSKEYDOWN = 0x104;
        /// <summary>
        /// //系统键释放
        /// </summary>
        public const int WM_SYSKEYUP = 0x105;
        #endregion

        #region 模拟键盘事件常量  这些常量通常用于模拟键盘输入，例如按下或释放某个键
        /// <summary>
        /// 键盘按键被按下 
        /// </summary>
        public const int KEYEVENTF_KEYDOWN = 0x0000;
        /// <summary>
        /// 表示按键是扩展键（例如功能键 F1-F12、方向键等）
        /// </summary>
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        /// <summary>
        /// 表示键盘按键被释放的事件
        /// </summary>
        public const int KEYEVENTF_KEYUP = 0x0002;
        #endregion

        #region 模拟鼠标事件常量 这些常量用于模拟鼠标的输入操作，例如移动、按键按下、按键释放、滚轮滚动等。MOUSEEVENTF_ABSOLUTE 表示使用绝对坐标（屏幕坐标），而不是相对坐标。
        /// <summary>
        /// 移动鼠标位置
        /// </summary>
        public const int MOUSEEVENTF_MOVE = 0x0001;
        /// <summary>
        /// 按下左键
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        /// <summary>
        /// 松开左键
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        /// <summary>
        /// 按下右键
        /// </summary>
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        /// <summary>
        /// 松开右键
        /// </summary>
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;

        /// <summary>
        /// 按下中键
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        /// <summary>
        /// 松开中键
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        /// <summary>
        /// 滚轮滚动
        /// </summary>
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        /// <summary>
        /// 按下 X 按钮（如鼠标侧键）
        /// </summary>
        public const int MOUSEEVENTF_XDOWN = 0x0080;
        /// <summary>
        /// 松开 X 按钮
        /// </summary>
        public const int MOUSEEVENTF_XUP = 0x0100;
        /// <summary>
        /// 滚轮倾斜（水平滚动）
        /// </summary>
        public const int MOUSEEVENTF_HWHEEL = 0x01000;
        /// <summary>
        /// 使用绝对坐标（屏幕坐标）  dx 和 dy 参数包含规范化的绝对坐标。如果未设置，则这些参数包含相对数据：自上次报告的位置以来的位置变化。
        /// 无论哪种类型的鼠标或类似鼠标的设备（如果有）连接到系统，都可以设置或不设置此标志。
        /// </summary>
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        #endregion

        #region 输入类型常量
        /// <summary>
        /// 表示输入类型为鼠标输入。通常用于区分输入设备（鼠标、键盘、硬件设备）
        /// </summary>
        public const uint INPUT_MOUSE = 0x0004;
        #endregion
    }
    #endregion

    #region 鼠标键盘输入的结构体

    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        public int type;
        public InputUnion U;

    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        /* dx、dy不是以象素为单位的，而是以鼠标设备移动量为单位的，它们之间的比值受鼠标移动速度设置的影响。
         * dwFlags可以设置一个MOUSEEVENTF ABSOLUTE标志，这使得可以用另外一种方法移动标，
         * 当dwFlags设置了MOUSEEVENTF ABSOLUTE标志，dx、dy为屏幕坐标值，表示将鼠标移动到dx，dy的位置，
         * 但是这个坐标值也不是以象素为单位的。这个值的范围是0到65535(SFFFF)，当dx等于0、dy等于0时表示屏幕的最左上角，
         * 当dx等于65535、d等于65535时表示屏幕的最右下角，相当于将屏幕的宽和高分别65536等分。
         * API函数GetSystemMetrics(SM_CXSCREEN-0)可以返回屏幕的宽度，函数GetSystemMetrics(SM_CYSCREEN=1)可以返回屏幕的高度，
         * 利用屏幕的宽度和高度就可以将象素坐标换算成相应的dx、dy。注意: 这种换算最多会出现1象素的误差。
         */
        public int dx;              // 鼠标移动时的x轴坐标差(不是象素单位)，在鼠标移动时有效
        public int dy;              // 鼠标移动时的y轴坐标差(不是象素单位)，在鼠标移动时有效
        public int mouseData;       /* 鼠标滚轮滚动值，在滚动鼠标滚轮时有效。
                                       当mouseData小于0时向下滚动，当mouseData大于0时向上滚动，
                                       mouseData的绝对值一般设为120*/
        public int dwFlags;         /* dwFlags指定鼠标所进行的操作，例，MOUSEEVENTF_MOVE表示移动光标，
                                       MOUSEEVENTF_LEFTDOWN表示按下鼠标左键，MOUSEEVENTF LEFTUP表示放开鼠标左键。
                                    */
        public int time;            // 时间戳，可以使用API函数GetTickCount的返回值，
        public IntPtr dwExtraInfo;  // 扩展信息，可以使用API函教GetMessageExtralnfo的返回值。
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }

    /// <summary>
    ///输入类型 https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/ns-winuser-input
    /// </summary>
    internal class InputType
    {
        public const int MOUSE = 0;
        public const int KEYBOARD = 1;
        public const int HARDWARE = 2;
    }
    #endregion

    #region 鼠标键盘所用到的Window API
    internal static class NativeMethods
    {
        /// <summary>
        /// 检索指定窗口的某个属性。常用于获取和修改窗口的样式，比如获取窗口是否被禁用等信息。
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <param name="nIndex">指定想要获取的属性（如窗口的样式等）</param>
        /// <returns>返回类型为IntPtr，表示窗口属性的值</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 设置指定窗口的属性  用于修改窗口样式或其他属性，比如使窗口可交换位置
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="nIndex">是要设置的属性</param>
        /// <param name="dwNewLong">新的属性值</param>
        /// <returns>返回类型为IntPtr，表示设置之前的属性值。</returns>

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// 合成虚拟输入事件（如鼠标移动、键盘按键等），模拟用户输入。 用于自动化测试、游戏助手等需要模拟用户输入的场景。
        /// </summary>
        /// <param name="nInputs">要输入的事件数量</param>
        /// <param name="pInputs">一个Input结构数组，定义要模拟的输入事件</param>
        /// <param name="cbSize">每个输入结构的大小</param>
        /// <returns>返回成功模拟的输入数量</returns>
        [DllImport("User32.dll", EntryPoint = "SendInput", CharSet = CharSet.Auto)]
        internal static extern UInt32 SendInput(UInt32 nInputs, Input[] pInputs, Int32 cbSize);

        /// <summary>
        /// 返回一个整数，表示自系统启动以来的毫秒数  用于时间测量或延迟操作的计时
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetTickCount", CharSet = CharSet.Auto)]
        internal static extern int GetTickCount();

        /// <summary>
        /// 获取指定虚拟键（由nVirtKey指定）的状态（按下或释放状态）。 用于检查特定键（如Shift、Ctrl等）是否被按下
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns>返回一个短整型，表示按键的状态（高位表示是否按下，低位表示是否锁定）。</returns>
        [DllImport("User32.dll", EntryPoint = "GetKeyState", CharSet = CharSet.Auto)]
        internal static extern short GetKeyState(int nVirtKey);

        /// <summary>
        /// 向指定窗口发送一个消息  用于窗口之间的通信，比如向窗口发送关闭命令
        /// </summary>
        /// <param name="hWnd">目标窗口的句柄。</param>
        /// <param name="msg">要发送的消息类型（如WM_CLOSE）</param>
        /// <param name="wParam">消息附带的参数</param>
        /// <param name="lParam">消息附带的参数</param>
        /// <returns>窗口处理消息后的返回值</returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        /// <summary>
        /// 查找具有指定类名称和窗口名称的窗口。
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
    #endregion

    #region 屏幕分辨率/尺寸
    /// <summary>
    /// 系统指标或系统配置设置，检索的所有维度都以像素为单位。
    /// </summary>
    public class SystemMetricsHelper
    {
        public const int SM_CXSCREEN = 0;//主显示器的屏幕宽度（以像素为单位）。 这是通过调用 GetDeviceCaps 获取的相同值，如下所示： GetDeviceCaps( hdcPrimaryMonitor, HORZRES)。
        public const int SM_CYSCREEN = 1;//主显示器的屏幕高度（以像素为单位）。 这是通过调用 GetDeviceCaps 获取的相同值，如下所示： GetDeviceCaps( hdcPrimaryMonitor, VERTRES)。
        public const int SM_CXVSCROLL = 2;//垂直滚动条的宽度（以像素为单位）。
        public const int SM_CYHSCROLL = 3;//水平滚动条的高度（以像素为单位）。
        public const int SM_CYCAPTION = 4;//描述文字区域的高度（以像素为单位）。窗口标题的高度（实际标题高度加上SM_CYBORDER）
        public const int SM_CXBORDER = 5;//窗口边框的宽度（以像素为单位）。 这等效于具有 3D 外观的窗口的 SM_CXEDGE 值。
        public const int SM_CYBORDER = 6;//窗口边框的高度（以像素为单位）。 这等效于具有 3D 外观的窗口的 SM_CYEDGE 值。
        public const int SM_CXFIXEDFRAME = 7;//窗口周围具有描述文字但不是相当大的（以像素为单位）的框架的粗细。 SM_CXFIXEDFRAME是水平边框的高度，SM_CYFIXEDFRAME是垂直边框的宽度。
        public const int SM_CXDLGFRAME = 7;//对话框边框宽度/此值与 SM_CXFIXEDFRAME 相同。
        public const int SM_CYFIXEDFRAME = 8;//窗口周围具有描述文字但不是相当大的（以像素为单位）的框架的粗细。 SM_CXFIXEDFRAME是水平边框的高度，SM_CYFIXEDFRAME是垂直边框的宽度。
        public const int SM_CYDLGFRAME = 8;//对话框边框高度/此值与 SM_CYFIXEDFRAME 相同。SM_CYFIXEDFRAME
        public const int SM_CYVTHUMB = 9;//垂直滚动条上滑块的宽度/垂直滚动条中拇指框的高度（以像素为单位）。
        public const int SM_CXHTHUMB = 10;//水平滚动条上滑块的宽度/水平滚动条中拇指框的宽度（以像素为单位）。
        public const int SM_CXICON = 11;//图标的系统大宽度（以像素为单位）。 LoadIcon 函数只能加载具有SM_CXICON和SM_CYICON指定尺寸的图标。 有关详细信息 ，请参阅图标大小 。
        public const int SM_CYICON = 12;//图标的系统高度（以像素为单位）。 LoadIcon 函数只能加载具有SM_CXICON和SM_CYICON指定尺寸的图标。 有关详细信息 ，请参阅图标大小 。
        public const int SM_CXCURSOR = 13;//光标的标称宽度（以像素为单位）。
        public const int SM_CYCURSOR = 14;//光标的标称高度（以像素为单位）。
        public const int SM_CYMENU = 15;//单行菜单栏的高度（以像素为单位）。
        public const int SM_CXFULLSCREEN = 16;//主显示器上全屏窗口的工作区宽度（以像素为单位）。 若要获取系统任务栏或应用程序桌面工具栏未遮挡的屏幕部分的坐标，请使用SPI_GETWORKAREA值调用 SystemParametersInfo 函数。
        public const int SM_CYFULLSCREEN = 17;//主显示器上全屏窗口的工作区高度（以像素为单位）。 若要获取系统任务栏或应用程序桌面工具栏未遮挡的屏幕部分的坐标，请使用 SPI_GETWORKAREA 值调用 SystemParametersInfo 函数。
        public const int SM_CYKANJIWINDOW = 18;//对于系统的双字节字符集版本，这是屏幕底部的汉字窗口的高度（以像素为单位）。
        public const int SM_MOUSEPRESENT = 19;//如果安装了鼠标，则为非零值;否则为 0。 此值很少为零，因为支持虚拟鼠标，并且某些系统检测到端口的存在，而不是鼠标的存在。
        public const int SM_CYVSCROLL = 20;//垂直滚动条上箭头位图的高度（以像素为单位）。
        public const int SM_CXHSCROLL = 21;//水平滚动条上箭头位图的宽度（以像素为单位）。
        public const int SM_DEBUG = 22;//如果安装了User.exe的调试版本，则为非零;否则为 0。
        public const int SM_SWAPBUTTON = 23;//如果交换了鼠标左键和右键的含义，则为非零值;否则为 0。

        public const int SM_CXMIN = 28;//窗口的最小宽度（以像素为单位）。
        public const int SM_CYMIN = 29;//窗口的最小高度（以像素为单位）。
        public const int SM_CXSIZE = 30;//窗口中按钮的宽度描述文字或标题栏（以像素为单位）。
        public const int SM_CYSIZE = 31;//窗口中按钮的高度描述文字或标题栏（以像素为单位）。SM_CXSIZEFRAME
        public const int SM_CXSIZEFRAME = 32;//可调整大小的窗口周边的大小边框的粗细（以像素为单位）。 SM_CXSIZEFRAME是水平边框的宽度，SM_CYSIZEFRAME是垂直边框的高度。此值与 SM_CXFRAME 相同。
        public const int SM_CXFRAME = 32;//此值与 SM_CXSIZEFRAME 相同。
        public const int SM_CYSIZEFRAME = 33;//可调整大小的窗口周边的大小边框的粗细（以像素为单位）。 SM_CXSIZEFRAME是水平边框的宽度，SM_CYSIZEFRAME是垂直边框的高度。此值与 SM_CYFRAME 相同。
        public const int SM_CYFRAME = 33;//此值与 SM_CYSIZEFRAME 相同。
        public const int SM_CXMINTRACK = 34;//窗口的最小跟踪宽度（以像素为单位）。 用户无法将窗口框架拖动到小于这些尺寸的大小。 窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。
        public const int SM_CYMINTRACK = 35;//窗口的最小跟踪高度（以像素为单位）。 用户无法将窗口框架拖动到小于这些尺寸的大小。 窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。
        public const int SM_CXDOUBLECLK = 36;//矩形围绕双击序列中第一次单击的位置的宽度（以像素为单位）。 第二次单击必须在由 SM_CXDOUBLECLK 和 SM_CYDOUBLECLK 定义的矩形内发生，
        //系统才能将两次单击视为双击。 两次单击也必须在指定时间内发生。若要设置双击矩形的宽度，请使用SPI_SETDOUBLECLKWIDTH调用 SystemParametersInfo 。
        public const int SM_CYDOUBLECLK = 37;//矩形围绕双击序列中第一次单击的位置的高度（以像素为单位）。 第二次单击必须在由 SM_CXDOUBLECLK 定义的矩形内发生，SM_CYDOUBLECLK系统会将两次单击视为双击。
        //两次单击也必须在指定时间内发生。若要设置双击矩形的高度，请使用SPI_SETDOUBLECLKHEIGHT调用 SystemParametersInfo 。
        public const int SM_CXICONSPACING = 38;//大图标视图中项的网格单元格的宽度（以像素为单位）。 每个项都适合在排列时按SM_CYICONSPACING SM_CXICONSPACING大小的矩形。 此值始终大于或等于 SM_CXICON。
        public const int SM_CYICONSPACING = 39;//大图标视图中项的网格单元格的高度（以像素为单位）。 每个项都适合在排列时按SM_CYICONSPACING SM_CXICONSPACING大小的矩形。 此值始终大于或等于 SM_CYICON。
        public const int SM_MENUDROPALIGNMENT = 40;//如果下拉菜单与相应的菜单栏项右对齐，则为非零值;如果菜单左对齐，则为 0。
        public const int SM_PENWINDOWS = 41;//如果安装了 Microsoft Windows for Pen 计算扩展，则为非零值;否则为零。
        public const int SM_DBCSENABLED = 42;//如果User32.dll支持 DBCS，则为非零值;否则为 0。
        public const int SM_CMOUSEBUTTONS = 43;//鼠标上的按钮数;如果未安装鼠标，则为零。
        public const int SM_SECURE = 44;//应忽略此系统指标;它始终返回 0。SM_CXEDGE
        public const int SM_CXEDGE = 45;//三维边框的宽度（以像素为单位）。 此指标是SM_CXBORDER的三维对应指标。
        public const int SM_CYEDGE = 46;//三维边框的高度（以像素为单位）。 这是SM_CYBORDER的三维对应项。
        public const int SM_CXMINSPACING = 47;//最小化窗口的网格单元格的宽度（以像素为单位）。 每个最小化窗口在排列时适合此大小的矩形。 此值始终大于或等于 SM_CXMINIMIZED。
        public const int SM_CYMINSPACING = 48;//最小化窗口的网格单元格的高度（以像素为单位）。 每个最小化窗口在排列时适合此大小的矩形。 此值始终大于或等于 SM_CYMINIMIZED。
        public const int SM_CXSMICON = 49;//图标的系统小宽度（以像素为单位）。 小图标通常显示在窗口标题和小图标视图中。 有关详细信息 ，请参阅图标大小 。
        public const int SM_CYSMICON = 50;//图标的系统小高度（以像素为单位）。 小图标通常显示在窗口标题和小图标视图中。 有关详细信息 ，请参阅图标大小 。
        public const int SM_CYSMCAPTION = 51;//小描述文字的高度（以像素为单位）。
        public const int SM_CXSMSIZE = 52;//小描述文字按钮的宽度（以像素为单位）。
        public const int SM_CYSMSIZE = 53;//小描述文字按钮的高度（以像素为单位）。
        public const int SM_CXMENUSIZE = 54;//菜单栏按钮的宽度，例如在多个文档界面中使用的子窗口关闭按钮（以像素为单位）。
        public const int SM_CYMENUSIZE = 55;//菜单栏按钮（例如在多个文档界面中使用的子窗口关闭按钮）的高度（以像素为单位）。
        public const int SM_ARRANGE = 56;//指定系统如何排列最小化窗口的标志。 有关详细信息，请参阅本主题中的“备注”部分。
        public const int SM_CXMINIMIZED = 57;//最小化窗口的宽度（以像素为单位）。
        public const int SM_CYMINIMIZED = 58;//最小化窗口的高度（以像素为单位）。
        public const int SM_CXMAXTRACK = 59;//具有描述文字和大小调整边框（以像素为单位）的窗口的默认最大宽度。 此指标是指整个桌面。 用户无法将窗口框架拖动到大于这些尺寸的大小。 窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。
        public const int SM_CYMAXTRACK = 60;//具有描述文字和大小调整边框的窗口的默认最大高度（以像素为单位）。 此指标是指整个桌面。 用户无法将窗口框架拖动到大于这些尺寸的大小。 窗口可以通过处理 WM_GETMINMAXINFO 消息来替代此值。
        public const int SM_CXMAXIMIZED = 61;//主显示监视器上最大化的顶级窗口的默认宽度（以像素为单位）。
        public const int SM_CYMAXIMIZED = 62;//主显示监视器上最大化的顶级窗口的默认高度（以像素为单位）。
        public const int SM_NETWORK = 63;//如果存在网络，则设置最小有效位;否则，将清除它。 其他位保留供将来使用。

        public const int SM_CLEANBOOT = 67;//指定系统启动方式的 值：0 正常启动，1 故障安全启动，2 通过网络启动实现故障安全，故障安全启动(也称为 SafeBoot、安全模式或干净启动) 会绕过用户启动文件。
        public const int SM_CXDRAG = 68;//鼠标指针在拖动操作开始之前可以移动的鼠标向下点任一侧的像素数。 这允许用户轻松单击并释放鼠标按钮，而不会无意中启动拖动操作。 如果此值为负值，则从鼠标向下点的左侧减去该值，并将其添加到其右侧。
        public const int SM_CYDRAG = 69;//鼠标指针在拖动操作开始之前可以移动的鼠标向下点上方和下方的像素数。 这允许用户轻松单击并释放鼠标按钮，而不会无意中启动拖动操作。 如果此值为负值，则从鼠标向下点上方减去该值，并将其添加到其下方。
        public const int SM_SHOWSOUNDS = 70;//如果用户要求应用程序在仅以声音形式显示信息的情况下直观显示信息，则为非零值;否则为 0。
        public const int SM_CXMENUCHECK = 71;//默认菜单的宽度检查标记位图（以像素为单位）。
        public const int SM_CYMENUCHECK = 72;//默认菜单的高度检查标记位图（以像素为单位）。
        public const int SM_SLOWMACHINE = 73;//如果计算机具有低端 (慢) 处理器，则为非零值;否则为 0。
        public const int SM_MIDEASTENABLED = 74;//如果为希伯来语和阿拉伯语启用系统，则为非零值;否则为 0。
        public const int SM_MOUSEWHEELPRESENT = 75;//如果安装了具有垂直滚轮的鼠标，则为非零值;否则为 0。
        public const int SM_XVIRTUALSCREEN = 76;//虚拟屏幕左侧的坐标。 虚拟屏幕是所有显示监视器的边框。 SM_CXVIRTUALSCREEN指标是虚拟屏幕的宽度。
        public const int SM_YVIRTUALSCREEN = 77;//虚拟屏幕顶部的坐标。 虚拟屏幕是所有显示监视器的边框。 SM_CYVIRTUALSCREEN指标是虚拟屏幕的高度。
        public const int SM_CXVIRTUALSCREEN = 78;//虚拟屏幕的宽度（以像素为单位）。 虚拟屏幕是所有显示监视器的边框。 SM_XVIRTUALSCREEN指标是虚拟屏幕左侧的坐标。
        public const int SM_CYVIRTUALSCREEN = 79;//虚拟屏幕的高度（以像素为单位）。 虚拟屏幕是所有显示监视器的边框。 SM_YVIRTUALSCREEN指标是虚拟屏幕顶部的坐标。
        public const int SM_CMONITORS = 80;//桌面上的显示监视器数。 有关详细信息，请参阅本主题中的“备注”部分。
        public const int SM_SAMEDISPLAYFORMAT = 81;//如果所有显示监视器具有相同的颜色格式，则为非零值，否则为 0。 两个显示器可以具有相同的位深度，但颜色格式不同。 例如，红色、绿色和蓝色像素可以使用不同位数进行编码，或者这些位可以位于像素颜色值的不同位置。
        public const int SM_IMMENABLED = 82;//如果启用了输入法管理器/输入法编辑器功能，则为非零值;否则为 0。 SM_IMMENABLED指示系统是否已准备好在 Unicode 应用程序上使用基于 Unicode 的 IME。
        //若要确保依赖于语言的 IME 正常工作，检查 SM_DBCSENABLED和系统 ANSI 代码页。 否则，ANSI 到 Unicode 的转换可能无法正确执行，或者某些组件（如字体或注册表设置）可能不存在。
        public const int SM_CXFOCUSBORDER = 83;//DrawFocusRect 绘制的焦点矩形的左边缘和右边缘的宽度。 此值以像素为单位。Windows 2000： 不支持此值。
        public const int SM_CYFOCUSBORDER = 84;//DrawFocusRect 绘制的焦点矩形的上边缘和下边缘的高度。 此值以像素为单位。Windows 2000： 不支持此值。

        public const int SM_TABLETPC = 86;//如果当前操作系统是 Windows XP 平板电脑版本，或者当前操作系统是 Windows Vista 或 Windows 7 并且平板电脑输入服务已启动，则为非零值;否则为 0。 SM_DIGITIZER设置指示运行 Windows 7 或 Windows Server 2008 R2 的设备支持的数字化器输入类型。 有关详细信息，请参阅“备注”。
        public const int SM_MEDIACENTER = 87;//如果当前操作系统是 Windows XP，则为非零，Media Center Edition 为 0（如果不是）。
        public const int SM_STARTER = 88;//如果当前操作系统为 Windows 7 简易版 Edition、Windows Vista 入门版 或 Windows XP Starter Edition，则为非零;否则为 0。
        public const int SM_SERVERR2 = 89;//系统为 Windows Server 2003 R2 时的内部版本号;否则为 0。
        public const int SM_MOUSEHORIZONTALWHEELPRESENT = 91;//如果安装了水平滚轮的鼠标，则为非零值;否则为 0。
        public const int SM_CXPADDEDBORDER = 92;//带字幕窗口的边框填充量（以像素为单位）。Windows XP/2000： 不支持此值。

        public const int SM_DIGITIZER = 94;//如果当前操作系统是 Windows 7 或 Windows Server 2008 R2 并且平板电脑输入服务已启动，则为非零;否则为 0。 
        //返回值是一个位掩码，用于指定设备支持的数字化器输入的类型。 有关详细信息，请参阅“备注”。
        //Windows Server 2008、Windows Vista 和 Windows XP/2000： 不支持此值。
        public const int SM_MAXIMUMTOUCHES = 95;//如果系统中存在数字化器，则为非零值;否则为 0。
        //SM_MAXIMUMTOUCHES返回系统中每个数字化器支持的最大接触数的聚合最大值。 如果系统只有单点触控数字化器，则返回值为 1。 如果系统具有多点触控数字化器，则返回值是硬件可以提供的同时触点数。
        //Windows Server 2008、Windows Vista 和 Windows XP/2000： 不支持此值。

        public const int SM_REMOTESESSION = 0x1000;//此系统指标用于终端服务环境。 如果调用进程与终端服务客户端会话相关联，则返回值为非零值。 如果调用进程与终端服务控制台会话相关联，则返回值为 0。 Windows Server 2003 和 Windows XP： 控制台会话不一定是物理控制台。 有关详细信息，请参阅 WTSGetActiveConsoleSessionId。
        public const int SM_SHUTTINGDOWN = 0x2000;//如果当前会话正在关闭，则为非零;否则为 0。Windows 2000： 不支持此值
        public const int SM_REMOTECONTROL = 0x2001;//此系统指标在终端服务环境中用于确定是否远程控制当前终端服务器会话。 如果远程控制当前会话，则其值为非零值;否则为 0。
        //可以使用终端服务管理工具（如终端服务管理器(tsadmin.msc) 和shadow.exe）来控制远程会话。 远程控制会话时，另一个用户可以查看该会话的内容，并可能与之交互。

        public const int SM_CONVERTIBLESLATEMODE = 0x2003;//反映笔记本电脑或平板模式的状态，0 表示板模式，否则为非零。 当此系统指标发生更改时，系统会通过 LPARAM 中带有“ConvertibleSlateMode”
        // 的WM_SETTINGCHANGE 发送广播消息。 请注意，此系统指标不适用于台式电脑。 在这种情况下，请使用 GetAutoRotationState。
        public const int SM_SYSTEMDOCKED = 0x2004;//反映停靠模式的状态，0 表示未停靠模式，否则为非零。 当此系统指标发生更改时，系统会通过 LPARAM 中带有“SystemDockMode” 的WM_SETTINGCHANGE 发送广播消息。



        [DllImport("user32")]
        public static extern int GetSystemMetrics(int nIndex);

    }

    /// <summary>
    /// 主显示器的分辨率和屏幕尺寸
    /// </summary>
    public class MonitorHelper
    {
        private const int HORZSIZE = 4;//以毫米为单位的显示宽度
        private const int VERTSIZE = 6;//以毫米为单位的显示高度
        private const int LOGPIXELSX = 88;//像素/逻辑英寸（水平）
        private const int LOGPIXELSY = 90; //像素/逻辑英寸（垂直）
        private const int DESKTOPVERTRES = 117;//垂直分辨率
        private const int DESKTOPHORZRES = 118;//水平分辨率

        /// <summary>
        /// 获取DC句柄
        /// </summary>
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hdc);
        /// <summary>
        /// 释放DC句柄
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);
        /// <summary>
        /// 获取句柄指定的数据
        /// </summary>
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// 获取分辨率
        /// </summary>
        /// <returns></returns>
        public static Size GetResolutionRatio()
        {
            Size size = new Size();
            IntPtr hdc = GetDC(IntPtr.Zero);
            size.Width = GetDeviceCaps(hdc, DESKTOPHORZRES);
            size.Height = GetDeviceCaps(hdc, DESKTOPVERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }
        /// <summary>
        /// 获取屏幕物理尺寸(mm,mm)
        /// </summary>
        /// <returns></returns>
        public static Size GetScreenSize()
        {
            Size size = new Size();
            IntPtr hdc = GetDC(IntPtr.Zero);
            size.Width = GetDeviceCaps(hdc, HORZSIZE);
            size.Height = GetDeviceCaps(hdc, VERTSIZE);
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }

        /// <summary>
        /// 获取屏幕(对角线)的尺寸---英寸
        /// </summary>
        /// <returns></returns>
        public static float GetScreenInch()
        {
            Size size = GetScreenSize();
            double inch = Math.Round(Math.Sqrt(Math.Pow(size.Width, 2) + Math.Pow(size.Height, 2)) / 25.4, 1);
            return (float)inch;
        }
    }

    #endregion

    #region 鼠标结构体的dwFlags设置为MOUSEEVENTF_ABSOLUTE,将dx与dy移动量变成屏幕坐标，以像素为单位
    /// <summary>
    ///  鼠标结构体的dwFlags设置为MOUSEEVENTF_ABSOLUTE,将dx与dy移动量变成屏幕坐标，以像素为单位
    /// </summary>
    public class MouseStructHelper
    {
        #region 获取鼠标屏幕坐标Window API，与Control.MousePosition功能一样
        /// <summary>
        /// 获取鼠标屏幕坐标Window API，与Control.MousePosition功能一样
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        /// <summary>
        /// 获取鼠标当前屏幕坐标
        /// </summary>
        public Point GetMousePosition()
        {
            Point mp = new Point();

            if (GetCursorPos(out mp))
            {
                return mp;
            }
            else
            {
                return Point.Empty;
            }
        }
        #endregion


        /// <summary>
        /// 当前鼠标坐标转换为鼠标结构体dx，dy
        /// </summary>
        /// <returns></returns>
        public static Point MouseDxDy()
        {
            Point Mousedxdy = new Point();
            Mousedxdy.X = Control.MousePosition.X * (65535 / SystemMetricsHelper.GetSystemMetrics(SystemMetricsHelper.SM_CXSCREEN));
            Mousedxdy.Y = Control.MousePosition.Y * (65535 / SystemMetricsHelper.GetSystemMetrics(SystemMetricsHelper.SM_CYSCREEN));

            return Mousedxdy;
        }

        /// <summary>
        /// 指定鼠标屏幕坐标转换为鼠标结构体dx，dy
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Point MouseDxDy(Point source)
        {
            Point Mousedxdy = new Point();
            Mousedxdy.X = source.X * (65535 / SystemMetricsHelper.GetSystemMetrics(SystemMetricsHelper.SM_CXSCREEN));
            Mousedxdy.Y = source.Y * (65535 / SystemMetricsHelper.GetSystemMetrics(SystemMetricsHelper.SM_CYSCREEN));

            return Mousedxdy;
        }

        /// <summary>
        /// 获取鼠标当前坐标到目的坐标之间的最短距离的坐标序列
        /// </summary>
        /// <param name="destination">目的点坐标</param>
        /// <returns></returns>
        public static List<Point> MovePoints(Point destination)
        {
            return MovePoints(destination, Control.MousePosition);
        }

        /// <summary>
        /// 获取鼠标起始点到目的点之间最短距离的坐标序列。以两点之间X，Y方向长为主计算。
        /// </summary>
        /// <param name="destination">目的点坐标</param>
        /// <param name="source">起始点坐标</param>
        /// <returns></returns>
        public static List<Point> MovePoints(Point destination, Point source)
        {
            List<Point> points = new List<Point>();
            //原点与目标点一样
            if (destination == source)
            {
                points.Add(destination);
                return points;
            }
            else if (destination.X == source.X)//垂直移动
            {
                for (int i = 0; i <= Math.Abs(destination.Y - source.Y); i++)
                {
                    Point tp = new Point();
                    tp.X = destination.X;
                    if (destination.Y < source.Y)
                    {
                        tp.Y = source.Y - i;
                    }
                    else
                    {
                        tp.Y = source.Y + i;
                    }
                    points.Add(tp);
                }
                return points;
            }
            else if (destination.Y == source.Y)//水平移动
            {
                for (int i = 0; i <= Math.Abs(destination.X - source.X); i++)
                {
                    Point tp = new Point();
                    tp.Y = destination.Y;
                    if (destination.Y < source.Y)
                    {
                        tp.X = source.X - i;
                    }
                    else
                    {
                        tp.X = source.X + i;
                    }
                    points.Add(tp);
                }
                return points;
            }

            //原点与目标点不一样，并且非水平或非垂直移动，所有有斜率K
            double K = Convert.ToDouble(Math.Abs(destination.Y - source.Y)) / Convert.ToDouble(Math.Abs(destination.X - source.X));


            if (destination.Y > source.Y)//笛卡尔坐标系第一、二象限
            {
                if (destination.X > source.X)//笛卡尔坐标系第一象限
                {
                    //向选择原点和目标点之间X和Y方向长边的一组移动
                    if ((destination.X - source.X) >= (destination.Y - source.Y))//X方向长
                    {
                        for (int i = 0; i <= (destination.X - source.X); i++)
                        {
                            Point tp = new Point();
                            tp.X = source.X + i;
                            tp.Y = source.Y + (int)(K * i);
                            points.Add(tp);
                        }
                    }
                    else//Y方向长
                    {
                        for (int i = 0; i <= (destination.Y - source.Y); i++)
                        {
                            Point tp = new Point();
                            tp.Y = source.Y + i;
                            tp.X = source.X + (int)(i / K);
                            points.Add(tp);
                        }
                    }
                }
                else//笛卡尔坐标系第二象限
                {
                    //向选择原点和目标点之间X和Y方向长边的一组移动
                    if ((source.X - destination.X) >= (destination.Y - source.Y))//X方向长
                    {
                        for (int i = 0; i <= (source.X - destination.X); i++)
                        {
                            Point tp = new Point();
                            tp.X = source.X - i;
                            tp.Y = source.Y + (int)(K * i);
                            points.Add(tp);
                        }
                    }
                    else//Y方向长
                    {
                        for (int i = 0; i <= (destination.Y - source.Y); i++)
                        {
                            Point tp = new Point();
                            tp.Y = source.Y + i;
                            tp.X = source.X - (int)(i / K);
                            points.Add(tp);
                        }
                    }
                }
            }
            else//笛卡尔坐标系第三、四象限
            {
                if (destination.X < source.X)//笛卡尔坐标系第三象限
                {
                    //向选择原点和目标点之间X和Y方向长边的一组移动
                    if ((source.X - destination.X) >= (source.Y - destination.Y))//X方向长
                    {
                        for (int i = 0; i <= (source.X - destination.X); i++)
                        {
                            Point tp = new Point();
                            tp.X = source.X - i;
                            tp.Y = source.Y - (int)(K * i);
                            points.Add(tp);
                        }
                    }
                    else//Y方向长
                    {
                        for (int i = 0; i <= (source.Y - destination.Y); i++)
                        {
                            Point tp = new Point();
                            tp.Y = source.Y - i;
                            tp.X = source.X - (int)(i / K);
                            points.Add(tp);
                        }
                    }
                }
                else//笛卡尔坐标系第四象限
                {
                    //向选择原点和目标点之间X和Y方向长边的一组移动
                    if ((destination.X - source.X) >= (source.Y - destination.Y))//X方向长
                    {
                        for (int i = 0; i <= (destination.X - source.X); i++)
                        {
                            Point tp = new Point();
                            tp.X = source.X + i;
                            tp.Y = source.Y - (int)(K * i);
                            points.Add(tp);
                        }
                    }
                    else//Y方向长
                    {
                        for (int i = 0; i <= (source.Y - destination.Y); i++)
                        {
                            Point tp = new Point();
                            tp.Y = source.Y - i;
                            tp.X = source.X + (int)(i / K);
                            points.Add(tp);
                        }
                    }
                }
            }

            return points;
        }
    }
    #endregion

    #region 模拟鼠标键盘输入
    public class SendKeyboardMouse
    {
        #region 虚拟键盘
        /// <summary>
        /// 虚拟按下键
        /// </summary>
        /// <param name="vkCode">键盘虚拟代码，可以从VKCODE类中取</param>
        public void SendKeyDown(byte vkCode)
        {
            Input[] input = new Input[1];
            input[0].type = InputType.KEYBOARD;
            input[0].U.ki.wVk = vkCode;
            input[0].U.ki.dwFlags = WindowsConstant.KEYEVENTF_KEYDOWN;
            input[0].U.ki.time = NativeMethods.GetTickCount();

            uint backNum = NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0]));
            if (backNum < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 虚拟释放键
        /// </summary>
        /// <param name="vkCode">键盘虚拟代码，可以从VKCODE类中取</param>
        public void SendKeyUp(byte vkCode)
        {
            Input[] input = new Input[1];
            input[0].type = InputType.KEYBOARD;
            input[0].U.ki.wVk = vkCode;
            input[0].U.ki.dwFlags = WindowsConstant.KEYEVENTF_KEYUP;
            input[0].U.ki.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 虚拟按下并释放键
        /// </summary>
        /// <param name="vkCode">键盘虚拟代码，可以从VKCODE类中取</param>
        public void SendKeyPress(byte vkCode)
        {
            SendKeyDown(vkCode);
            //System.Threading.Thread.Sleep(200);
            SendKeyUp(vkCode);
        }
        #endregion

        #region 虚拟鼠标
        /// <summary>
        /// 虚拟鼠标滚轮滚动
        /// </summary>
        /// <param name="delta">正数表示向上滚动，负数表示向下滚动</param>
        public void MouseWhell(int delta)
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;

            input[0].U.mi.mouseData = delta;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_WHEEL;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 虚拟将鼠标光标直接移动到指定屏幕坐标处
        /// </summary>
        /// <param name="destination">目标处的屏幕坐标</param>
        public void MouseMove(Point destination)
        {
            //将鼠标结构体dx,dy移动量转换为屏幕坐标
            Point tp = MouseStructHelper.MouseDxDy(destination);

            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dx = tp.X;
            input[0].U.mi.dy = tp.Y;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_ABSOLUTE | WindowsConstant.MOUSEEVENTF_MOVE;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 虚拟有鼠标移动轨迹的鼠标移动，参数均是屏幕像素坐标
        /// </summary>
        /// <param name="destination">鼠标要移动到的目的点坐标</param>
        /// <param name="source">鼠标移动的起点坐标，如果是鼠标当前坐标，用 Control.MousePosition</param>
        /// <param name="lowSpeed">鼠标移动速度，数字越大越慢</param>
        public void MouseMove_A(Point destination, Point source, uint lowSpeed = 200)
        {
            //计算鼠标起始点与目标点之间移动时最短距离的点序列
            List<Point> movePoints = MouseStructHelper.MovePoints(destination, source);

            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;

            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_ABSOLUTE | WindowsConstant.MOUSEEVENTF_MOVE;
            for (int i = 0; i < movePoints.Count; i++)
            {
                Point tp = new Point();
                tp = MouseStructHelper.MouseDxDy(movePoints[i]);
                input[0].U.mi.dx = tp.X;
                input[0].U.mi.dy = tp.Y;
                input[0].U.mi.time = NativeMethods.GetTickCount();

                if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                //以下这段循环，用于减缓鼠标移动速度，如果用Thread.Sleep(1),虽然只有1毫秒，也显得有点缓慢，大家可以根据自己需要选择使用。
                int j = 0;
                while (j < lowSpeed * 1000)
                {
                    j++;
                }
            }
        }

        /// <summary>
        /// 鼠标相对移动，参数是移动量
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <exception cref="Win32Exception"></exception>
        public void MouseMove_Relative(int dx, int dy)
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dx = dx;
            input[0].U.mi.dy = dy;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_MOVE;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        ///  鼠标相对移动，参数是移动量，带移动轨迹动画
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="lowSpeed"></param>
        /// <exception cref="Win32Exception"></exception>
        public void MouseMove_Relative_A(int dx, int dy, uint lowSpeed = 200)
        {
            Point source = Control.MousePosition;
            Point destination = new Point(source.X + dx, source.Y + dy);

            //计算鼠标起始点与目标点之间移动时最短距离的点序列
            List<Point> movePoints = MouseStructHelper.MovePoints(destination, source);

            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;

            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_ABSOLUTE | WindowsConstant.MOUSEEVENTF_MOVE;
            for (int i = 0; i < movePoints.Count; i++)
            {
                Point tp = new Point();
                tp = MouseStructHelper.MouseDxDy(movePoints[i]);
                input[0].U.mi.dx = tp.X;
                input[0].U.mi.dy = tp.Y;
                input[0].U.mi.time = NativeMethods.GetTickCount();

                if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                //以下这段循环，用于减缓鼠标移动速度，如果用Thread.Sleep(1),虽然只有1毫秒，也显得有点缓慢，大家可以根据自己需要选择使用。
                int j = 0;
                while (j < lowSpeed * 1000)
                {
                    j++;
                }
            }
        }


        /// <summary>
        /// 虚拟鼠标左键按下
        /// </summary>
        public void MouseLeftDown()
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_LEFTDOWN;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 虚拟鼠标左键释放
        /// </summary>
        public void MouseLeftUp()
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_LEFTUP;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 点击鼠标左键并释放
        /// </summary>
        public void MouseLeftClick(int delay = 0)
        {
            MouseLeftDown();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseLeftUp();
        }
        /// <summary>
        /// 虚拟双击鼠标左键并释放
        /// </summary>
        public void MouseLeftDBClick(int delay = 0)
        {
            MouseLeftDown();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseLeftUp();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseLeftDown();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseLeftUp();
        }


        /// <summary>
        /// 虚拟鼠标右键按下
        /// </summary>
        public void MouseRightDown()
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_RIGHTDOWN;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 虚拟鼠标右键释放
        /// </summary>
        public void MouseRightUp()
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_RIGHTUP;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 点击鼠标右键并释放
        /// </summary>
        public void MouseRightClick(int delay = 0)
        {
            MouseRightDown();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseRightUp();
        }
        /// <summary>
        /// 虚拟双击鼠标右键并释放
        /// </summary>
        public void MouseRightDBClick(int delay = 0)
        {
            MouseRightDown();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseRightUp();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseRightDown();
            if (delay > 0)
                Thread.Sleep(delay);
            MouseRightUp();
        }


        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        public void MouseMiddleDown()
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_MIDDLEDOWN;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 松开鼠标中键
        /// </summary>
        public void MouseMiddleUp()
        {
            Input[] input = new Input[1];
            input[0].type = InputType.MOUSE;
            input[0].U.mi.dwFlags = WindowsConstant.MOUSEEVENTF_MIDDLEUP;
            input[0].U.mi.time = NativeMethods.GetTickCount();

            if (NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(input[0])) < input.Length)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
        /// <summary>
        /// 点击鼠标中键并释放
        /// </summary>
        public void MouseMiddleClick()
        {
            MouseMiddleDown();
            MouseMiddleUp();
        }
        #endregion
    }
    #endregion

    #region    调用举例
    //4.1 虚拟键盘。
    //比如我们虚拟按下A键，可以使用下面代码：
    //    private VKCODE vkCODE = new VKCODE();
    //    private SendKeyboardMouse sendKeyMouse = new SendKeyboardMouse();
    //    sendKeyMouse.SendKeyDown(vkCODE.VK_A);
    //    sendKeyMouse.SendKeyUp(vkCODE.VK_A);
    //和第二篇介绍keybd_event方法时一样，我们在按下键后，记得要释放该键，特别是Shfit这些功能键，怕影响到其他程序。同样，上面虚拟按A键，也可以用Press：
    //sendKeyMouse.SendKeyPress(vkCODE.VK_A);


    //4.2 虚拟鼠标
    //(1)、 模拟鼠标滚轮运动，可以用下面的代码，参数值“-10”表示滚轮向下转动：
    //sendKeyMouse.MouseWhell(-10);

    //(2)、 模拟鼠标移动，直接把鼠标光标移动到屏幕偏右下角的x=1600，y=800的坐标处。
    //sendKeyMouse.MouseMove(new Point(1600, 800));

    //(3)、 模拟鼠标移动，并且在鼠标移动过程中有鼠标指针移动的动画效果。参数有三个，第一个是要移动到的屏幕坐标，第二个是鼠标起始坐标，它可以随意指定，但我们常用的是鼠标当前位置，用Control.MousePosition可以获取到，第三个参数是鼠标指针移动的速度，可以不填，默认值是200。如果要调节鼠标移动速度的快慢，可以修改其值，值越大，移动的速度越慢。
    //sendKeyMouse.MouseMove(new Point(1600, 800), Control.MousePosition, 150);

    //(4)、 模拟鼠标单击，用左键按下，右键按下、左键弹起、右键弹起都能起到作用，下面就举一个例子。
    //sendKeyMouse.MouseLeftDown();

    //(5)、 模拟鼠标双击，我们常用的是左键双击，右键双击也能激发Window双击事件，下面就是左键双击的例子。
    //sendKeyMouse.MouseLeftDBClick();
    #endregion
}
