using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public static class GestorDeJuego
    {
        public static int DiaActual { get; private set; } = 1;
        public static int ContadorPedidos { get; private set; } = 0;
        public static float TiempoEntreClientes { get; private set; } = 3;
        public static int CantidadDeClientesPorDia { get; private set; } = 3;
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
        }

    }

}
