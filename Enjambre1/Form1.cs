using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Enjambre1
{
    public partial class Form1 : Form
    {
        Pen pc = new Pen(Color.Blue,3);
        Pen pr = new Pen(Color.Red,3);
        Pen po = new Pen(Color.Orange, 3);
        Pen pg = new Pen(Color.LimeGreen, 3);
        Pen pp = new Pen(Color.Purple, 3);

        Pen pcl = new Pen(Color.Blue, 1);
        Pen prl = new Pen(Color.Red, 1);
        Pen pol = new Pen(Color.Orange, 1);
        Pen pgl = new Pen(Color.LimeGreen, 1);
        Pen ppl = new Pen(Color.Purple, 1);

        public static bool aleat = false;

        int radius = 10;

        int k = 5;//Nº boids simulación

        public Boid cursor = new Boid(-1,500,500,1);

        public Form1()
        {
            InitializeComponent();

            //Temporizador:
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            time.Tick += new EventHandler(Timer1);
            time.Interval = 100;
            time.Start();

            int a = (int)DateTime.Now.Ticks;
            Random alea = new Random(a);

            for (int i = 0; i < k; i++)
            {
                float aleat1 = alea.Next(0, 1366);
                float aleat2 = alea.Next(0,768);
                float aleat3 = alea.Next(95,105);
                float vel = aleat3/500;
                Boid boid = new Boid(i,aleat1,aleat2, vel);
            }
            foreach (Boid boid in Boid.listboids )
            {
                Alpha alpha = new Alpha(boid);
                boid.Thread = alpha;
                Thread threadBoid = new Thread(alpha.Mueveprueba);
                threadBoid.Start();
            }
        }

        private void Timer1(Object myObject, EventArgs myEventArgs)
        {
            int conta = 0;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);

            foreach (Boid boid in Boid.listboids)
            {
                if (boid.name != -1)
                {
                    int resto = conta % 5;
                    switch (resto){
                        case 0:
                            {
                                g.DrawEllipse(pc, boid.x - radius / 2, boid.y - radius / 2, radius, radius);
                                Point pt1 = new Point((int)boid.x, (int)boid.y);
                                Point pt2 = new Point((int)(boid.x + 100 * boid.speedx), (int)(boid.y + 100 * boid.speedy));
                                g.DrawLine(pcl, pt1, pt2);
                                break;
                            }
                        case 1:
                            {
                                g.DrawEllipse(pr, boid.x - radius / 2, boid.y - radius / 2, radius, radius);
                                Point pt1 = new Point((int)boid.x, (int)boid.y);
                                Point pt2 = new Point((int)(boid.x + 100 * boid.speedx), (int)(boid.y + 100 * boid.speedy));
                                g.DrawLine(prl, pt1, pt2);
                                break;
                            }
                        case 2:
                            {
                                g.DrawEllipse(po, boid.x - radius / 2, boid.y - radius / 2, radius, radius);
                                Point pt1 = new Point((int)boid.x, (int)boid.y);
                                Point pt2 = new Point((int)(boid.x + 100 * boid.speedx), (int)(boid.y + 100 * boid.speedy));
                                g.DrawLine(pol, pt1, pt2);
                                break;
                            }
                        case 3:
                            {
                                g.DrawEllipse(pg, boid.x - radius / 2, boid.y - radius / 2, radius, radius);
                                Point pt1 = new Point((int)boid.x, (int)boid.y);
                                Point pt2 = new Point((int)(boid.x + 100 * boid.speedx), (int)(boid.y + 100 * boid.speedy));
                                g.DrawLine(pgl, pt1, pt2);
                                break;
                            }
                        case 4:
                            {
                                g.DrawEllipse(pp, boid.x - radius / 2, boid.y - radius / 2, radius, radius);
                                Point pt1 = new Point((int)boid.x, (int)boid.y);
                                Point pt2 = new Point((int)(boid.x + 100 * boid.speedx), (int)(boid.y + 100 * boid.speedy));
                                g.DrawLine(ppl, pt1, pt2);
                                break;
                            }
                    }
                    conta += 1;
                }
            }
            g.DrawEllipse(pc, cursor.x-radius/2, cursor.y-radius/2, radius, radius);
        }
        
        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            aleat = !aleat;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //Añado nuevo boid a la lista:
            k += 1;

            //Creo el boid:
            int a = (int)DateTime.Now.Ticks;
            Random alea = new Random(a);
            float aleat1 = alea.Next(0, 1366);
            float aleat2 = alea.Next(0, 768);
            float aleat3 = alea.Next(95, 105);
            float vel = aleat3 / 500;
            Boid boid = new Boid(k, aleat1, aleat2, vel);

            //Inicializo el hilo:
            Alpha alpha = new Alpha(boid);
            boid.Thread = alpha;
            Thread threadBoid = new Thread(alpha.Mueveprueba);
            threadBoid.Start();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mouse -> boid (-1)
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);
            cursor.x = Cursor.Position.X;
            cursor.y = Cursor.Position.Y - 30;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Boid.listboids[k].Delete();
            k -= 1;
        }
    }
}