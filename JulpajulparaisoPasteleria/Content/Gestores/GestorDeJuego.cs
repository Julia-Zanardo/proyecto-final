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
        public static bool DiaTerminado;
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
            TiempoEntreClientes -= 5; // despues lo cambio con una clase aleatorio o algo asi
            CantidadDeClientesPorDia++;
            CantidadDeClientesAntendidosHoy = 0;
        }
        public static void ReiniciarPedidos() 
        {
            ContadorPedidos = 0;
        }
        public static void GuardarPartida(string nombreJugador, int promedio, int satisfechos, int enojados)
        {
            AvanzarDeDia();

            RepositorioPartida.GuardarProgreso(nombreJugador, promedio, satisfechos, enojados);
        }
    }

}
