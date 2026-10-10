
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
        private BotonBase ventanaDeDialogo;
        private Texture2D texturaTicket;
        private SpriteFont fuentePedido;
        private GestorDeClientes gestorDeClientes;
        public EstacionDeOrdenes(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes, GestorDeClientes gestorDeClientes) : base(gestorDeTickets, gestorDePedidos, repositorioImagenes)
        {
            this.gestorDeClientes = gestorDeClientes;
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/fondos/estacionOrdenes");
            ventanaDeDialogo = new BotonBase(content.Load<Texture2D>("imagenes/Botones/dialogo"), new Rectangle(410, 340, 300, 200));
            texturaTicket = content.Load<Texture2D>("imagenes/EstacionOrdenes/ticket");
            fuentePedido = content.Load<SpriteFont>("Fuentes/fuenteEscritura");
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            if (gestorDeClientes.clientesActivosFilaPedido.Count >0) 
            { 
                Cliente clienteEnPrimeraFila = gestorDeClientes.clientesActivosFilaPedido[0];
                if (clienteEnPrimeraFila.Estado == EstadoCliente.Esperando)
                {
                    if (ventanaDeDialogo.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        clienteEnPrimeraFila.CambiarDeEstado(EstadoCliente.Saliendo);
                        Ticket ticket = new Ticket(new Vector2(base.gestorDeTickets.ObtenerCantidadTickets() > 0 ? base.gestorDeTickets.ObtenerCantidadTickets() * 200 : 0, 00), clienteEnPrimeraFila.pedido, r, texturaTicket, fuentePedido);
                        base.gestorDeTickets.AgregarTikcet(ticket);
                        Torta torta = new Torta(new Rectangle(0, 0, Constante.MEDIDA_TORTA_X, Constante.MEDIDA_TORTA_Y));
                        torta.EstacionActual = EstacionActual.EstacionDeMezcla;
                        gestorDePedidos.AgregarTorta(torta);
                    }
                }
            }
            base.gestorDeTickets.ActualizarTickets(posicionVirtual);
            gestorDeClientes.ActualizarClientes(gameTime);
            ventanaDeDialogo.Actualizar(posicionVirtual);
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
            gestorDeClientes.ActualizarClientes(gameTime);
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            gestorDeClientes.Dibujar(spriteBatch);
            if (gestorDeClientes.clientesActivosFilaPedido.Count >0)
            {
                if (gestorDeClientes.clientesActivosFilaPedido[0].Estado == EstadoCliente.Esperando)
                {
                    ventanaDeDialogo.Dibujar(spriteBatch);
                }
            }
            DibujarTickets(spriteBatch);
        }

    }
}