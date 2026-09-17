using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Logica;
using JulpajulparaisoPasteleria.Content.Modelos;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Personajes
{
    public class Cliente
    {
        private Vector2 posicion;
        private float escala;
        private Animacion animacion;
        private Rectangle destino;
        private int velocidad;
        private SkinCliente skin;
        private Pedido pedido;
        public Rectangle AreaCliente { get; private set; }
        public EstadoCliente estado { get; private set; }
        public Cliente(SkinCliente skin, Vector2 posicion, Rectangle destino, Pedido pedido)
        {
            this.skin = skin;
            animacion = new Animacion(skin.Caminando, 12);
            this.posicion = posicion;
            this.destino = destino;
            velocidad = 3;
            escala = 6.0f;
            this.pedido = pedido;
        }
        public void Actualizar(GameTime tiempo)
        {
                Vector2 distanciaVector = new Vector2(destino.X, destino.Y) - posicion;
                float distanciaTotal = distanciaVector.Length();
            if (distanciaTotal > 1)
            {
                Vector2 direccion = Vector2.Normalize(distanciaVector); // .normalize es una funcion que devuelve un vector unitario en la misma direccion que el vector original
                posicion += direccion * velocidad;
                estado = EstadoCliente.Caminando;
                animacion.Actualizar(tiempo);
                AreaCliente = new Rectangle((int)posicion.X, (int)posicion.Y, (skin.Caminando.Width / skin.ColumnasCaminando) * (int)escala, skin.Caminando.Height * (int)escala); 
            }
            else
            {
                estado = EstadoCliente.Esperando;
                animacion = new Animacion(skin.Esperando, 9);
                AreaCliente = new Rectangle((int)posicion.X, (int)posicion.Y, (skin.Esperando.Width/skin.ColumnasEsperando)*(int)escala, skin.Esperando.Height*(int)escala);
            }
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            animacion.Dibujar(spriteBatch, posicion, escala);
        }
    }
}
