using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Interfaces;
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
    public class PantallaPausa : IPantalla
    {
        private BotonBase botonContinuar;
        private BotonBase botonVolver;
        private Texture2D fondo;
        private GestorDePantalla gestorDePantalla;
        private PantallaJuego pantallaJuego;
        public PantallaPausa(GestorDePantalla gestorDePantalla, PantallaJuego pantalla)
        {
            this.gestorDePantalla = gestorDePantalla;
            this.pantallaJuego = pantalla;
            GestorDeAudio.PausarMusica();
        }
        public void LoadContent(ContentManager content)
        {
            fondo = content.Load<Texture2D>("imagenes/fondos/pantallaPausa");
            botonContinuar = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonContinuar"), new Rectangle(800, 600, 400, 200));
            botonVolver = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonVolver"), new Rectangle(800, 850, 400, 200));
        }
        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
           
        {
            if (botonContinuar.FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
            {
                GestorDeAudio.ReanudarMusica();
                gestorDePantalla.CambiarPantalla(pantallaJuego);
            }
            if (botonVolver.FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
            {
                gestorDePantalla.CambiarPantalla(new MenuPrincipal(gestorDePantalla));
            }
            botonContinuar.Actualizar(posVirtual);
            botonVolver.Actualizar(posVirtual);
        }
        public void Dibujar(SpriteBatch dibujo)
        {
            dibujo.Draw(fondo, new Rectangle(0, 0, Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL),Color.White );
            botonContinuar.Dibujar(dibujo);
            botonVolver.Dibujar(dibujo);
        }
    }
}
