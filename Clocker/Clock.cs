using System;
using System.Drawing;
using System.Windows.Forms;

using Clocker.UI;
using Clocker.Interfaces;
using Clocker.Services;
using System.ComponentModel;

namespace Clocker
{
    public partial class Clock : Form
    {
        private const int SNAP_DISTANCE = 50;

        private Timer _timer;
        private IGraphicsService _graphicsService = new GraphicsService();
        private IMathService _mathService;
        private IDrawable _background;
        private IDrawable _hands;
        private IDrawable _numerals;
        private IDrawable _ticks;
        private IDrawable _center;
        private IDrawable _resizeGrip;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Clocker.Win32.NativeMethods.SendMessage(System.IntPtr,System.Int32,System.Int32,System.Int32)", Justification = "Can't do anything with it ")]
        public Clock()
        {
            InitializeComponent();
            InitializeMenu();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            _mathService = new MathService(ClientRectangle);
            _background = new Background(Properties.Settings.Default.backgroundColor);
            _hands = new Hands(_mathService, new DateTimeService(), Properties.Settings.Default.handColor);
            _numerals = new Numerals(_mathService, Properties.Settings.Default.foreColor);
            _ticks = new Tick(_mathService, Properties.Settings.Default.tickColor);
            _center = new Center(_mathService, Properties.Settings.Default.handColor);
            _resizeGrip = new ResizeGrip(_mathService);

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
                if (e.Button == MouseButtons.Left)
                {
                    Win32.NativeMethods.ReleaseCapture();
                    Win32.NativeMethods.SendMessage(Handle, Win32.Constants.WM_NCLBUTTONDOWN, Win32.Constants.HTCAPTION, 0);
                }
            };

            ResizeBegin += (o, e) => { _timer.Stop(); };
            ResizeEnd += (o, e) => { _timer.Start(); };
            Resize += (o, e) => { _mathService.Rectangle = ClientRectangle; };

            Properties.Settings.Default.PropertyChanged += (o, e) =>
            {
                _hands.Color = Properties.Settings.Default.handColor;
                _numerals.Color = Properties.Settings.Default.foreColor;
                _ticks.Color = Properties.Settings.Default.tickColor;
                _background.Color = Properties.Settings.Default.backgroundColor;
                _center.Color = Properties.Settings.Default.handColor;
                _resizeGrip.Color = Properties.Settings.Default.backgroundColor;
                Invalidate();
            };

            Paint += (object o, PaintEventArgs e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                _graphicsService.Graphics = e.Graphics;
                _background.Draw(_graphicsService);
                _hands.Draw(_graphicsService);
                _numerals.Draw(_graphicsService);
                _ticks.Draw(_graphicsService);
                _center.Draw(_graphicsService);
                _resizeGrip.Draw(_graphicsService);
            };

            FormClosing += (o, e) =>
            {
                Properties.Settings.Default.lastWindowLocation = Location;
                Properties.Settings.Default.lastWindowSize = Size;
                Properties.Settings.Default.Save();
            };

            OnResize(EventArgs.Empty);

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += (o, e) => { Invalidate(); };
            _timer.Start();

        }

        /// <summary>
        /// Handles window messages - if inside the resize grip, .
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            var handled = false;
            if (m.Msg == Win32.Constants.WM_NCHITTEST || m.Msg == Win32.Constants.WM_MOUSEMOVE)
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = PointToClient(screenPoint);
                if (_mathService.ResizeRect.Contains(clientPoint))
                {
                    m.Result = (IntPtr)Win32.Constants.HTBOTTOMRIGHT;
                    handled = true;
                }
            }

            if (!handled)
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
        private static bool DoSnap(int pos, int edge)
        {
            int delta = pos - edge;
            return delta > 0 && delta <= SNAP_DISTANCE;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_resizeGrip != null) { _resizeGrip.Dispose(); }
                if (_center != null) { _center.Dispose(); }
                if (_background != null) { _background.Dispose();  }
                if (_timer != null) { _timer.Dispose(); }
                if (_hands != null) { _hands.Dispose(); }
                if (_numerals != null) { _numerals.Dispose(); }
                if (_ticks != null) { _ticks.Dispose(); }
                if (_components != null) { _components.Dispose(); }
            }
            base.Dispose(disposing);
        }

        #region "Context menu, options"

        private IContainer _components;
        private ContextMenuStrip _contextMenuStrip1;
        private ToolStripMenuItem _menuExit;
        private ToolStripSeparator _menuSeparator1;
        private ToolStripMenuItem _menuBackgroundColor;
        private ToolStripMenuItem _menuForeColor;
        private ToolStripMenuItem _menuHandColor;
        private ToolStripMenuItem _menuTickColor;

        private void InitializeMenu ()
        {
            _components = new Container();
            _contextMenuStrip1 = new ContextMenuStrip(_components);
            _contextMenuStrip1.RenderMode = ToolStripRenderMode.Professional;
            _contextMenuStrip1.Size = new Size(143, 120);

            _menuBackgroundColor = new ToolStripMenuItem();
            _menuBackgroundColor.Text = Strings.MENUS_BACKGROUND;
            _menuBackgroundColor.Size = new Size(142, 22);

            _menuForeColor = new ToolStripMenuItem();
            _menuForeColor.Text = Strings.MENUS_FORE_COLOR;
            _menuForeColor.Size = new Size(142, 22);

            _menuHandColor = new ToolStripMenuItem();
            _menuHandColor.Text = Strings.MENUS_HAND_COLOR;
            _menuHandColor.Size = new Size(142, 22);

            _menuTickColor = new ToolStripMenuItem();
            _menuTickColor.Text = Strings.MENUS_TICK_COLOR;
            _menuTickColor.Size = new Size(142, 22);

            _menuSeparator1 = new ToolStripSeparator();
            _menuSeparator1.Size = new Size(139, 6);

            _menuExit = new ToolStripMenuItem();
            _menuExit.Text = Strings.MENUS_EXIT;
            _menuExit.Size = new Size(142, 22);

            _contextMenuStrip1.Items.AddRange(new ToolStripItem[]
            {
                _menuBackgroundColor,
                _menuForeColor,
                _menuHandColor,
                _menuTickColor,
                _menuSeparator1,
                _menuExit
            });

            _menuExit.Click += (o, e) => { Close(); };
            _menuForeColor.Click += (o, e) => { showColourDialog("foreColor"); };
            _menuHandColor.Click += (o, e) => { showColourDialog("handColor"); };
            _menuTickColor.Click += (o, e) => { showColourDialog("tickColor"); };
            _menuBackgroundColor.Click += (o, e) => { showColourDialog("backgroundColor"); };

            ContextMenuStrip = _contextMenuStrip1;
        }

        /// <summary>
        /// Shows a colour selection dialog and sets related setting.
        /// </summary>
        /// <param name="settingName">setting name to set.</param>
        private static void showColourDialog(string settingName)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = (Color)Properties.Settings.Default[settingName];
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default[settingName] = dialog.Color;
                    Properties.Settings.Default.Save();
                }
            }
        }

        #endregion
    }
}
