using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JulpajulparaisoPasteleria.Content.Estaciones
{
    public class EstacionDeOrdenes : Estacion
    {
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("Fondos/EstacionOrdenes");
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Fondo != null)
            {
               
                Rectangle destino = new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height);
                spriteBatch.Draw(Fondo, destino, Color.White);
            }
        }

    }
}
