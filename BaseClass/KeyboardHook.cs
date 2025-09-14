using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TFD_DJ_LUNA
{
    /// <summary>
    /// Win32Api
    /// </summary>
    class Win32Api
    {
        #region 常数和结构
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;

        [StructLayout(LayoutKind.Sequential)] //声明键盘钩子的封送结构类型 
        public class KeyboardHookStruct
        {
            /// <summary>
            /// 表示一个在1到254间的虚似键盘码
            /// </summary>
            public int vkCode;
            /// <summary>
            /// 表示硬件扫描码
            /// </summary>
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        #endregion

        #region Api
        /// <summary>
        /// 键盘钩子回调函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        /// <summary>
        ///安装一个钩子，用于侦听特定类型的事件，比如键盘输入
        /// </summary>
        /// <param name="idHook">指定要安装的钩子的类型（例如，键盘钩子）</param>
        /// <param name="lpfn">指向钩子回调函数的指针，此函数会在钩子检测到事件时被调用。</param>
        /// <param name="hInstance">模块的句柄，通常是当前进程的句柄</param>
        /// <param name="threadId">指定钩子所应用的线程的ID（如果为0，则对所有线程有效）</param>
        /// <returns>如果成功，返回钩子的ID；如果失败，返回0</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        /// <summary>
        /// 如果成功，返回钩子的ID；如果失败，返回0
        /// </summary>
        /// <param name="idHook">要卸下的钩子的ID，通常是之前通过SetWindowsHookEx获得的。</param>
        /// <returns>如果成功返回true，失败则返回false。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// 将消息传递给下一个钩子或钩子链中的下一个处理程序。
        /// </summary>
        /// <param name="idHook">当前钩子的ID</param>
        /// <param name="nCode">钩子接收到的消息代码。</param>
        /// <param name="wParam">消息的额外信息</param>
        /// <param name="lParam">指向与消息相关的结构体的指针</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        /// <summary>
        /// 将虚拟键（uVirtKey）和扫描码（uScanCode）转换为ASCII字符。
        /// </summary>
        /// <param name="uVirtKey">虚拟键的代码</param>
        /// <param name="uScanCode">键的扫描码</param>
        /// <param name="lpbKeyState">描述当前按键状态的数组。</param>
        /// <param name="lpwTransKey">用于接收转换结果的数组</param>
        /// <param name="fuState">状态标志</param>
        /// <returns>成功时返回ASCII字符的数量，失败返回0</returns>
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        /// <summary>
        /// 获取当前键盘的状态
        /// </summary>
        /// <param name="pbKeyState">用于接收键盘状态的数组</param>
        /// <returns>返回实际填充的字节数</returns>
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        /// <summary>
        /// 获取指定模块的句柄。
        /// </summary>
        /// <param name="lpModuleName">模块的名称，可以是DLL或EXE。</param>
        /// <returns>返回模块的句柄，失败时返回IntPtr.Zero。</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion
    }

    public class KeyboardHook
    {
        /// <summary>
        /// 钩子句柄
        /// </summary>
        public int hHook;
        /// <summary>
        /// 键盘钩子委托
        /// </summary>
        Win32Api.HookProc KeyboardHookDelegate;
        /// <summary>
        /// 键盘按下事件  若未订阅此事件或OnKeyPressEvent事件，ctrl/shift/alt组合键监听可能会失效
        /// </summary>
        public event KeyEventHandler OnKeyDownEvent;
        /// <summary>
        /// 键盘弹起事件
        /// </summary>
        public event KeyEventHandler OnKeyUpEvent;
        /// <summary>
        /// 键盘按下事件   若未订阅此事件或OnKeyDownEvent事件，ctrl/shift/alt组合键监听可能会失效
        /// </summary>
        public event KeyPressEventHandler OnKeyPressEvent;

        public KeyboardHook() { }

        /// <summary>
        /// 安装钩子
        /// </summary>
        public void SetHook()
        {
            KeyboardHookDelegate = new Win32Api.HookProc(KeyboardHookProc);
            Process cProcess = Process.GetCurrentProcess();
            ProcessModule cModule = cProcess.MainModule;
            var mh = Win32Api.GetModuleHandle(cModule.ModuleName);
            hHook = Win32Api.SetWindowsHookEx(Win32Api.WH_KEYBOARD_LL, KeyboardHookDelegate, mh, 0);
        }
        /// <summary>
        /// 卸载钩子
        /// </summary>
        public void UnHook() { Win32Api.UnhookWindowsHookEx(hHook); }

        /// <summary>
        /// 存放被按下的控制键，用来生成具体的键
        /// </summary>
        private List<Keys> preKeysList = new List<Keys>();

        /// <summary>
        /// 键盘钩子回调函数
        /// </summary>
        /// <param name="nCode">钩子程序接收到的消息类型</param>
        /// <param name="wParam">有关键盘事件的附加信息</param>
        /// <param name="lParam">指向一个结构的指针，其中包含额外的键盘信息。</param>
        /// <returns></returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //如果该消息被丢弃（nCode<0）或者没有事件绑定处理程序则不会触发事件
            if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
            {
                //【获取按键信息】 将 lParam 指向的结构转换为 KeyboardHookStruct，并获取其中的虚拟键码。
                Win32Api.KeyboardHookStruct KeyDataFromHook = (Win32Api.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.KeyboardHookStruct));
                Keys keyData = (Keys)KeyDataFromHook.vkCode;

                //【处理按下事件】 若按下控制键，将控制键（Ctrl、Alt、Shift）加入 preKeysList
                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                {
                    if (IsCtrlAltShiftKeys(keyData) && preKeysList.IndexOf(keyData) == -1)
                    {
                        preKeysList.Add(keyData);
                    }
                }

                //【触发按下事件】 WM_KEYDOWN和WM_SYSKEYDOWN消息，将会引发OnKeyDownEvent事件
                if (OnKeyDownEvent != null && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                {
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                    OnKeyDownEvent(this, e);
                }

                //【处理字符输入事件】 WM_KEYDOWN消息将引发OnKeyPressEvent 
                if (OnKeyPressEvent != null && wParam == Win32Api.WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    Win32Api.GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (Win32Api.ToAscii(KeyDataFromHook.vkCode, KeyDataFromHook.scanCode, keyState, inBuffer, KeyDataFromHook.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        OnKeyPressEvent(this, e);
                    }
                }

                //【处理松开事件】 松开控制键，从 preKeysList 中移除控制键
                if ((OnKeyDownEvent != null || OnKeyPressEvent != null) && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                {
                    if (IsCtrlAltShiftKeys(keyData))
                    {
                        for (int i = preKeysList.Count - 1; i >= 0; i--)
                        {
                            if (preKeysList[i] == keyData)
                            {
                                preKeysList.RemoveAt(i);
                            }
                        }
                    }
                }

                //【触发松开事件】 WM_KEYUP和WM_SYSKEYUP消息，将引发OnKeyUpEvent事件 
                if (OnKeyUpEvent != null && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                {
                    KeyEventArgs e = new KeyEventArgs(GetDownKeys(keyData));
                    OnKeyUpEvent(this, e);
                }
            }

            return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 根据已经按下的控制键生成key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Keys GetDownKeys(Keys key)
        {
            Keys rtnKey = Keys.None;
            foreach (Keys i in preKeysList)
            {
                if (i == Keys.LControlKey || i == Keys.RControlKey)
                {
                    rtnKey = rtnKey | Keys.Control;
                }
                if (i == Keys.LMenu || i == Keys.RMenu)
                {
                    rtnKey = rtnKey | Keys.Alt;
                }
                if (i == Keys.LShiftKey || i == Keys.RShiftKey)
                {
                    rtnKey = rtnKey | Keys.Shift;
                }
            }
            return rtnKey | key;
        }
        /// <summary>
        /// 根据已经按下的控制键生成key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean IsCtrlAltShiftKeys(Keys key)
        {
            if (key == Keys.LControlKey || key == Keys.RControlKey || key == Keys.LMenu || key == Keys.RMenu || key == Keys.LShiftKey || key == Keys.RShiftKey)
                return true;
            return false;
        }
    }
}
