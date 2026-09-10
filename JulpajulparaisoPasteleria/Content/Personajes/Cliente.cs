using JulpajulparaisoPasteleria.Content;
using JulpajulparaisoPasteleria.Enumeradores;
using JulpajulparaisoPasteleria.Modelos;
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
        private Vector2 destino;
        private int velocidad;
        private SkinCliente skin;
        private Pedido pedido;
        public EstadoCliente estado { get; private set; }
        public Cliente(SkinCliente skin, Vector2 posicion, Vector2 destino, Pedido pedido)
        {
            this.skin = skin;
            animacion = new Animacion(skin.caminando, 12);
            this.posicion = posicion;
            this.destino = destino;
            velocidad = 2;
            escala = 6.0f;
            this.pedido = pedido;
        }
        public void Actualizar(GameTime tiempo)
        {
                
                Vector2 distancia = destino - posicion;
                float distanciaTotal = distancia.Length();
            if (distanciaTotal > 1)
            {
                Vector2 direccion = Vector2.Normalize(distancia);
                posicion += direccion * velocidad;
                estado = EstadoCliente.Caminando;
                animacion.Actualizar(tiempo);
            }
            else
            {
                estado = EstadoCliente.Esperando;
                animacion = new Animacion(skin.esperando, 9);
            }
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            animacion.Dibujar(spriteBatch, posicion, escala);
        }
    }
}
