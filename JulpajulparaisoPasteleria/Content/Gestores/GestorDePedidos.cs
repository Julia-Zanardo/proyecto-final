using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public class GestorDePedidos
    {
        public List<Torta> tortas { get; private set; } = new List<Torta>();
        public void AgregarTorta(Torta torta)
        {
            tortas.Add(torta);
        }
        public void QuitarTorta(Torta torta)
        {
            tortas.Remove(torta);
        }
        public int getCantidadDeTortas()
        {
            return tortas.Count;
        }
        public List<Torta> getTortasEstacion(EstacionActual estacion)
        {
            List<Torta> tortasEstacion = new List<Torta>();
            foreach (Torta torta in tortas)
            {
                if (torta.EstacionActual == estacion)
                {
                    tortasEstacion.Add(torta);
                }
            }
            return tortasEstacion;
        }
    }
}
