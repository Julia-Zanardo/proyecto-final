using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public class GestorDeClientes
    {
        private GeneradorDeClientes generadorClientes;
        public List<Cliente> clientesActivosFilaPedido = new List<Cliente>();
        public List<Cliente> clientesActivosFilaEntrega = new List<Cliente>();
        private Fila fila;
        private int cantidadClientesMaxima;
        private int contadorClientes = 0;
        public GestorDeClientes(List<SkinCliente> texturasClientes)
        {
            this.clientesActivosFilaEntrega = new List<Cliente>();
            this.cantidadClientesMaxima = GestorDeJuego.CantidadDeClientesPorDia;
            this.generadorClientes = new GeneradorDeClientes(texturasClientes);
            this.fila = new Fila();
        }
        public void ActualizarClientes(GameTime gameTime)
        {
            if (clientesActivosFilaPedido.Count > 0)
            {
                Cliente clienteEnPrimeraFila = clientesActivosFilaPedido[0];

                if (clienteEnPrimeraFila.TerminoDeSalir)
                {
                    clienteEnPrimeraFila.CambiarPosicionInicial(new Vector2(1920, 100));
                    AniadirClientesAFilaEntrega(clienteEnPrimeraFila);
                    this.clientesActivosFilaPedido.Remove(clienteEnPrimeraFila);

                    for (int i = 0; i < clientesActivosFilaPedido.Count; i++)
                    {
                        Cliente clienteActual = clientesActivosFilaPedido[i];
                        Rectangle posicionFila = fila.obtenerPosicionFilaPedido(i);
                        clienteActual.CaminarHaciaPosicion(posicionFila);
                    }

                }
            }
            if (clientesActivosFilaEntrega.Count > 0)
            {
                clientesActivosFilaEntrega[0].EstaEnPrimeraFila = true;
                if (clientesActivosFilaEntrega[0].TerminoDeSalir)
                {
                    clientesActivosFilaEntrega.Remove(clientesActivosFilaEntrega[0]);
                    for (int i = 0; i < clientesActivosFilaEntrega.Count; i++)
                    {
                        Cliente clienteActual = clientesActivosFilaEntrega[i];
                        Rectangle posicionFila = fila.obtenerPosicionFilaEntrega(i);
                        clienteActual.CaminarHaciaPosicion(posicionFila);
                    }
                }
            }

            if (generadorClientes.Actualizar(gameTime) && contadorClientes < cantidadClientesMaxima)
            {
                clientesActivosFilaPedido.Add(generadorClientes.GenerarCliente(fila.obtenerPosicionFilaPedido(clientesActivosFilaPedido.Count)));
                contadorClientes++;
            }
            foreach (Cliente cliente in clientesActivosFilaPedido)
            {
                cliente.Actualizar(gameTime);
            }
            foreach (Cliente cliente in clientesActivosFilaEntrega)
            {
                cliente.Actualizar(gameTime);
            }
        }
        private void AniadirClientesAFilaEntrega(Cliente cliente)
        {
            cliente.ReiniciarSalida();
            clientesActivosFilaEntrega.Add(cliente);
            Rectangle posicionFila = fila.obtenerPosicionFilaEntrega(clientesActivosFilaEntrega.Count - 1);
            cliente.CambiarPosicionInicial(new Vector2(1920, 100));
            cliente.CaminarHaciaPosicion(posicionFila);
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            foreach (Cliente cliente in clientesActivosFilaEntrega)
            {
                cliente.Dibujar(spriteBatch);
            }
            foreach (Cliente cliente in clientesActivosFilaPedido)
            {
                cliente.Dibujar(spriteBatch);
            }
        }



    }
}
