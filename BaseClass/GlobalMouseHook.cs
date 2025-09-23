using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFD_DJ_LUNA.Tools;

namespace TFD_DJ_LUNA.BaseClass
{
    public class GlobalMouseHook : IDisposable
    {
        // 导入必要的Windows API函数
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        // 定义钩子类型
        private const int WH_MOUSE_LL = 14; // 低级鼠标钩子

        // 定义Windows消息
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;
        private const int WM_MBUTTONDOWN = 0x0207;
        private const int WM_MBUTTONUP = 0x0208;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_XBUTTONDOWN = 0x020B;
        private const int WM_XBUTTONUP = 0x020C;
        private const int WM_MOUSEHWHEEL = 0x020E; // 水平滚轮

        // 定义鼠标数据中标识侧键的常量
        private const int XBUTTON1 = 0x0001;
        private const int XBUTTON2 = 0x0002;

        // 钩子句柄和过程
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelMouseProc _proc;

        /// <summary>
        /// 处理所有鼠标动作事件
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseAction;
        /// <summary>
        /// 处理鼠标移动事件
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMove;
        /// <summary>
        /// 处理鼠标滚轮事件  [direction = e.Delta > 0 ? "向上" : "向下"] [orientation = e.IsHorizontal ? "水平" : "垂直"]
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheel;
        /// <summary>
        /// 处理鼠标点击事件
        /// </summary>
        public event EventHandler<MouseClickEventArgs> MouseClick;
        /// <summary>
        /// 处理鼠标按下事件
        /// </summary>
        public event EventHandler<MouseClickEventArgs> MouseDown;
        /// <summary>
        /// 处理鼠标释放事件
        /// </summary>
        public event EventHandler<MouseClickEventArgs> MouseUp;

        /// <summary>
        /// 是否启用鼠标移动事件捕获
        /// </summary>
        public bool CaptureMoveEvents { get; set; } = true;
        /// <summary>
        /// 是否启用鼠标点击事件捕获
        /// </summary>
        public bool CaptureClickEvents { get; set; } = true;
        /// <summary>
        /// 是否启用鼠标滚轮事件捕获
        /// </summary>
        public bool CaptureWheelEvents { get; set; } = true;

        public GlobalMouseHook()
        {
            _proc = HookCallback;
        }

        public void StartHook()
        {
            if (_hookID == IntPtr.Zero)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    _hookID = SetWindowsHookEx(WH_MOUSE_LL, _proc,
                                              GetModuleHandle(curModule.ModuleName), 0);
                }

                // 添加详细的错误检查
                if (_hookID == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    string errorMsg = new Win32Exception(errorCode).Message;
                    MessageShowList.SendEventMsg($"安装鼠标钩子失败 (错误代码: {errorCode}): {errorMsg}");
                    return;
                }
                else
                {
                }
            }
        }

        public void StopHook()
        {
            if (_hookID != IntPtr.Zero)
            {
                bool success = UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
                if (!success)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    string errorMsg = new Win32Exception(errorCode).Message;
                    MessageShowList.SendEventMsg(string.Format("卸载鼠标钩子失败！错误码={0},{1}.", errorCode, errorMsg));
                }
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int message = wParam.ToInt32();
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                // 获取按键状态
                bool leftButton = (GetAsyncKeyState(Keys.LButton) & 0x8000) != 0;
                bool rightButton = (GetAsyncKeyState(Keys.RButton) & 0x8000) != 0;
                bool middleButton = (GetAsyncKeyState(Keys.MButton) & 0x8000) != 0;

                // 处理不同消息类型
                switch (message)
                {
                    case WM_MOUSEMOVE:
                        if (CaptureMoveEvents)
                        {
                            HandleMouseMove(hookStruct, leftButton, rightButton, middleButton);
                        }
                        break;
                    case WM_LBUTTONDOWN:
                    case WM_RBUTTONDOWN:
                    case WM_MBUTTONDOWN:
                    case WM_XBUTTONDOWN:
                        if (CaptureClickEvents)
                        {
                            HandleMouseDown(message, hookStruct);
                        }
                        break;
                    case WM_LBUTTONUP:
                    case WM_RBUTTONUP:
                    case WM_MBUTTONUP:
                    case WM_XBUTTONUP:
                        if (CaptureClickEvents)
                        {
                            HandleMouseUp(message, hookStruct);
                        }
                        break;
                    case WM_MOUSEWHEEL:
                    case WM_MOUSEHWHEEL:
                        if (CaptureWheelEvents)
                        {
                            HandleMouseWheel(message, hookStruct);
                        }
                        break;
                }
            }

            // 将消息传递给钩子链中的下一个钩子
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void HandleMouseMove(MSLLHOOKSTRUCT hookStruct, bool leftButton, bool rightButton, bool middleButton)
        {
            var args = new MouseMoveEventArgs
            {
                X = hookStruct.pt.x,
                Y = hookStruct.pt.y,
                LeftButton = leftButton,
                RightButton = rightButton,
                MiddleButton = middleButton
            };

            MouseMove?.Invoke(this, args);
            MouseAction?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, hookStruct.pt.x, hookStruct.pt.y, 0));
        }

        private void HandleMouseDown(int message, MSLLHOOKSTRUCT hookStruct)
        {
            MouseButtons button = MouseButtons.None;
            int buttonId = 0;

            switch (message)
            {
                case WM_LBUTTONDOWN:
                    button = MouseButtons.Left;
                    buttonId = 1;
                    break;
                case WM_RBUTTONDOWN:
                    button = MouseButtons.Right;
                    buttonId = 2;
                    break;
                case WM_MBUTTONDOWN:
                    button = MouseButtons.Middle;
                    buttonId = 3;
                    break;
                case WM_XBUTTONDOWN:
                    int xButton = (int)(hookStruct.mouseData >> 16) & 0xFFFF;
                    if (xButton == XBUTTON1)
                    {
                        button = MouseButtons.XButton1;
                        buttonId = 4;
                    }
                    else if (xButton == XBUTTON2)
                    {
                        button = MouseButtons.XButton2;
                        buttonId = 5;
                    }
                    break;
            }

            if (button != MouseButtons.None)
            {
                var args = new MouseClickEventArgs
                {
                    Button = button,
                    ButtonId = buttonId,
                    X = hookStruct.pt.x,
                    Y = hookStruct.pt.y,
                    ClickCount = 1 // 需要额外逻辑来检测双击
                };

                MouseDown?.Invoke(this, args);
                MouseAction?.Invoke(this, new MouseEventArgs(button, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
            }
        }

        private void HandleMouseUp(int message, MSLLHOOKSTRUCT hookStruct)
        {
            MouseButtons button = MouseButtons.None;
            int buttonId = 0;

            switch (message)
            {
                case WM_LBUTTONUP:
                    button = MouseButtons.Left;
                    buttonId = 1;
                    break;
                case WM_RBUTTONUP:
                    button = MouseButtons.Right;
                    buttonId = 2;
                    break;
                case WM_MBUTTONUP:
                    button = MouseButtons.Middle;
                    buttonId = 3;
                    break;
                case WM_XBUTTONUP:
                    int xButton = (int)(hookStruct.mouseData >> 16) & 0xFFFF;
                    if (xButton == XBUTTON1)
                    {
                        button = MouseButtons.XButton1;
                        buttonId = 4;
                    }
                    else if (xButton == XBUTTON2)
                    {
                        button = MouseButtons.XButton2;
                        buttonId = 5;
                    }
                    break;
            }

            if (button != MouseButtons.None)
            {
                var args = new MouseClickEventArgs
                {
                    Button = button,
                    ButtonId = buttonId,
                    X = hookStruct.pt.x,
                    Y = hookStruct.pt.y,
                    ClickCount = 1
                };

                MouseUp?.Invoke(this, args);
                MouseClick?.Invoke(this, args);
                MouseAction?.Invoke(this, new MouseEventArgs(button, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
            }
        }

        private void HandleMouseWheel(int message, MSLLHOOKSTRUCT hookStruct)
        {
            int delta = (short)((hookStruct.mouseData >> 16) & 0xFFFF);
            bool isHorizontal = (message == WM_MOUSEHWHEEL);

            var args = new MouseWheelEventArgs
            {
                Delta = delta,
                IsHorizontal = isHorizontal,
                X = hookStruct.pt.x,
                Y = hookStruct.pt.y
            };

            MouseWheel?.Invoke(this, args);
            MouseAction?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, hookStruct.pt.x, hookStruct.pt.y, delta));
        }

        // 定义低级鼠标钩子结构
        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        // 自定义事件参数类
        public class MouseMoveEventArgs : EventArgs
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool LeftButton { get; set; }
            public bool RightButton { get; set; }
            public bool MiddleButton { get; set; }
        }

        public class MouseWheelEventArgs : EventArgs
        {
            public int Delta { get; set; }
            public bool IsHorizontal { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class MouseClickEventArgs : EventArgs
        {
            public MouseButtons Button { get; set; }
            public int ButtonId { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int ClickCount { get; set; }
        }

        // 实现IDisposable接口
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                }

                // 释放非托管资源
                StopHook();

                disposed = true;
            }
        }

        ~GlobalMouseHook()
        {
            Dispose(false);
        }
    }
}
