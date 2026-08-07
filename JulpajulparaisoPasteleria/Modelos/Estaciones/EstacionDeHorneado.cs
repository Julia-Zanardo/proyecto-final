using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Modelos.Estaciones
{
    public class EstacionDeHorneado : Estacion
    {
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("Fondos/EstacionHorneado");
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destino = new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(Fondo, destino, Color.White);
        }

    }
}
