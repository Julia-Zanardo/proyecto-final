
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
        private List<Cliente> clientesActivos = new List<Cliente>();
        private Fila fila;
        private BotonBase ventanaDeDialogo;
        private ContentManager contentManager;
        private int contadorClientes=0;
        public EstacionDeOrdenes()
        {
            this.fila = new Fila();
        }
        public override void LoadContent(ContentManager content)
        {
            contentManager = content;
            Fondo = content.Load<Texture2D>("imagenes/fondos/estacionOrdenes");
            cantidadClientesMaxima = GestorDeJuego.CantidadDeClientesPorDia;
            int cantidadPersonajes = Constante.CANTIDAD_PERSONAJES;
            texturasClientes = CargadorDeSkins.CargarSkinsClientes(content, cantidadPersonajes);
            generadorClientes = new GeneradorDeClientes(texturasClientes);
            ventanaDeDialogo = new BotonBase(content.Load<Texture2D>("imagenes/Botones/dialogo"), new Rectangle(410, 340, 300, 200));
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            if (clientesActivos.Count >0) 
            { 
                Cliente clienteEnPrimeraFila = clientesActivos[0];
                switch (clienteEnPrimeraFila.Estado)
                {
                    case EstadoCliente.Caminando:
                        if (DetectorDeColisiones.DetectarColision(clientesActivos[0].AreaCliente, fila.posiciones[0]))
                        {
                            clienteEnPrimeraFila.CambiarDeEstado(EstadoCliente.Esperando);
                        }
                        break;
                    case EstadoCliente.Esperando:
                        if (ventanaDeDialogo.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                        {
                            clienteEnPrimeraFila.CambiarDeEstado(EstadoCliente.Saliendo);
                            DibujadorDeTicket ticket = new DibujadorDeTicket(new Vector2(base.gestorDeTickets.ObtenerCantidadTickets()>0?base.gestorDeTickets.ObtenerCantidadTickets()*200:0, 00), clienteEnPrimeraFila.pedido);
                            ticket.LoadContent(contentManager);
                            base.gestorDeTickets.AgregarTikcet(ticket);
                        }

                        break;
                    case EstadoCliente.Saliendo:
                        if (clienteEnPrimeraFila.TerminoDeSalir)
                        {
                            clientesActivos.Remove(clienteEnPrimeraFila);
                            for(int i = 0; i<clientesActivos.Count; i++)
                            {
                                Cliente clienteActual = clientesActivos[i];
                                Rectangle posicionFila = fila.obtenerPosicionFila(i);
                                clienteActual.CaminarHaciaPosicion(posicionFila);
                            }
                        }
                        break;
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
            if (generadorClientes.Actualizar(gameTime) && contadorClientes < cantidadClientesMaxima)
            {
                clientesActivos.Add(generadorClientes.GenerarCliente(fila.obtenerPosicionFila(clientesActivos.Count)));
                contadorClientes++;
                Torta torta = new Torta();
                torta.CambiarDeEstacion(EstacionActual.EstacionDeOrdenes);
                gestorDePedidos.AgregarTorta(torta);
            }
            foreach (Cliente cliente in clientesActivos)
            {
                cliente.Actualizar(gameTime);
            }
        }

        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            foreach (Cliente cliente in clientesActivos)
            {
                cliente.Dibujar(spriteBatch);
            }
            if (clientesActivos.Count >0)
            {
                switch (clientesActivos[0].Estado)
                {
                    case EstadoCliente.Esperando:
                        ventanaDeDialogo.Dibujar(spriteBatch);
                        break;
                    case EstadoCliente.Saliendo:
                        break;
                }
            }
            DibujarTickets(spriteBatch);
        }

    }
}
