using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.BaseDeDatos
{
    public class DatosResumenDia
    {
        public int Dia { get; set; }
        public int Puntuacion { get; set; }
        public int ClientesSatisfechos { get; set; }
        public int ClientesEnojados { get; set; }
        public int PromedioPuntuacion { get; set; }
    }
}
