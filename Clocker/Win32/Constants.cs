using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clocker.Win32
{
    static class Constants
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        public const uint WM_NCHITTEST = 0x0084;
        public const uint WM_MOUSEMOVE = 0x0200;
        public const uint HTBOTTOMRIGHT = 17;
        public const int RESIZE_HANDLE_SIZE = 10;
    }
}
