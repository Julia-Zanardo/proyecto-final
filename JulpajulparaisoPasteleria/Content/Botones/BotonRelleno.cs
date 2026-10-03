using JulpajulparaisoPasteleria.Content.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Botones
{
    public class BotonRelleno : BotonBase
    {
        public SaborRelleno Sabor { get; private set; }
        public BotonRelleno(Texture2D textura, Rectangle area, SaborRelleno sabor) : base(textura, area)
        {
            this.Sabor = sabor;
        }
    }
}
