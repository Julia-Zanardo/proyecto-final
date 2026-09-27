using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public class DetectorDeColisiones
    {
        public static bool DetectarColision(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }
    }
}
