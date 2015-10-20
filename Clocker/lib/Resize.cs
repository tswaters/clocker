using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clocker
    {
        private const uint WM_NCHITTEST = 0x0084;
        private const uint WM_MOUSEMOVE = 0x0200;
        private const uint HTBOTTOMRIGHT = 17;
        private const int RESIZE_HANDLE_SIZE = 10;

        private Rectangle resizeRect;

        private void InitializeResize()
        {
            Paint += (object o, PaintEventArgs e) =>
            {
                ControlPaint.DrawSizeGrip(e.Graphics, Color.Black, resizeRect);
            };

            Resize += (object o, EventArgs e) =>
            {
                resizeRect = new Rectangle(Size.Width - RESIZE_HANDLE_SIZE, Size.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE);
            };
        }

        protected override void WndProc(ref Message m)
        {
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = PointToClient(screenPoint);
                if (resizeRect.Contains(clientPoint))
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    handled = true;
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }

    }
}
