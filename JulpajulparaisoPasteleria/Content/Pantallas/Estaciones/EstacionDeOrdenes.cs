
using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Content.Pantallas.Estaciones
{
    public class EstacionDeOrdenes : Estacion
    {
        private List<SkinCliente> texturasClientes;
        private GeneradorDeClientes generadorClientes;
        private int cantidadClientesMaxima;
        private List<Cliente> clientesActivosFilaPedido = new List<Cliente>();
        private List<Cliente> clientesActivosFilaEntrega = new List<Cliente>();
        private Fila fila;
        private BotonBase ventanaDeDialogo;
        private int contadorClientes=0;
        private Texture2D texturaTicket;
        private SpriteFont fuentePedido;
        public EstacionDeOrdenes(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes) : base(gestorDeTickets, gestorDePedidos, repositorioImagenes)
        {
            this.fila = new Fila();
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/fondos/estacionOrdenes");
            cantidadClientesMaxima = GestorDeJuego.CantidadDeClientesPorDia;
            int cantidadPersonajes = Constante.CANTIDAD_PERSONAJES;
            texturasClientes = CargadorDeSkins.CargarSkinsClientes(content, cantidadPersonajes);
            generadorClientes = new GeneradorDeClientes(texturasClientes);
            ventanaDeDialogo = new BotonBase(content.Load<Texture2D>("imagenes/Botones/dialogo"), new Rectangle(410, 340, 300, 200));
            texturaTicket = content.Load<Texture2D>("imagenes/EstacionOrdenes/ticket");
            fuentePedido = content.Load<SpriteFont>("Fuentes/fuenteEscritura");
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            if (clientesActivosFilaPedido.Count >0) 
            { 
                Cliente clienteEnPrimeraFila = clientesActivosFilaPedido[0];
                if (clienteEnPrimeraFila.Estado == EstadoCliente.Esperando)
                {
                    if (ventanaDeDialogo.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        clienteEnPrimeraFila.CambiarDeEstado(EstadoCliente.Saliendo);
                        DibujadorDeTicket ticket = new DibujadorDeTicket(new Vector2(base.gestorDeTickets.ObtenerCantidadTickets() > 0 ? base.gestorDeTickets.ObtenerCantidadTickets() * 200 : 0, 00), clienteEnPrimeraFila.pedido, r, texturaTicket, fuentePedido);
                        base.gestorDeTickets.AgregarTikcet(ticket);
                        Torta torta = new Torta(new Rectangle(0, 0, Constante.MEDIDA_TORTA_X, Constante.MEDIDA_TORTA_Y));
                        torta.EstacionActual = EstacionActual.EstacionDeMezcla;
                        gestorDePedidos.AgregarTorta(torta);
                    }
                }
            }
            base.gestorDeTickets.ActualizarTickets(posicionVirtual);
            ActualizarClientes(gameTime);
            ventanaDeDialogo.Actualizar(posicionVirtual);
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
            ActualizarClientes(gameTime);
        }
        private void ActualizarClientes(GameTime gameTime)
        {
            if (clientesActivosFilaPedido.Count > 0)
            {
                
                Cliente clienteEnPrimeraFila = clientesActivosFilaPedido[0];

                if (clienteEnPrimeraFila.TerminoDeSalir)
                {
                    clienteEnPrimeraFila.CambiarPosicionInicial(new Vector2(1920, 100));
                    AniadirClientesAFilaEntrega(clienteEnPrimeraFila);
                    clientesActivosFilaPedido.Remove(clienteEnPrimeraFila);

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
                    gestorDeTickets.EliminarTicket(0);
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
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            foreach (Cliente cliente in clientesActivosFilaEntrega)
            {
                cliente.Dibujar(spriteBatch);
            }
            foreach (Cliente cliente in clientesActivosFilaPedido)
            {
                cliente.Dibujar(spriteBatch);
            }
            if (clientesActivosFilaPedido.Count >0)
            {
                if (clientesActivosFilaPedido[0].Estado == EstadoCliente.Esperando)
                {
                    ventanaDeDialogo.Dibujar(spriteBatch);
                }
            }
            DibujarTickets(spriteBatch);
        }

    }
}