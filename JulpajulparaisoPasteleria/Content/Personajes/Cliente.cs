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
        private Vector2 posicionSalida;
        private float escala;
        private Animacion animacion;
        private Rectangle destino;
        private float velocidad;
        private SkinCliente skin;
        public Pedido pedido { get; private set; }
        public bool TerminoDeSalir { get; private set; }
        public Rectangle AreaCliente { get; private set; }
        public EstadoCliente Estado { get; private set; }
        public Cliente(SkinCliente skin, Vector2 posicion, Rectangle destino, Pedido pedido)
        {
            this.skin = skin;
            animacion = new Animacion(skin.Caminando, skin.ConfiguracionSkin.ColumnasCaminando, -1);
            this.posicion = posicion;
            this.posicionSalida = posicion;
            this.destino = destino;
            velocidad = 300f;
            escala = 6.0f;
            this.pedido = pedido;
            this.Estado = EstadoCliente.Caminando;
        }
        public void Actualizar(GameTime tiempo)
        {
            float tiempoTranscurrido = (float)tiempo.ElapsedGameTime.TotalSeconds;
            Vector2 distanciaVector = new Vector2(destino.X, destino.Y) - posicion;
            float distanciaTotal = distanciaVector.Length();
            switch (Estado)
            {
                case EstadoCliente.Caminando:
                    if (distanciaTotal > 1)
                    {
                        Vector2 direccion = Vector2.Normalize(distanciaVector);
                        posicion += direccion * velocidad *tiempoTranscurrido;
                    }
                    else
                    {
                        CambiarDeEstado(EstadoCliente.Esperando);
                    }
                    AreaCliente = new Rectangle((int)posicion.X, (int)posicion.Y, (skin.Caminando.Width / skin.ConfiguracionSkin.ColumnasCaminando) * (int)escala, skin.Caminando.Height * (int)escala);
                    break;
                case EstadoCliente.Esperando:
                    AreaCliente = new Rectangle((int)posicion.X, (int)posicion.Y, (skin.Esperando.Width / skin.ConfiguracionSkin.ColumnasEsperando) * (int)escala, skin.Esperando.Height * (int)escala);
                    break;
                case EstadoCliente.Saliendo:
                    Vector2 distanciaSalidaVector = posicionSalida - posicion;
                    float distanciaSalidaTotal = distanciaSalidaVector.Length();
                    if (distanciaSalidaTotal > 1)
                    {
                        Vector2 direccionSalida = Vector2.Normalize(distanciaSalidaVector);
                        posicion += direccionSalida * velocidad * tiempoTranscurrido;
                    }
                    else
                    {
                        posicion = posicionSalida;
                        TerminoDeSalir = true;

                    }
                    AreaCliente = new Rectangle((int)posicion.X, (int)posicion.Y, (skin.Caminando.Width / skin.ConfiguracionSkin.ColumnasSaliendo) * (int)escala, skin.Caminando.Height * (int)escala);
                    break;
            }
            animacion.Actualizar(tiempo);
        }
        public void Dibujar(SpriteBatch spriteBatch)
        {
            animacion.Dibujar(spriteBatch, posicion, escala);
        }
        public void CambiarDeEstado(EstadoCliente estado)
        {
            if (this.Estado == estado)
            { 
                return;
             }
            this.Estado = estado;
            animacion = ObtenerAnimacion(estado);
        }
        private Animacion ObtenerAnimacion(EstadoCliente estado)
        {
            switch (estado)
            {
                case EstadoCliente.Caminando:
                    return new Animacion(skin.Caminando,skin.ConfiguracionSkin.ColumnasCaminando, -1);
                case EstadoCliente.Esperando:
                    return new Animacion(skin.Esperando, skin.ConfiguracionSkin.ColumnasEsperando, -1);
                case EstadoCliente.Saliendo:
                    return new Animacion(skin.Caminando, skin.ConfiguracionSkin.ColumnasSaliendo, 1);
                default:
                    return null;
            }
        }
        public void CaminarHaciaPosicion(Rectangle posicionFila)
        {

            this.destino = posicionFila;
            if (this.Estado == EstadoCliente.Esperando)
            {
                CambiarDeEstado(EstadoCliente.Caminando);
            }
        }
    }
}
