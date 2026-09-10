using JulpajulparaisoPasteleria.Content.Interfaces;
using JulpajulparaisoPasteleria.Enumeradores;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace JulpajulparaisoPasteleria.Content.Objetos
{
    internal class Tazon : Idibujable
    {
        private Texture2D texturaActual;
        public bool estaLleno { get; private set; }
        private Rectangle area = new Rectangle(620, 580, 600, 400);
        private Dictionary<SaborBizcochuelo, Texture2D> texturasMasa;
        public SaborBizcochuelo Sabor { get; private set; }
        public Tazon(Texture2D texturaVacia, Dictionary<SaborBizcochuelo, Texture2D> texturasMasa)
        {
            this.texturaActual = texturaVacia;
            this.texturasMasa = texturasMasa;
        }
        public void AgregarSabor(SaborBizcochuelo sabor)
        {
            texturaActual = texturasMasa[sabor];
            Sabor = sabor;
            estaLleno = true;
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaActual, area, Color.White);
        }
        public void Actualizar(GameTime tiempo)
        {
        }
    }
}
