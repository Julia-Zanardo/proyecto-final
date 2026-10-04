using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public abstract class ObjetoArrastrable
    {
        public Vector2 Posicion { get; set; }
        public Rectangle Area { get;  set; }
        public bool EstaSiendoArrastrado { get; private set; }
        protected Vector2 desplazamiento;
        public ObjetoArrastrable(Vector2 posicionInicial, Rectangle area)
        {
            Posicion = posicionInicial;
            Area = area;
            EstaSiendoArrastrado = false;
        }
        public virtual void ActualizarArrastre(Vector2 posicionVirtual)
        {
            if (ManejoEntrada.ElementoPresionado() && Area.Contains(posicionVirtual))
            {
                EstaSiendoArrastrado = true;
                desplazamiento = Posicion - posicionVirtual;
            }

            if (EstaSiendoArrastrado)
            {
                Posicion = posicionVirtual + desplazamiento;
                Area = new Rectangle((int)Posicion.X, (int)Posicion.Y, Area.Width, Area.Height);
                if (ManejoEntrada.ElementoSoltado())
                {
                    EstaSiendoArrastrado = false;
                }
            }
        }
    }
}
