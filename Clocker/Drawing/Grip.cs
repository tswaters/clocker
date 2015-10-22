using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clocker.Drawing
{
    public class Grip
    {
        public static void DrawGrip (Graphics g)
        {
            var backColor = Properties.Settings.Default.backgroundColor;
            var hexColor = BitConverter.ToInt32(new byte[] { backColor.A, backColor.R, backColor.G, backColor.B }, 0);
            var foreColor = hexColor > (0xffffff / 6) ? Color.Black : Color.White;
            ControlPaint.DrawSizeGrip(g, foreColor, Win32.Resize.ResizeRect);
        }
    }
}
