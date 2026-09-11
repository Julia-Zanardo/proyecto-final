using JulpajulparaisoPasteleria.Content.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Controladores
{
    public class GestorDePantalla
    {
        private IPantalla pantallaActual;
        private ContentManager content;
        public GestorDePantalla(ContentManager content)
        {
            this.content = content;
        }
        public void CambiarPantalla(IPantalla nuevaPantalla)
        {
            pantallaActual = nuevaPantalla;
            pantallaActual.LoadContent(content);
        }
        public void LoadContent(ContentManager content)
        {
            pantallaActual.LoadContent(content);
        }
        public void Actualizar(GameTime gameTime, Vector2 posVirtual)
        {
            pantallaActual.Actualizar(gameTime, posVirtual);
            
        }

        public void Dibujar(SpriteBatch dibujo)
        {
            pantallaActual.Dibujar(dibujo);
        }
    }
}
