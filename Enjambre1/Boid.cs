using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enjambre1
{
    public class Boid
    {
        public float x { get; set; }
        public float y { get; set; }
        public float x2 { get; set; }
        public float y2 { get; set; }
        public Boid reference { get; set; }
        public float speed { get; set; }
        public float speedx { get; set; }
        public float speedy { get; set; } 
        public int name { get; set; }
        public int sleeptime { get; set; }
        public Alpha Thread { get; set; }
        public static List<Boid> listboids = new List<Boid>();
        
        public Boid(int num, float inix, float iniy, float velo)
        {
            x = inix;
            y = iniy;
            name = num;
            speed = velo;
            sleeptime =(int)(1 /speed);

            if (name != -1)
            {
                if (name == 0)
                {
                    reference = listboids[0];
                }
                else
                {
                    float dboidactual; 
                    float dmin = 10000000;
                    Boid auxboid = null;
                    foreach (Boid compareboid in listboids)
                    {
                        dboidactual = Distancia(x, y, compareboid.x, compareboid.y);
                        if (dboidactual < dmin)
                        {
                            dmin = dboidactual;
                            auxboid = compareboid;
                        }
                    }
                    reference = auxboid;
                }
            }
            listboids.Add(this);
        }
        public float Distancia(float x_act, float y_act,float x_comp, float y_comp)
        {
            float dist = (float)Math.Sqrt((Math.Pow((double)(x_comp - x_act), 2) + Math.Pow((double)(y_comp - y_act), 2)));
            return dist;
        }
        public void Delete()
        {
            listboids.Remove(this);
        }
    }
}
