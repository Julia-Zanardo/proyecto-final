using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Estaciones
{
    public class EstacionDeOrdenes : Estacion
    {
        private DetectorDeColisiones detectorDeColisiones;
        private List<SkinCliente> texturasClientes;
        private GestorDeJuego gestor;
        private GeneradorDeClientes generadorClientes;
        private int cantidadClientesMaxima;
        private List<Cliente> clientesActivos = new List<Cliente>();
        private Fila fila;
        private BotonBase ventanaDeDialogo;
        private Texture2D texturaDialogo;
        private bool clienteEsperando = false;
        private bool dialogoClickeado = false;
        private DibujadorDeTicket ticket;
        private Rectangle areaDeTickets;
        public EstacionDeOrdenes(GestorDeJuego gestor)
        {
            this.gestor = gestor;
            this.fila = new Fila();
            this.detectorDeColisiones = new DetectorDeColisiones();
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/fondos/estacionOrdenes");
            cantidadClientesMaxima = gestor.CantidadDeClientesPorDia;
            int cantidadPersonajes = Constante.CANTIDAD_PERSONAJES;
            texturasClientes = CargadorDeSkins.CargarSkinsClientes(content, cantidadPersonajes);
            generadorClientes = new GeneradorDeClientes(gestor, texturasClientes);
            texturaDialogo = content.Load<Texture2D>("imagenes/Botones/dialogo");
            ventanaDeDialogo = new BotonBase(texturaDialogo, new Rectangle(410, 340, 300, 200));
            ticket = new DibujadorDeTicket();
            ticket.LoadContent(content);
            areaDeTickets = new Rectangle(0, 0, 900, 200);
        }
        public override void Update(GameTime gameTime, Vector2 posicionVirtual)
        {
            if (generadorClientes.Actualizar(gameTime) && clientesActivos.Count < cantidadClientesMaxima)
            {
                clientesActivos.Add(generadorClientes.GenerarCliente(fila.obtenerPosicionFila(clientesActivos.Count)));
            }
            foreach (Cliente cliente in clientesActivos)
            {
                cliente.Actualizar(gameTime);
            }
            if (clientesActivos.Count > 0 && !clienteEsperando)
            {
                if (detectorDeColisiones.DetectarColision(clientesActivos[0].AreaCliente, fila.posiciones[0]))
                {
                    clienteEsperando = true;
                }
            }
                if (clienteEsperando && !dialogoClickeado)
                {
                    if (ventanaDeDialogo.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        clienteEsperando = false;
                        dialogoClickeado = true;
                        
                    }
                }
            if (dialogoClickeado)
            {
                ticket.Actualizar(posicionVirtual);
                if(detectorDeColisiones.DetectarColision(ticket.AreaTicket, areaDeTickets) && ManejoEntrada.ElementoSoltado())
                {
                    ticket.acomodarTicket(areaDeTickets, ticket.Escala==Constante.ESCALA_TICKET? ticket.Escala/2 : ticket.Escala);
                }
                if(detectorDeColisiones.DetectarColision(ticket.AreaTicket, ticket.AreaTicketDefaut) && ManejoEntrada.ElementoSoltado())
                {
                    ticket.acomodarTicket(ticket.AreaTicketDefaut, ticket.Escala==Constante.ESCALA_TICKET/2 ? ticket.Escala*2 : ticket.Escala);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Cliente cliente in clientesActivos)
            {
                cliente.Dibujar(spriteBatch);
            }
            if (clienteEsperando && !dialogoClickeado)
            {
                ventanaDeDialogo.Dibujar(spriteBatch);
            }
            else if (dialogoClickeado)
            {
                ticket.Dibujar(spriteBatch);
            }
        }

    }
}
