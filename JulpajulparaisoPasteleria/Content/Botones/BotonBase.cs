using JulpajulparaisoPasteleria.Content.Interfaces;
using Microsoft.Xna.Framework;
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
        public BotonBase(Texture2D textura, Rectangle area)
        {
            this.Textura = textura;
            this.Area = area;
        }
        public bool FueClickeado(Vector2 posicionMouse, bool click)
        {
            if (click && Area.Contains(posicionMouse))
            {
                return true;
            } 
            return false;
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Textura, Area , Color.White);
        }
    }
}
