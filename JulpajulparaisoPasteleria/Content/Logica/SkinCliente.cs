using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class SkinCliente
    {
        public Texture2D caminando { get; set; }
        public Texture2D esperando { get; set; }
        public int ColumnasCaminando { get; private set; } = 12;
        public int ColumnasEsperando { get; private set; } = 9;
        public SkinCliente(Texture2D caminando, Texture2D esperando)
        {
            this.caminando = caminando;
            this.esperando = esperando;
        }
    }
}
