using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public class GestorDeJuego
    {
        public int DiaActual { get; private set; }
        public int ContadorPedidos { get; private set; }
        public float TiempoEntreClientes { get; private set; }
        public int CantidadDeClientesPorDia { get; private set; }
        public GestorDeJuego()
        {
            DiaActual = 1;
            ContadorPedidos = 0;
            TiempoEntreClientes = 10;
            CantidadDeClientesPorDia = 3;
        }
        public int ObtenerIdPedido()
        {
            return ++ContadorPedidos;
        }
        public void AvanzarDeDia()
        {
            DiaActual++;
            ContadorPedidos = 0;
            TiempoEntreClientes -= 5; // despues lo cambio con una clase aleatorio o algo asi
            CantidadDeClientesPorDia++;
        }

    }

}
