using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clock : Form
    {
        private const int SnapDistance = 50;

        public Clock()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ResizeRedraw = true;

            Drawing.Hands.Setup(Properties.Settings.Default.handColor);
            Drawing.Tick.Setup(Properties.Settings.Default.tickColor);
            Drawing.Numerals.Setup(Properties.Settings.Default.foreColor);

            if (!Properties.Settings.Default.lastWindowSize.IsEmpty)
            {
                Size = Properties.Settings.Default.lastWindowSize;
            }

            if (!Properties.Settings.Default.lastWindowLocation.IsEmpty)
            {
                Location = Properties.Settings.Default.lastWindowLocation;
            }

            MouseDown += (object o, MouseEventArgs e) =>
            {
                Win32.Events.MouseDown(this, e);
            };

            Resize += (object o, EventArgs e) =>
            {
                Win32.Resize.SetResizeRect(Size);
                var center = new PointF(ClientRectangle.Width / 2, ClientRectangle.Height / 2);
                Drawing.Maths.center = center;
                Drawing.Maths.radius = (center.X < center.Y ? center.X : center.Y) * 0.7;
            };

            Paint += (object o, PaintEventArgs e) =>
            {
                e.Graphics.Clear(Properties.Settings.Default.backgroundColor);
                Drawing.Hands.DrawHands(e.Graphics);
                Drawing.Numerals.DrawNumerals(e.Graphics);
                Drawing.Tick.DrawTicks(e.Graphics);
                Drawing.Grip.DrawGrip(e.Graphics);
            };

            exitMenu.Click += (o, e) => { Close(); };

            foreColorMenu.Click += (o, e) => { showColourDialog("foreColor"); };

            handColorMenu.Click += (o, e) => { showColourDialog("handColor"); };

            tickColorMenu.Click += (o, e) => { showColourDialog("tickColor"); };

            backgroundColorMenu.Click += (o, e) => { showColourDialog("backgroundColor"); };

            FormClosing += (o, e) =>
            {
                Properties.Settings.Default.lastWindowLocation = Location;
                Properties.Settings.Default.lastWindowSize = Size;
                Properties.Settings.Default.Save();
            };

            OnResize(EventArgs.Empty);

            var timer = new Timer { Interval = 1000 };
            timer.Tick += (o, e) => { Invalidate(); };
            timer.Start();

        }

        /// <summary>
        /// Handles window messages - passes along to Win32.Resize.HandleMessage
        /// If that returns false, pass along contorl to the base.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (!Win32.Resize.HandleMessage(ref m, this))
            {
                base.WndProc(ref m);
            };
        }

        /// <summary>
        /// When resize is finished and the form is close to the edge of the screen, snap it.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            var screen = Screen.FromPoint(Location);
            if (DoSnap(Left, screen.WorkingArea.Left)) { Left = screen.WorkingArea.Left; }
            if (DoSnap(Top, screen.WorkingArea.Top)) { Top = screen.WorkingArea.Top; }
            if (DoSnap(screen.WorkingArea.Right, Right)) { Left = screen.WorkingArea.Right - Width; }
            if (DoSnap(screen.WorkingArea.Bottom, Bottom)) { Top = screen.WorkingArea.Bottom - Height; }
        }

        /// <summary>
        /// Determines whether the form should snap to the edge of the screen.
        /// </summary>
        /// <param name="pos">Position to check</param>
        /// <param name="edge">The edge to check against</param>
        /// <returns></returns>
        private bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SnapDistance;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Drawing.Hands.Cleanup();
                Drawing.Numerals.Cleanup();
                Drawing.Tick.Cleanup();
                if (components != null) { components.Dispose(); }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Shows a colour selection dialog and sets related setting.
        /// </summary>
        /// <param name="settingName">setting name to set.</param>
        private void showColourDialog (string settingName)
        {
            var dialog = new ColorDialog();
            dialog.Color = (Color) Properties.Settings.Default[settingName];
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default[settingName] = dialog.Color;
                Properties.Settings.Default.Save();
            }
        }
    }
}
