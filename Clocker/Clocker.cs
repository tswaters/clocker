using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clocker : Form
    {

        private const int SnapDistance = 50;

        private double centerX;
        private double centerY;
        private PointF centerPoint;
        private double radius;

        private readonly Pen secondHand = new Pen(Color.FromArgb(255, 255, 255), 1);
        private readonly Pen minuteHand = new Pen(Color.FromArgb(255, 255, 255), 2);
        private readonly Pen hourlyHand = new Pen(Color.FromArgb(255, 255, 255), 3);
        private readonly Pen tickHand = new Pen(Color.FromArgb(255, 0, 255), 1);

        private string[] numerals = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

        public Clocker()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            ResizeRedraw = true;

            Resize += (object o, EventArgs e) =>
            {
                centerX = ClientRectangle.Width / 2;
                centerY = ClientRectangle.Height / 2;
                centerPoint = new PointF((float)centerX, (float)centerY);
                radius = (centerX < centerY ? centerX : centerY) * 0.7;
            };

            Paint += (object o, PaintEventArgs e) =>
            {
                var now = DateTime.Now;
                var second = (now.Second / 60d);
                var minute = (now.Minute / 60d) + (second / 60d);
                var hourly = (now.Hour / 12d) + (minute / 12d);
                
                for (var x=1; x<=60; x++)
                {
                    var begPoint = getPoint(x / 60d, x % 5 == 0 ? 0.8 : 0.9);
                    var endPoint = getPoint(x / 60d, 1.0);
                    e.Graphics.DrawLine(tickHand, begPoint, endPoint);
                    if (x % 5 == 0)
                    {
                        var numeral = numerals[(x / 5) - 1];
                        var font = new Font("Segui UI", 16);
                        var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        e.Graphics.DrawString(numeral, font, Brushes.White, getPoint(x / 60d, 1.2d), format);
                    }
                }

                e.Graphics.DrawLine(secondHand, centerPoint, getPoint(second, 0.7d));
                e.Graphics.DrawLine(minuteHand, centerPoint, getPoint(minute, 0.6d));
                e.Graphics.DrawLine(hourlyHand, centerPoint, getPoint(hourly, 0.5d));
            };
            
            var timer = new Timer { Interval = 1000 };
            timer.Tick += (o, e) => { Invalidate(); };
            timer.Start();

            OnResize(EventArgs.Empty);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            var screen = Screen.FromPoint(Location);
            if (DoSnap(Left, screen.WorkingArea.Left)) { Left = screen.WorkingArea.Left; }
            if (DoSnap(Top, screen.WorkingArea.Top)) { Top = screen.WorkingArea.Top; }
            if (DoSnap(screen.WorkingArea.Right, Right)) { Left = screen.WorkingArea.Right - Width; }
            if (DoSnap(screen.WorkingArea.Bottom, Bottom)) { Top = screen.WorkingArea.Bottom - Height; }
        }

        private bool DoSnap (int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SnapDistance;
        }

        private PointF getPoint (double interval, double length)
        {
            var value = (Math.PI * 2d * interval) - (Math.PI / 2d);
            var targetX = centerX + (Math.Cos(value) * radius * length);
            var targetY = centerY + (Math.Sin(value) * radius * length);
            return new PointF((float)targetX, (float)targetY);
        }
        
    }
}
