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
        private Texture2D texturaVacia;
        private Dictionary<SaborBizcochuelo, Texture2D> texturasMasa;
        private Texture2D texturaActual;
        private Rectangle area = new Rectangle(300, 300, 150, 150);
        public SaborBizcochuelo Sabor { get; private set; }
        public Tazon(Texture2D texturaVacia)
        {
            this.texturaVacia = texturaVacia;
            this.texturasMasa = new Dictionary<SaborBizcochuelo, Texture2D>();
            this.texturaActual = texturaVacia;
        }
        public bool estaLleno { get; private set; }
        public void agregarSabor(SaborBizcochuelo sabor)
        {
            texturaActual = texturasMasa[sabor];
            estaLleno = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaActual, area, Color.White);
        }
        public void LoadContent(ContentManager content)
        {
            texturaActual = content.Load<Texture2D>("Objetos/TazonVacio");
            texturasMasa.Add(SaborBizcochuelo.CarameloVainilla, content.Load<Texture2D>("Objetos/TazonVainilla"));

        }
    }
}
