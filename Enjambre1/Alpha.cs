using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enjambre1
{
    public class Alpha
    {
        private Boid boid;

        public Alpha(Boid boid)
        {
            this.boid = boid;
        }

        public float Distancia(float distx, float disty)
        {
            float dist = (float)Math.Sqrt((Math.Pow((double)distx, 2) + Math.Pow((double)disty, 2)));
            return dist;
        }

        public void Mueveprueba()
        {
            //Variable aleatoria se inicia aquí para evitar que todos los boid tengan la misma dirección de movimiento
            int a = (int)DateTime.Now.Ticks;
            Random ale = new Random(a);
            //Radians se inicializa fuera del while para poder llamarla tanto en modo aleatorio como con referencia
            //y que conserve valor en los cambios de un modo al otro
            double radians = 0;

            while (true)
            {
                if (boid.name != -1)
                {
                    if (Form1.aleat == false)
                    {
                        float distancex = boid.reference.x - boid.x;
                        float distancey = boid.reference.y - boid.y;
                        float distance = Distancia(distancex, distancey);

                        radians = Math.Atan2(distancey, distancex);

                        boid.speedx = (float)(boid.speed * Math.Cos(radians));
                        boid.speedy = (float)(boid.speed * Math.Sin(radians));

                        boid.x += boid.speedx;
                        boid.y += boid.speedy;

                        //compruebo que no se acerca más de 30 uds al líder:

                        distancex = boid.reference.x - boid.x;
                        distancey = boid.reference.y - boid.y;
                        distance = Distancia(distancex, distancey);

                        if (distance < 30)
                        {
                            float pasadax = 30 * (float)Math.Cos(radians) - distancex;
                            float pasaday = 30 * (float)Math.Sin(radians) - distancey;

                            boid.x -= pasadax;
                            boid.y -= pasaday;
                        }
                        //*NOTA: Esta condición de no acercarse más de 10 uds al líder provoca que aunque un void esté parado (a 60 uds del líder)
                        //      el vector (gráfico) indicador de velocidad sigue teniendo módulo y dirección ya que el boid nunca alcanza su referencia
                    }
                    else
                    {
                        boid.speedx = (float)(boid.speed * Math.Cos(radians));
                        boid.speedy = (float)(boid.speed * Math.Sin(radians));

                        boid.x += boid.speedx;
                        boid.y += boid.speedy;

                        //Limites pantalla: 1366 x 768 (-) radio boids y marco superior ventana ~20 pixel
                        //ángulos: 2 * pi = 6.2832 (aleatorio solo admite tipo int)

                        if (boid.x > 1361)
                        {
                            radians = ale.Next(1571, 3*1571) /1000; // pi/2 y 3*pi/2
                        }
                        if (boid.x < 5)
                        {
                            radians = ale.Next(-1571, 1571) / 1000;// -pi/2 y pi/2
                        }
                        if (boid.y > 733)
                        {
                            radians = ale.Next(3142, 6283) / 1000; // pi y 2*pi
                        }
                        if (boid.y < 5)
                        {
                            radians = ale.Next(0, 3142) / 1000; // 0 y pi
                        }

                        boid.speedx = (float)(boid.speed * Math.Cos(radians));
                        boid.speedy = (float)(boid.speed * Math.Sin(radians));

                        boid.x += boid.speedx;
                        boid.y += boid.speedy;
                    }
                }
                Thread.Sleep(boid.sleeptime);
            }
        }
    }
}