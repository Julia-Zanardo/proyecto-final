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
    public class BotonCobertura : BotonBase
    {
        public TipoCobertura Cobertura { get; private set; }
        public BotonCobertura(Texture2D textura, Rectangle area, TipoCobertura cobertura) : base(textura, area)
        {
            this.Cobertura = cobertura;
        }
    }
}
