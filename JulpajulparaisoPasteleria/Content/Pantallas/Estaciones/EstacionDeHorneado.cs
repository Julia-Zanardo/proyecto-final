using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Pantallas.Estaciones
{
    public class EstacionDeHorneado : Estacion
    {
        private float tiempoTranscurrido = 0f;
        private Rectangle areaTortaLista = new Rectangle(1500, 850, 100, 100);
        private Vector2[] posicionesTortas = new Vector2[3]
        {
            new Vector2(200, 250),
            new Vector2(600, 250),
            new Vector2(1000, 250),
        };
        public EstacionDeHorneado(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes r) : base(gestorDeTickets, gestorDePedidos, r)
        {
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionHorneado");
        }
        public override void Actualizar (GameTime gameTime, Vector2 posicionVirtual)
        {
            ActualizarTiempoHorneado(gameTime);

            for (int i = 0; i < tortasEstacion.Count; i++)
            {
                Torta torta = tortasEstacion[i];
                Rectangle area = new Rectangle((int)posicionesTortas[i].X, (int)posicionesTortas[i].Y, 300, 200);
                if (!torta.EstaSiendoArrastrado)
                {
                    torta.cambiarArea(area);
                }
                    torta.Actualizar(posicionVirtual);
                if (torta.EstaSiendoArrastrado)
                {
                    if(DetectorDeColisiones.DetectarColision(torta.Area, areaTortaLista))
                    {
                        torta.EstacionActual = EstacionActual.EstacionDeDecoracion;
                        base.tortasEstacion.RemoveAt(i);
                        i--;
                    }
                }
            }
            base.gestorDeTickets.ActualizarTickets(posicionVirtual);
        }
        public void ActualizarTiempoHorneado(GameTime gameTime)
        {
            tiempoTranscurrido = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeHorneado);
            if (tortasEstacion.Count > 0)
            {
                foreach (Torta torta in tortasEstacion)
                {
                    torta.tiempoDeHorneadoActual += tiempoTranscurrido;
                    if (torta.tiempoDeHorneadoActual >= torta.tiempoDeHorneadoNecesario)
                    {
                        torta.EstadoCoccion = EstadoCoccion.Perfecto;
                    }
                    if (torta.tiempoDeHorneadoActual >= torta.tiempoDeHorneadoQuemado)
                    {
                        torta.EstadoCoccion = EstadoCoccion.Quemado;
                    }
                }
            }
            
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
            ActualizarTiempoHorneado(gameTime);
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            if(tortasEstacion.Count > 0)
            {
                for (int i = 0; i < tortasEstacion.Count; i++)
                {
                    Torta torta = tortasEstacion[i];
                    Texture2D textura;

                    if (torta.EstadoCoccion != EstadoCoccion.Quemado)
                    {
                        textura = r.TexturasMoldeSaborCoccion[(torta.SaborBizcochuelo, torta.FormaBizcochuelo, torta.EstadoCoccion)];
                    }
                    else
                    {
                        textura = r.TexturasFormaQuemada[torta.FormaBizcochuelo];
                    }
                    torta.Dibujar(spriteBatch, torta.Area, textura);
                }
            }
            DibujarTickets(spriteBatch);
        }

    }
}
