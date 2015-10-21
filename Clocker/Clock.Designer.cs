namespace Clocker
{
    partial class Clock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.backgroundColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.foreColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.handColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tickColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundColorMenu,
            this.foreColorMenu,
            this.handColorMenu,
            this.tickColorMenu,
            this.toolStripSeparator1,
            this.exitMenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.ShowCheckMargin = true;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 120);
            // 
            // backgroundColorMenu
            // 
            this.backgroundColorMenu.Name = "backgroundColorMenu";
            this.backgroundColorMenu.Size = new System.Drawing.Size(142, 22);
            this.backgroundColorMenu.Text = "Background";
            // 
            // foreColorMenu
            // 
            this.foreColorMenu.Name = "foreColorMenu";
            this.foreColorMenu.Size = new System.Drawing.Size(142, 22);
            this.foreColorMenu.Text = "Fore Colour";
            // 
            // handColorMenu
            // 
            this.handColorMenu.Name = "handColorMenu";
            this.handColorMenu.Size = new System.Drawing.Size(142, 22);
            this.handColorMenu.Text = "Hand Colour";
            // 
            // tickColorMenu
            // 
            this.tickColorMenu.Name = "tickColorMenu";
            this.tickColorMenu.Size = new System.Drawing.Size(142, 22);
            this.tickColorMenu.Text = "Tick Colour";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitMenu.Name = "exitToolStripMenuItem";
            this.exitMenu.Size = new System.Drawing.Size(142, 22);
            this.exitMenu.Text = "Exit";
            // 
            // Clock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(288, 215);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Clock";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Clocker";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem backgroundColorMenu;
        private System.Windows.Forms.ToolStripMenuItem foreColorMenu;
        private System.Windows.Forms.ToolStripMenuItem handColorMenu;
        private System.Windows.Forms.ToolStripMenuItem tickColorMenu;
    }
}

