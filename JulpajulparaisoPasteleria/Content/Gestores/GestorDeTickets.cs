using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public class GestorDeTickets
    {
        public List<DibujadorDeTicket> TicketsActuales { get; private set; } = new List<DibujadorDeTicket> { };
        private  Rectangle areaDeTicket = new Rectangle(1530, 0, 900, 200);
        private  bool areaDeTicketOcupada = false;
        private int contadorDeTickets = 0;
        public  void AgregarTikcet(DibujadorDeTicket nuevoTicket)
        {
            TicketsActuales.Add(nuevoTicket);
            contadorDeTickets++;
        }
        public void ActualizarTickets(Vector2 posVirtual)
        {
            foreach (var ticket in TicketsActuales)
            {
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
        public void EliminarTicket(DibujadorDeTicket ticketAEliminar)
        {
            TicketsActuales.Remove(ticketAEliminar);
        }
        public DibujadorDeTicket verificarColision(Rectangle area)
        {
            foreach (DibujadorDeTicket ticket in TicketsActuales)
            {
                if (DetectorDeColisiones.DetectarColision(area, ticket.Area))
                {
                    return ticket;
                }
            }
            return null;
        }
        public DibujadorDeTicket obtenerTikcetSegunPedido(Pedido pedido) { 
            int indice = 0;
            while(indice < TicketsActuales.Count) 
            {
                if(pedido == TicketsActuales[indice].pedido)
                {
                    return TicketsActuales[indice];
                }
            }
            return null;
        }
    }
}
