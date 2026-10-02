using JulpajulparaisoPasteleria.Content.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JulpajulparaisoPasteleria.Content.Botones
{
    public class BotonForma : BotonBase
    {
        public FormaBizcochuelo Forma { get; private set; }
        public BotonForma(Texture2D textura, Rectangle area, FormaBizcochuelo forma) : base(textura, area)
        {
            this.Forma = forma;
        }
    }
}
