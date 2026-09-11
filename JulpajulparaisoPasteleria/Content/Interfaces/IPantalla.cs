using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Interfaces
{
    public interface IPantalla
    {
        void LoadContent(ContentManager content);
        void Actualizar(GameTime gameTime, Vector2 posVirtual);
        void Dibujar(SpriteBatch dibujo);
    }
}
