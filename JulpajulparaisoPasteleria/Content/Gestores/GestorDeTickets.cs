using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Content.Gestores
{
    public class GestorDeTickets
    {
        public static List<DibujadorDeTicket> TicketsActuales { get; private set; } = new List<DibujadorDeTicket> { };
        private static Rectangle areaDeTicket = new Rectangle(1450, 0, 900, 200);
        private static bool areaDeTicketOcupada = false;
        public static void AgregarTikcet(DibujadorDeTicket nuevoTicket)
        {
            TicketsActuales.Add(nuevoTicket);
        }
        public static void ActualizarTickets(Vector2 posVirtual)
        {
            foreach (var ticket in TicketsActuales)
            {
                ticket.Actualizar(posVirtual);

                if (!ticket.EstaSiendoArrastrado && ManejoEntrada.ElementoSoltado())
                {
                    if (!areaDeTicketOcupada && DetectorDeColisiones.DetectarColision(ticket.AreaTicket, areaDeTicket) && ManejoEntrada.ElementoSoltado())
                    {
                        areaDeTicketOcupada = true;
                        ticket.AcomodarTicket(areaDeTicket, ticket.Escala == Constante.ESCALA_TICKET ? ticket.Escala * 2 : ticket.Escala);
                    }
                    if (DetectorDeColisiones.DetectarColision(ticket.AreaTicket, ticket.AreaTicketDefaut) && ManejoEntrada.ElementoSoltado())
                    {
                        areaDeTicketOcupada = false;
                        ticket.AcomodarTicket(ticket.AreaTicketDefaut, ticket.Escala == Constante.ESCALA_TICKET * 2 ? ticket.Escala / 2 : ticket.Escala);
                    }

                }
            }
        }
        public static int ObtenerCantidadTickets()
        {
            return TicketsActuales.Count;
        }
    }
}
