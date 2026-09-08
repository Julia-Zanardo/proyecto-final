using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content
{
    public class Animacion
    {
        private Texture2D textura;
        private int cantidadFrames;
        private int anchoFrame;
        private int altoFrame;
        private float temporizador;
        private int cuadroActual;
        private float tiempoPorCuadro = 0.1f;
        public Animacion(Texture2D textura, int cantidadFrames)
        {
            this.textura = textura;
            this.cantidadFrames = cantidadFrames;
            anchoFrame = textura.Width / cantidadFrames;
            altoFrame = textura.Height;
            temporizador = 0;
            cuadroActual = 0;
        }
        public void Actualizar(GameTime tiempo)
        {
            temporizador += (float)tiempo.ElapsedGameTime.TotalSeconds;
            if (temporizador >= tiempoPorCuadro)
            {
                cuadroActual = (cuadroActual - 1);
                if(cuadroActual < 0)
                {
                    cuadroActual = cantidadFrames - 1;
                }
                temporizador = 0;
            }
        }
        public void Dibujar(SpriteBatch spriteBatch, Vector2 posicion, float escala)
        {

            Rectangle areaRecortada = new Rectangle(anchoFrame * cuadroActual, 0, anchoFrame, altoFrame);
            spriteBatch.Draw(textura, posicion, areaRecortada, Color.White, 0f, Vector2.Zero, escala, SpriteEffects.None, 1);
        }
    }
}
