using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clock
    {
        private const int SnapDistance = 50;

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            var screen = Screen.FromPoint(Location);
            if (DoSnap(Left, screen.WorkingArea.Left)) { Left = screen.WorkingArea.Left; }
            if (DoSnap(Top, screen.WorkingArea.Top)) { Top = screen.WorkingArea.Top; }
            if (DoSnap(screen.WorkingArea.Right, Right)) { Left = screen.WorkingArea.Right - Width; }
            if (DoSnap(screen.WorkingArea.Bottom, Bottom)) { Top = screen.WorkingArea.Bottom - Height; }
        }

        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SnapDistance;
        }
    }
}
