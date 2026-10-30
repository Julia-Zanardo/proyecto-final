using JulpajulparaisoPasteleria.Content.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public static class GestorDeJuego
    {
        public static int DiaActual { get; set; } = 1;
        public static int ContadorPedidos { get; private set; } = 0;
        public static float TiempoEntreClientes { get;  set; } = 15;
        public static int CantidadDeClientesPorDia { get;  set; } = 1;
        public static int CantidadDeClientesAntendidosHoy { get;  set; } = 0;
        public static int CantidadDeClientesEnojados { get; set; } = 0;
        public static int CantidadDeClientesFelices { get; set; } = 0;
        public static int SlotActual { get; set; } = 0;
        public static void RegistrarCliente()
        {
            CantidadDeClientesAntendidosHoy++;
        }
        public static bool ObtenerSiDiaTermino() 
        {
            if (CantidadDeClientesAntendidosHoy >= CantidadDeClientesPorDia)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static int ObtenerIdPedido()
        {
            return ++ContadorPedidos;
        }
        public static void AvanzarDeDia()
        {
            DiaActual++;
            ContadorPedidos = 0;
            TiempoEntreClientes -= 5; 
            CantidadDeClientesPorDia++;
            CantidadDeClientesAntendidosHoy = 0;
        }
        public static void GuardarPartida(string numeroDePartida, int promedio, int satisfechos, int enojados)
        {
            RepositorioPartida.GuardarProgreso(numeroDePartida, promedio, satisfechos, enojados);
        }
        public static void ReiniciarPartidaNueva()
        {
            DiaActual = 1;
            ContadorPedidos = 0;
            TiempoEntreClientes = 15; 
            CantidadDeClientesPorDia = 1; 
            CantidadDeClientesAntendidosHoy = 0;
            CantidadDeClientesEnojados = 0;
            CantidadDeClientesFelices = 0;
        }
    }

}
