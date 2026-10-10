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
        public int ColumnasSaliendo { get; private set; }
        public int ColumasEnojado { get; private set; }
        public int ColumnasHablando { get; private set; }
        public ConfiguracionSkin(int ColumnasCaminando, int ColumnasEsperando, int ColumnasSaliendo, int ColumnasEnojado, int ColumnasHablando) 
        {
            this.ColumnasSaliendo = ColumnasSaliendo;
            this.ColumnasCaminando = ColumnasCaminando;
            this.ColumnasEsperando = ColumnasEsperando;
            this.ColumasEnojado = ColumnasEnojado;
            this.ColumnasHablando = ColumnasHablando;
        }
    }
}
