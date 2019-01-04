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

namespace Tracking
{
    public partial class Form1 : Form
    {
        //private Graphics g;
        //private Brush curbrush = new SolidBrush(Color.Green);
        //private Brush wipebrush = new SolidBrush(SystemColors.Control);
        //private int size = 10;
        private const int speed = 10;

        public Form1()
        {
            InitializeComponent();
            ThreadMaker.Speed = speed;
            //g = Graphics.FromHwnd(this.Handle);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Visible = false;
            Color c = ThreadMaker.Escape ? Color.Blue : Color.Red;
            ThreadMaker tm = new ThreadMaker(e.X, e.Y, 10, c);
            tm.ThreadLauncher();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ThreadMaker.Escape = !ThreadMaker.Escape;
            ThreadMaker.Col = ThreadMaker.Escape ? Color.Blue : Color.Red;
            ThreadMaker.Speed = ThreadMaker.Escape ? -speed : speed;
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
            ThreadMaker.Prevcol = ThreadMaker.Col;
            ThreadMaker.Col = Color.Green;
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            ThreadMaker.Target = true;
            ThreadMaker.Col = ThreadMaker.Prevcol;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
