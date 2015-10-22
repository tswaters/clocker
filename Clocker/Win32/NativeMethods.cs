using System;
using System.Runtime.InteropServices;

namespace Clocker.Win32
{
    public class NativeMethods
    {

        [DllImport("User32.dll")]
        internal static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        internal static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    }
}
