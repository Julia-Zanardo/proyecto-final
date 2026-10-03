using JulpajulparaisoPasteleria.Content.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using JulpajulparaisoPasteleria.Content.Enumeradores;
namespace JulpajulparaisoPasteleria.Content.Objetos
{
    internal class Tazon : Idibujable
    {
        private Texture2D texturaActual;
        public bool estaLleno { get; private set; }
        private Rectangle area = new Rectangle(620, 580, 600, 400);
        private Dictionary<SaborBizcochuelo, Texture2D> texturasMasa;
        public SaborBizcochuelo Sabor { get; private set; }
        private Texture2D texturaVacia;
        public Tazon(Texture2D texturaVacia, Dictionary<SaborBizcochuelo, Texture2D> texturasMasa)
        {
            this.texturaActual = texturaVacia;
            this.texturaVacia = texturaVacia;
            this.texturasMasa = texturasMasa;
        }
        public void AgregarSabor(SaborBizcochuelo sabor)
        {
            if (!estaLleno)
            { 
            texturaActual = texturasMasa[sabor];
            Sabor = sabor;
            estaLleno = true;
            }
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaActual, area, Color.White);
        }
        public void Reiniciar()
        {
            this.texturaActual = this.texturaVacia;
            estaLleno = false;
        }
    }
}
