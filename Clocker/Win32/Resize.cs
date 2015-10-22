using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker.Win32
{
    public class Resize
    {
        private static Rectangle resizeRect;

        public static Rectangle ResizeRect {
            get
            {
                return resizeRect;
            }
        }

        public static void SetResizeRect (Size size)
        {
            resizeRect = new Rectangle
            {
                X = size.Width - Constants.RESIZE_HANDLE_SIZE,
                Y = size.Height - Constants.RESIZE_HANDLE_SIZE,
                Width = Constants.RESIZE_HANDLE_SIZE,
                Height = Constants.RESIZE_HANDLE_SIZE
            };
        }

        public static bool HandleMessage(ref Message m, Form form)
        {
            if (m.Msg == Constants.WM_NCHITTEST || m.Msg == Constants.WM_MOUSEMOVE)
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = form.PointToClient(screenPoint);
                if (ResizeRect.Contains(clientPoint))
                {
                    m.Result = (IntPtr)Constants.HTBOTTOMRIGHT;
                    return true;
                }
            }
            return false;
        }

    }
}
