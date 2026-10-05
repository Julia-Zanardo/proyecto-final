using JulpajulparaisoPasteleria.Content.Personajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public class GestorDeClientes
    {
        public List<Cliente> clientesEsperandoEntrega { get; set; }

        public GestorDeClientes()
        {
            clientesEsperandoEntrega = new List<Cliente>();
        }
        public void agregarCliente(Cliente cliente)
        {
            clientesEsperandoEntrega.Add(cliente);
        }
        public void eliminarCliente(Cliente cliente)
        {
            clientesEsperandoEntrega.Remove(cliente);
        }
    }
}
