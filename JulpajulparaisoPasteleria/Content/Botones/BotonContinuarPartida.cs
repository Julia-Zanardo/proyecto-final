using JulpajulparaisoPasteleria.Content.BaseDeDatos;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JulpajulparaisoPasteleria.Content.Botones
{
    public class BotonContinuarPartida : BotonBase
    {
        public DatosResumenPartida Partida { get; private set; }
        public BotonContinuarPartida(Texture2D textura, Rectangle area, DatosResumenPartida datosResumenPartida) : base(textura, area)
        {
            this.Partida = datosResumenPartida;
        }
    }
}
