using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public class GestorDeTickets
    {
        public List<Ticket> TicketsActuales { get; private set; } = new List<Ticket> { };
        private  Rectangle areaDeTicket = new Rectangle(1530, 0, 900, 200);
        private  bool areaDeTicketOcupada = false;
        private int contadorDeTickets = 0;
        public bool ticketListoParaEntregar = false;
        public  void AgregarTikcet(Ticket nuevoTicket)
        {
            TicketsActuales.Add(nuevoTicket);
            contadorDeTickets++;
        }
        public void ActualizarTickets(Vector2 posVirtual)
        {
            foreach (var ticket in TicketsActuales)
            {
                if (ticket.Escala == Constante.ESCALA_TICKET / 2)
                {
                    ticket.AcomodarTicket(ticket.AreaTicketDefaut, Constante.ESCALA_TICKET);
                }
                ticket.Actualizar(posVirtual);

                if (!ticket.EstaSiendoArrastrado && ManejoEntrada.ElementoSoltado())
                {
                    if (!areaDeTicketOcupada && DetectorDeColisiones.DetectarColision(ticket.Area, areaDeTicket) && ManejoEntrada.ElementoSoltado())
                    {
                        areaDeTicketOcupada = true;
                        ticket.AcomodarTicket(areaDeTicket, ticket.Escala == Constante.ESCALA_TICKET ? ticket.Escala * 2 : ticket.Escala);
                    }
                    if (DetectorDeColisiones.DetectarColision(ticket.Area, ticket.AreaTicketDefaut) && ManejoEntrada.ElementoSoltado())
                    {
                        areaDeTicketOcupada = false;
                        ticket.AcomodarTicket(ticket.AreaTicketDefaut, ticket.Escala == Constante.ESCALA_TICKET * 2 ? ticket.Escala / 2 : ticket.Escala);
                    }
                }
            }
        }
        public int ObtenerCantidadTickets()
        {
            return contadorDeTickets;
        }
        public void EliminarTicket(Ticket ticketAEliminar)
        {
            TicketsActuales.Remove(ticketAEliminar);
        }
        public void ActualizarTicketsEnZonaDeEntrega(Rectangle areaDeEntrega, Vector2 posicionVirtual, Pedido pedido)
        {
            foreach (Ticket ticket in TicketsActuales)
            {
                ticket.Actualizar(posicionVirtual);
                if (!ticket.EstaSiendoArrastrado && ManejoEntrada.ElementoSoltado())
                {
                    if (!areaDeTicketOcupada && DetectorDeColisiones.DetectarColision(ticket.Area, areaDeTicket))
                    {
                        areaDeTicketOcupada = true;
                        float escalaTicket;
                        ticketListoParaEntregar = false;
                        if (ticket.Escala == Constante.ESCALA_TICKET)
                        {
                            escalaTicket = ticket.Escala * 2;
                        }
                        else if (ticket.Escala == Constante.ESCALA_TICKET / 2f)
                        {
                            escalaTicket = (ticket.Escala * 4);
                        }
                        else
                        {
                            escalaTicket = ticket.Escala;
                        }
                        ticket.AcomodarTicket(areaDeTicket, escalaTicket);
                    }
                    else if (DetectorDeColisiones.DetectarColision(ticket.Area, areaDeEntrega) && (pedido == ticket.pedido))
                    {
                        Rectangle areaDeTicketEntrega = new Rectangle(areaDeEntrega.X + 30, areaDeEntrega.Y + 50, areaDeEntrega.Width, areaDeEntrega.Height);
                        float escalaTicket;
                        ticketListoParaEntregar = true;
                        areaDeTicketOcupada = false;
                        if (ticket.Escala == Constante.ESCALA_TICKET * 2)
                        {
                            escalaTicket = ticket.Escala /4;
                        }
                        else if (ticket.Escala == Constante.ESCALA_TICKET)
                        {
                            escalaTicket = ticket.Escala / 2f;
                        }
                        else
                        {
                            escalaTicket = ticket.Escala;
                        }
                        ticket.AcomodarTicket(areaDeTicketEntrega, escalaTicket);
                        ticket.puestoEnZonaDeEntrega = true;
                    }
                    else if (DetectorDeColisiones.DetectarColision(ticket.Area, ticket.AreaTicketDefaut))
                    {
                        areaDeTicketOcupada = false;
                        float escalaTicket;
                        ticketListoParaEntregar = false;
                        if (ticket.Escala == Constante.ESCALA_TICKET / 2)
                        {
                            escalaTicket = ticket.Escala * 2;
                        }
                        else if (ticket.Escala == Constante.ESCALA_TICKET * 2 )
                        {
                            escalaTicket = ticket.Escala / 2f;
                        }
                        else 
                        {
                            escalaTicket = ticket.Escala;
                        }
                        ticket.AcomodarTicket(ticket.AreaTicketDefaut, escalaTicket);
                    }
                }
            }
        }
        public Ticket ObtenerTikcetSegunPedido(Pedido pedido) { 
            int indice = 0;
            while(indice < TicketsActuales.Count) 
            {
                if(pedido == TicketsActuales[indice].pedido)
                {
                    return TicketsActuales[indice];
                }
                indice++;
            }
            return null;
        }
    }
}
