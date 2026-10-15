using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Content.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Logica
{
    public class RegistroDeDia
    {
        public int PuntuacionAcumuladaTotal { get; private set; }
        public int PuntuacionAcumuladaOrdenes { get; private set; }
        public int PuntuacionAcumuladaMezcla { get; private set; }
        public int PuntuacionAcumuladaHorneado { get; private set; }
        public int PuntuacionAcumuladaDecoracion { get; private set; }
        public void RegistrarEntrega(int ordenes, int mezcla, int horneado, int decoracion, int total)
        {
            PuntuacionAcumuladaOrdenes += ordenes;
            PuntuacionAcumuladaMezcla += mezcla;
            PuntuacionAcumuladaHorneado += horneado;
            PuntuacionAcumuladaDecoracion += decoracion;
            PuntuacionAcumuladaTotal += total;
        }
        public void ReiniciarAcumuladoresDia()
        {
            this.PuntuacionAcumuladaOrdenes = 0;
            this.PuntuacionAcumuladaMezcla = 0;
            this.PuntuacionAcumuladaHorneado = 0;
            this.PuntuacionAcumuladaDecoracion = 0;
            this.PuntuacionAcumuladaTotal = 0;
        }
    }
}
