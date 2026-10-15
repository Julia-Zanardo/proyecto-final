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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Pantallas
{
    public class PantallaResumenDia : IPantalla
    {
        private Texture2D fondo;
        private BotonBase botonSiguienteDia;
        private GestorDePantalla gestorDePantalla;
        private RegistroDeDia registroDeDia;
        private SpriteFont fuente;
        public PantallaResumenDia(GestorDePantalla gestorDePantalla, RegistroDeDia registroDeDia)
        {
            this.gestorDePantalla = gestorDePantalla;
            this.registroDeDia = registroDeDia;
        }
        public void LoadContent(ContentManager content)
        {
            fondo = content.Load<Texture2D>("imagenes/Fondos/pantallaResumenDia");
            botonSiguienteDia = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonSiguienteDia"), new Rectangle(750,900,400,200));
            fuente = content.Load<SpriteFont>("fuentes/fuenteGruesa");
        }

        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
        {
            if (botonSiguienteDia.FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
            {
                GestorDeJuego.AvanzarDeDia();
                gestorDePantalla.CambiarPantalla(new PantallaJuego(gestorDePantalla));
                registroDeDia.ReiniciarAcumuladoresDia();
            }
            botonSiguienteDia.Actualizar(posVirtual);
        }

        public void Dibujar(SpriteBatch dibujo)
        {
            dibujo.Draw(fondo, new Rectangle(0, 0, Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL), Color.White);
            DibujarPuntuacion(dibujo);
            botonSiguienteDia.Dibujar(dibujo);
        }
        private void DibujarPuntuacion(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente, Convert.ToString(registroDeDia.PuntuacionAcumuladaOrdenes/GestorDeJuego.CantidadDeClientesAntendidosHoy), new Vector2(880, 490), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(registroDeDia.PuntuacionAcumuladaMezcla / GestorDeJuego.CantidadDeClientesAntendidosHoy), new Vector2(880, 640), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(registroDeDia.PuntuacionAcumuladaHorneado / GestorDeJuego.CantidadDeClientesAntendidosHoy), new Vector2(1200, 490), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(registroDeDia.PuntuacionAcumuladaDecoracion / GestorDeJuego.CantidadDeClientesAntendidosHoy), new Vector2(1200, 640), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(fuente, Convert.ToString(registroDeDia.PuntuacionAcumuladaTotal / GestorDeJuego.CantidadDeClientesAntendidosHoy), new Vector2(1050, 730), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

        }
    }
}