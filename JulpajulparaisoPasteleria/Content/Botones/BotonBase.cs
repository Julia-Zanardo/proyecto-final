using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Botones
{
    public class BotonBase : Idibujable
    {
         public Texture2D Textura { get; private set; }
         public Rectangle Area { get; private set; }
         private Color colorBoton = Color.White;
        private static SoundEffect sonidoClicl;
        public BotonBase(Texture2D textura, Rectangle area)
        {
            this.Textura = textura;
            this.Area = area;
        }
        public bool FueClickeado(Vector2 posicionMouse, bool click)
        {
            if (click && Area.Contains(posicionMouse))
            {
                GestorDeAudio.ReproducirClick();
                return true;
            } 
            return false;
        }
        public void Actualizar(Vector2 posVirtual)
        {
            if (Area.Contains(posVirtual))
            {
                colorBoton = new Color(255, 143, 163);
            }
            else
            {
                colorBoton = Color.White;
            }
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Textura, Area , colorBoton);
        }
    }
}
