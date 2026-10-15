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
using static System.Net.Mime.MediaTypeNames;

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
        public CalculadorDePuntuacion calculadorDePuntuacion = new CalculadorDePuntuacion();
        private Torta tortaAEntregar = null;
        private Texture2D dialogoPuntaje;
        private bool areaDeTortaOcupada = false;
        private SpriteFont fuente;
        private RegistroDeDia registroDeDia;
        private int puntajeOrdenes;
        private int puntajeMezcla;
        private int puntajeHorneado;
        private int puntajeDecoracion;
        private int puntajeTotal;
        public EstacionDeEntrega(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes repositorioImagenes, GestorDeClientes gestorDeClientes, RegistroDeDia registroDeDia ) : base(gestorDeTickets, gestorDePedidos, repositorioImagenes)
        {
            this.gestorDeClientes = gestorDeClientes;
            posicionesTortas = new Rectangle[] {
                new Rectangle(100,420,Constante.MEDIDA_TORTA_X,Constante.MEDIDA_TORTA_Y/2),
                new Rectangle(400,420,Constante.MEDIDA_TORTA_X,Constante.MEDIDA_TORTA_Y/2) };
            this.registroDeDia = registroDeDia;
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionDeEntrega");
            botonEntregar = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonEntregar"), new Rectangle(1500, 750, 300, 100));
            fuente = content.Load<SpriteFont>("fuentes/fuenteGruesa");
            dialogoPuntaje = content.Load<Texture2D>("imagenes/EstacionDeEntrega/dialogoPuntaje");
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            Torta tortaAEliminar = null;
            if (gestorDeClientes.clientesActivosFilaEntrega.Count > 0)
            {
                clienteActual = gestorDeClientes.clientesActivosFilaEntrega[0];
            }
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeEntrega);
            int puestoEnEstante = 0;
            for (int i = 0; i < tortasEstacion.Count; i++)
            {
                Torta torta = tortasEstacion[i];
                if (torta.ListaParaEntregar)
                {
                    torta.cambiarArea(areaTortaEntregar);
                    tortaAEntregar = torta;
                } else
                {
                    if (!torta.EstaSiendoArrastrado && puestoEnEstante < posicionesTortas.Length)
                    {
                        torta.cambiarArea(posicionesTortas[puestoEnEstante]);
                    }
                    puestoEnEstante++;
                }
                torta.Actualizar(posicionVirtual);
                if (!torta.ListaParaEntregar && torta.EstaSiendoArrastrado)
                {
                    if (DetectorDeColisiones.DetectarColision(torta.Area, areaTortaEntregar) && !areaDeTortaOcupada)
                    {
                        torta.cambiarArea(areaTortaEntregar);
                        torta.ListaParaEntregar = true;
                        areaDeTortaOcupada = true;
                        tortaAEntregar = torta;
                        torta.ForzarSoltar();
                    }
                }
                if (DetectorDeColisiones.DetectarColision(areaBasura, torta.Area))
                {
                    if (tortaAEntregar == torta)
                    {
                        tortaAEntregar = null;
                        areaDeTortaOcupada = false;
                    }
                    tortaAEliminar = torta;
                    Torta nuevaTorta = new Torta(new Rectangle(0, 0, Constante.MEDIDA_TORTA_X, Constante.MEDIDA_TORTA_Y));
                    nuevaTorta.EstacionActual = EstacionActual.EstacionDeMezcla;
                    gestorDePedidos.AgregarTorta(nuevaTorta);
                }
                if (torta.EstaSiendoArrastrado && torta.ListaParaEntregar)
                {
                    if (DetectorDeColisiones.DetectarColision(posicionesTortas[0], torta.Area))
                    {
                        tortaAEntregar = null;
                        areaDeTortaOcupada = false;
                        torta.ListaParaEntregar = false;
                        AcomodarTortas(torta);
                    }
                }
            }
            if (tortaAEliminar != null)
            {
                gestorDePedidos.QuitarTorta(tortaAEliminar);
            }
            if (clienteActual != null)
            {
                base.gestorDeTickets.ActualizarTicketsEnZonaDeEntrega(areaTicket, posicionVirtual, clienteActual.pedido);
                if(clienteActual.SeFueEnojado)
                {
                    gestorDeTickets.EliminarTicket(gestorDeTickets.ObtenerTikcetSegunPedido(clienteActual.pedido));
                }
            }
            else
            {
                base.gestorDeTickets.ActualizarTickets(posicionVirtual);
            }
            if (botonEntregar.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
            {
                RegistrarEntrega();
                base.gestorDePedidos.QuitarTorta(tortaAEntregar);
                clienteActual.CambiarDeEstado(EstadoCliente.Hablando);
                areaDeTortaOcupada = false;
                gestorDeTickets.EliminarTicket(gestorDeTickets.ObtenerTikcetSegunPedido(clienteActual.pedido));
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
                int tortasDibujadasEnEstante = 0;
                for (int i = 0; i < tortasEstacion.Count; i++)
                {
                    if (tortasDibujadasEnEstante < posicionesTortas.Length || tortasEstacion[i].ListaParaEntregar || tortasEstacion[i].EstaSiendoArrastrado)
                    {
                        if (!tortasEstacion[i].ListaParaEntregar && !tortasEstacion[i].EstaSiendoArrastrado)
                        {
                            tortasDibujadasEnEstante++;
                        }
                        tortasEstacion[i].Dibujar(spriteBatch, tortasEstacion[i].Area, r.TexturasBizcochueloConCobertura[(tortasEstacion[i].FormaBizcochuelo, tortasEstacion[i].Cobertura)]);
                    }
                }
            }
            if (clienteActual != null)
            {
                clienteActual.Dibujar(spriteBatch, new Vector2(800, -300), 10f);
                if (clienteActual.Estado == EstadoCliente.Hablando && tortaAEntregar != null)
                {
                    DibujarDialogoDePuntuacion(spriteBatch);
                }
                if (base.gestorDeTickets.ElTicketEstaEnZonaDeEntrega(clienteActual.pedido, areaTicket) && tortaAEntregar != null && clienteActual.Estado == EstadoCliente.Esperando)
                {
                    botonEntregar.Dibujar(spriteBatch);
                }
                if (clienteActual.SeFueEnojado)
                {
                    registroDeDia.RegistrarEntrega(0, 0, 0, 0,0);
                }
            }
            spriteBatch.Draw(r.TexturaPosicionarTicket, areaTicket, Color.White);
            spriteBatch.Draw(r.TexturaBasura, areaBasura, Color.White);
            DibujarTickets(spriteBatch);
        }
        private void AcomodarTortas(Torta torta)
        {
            if (tortasEstacion.Count >= 2)
            {
                Torta tortaActualEnPosicion0 = tortasEstacion[0];
                tortasEstacion[0] = torta;
                tortasEstacion[1] = tortaActualEnPosicion0;
            }
            else if (tortasEstacion.Count == 1)
            {
                tortasEstacion[0] = torta;
            }
            torta.ForzarSoltar();
        }
        private void DibujarDialogoDePuntuacion(SpriteBatch spriteBatch)
        {
            calculadorDePuntuacion.RecibirElemetos(tortaAEntregar, clienteActual.pedido);
            Rectangle areaDialogo = new Rectangle(1350, 100, 300, 300);
            spriteBatch.Draw(dialogoPuntaje, areaDialogo, Color.White);

            spriteBatch.DrawString(fuente, Convert.ToString(puntajeOrdenes), new Vector2(areaDialogo.X+150, areaDialogo.Y+20), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(puntajeMezcla), new Vector2(areaDialogo.X + 150, areaDialogo.Y + 75), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(puntajeHorneado), new Vector2(areaDialogo.X + 150, areaDialogo.Y + 130), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(puntajeDecoracion), new Vector2(areaDialogo.X + 150, areaDialogo.Y + 190), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(puntajeTotal), new Vector2(areaDialogo.X + 150, areaDialogo.Y + 250), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
        private void RegistrarEntrega()
        {
            calculadorDePuntuacion.RecibirElemetos(tortaAEntregar, clienteActual.pedido);

            puntajeOrdenes = calculadorDePuntuacion.CalcularPuntuacionEstacionDeOrdenes(clienteActual);
            puntajeMezcla = calculadorDePuntuacion.CalcularPuntuacionEstacionDeMezcla();
            puntajeHorneado = calculadorDePuntuacion.CalcularPuntuacionEstacionDeHorneado();
            puntajeDecoracion = calculadorDePuntuacion.CalcularPuntucacionEstacionDeDecoracion();
            puntajeTotal = calculadorDePuntuacion.CalcularPuntuacionTotal();

            registroDeDia.RegistrarEntrega(puntajeOrdenes, puntajeMezcla, puntajeHorneado, puntajeDecoracion, puntajeTotal);
        }
    }
}
