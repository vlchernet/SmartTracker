using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracking
{
    class ThreadMaker
    {
        private int x, y;
        private int speed = 10;
        private int size = 10;
        private Brush curbrush;
        private Color color;
        private Brush wbrush = new SolidBrush(SystemColors.Control);
        private Graphics G { get; set; } = Graphics.FromHwnd(Form1.ActiveForm.Handle);
        public static int Tpx { get => tpx; set => tpx = value; }
        public static int Tpy { get => tpy; set => tpy = value; }
        public static bool Target { get => target; set => target = value; }

        private static bool target = false;
        private static int tpx;
        private static int tpy;

        public ThreadMaker(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            curbrush = new SolidBrush(color);
        }

        public void ThreadLauncher()
        {
            Thread myThread = new Thread(Tracker);
            myThread.Start();
        }

        private void Tracker()
        {
            double angle = 0;
            while (true)
            {
                if (!Target)
                {
                    angle = Random() * Math.PI / 180;
                }
                else {
                    angle = GetAngle();
                }
                G.FillEllipse(wbrush, x, y, size, size);
                y = (int)(y + speed * Math.Cos(angle));
                x = (int)(x + speed * Math.Sin(angle));
                G.FillEllipse(curbrush, x, y, size, size);
                Thread.Sleep(100);
            }
        }

        private int Random()
        {
            Random rand = new Random();
            return rand.Next(360);
        }

        private double GetAngle()
        {
            return Math.Atan2((Tpx-x),(Tpy-y));
        }
    }
}
