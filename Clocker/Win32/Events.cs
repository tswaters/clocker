using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clocker.Win32
{
    class Events
    {
        public static void MouseDown(Form o, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(o.Handle, Constants.WM_NCLBUTTONDOWN, Constants.HTCAPTION, 0);
            }
        }
    }
}
