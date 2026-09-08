using JulpajulparaisoPasteleria.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Modelos
{
    public class Cliente
    {
        private Vector2 posicion;
        private float escala;
        private Animacion animacion;
        private Vector2 destino;
        private int velocidad;
        private Texture2D textura;
        private bool estaCaminando;
        public Cliente(Texture2D textura, Vector2 posicion, Vector2 destino)
        {
            this.textura = textura;
            animacion = new Animacion(textura, 12);
            this.posicion = posicion;
            this.destino = destino;
            velocidad = 2;
            escala = 6.0f;
        }
        public void Actualizar(GameTime tiempo)
        {
                
                Vector2 distancia = destino - posicion;
                float distanciaTotal = distancia.Length();
            if (distanciaTotal > 1)
            {
                Vector2 direccion = Vector2.Normalize(distancia);
                posicion += direccion * velocidad;
                estaCaminando = true;
                animacion.Actualizar(tiempo);
            }
            else
            {
                estaCaminando = false;
            }
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            animacion.Dibujar(spriteBatch, posicion, escala);
        }
    }
}
