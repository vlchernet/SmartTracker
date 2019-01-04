using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Tracking
{
    class ThreadMaker
    {
        private int x, y;
        private static int speed;
        private static int size;
        private Brush drawbrush;
        private static Color col;
        private static Color prevcol;
        private Brush wipebrush = new SolidBrush(SystemColors.Control);
        private Graphics G { get; set; } = Graphics.FromHwnd(Form1.ActiveForm.Handle);
        public static int Tpx { get => tpx; set => tpx = value; }
        public static int Tpy { get => tpy; set => tpy = value; }
        public static bool Target { get => target; set => target = value; }
        public static bool Escape { get => escape; set => escape = value; }
        public static Color Col { get => col; set => col = value; }
        public static Color Prevcol { get => prevcol; set => prevcol = value; }
        public static int Speed { get => speed; set => speed = value; }
        public static int Size { get => size; set => size = value; }

        private static bool escape = false;
        private static bool target = false;
        private static int tpx;
        private static int tpy;
        private int eps = 2;
        private static DateTime dob;

        public ThreadMaker(int x, int y, int size, Color color)
        {
            this.x = x;
            this.y = y;
            Col = color;
            drawbrush = new SolidBrush(color);
            Size = size;
            dob = DateTime.Now;
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
                else
                {
                    angle = GetAngle();
                }
                G.FillEllipse(wipebrush, x, y, Size, Size);
                y = (int)(y + Speed * Math.Cos(angle));
                x = (int)(x + Speed * Math.Sin(angle));
                drawbrush = new SolidBrush(Col);
                Hit kill = new Hit(() => ((Tpx - x) < eps) && ((Tpy - y) < eps));
                Hit notRecent = new Hit(() => (DateTime.Now.Subtract(dob).TotalMilliseconds > 500));
                if (kill.Invoke() && notRecent.Invoke())
                {
                    try
                    {
                        drawbrush = new SolidBrush(Color.Orange);
                        G.FillEllipse(drawbrush, x, y, Size * 2, Size * 2);
                        Thread.Sleep(100);
                        G.FillEllipse(wipebrush, x, y, Size * 2, Size * 2);
                        Thread.CurrentThread.Abort();
                    }
                    catch (ThreadAbortException tae)
                    {
                        Console.WriteLine(tae.Message + "Thread is dead!");
                    }
                }
                G.FillEllipse(drawbrush, x, y, Size, Size);
                Thread.Sleep(100);
            }
        }

        public delegate bool Hit();

        private int Random()
        {
            Random rand = new Random();
            return rand.Next(360);
        }

        private double GetAngle()
        {
            return Math.Atan2((Tpx - x), (Tpy - y));
        }

        private bool IsNearby(int eps)
        {
            return (Tpx - x) < eps && (Tpy - y) < eps;
        }
    }
}
