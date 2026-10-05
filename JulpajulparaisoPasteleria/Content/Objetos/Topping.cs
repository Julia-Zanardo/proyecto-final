using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace JulpajulparaisoPasteleria.Content.Objetos
{
    public class Topping : ObjetoArrastrable
    {
        public TipoTopping tipoTopping { get; private set; }
        public Texture2D textura { get; private set; }
        public bool puestoEnTorta { get; set; } = false;
        public Topping(TipoTopping tipoTopping, Texture2D textura, Rectangle area) : base(new Vector2(area.X, area.Y), area)
        {
            this.tipoTopping = tipoTopping;
            this.textura = textura;
        }
        public void Actualizar(Vector2 posVirtual)
        {
            base.ActualizarArrastre(posVirtual);
            base.Area = new Rectangle((int)base.Posicion.X, (int)base.Posicion.Y, Area.Width, Area.Height);
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, base.Area, Color.White);
        }
        public void ActualizarPosicion(Rectangle nuevaPosicion)
        {
            this.Area = nuevaPosicion;
        }
    }
}
