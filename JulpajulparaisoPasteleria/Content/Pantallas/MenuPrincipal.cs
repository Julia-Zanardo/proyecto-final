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
    public class MenuPrincipal : IPantalla
    {
        private Texture2D fondo;
        private BotonBase botonJugar;
        private GestorDePantalla gestorDePantalla;
        public MenuPrincipal(GestorDePantalla gestorDePantalla)
        {
            this.gestorDePantalla = gestorDePantalla;
        }

        public void LoadContent(ContentManager content)
        {
            fondo = content.Load<Texture2D>("imagenes/Fondos/menuPrincipal");
            botonJugar = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonJugar"), new Rectangle(800, 850, 400, 200));
            GestorDeAudio.LoadContent(content);
        }

        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
        {
            if (botonJugar.FueClickeado(posVirtual, ManejoEntrada.ElementoClickeado()))
            {
                gestorDePantalla.CambiarPantalla(new PantallaJuego(gestorDePantalla));
            }
            botonJugar.Actualizar(posVirtual);
        }

        public void Dibujar(SpriteBatch dibujo)
        {
            dibujo.Draw(fondo, new Rectangle(0, 0, Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL), Color.White);
            botonJugar.Dibujar(dibujo);
        }
    }
}
