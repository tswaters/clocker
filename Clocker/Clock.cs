using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
            InitializeDrag();
            InitializeResize();
            InitializePaint();
            
            if (!Properties.Settings.Default.lastWindowSize.IsEmpty)
            {
                Size = Properties.Settings.Default.lastWindowSize;
            }

            if (!Properties.Settings.Default.lastWindowLocation.IsEmpty)
            {
                Location = Properties.Settings.Default.lastWindowLocation;
            }

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

        }
        

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
