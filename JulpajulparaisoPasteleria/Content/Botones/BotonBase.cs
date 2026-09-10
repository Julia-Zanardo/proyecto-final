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
    public abstract class BotonBase : Idibujable
    {
         public Texture2D textura { get; private set; }
         public Rectangle area { get; private set; }
        public BotonBase(Texture2D textura, Rectangle area)
        {
            this.textura = textura;
            this.area = area;
        }
        public bool FueClickeado(Vector2 posicionMouse, bool click)
        {
            if (click && area.Contains(posicionMouse))
            {
                return true;
            } 
            return false;
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, area, Color.White);
        }
    }
}
