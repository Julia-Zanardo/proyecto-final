using JulpajulparaisoPasteleria.Content.BaseDeDatos;
using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace JulpajulparaisoPasteleria.Content.Pantallas
{
    public class PantallaPartidasGuardadas : IPantalla
    {
        private GestorDePantalla gestorDePantalla;
        private List<DatosResumenPartida> datosResumenPartidas;
        private BotonContinuarPartida[] botonesContinuarPartida;
        private Texture2D fondo;
        private SpriteFont fuente;
        private int borde=150;
        public PantallaPartidasGuardadas(GestorDePantalla gestorDePantalla, List<DatosResumenPartida> datosResumenPartidas)
        {
            this.gestorDePantalla = gestorDePantalla;
            this.datosResumenPartidas = datosResumenPartidas;

        }
        public void LoadContent(ContentManager content)
        {
            fondo = content.Load<Texture2D>("imagenes/Fondos/pantallaPartidasGuardadas");
            fuente = content.Load<SpriteFont>("fuentes/fuenteGruesa");
            botonesContinuarPartida = new BotonContinuarPartida[datosResumenPartidas.Count];
            for (int i = 0; i < botonesContinuarPartida.Length; i++)
            {
                botonesContinuarPartida[i] = new BotonContinuarPartida(content.Load<Texture2D>("imagenes/Botones/botonContinuar"), new Rectangle(borde-20 +(450 * i), 750, 320, 160), datosResumenPartidas[i]);
            }
        }
        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
        {
            for (int i = 0; i < botonesContinuarPartida.Length; i++)
            {
                botonesContinuarPartida[i].Actualizar(posVirtual);

                if (botonesContinuarPartida[i].FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
                {
                    DatosResumenPartida partidaSeleccionada = datosResumenPartidas[i];
                    RepositorioPartida.CargarPartidaEnJuego(partidaSeleccionada.NombreJugador);

                    gestorDePantalla.CambiarPantalla(new PantallaJuego(gestorDePantalla));
                    break;
                }
            }
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fondo, new Rectangle(0, 0, Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL), Color.White);
            for (int i = 0; i < datosResumenPartidas.Count; i++)
            {
                DatosResumenPartida partida = datosResumenPartidas[i];
                string textoNombre = partida.NombreJugador;
                string textoDiaACtual = Convert.ToString(partida.DiaActual);

                spriteBatch.DrawString(fuente, textoNombre, new Vector2(borde+(450*i), 330), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(fuente, textoDiaACtual, new Vector2(borde+100+(450*i), 500), Color.Black, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
                botonesContinuarPartida[i].Dibujar(spriteBatch);
            }
        }
    }
}
