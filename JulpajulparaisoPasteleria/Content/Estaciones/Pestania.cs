using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace JulpajulparaisoPasteleria.Content.Estaciones
{
    public class Pestania
    {
        private Rectangle area { get; set; }
        private int IndiceEstacion { get; set; }
        public Pestania(Rectangle area, int indiceEstacion)
        {
            this.area = area;
            this.IndiceEstacion = indiceEstacion;
        }
        public bool fueClickeada(Point posicionMouse)
        {
            return area.Contains(posicionMouse);
        }
        public void ActualizarArea(Rectangle nuevaArea)
        {
            this.area = nuevaArea;
        }

    }
}
