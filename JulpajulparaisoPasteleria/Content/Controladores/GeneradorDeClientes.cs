using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Modelos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public class GeneradorDeClientes
    {
        private GestorDeJuego gestor;
        private float cronometro;
        private List<SkinCliente> texturasClientes = new List<SkinCliente>();

        public GeneradorDeClientes(GestorDeJuego gestor, List<SkinCliente> texturas)
        {
            this.gestor = gestor;
            this.texturasClientes = texturas;
            cronometro = 0f;
        }
        public bool Actualizar(GameTime tiempo)
        {
            cronometro += (float)tiempo.ElapsedGameTime.TotalSeconds;
            if (cronometro >= gestor.TiempoEntreClientes)
            {
                cronometro = 0f;
                return true;
            }
            return false;
        }
        public Cliente GenerarCliente(Vector2 objetivo)
        {
            FabricaDePedidos generadorDePedidos = new FabricaDePedidos();
            Random random = new Random();
            int indice = random.Next(texturasClientes.Count);
            SkinCliente skin = texturasClientes[indice];
            Vector2 inicio = new Vector2(1920, 200);
            Pedido pedido = generadorDePedidos.CrearPedido(gestor.ObtenerIdPedido());
            Cliente nuevoCliente = new Cliente(skin, inicio, objetivo, pedido);
            return nuevoCliente;
        }
    }
}
