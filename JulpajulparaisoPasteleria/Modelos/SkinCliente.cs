using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Modelos
{
    public class SkinCliente
    {
        public Texture2D caminando { get; set; }
        public Texture2D esperando { get; set; }
        public SkinCliente(Texture2D caminando, Texture2D esperando)
        {
            this.caminando = caminando;
            this.esperando = esperando;
        }
    }
}
