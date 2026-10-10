using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Objetos;
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

namespace JulpajulparaisoPasteleria.Content.Pantallas.Estaciones
{
    public class EstacionDeEntrega : Estacion
    {
        private GestorDeClientes gestorDeClientes;
        private Rectangle[] posicionesTortas;
        private Cliente clienteActual;
        private Rectangle areaTicket = new Rectangle(800, 400, 150, 230);
        private Rectangle areaBasura = new Rectangle(200, 700, 100, 100);
        private Rectangle areaTortaEntregar = new Rectangle(800, 750, Constante.MEDIDA_TORTA_X, Constante.MEDIDA_TORTA_Y);
        private BotonBase botonEntregar;
        private CalculadorDePuntuacion calculadorDePuntuacion = new CalculadorDePuntuacion();
        private Torta tortaAEntregar = null;
        public EstacionDeEntrega(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes, GestorDeClientes gestorDeClientes) : base(gestorDeTickets, gestorDePedidos, repositorioImagenes)
        {
            this.gestorDeClientes = gestorDeClientes;
            posicionesTortas = new Rectangle[] { 
                new Rectangle(100,420,Constante.MEDIDA_TORTA_X,Constante.MEDIDA_TORTA_Y/2),
                new Rectangle(400,420,Constante.MEDIDA_TORTA_X,Constante.MEDIDA_TORTA_Y/2) };
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionDeEntrega");
            botonEntregar = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonEntregar"), new Rectangle(1500, 750, 300, 100));
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            Torta tortaAEliminar = null;
            if (gestorDeClientes.clientesEsperandoEntrega.Count > 0)
            {
                clienteActual = gestorDeClientes.clientesEsperandoEntrega[0];
            }
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeEntrega);
            for (int i = 0; i < tortasEstacion.Count; i++)
            {
                Torta torta = tortasEstacion[i];
                if (!torta.EstaSiendoArrastrado)
                {
                    if (torta.ListaParaEntregar)
                    {
                        torta.cambiarArea(areaTortaEntregar);
                    } else if (i < posicionesTortas.Length)
                    {
                        torta.cambiarArea(posicionesTortas[i]);
                    }
                }
                torta.Actualizar(posicionVirtual);
                if (!torta.ListaParaEntregar && torta.EstaSiendoArrastrado)
                {
                    if (DetectorDeColisiones.DetectarColision(torta.Area, areaTortaEntregar))
                    {
                        torta.cambiarArea(areaTortaEntregar);
                        torta.ListaParaEntregar = true;
                        tortaAEntregar = torta;
                    }
                }
                if (DetectorDeColisiones.DetectarColision(areaBasura, torta.Area))
                {
                    if (tortaAEntregar == torta)
                    {
                        tortaAEntregar = null;
                    }
                    tortaAEliminar = torta;
                    Torta nuevaTorta = new Torta(new Rectangle(0, 0, Constante.MEDIDA_TORTA_X, Constante.MEDIDA_TORTA_Y));
                    nuevaTorta.EstacionActual = EstacionActual.EstacionDeMezcla;
                    gestorDePedidos.AgregarTorta(nuevaTorta);
                }
            }
            if (tortaAEliminar != null)
            {
                gestorDePedidos.QuitarTorta(tortaAEliminar);
            }
            if (clienteActual != null)
            {
                base.gestorDeTickets.ActualizarTicketsEnZonaDeEntrega(areaTicket, posicionVirtual, clienteActual.pedido);
            }
            else
            {
                base.gestorDeTickets.ActualizarTickets(posicionVirtual);
            }
            if (botonEntregar.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
            {
                calculadorDePuntuacion.RecibirElemetos(tortaAEntregar, clienteActual.pedido);
                base.gestorDePedidos.QuitarTorta(tortaAEntregar);
                clienteActual.CambiarDeEstado(EstadoCliente.Saliendo);
                tortaAEntregar = null;
            }
            botonEntregar.Actualizar(posicionVirtual);
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            if (tortasEstacion.Count > 0)
            {
                for (int i = 0; i < tortasEstacion.Count; i++)
                {
                    if (i < posicionesTortas.Length) 
                    {
                        tortasEstacion[i].Dibujar(spriteBatch, tortasEstacion[i].Area, r.TexturasBizcochueloConCobertura[(tortasEstacion[i].FormaBizcochuelo, tortasEstacion[i].Cobertura)]);
                    }
                }
            }
            if (clienteActual != null)
            {
                clienteActual.Dibujar(spriteBatch, new Vector2(800, -300), 10f);
            }
            spriteBatch.Draw(r.TexturaPosicionarTicket, areaTicket, Color.White);
            spriteBatch.Draw(r.TexturaBasura, areaBasura, Color.White);
            if (base.gestorDeTickets.ticketListoParaEntregar && tortaAEntregar!=null)
            {
                botonEntregar.Dibujar(spriteBatch);
            }
            DibujarTickets(spriteBatch);
        }

    }
}
