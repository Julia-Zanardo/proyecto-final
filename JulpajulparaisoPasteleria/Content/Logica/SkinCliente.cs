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
        public Texture2D Caminando { get; private set; }
        public Texture2D Esperando { get; private set; }
        public int ColumnasCaminando { get; private set; }
        public int ColumnasEsperando { get; private set; }
        public int ColumnasAtendido { get; private set; }
        public SkinCliente(Texture2D caminando, Texture2D esperando, int ColumnasCaminando, int ColumnasEsperando, int ColumnasAtendido)
        {
            this.Caminando = caminando;
            this.Esperando = esperando;
            this.ColumnasCaminando = ColumnasCaminando;
            this.ColumnasEsperando = ColumnasEsperando;
            this.ColumnasAtendido = ColumnasAtendido;
        }
    }
}
