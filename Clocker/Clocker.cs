using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clocker
{
    public partial class Clocker : Form
    {
        public Clocker()
        {
            InitializeComponent();
            InitializeDrag();
            InitializeResize();
            InitializePaint();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
    }
}
