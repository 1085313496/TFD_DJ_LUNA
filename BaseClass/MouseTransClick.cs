using System;
using System.Runtime.InteropServices;

namespace TFD_DJ_LUNA
{
    public class MouseTransClick
    {
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

        /// <summary>
        /// 窗体句柄
        /// </summary>
        private IntPtr WindowHandle;

        public MouseTransClick() { }
        public MouseTransClick(IntPtr whd) { WindowHandle = whd; }

        /// <summary>
        /// 设置窗体具有鼠标穿透效果
        /// </summary>
        /// <param name="flag">true穿透，false不穿透</param>
        public void SetPenetrate(bool flag = true)
        {
            uint style = GetWindowLong(WindowHandle, GWL_EXSTYLE);
            if (flag)
                SetWindowLong(WindowHandle, GWL_EXSTYLE, style | WS_EX_TRANSPARENT | WS_EX_LAYERED);
            else
                SetWindowLong(WindowHandle, GWL_EXSTYLE, style & ~(WS_EX_TRANSPARENT | WS_EX_LAYERED));
           // SetLayeredWindowAttributes(WindowHandle, 0, 100, LWA_ALPHA);
        }
    }
}
