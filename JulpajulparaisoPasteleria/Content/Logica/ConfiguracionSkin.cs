using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class ConfiguracionSkin
    {
        public int ColumnasCaminando { get; private set; }
        public int ColumnasEsperando { get; private set; }
        public int ColumnasAtendido { get; private set; }
        public ConfiguracionSkin(int ColumnasCaminando, int ColumnasEsperando, int ColumnasAtendido) 
        {
            this.ColumnasAtendido = ColumnasAtendido;
            this.ColumnasCaminando = ColumnasCaminando;
            this.ColumnasEsperando = ColumnasEsperando;
        }
    }
}
