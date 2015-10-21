using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clock
    {

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void InitializeDrag()
        {
            MouseDown += (object o, MouseEventArgs e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
            };
        }
    }
}
