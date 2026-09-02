using JulpajulparaisoPasteleria.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Botones
{
    public class BotonSabor : BotonBase
    {
        public SaborBizcochuelo Sabor { get; private set; }
        public BotonSabor(Texture2D textura, Rectangle area, SaborBizcochuelo sabor) : base(textura, area)
        {
            this.Sabor = sabor;
        }
    }
}
