using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Tracking
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Brush curbrush = new SolidBrush(Color.Green);
        private Brush wbrush = new SolidBrush(SystemColors.Control);
        private int size = 10;

        public Form1()
        {
            InitializeComponent();
            g = Graphics.FromHwnd(this.Handle);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            ThreadMaker tm = new ThreadMaker(e.X, e.Y, Color.Red);
            tm.ThreadLauncher();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*g.FillEllipse(curbrush, e.X, e.Y, size, size);
            ThreadMaker.Target = true;
            ThreadMaker.Tpx = e.X;
            ThreadMaker.Tpy = e.Y;*/
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetProcessesByName("Escaper")[0].Kill();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            ThreadMaker.Target = true;
            ThreadMaker.Tpx = e.X;
            ThreadMaker.Tpy = e.Y;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            ThreadMaker.Target = false;
        }
    }
}
