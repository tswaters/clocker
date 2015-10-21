using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clock
    {

        private double centerX;
        private double centerY;
        private PointF centerPoint;
        private double radius;
        private string[] numerals = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };

        private Color backColor;
        private Pen secondPen;
        private Pen minutePen;
        private Pen hourlyPen;
        private Pen tickPen;
        private SolidBrush fontBrush;

        private void InitializePaint()
        {
            updatePens();
            Properties.Settings.Default.PropertyChanged += (o, e) => {
                updatePens();
                Invalidate();
            };

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

                e.Graphics.Clear(backColor);

                var color = Properties.Settings.Default.backgroundColor;
                var bgColor = BitConverter.ToInt32(new byte[] { color.A, color.R, color.G, color.B }, 0);
                var foreColor = bgColor > (0xffffff / 6) ? Color.Black : Color.White;
                ControlPaint.DrawSizeGrip(e.Graphics, foreColor, resizeRect);

                for (var x = 1; x <= 60; x++)
                {
                    var begPoint = getPoint(x / 60d, x % 5 == 0 ? 0.8 : 0.9);
                    var endPoint = getPoint(x / 60d, 1.0);
                    e.Graphics.DrawLine(tickPen, begPoint, endPoint);
                    if (Properties.Settings.Default.showNumerals && x % 5 == 0)
                    {
                        var numeral = numerals[(x / 5) - 1];
                        var font = new Font("Segui UI", 16);
                        var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        e.Graphics.DrawString(numeral, font, fontBrush, getPoint(x / 60d, 1.2d), format);
                    }
                }
                e.Graphics.DrawLine(secondPen, centerPoint, getPoint(second, 0.7d));
                e.Graphics.DrawLine(minutePen, centerPoint, getPoint(minute, 0.6d));
                e.Graphics.DrawLine(hourlyPen, centerPoint, getPoint(hourly, 0.5d));
            };

            var timer = new Timer { Interval = 1000 };
            timer.Tick += (o, e) => { Invalidate(); };
            timer.Start();

            OnResize(EventArgs.Empty);
        }

        private void updatePens()
        {
            secondPen = new Pen(Properties.Settings.Default.handColor, 1);
            minutePen = new Pen(Properties.Settings.Default.handColor, 2);
            hourlyPen = new Pen(Properties.Settings.Default.handColor, 3);
            tickPen = new Pen(Properties.Settings.Default.tickColor, 1);
            backColor = Properties.Settings.Default.backgroundColor;
            fontBrush = new SolidBrush(Properties.Settings.Default.foreColor);
        }

        private PointF getPoint(double interval, double length)
        {
            var value = (Math.PI * 2d * interval) - (Math.PI / 2d);
            var targetX = centerX + (Math.Cos(value) * radius * length);
            var targetY = centerY + (Math.Sin(value) * radius * length);
            return new PointF((float)targetX, (float)targetY);
        }

    }
}
