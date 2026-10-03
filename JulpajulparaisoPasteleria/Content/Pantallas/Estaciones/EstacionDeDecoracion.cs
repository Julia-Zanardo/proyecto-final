using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Enumeradores;
using JulpajulparaisoPasteleria.Content.Gestores;
using JulpajulparaisoPasteleria.Content.Modelos;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Content.Utilidades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace JulpajulparaisoPasteleria.Content.Pantallas.Estaciones
{
    public class EstacionDeDecoracion : Estacion
    {
        private Rectangle areaTorta = new Rectangle(620, 500, Constante.MEDIDA_TORTA_X * 2, Constante.MEDIDA_TORTA_Y * 2);
        private BotonBase botonSiguiente;
        private Topping[] toppingsDisponibles;
        private int contadorToppingsPuestos = 0;
        private Topping toppingActivo;
        private Torta tortaActual;
        private SpriteFont fuente;
        private Rectangle areaDeBasura = new Rectangle(200,600,150,150);
        private bool rellenoSeleccionado = false;
        public EstacionDeDecoracion(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes r) : base(gestorDeTickets, gestorDePedidos, r)
        {
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionDecoracion");
            botonSiguiente = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonSiguiente"), new Rectangle(1500, 600, 300, 100));
            r.LoadContent(content);
            toppingsDisponibles = new Topping[]
            {
                new Topping(TipoTopping.Banana, r.IconosToppings[TipoTopping.Banana], new Rectangle(480, 470, 100, 50)),
                new Topping(TipoTopping.Cereza, r.IconosToppings[TipoTopping.Cereza], new Rectangle(480, 590, 100, 50)),
                new Topping(TipoTopping.Oreo, r.IconosToppings[TipoTopping.Oreo], new Rectangle(480, 710, 100, 50)),
                new Topping(TipoTopping.Waffle, r.IconosToppings[TipoTopping.Waffle], new Rectangle(1300, 470, 100, 50)),
                new Topping(TipoTopping.Cubanito, r.IconosToppings[TipoTopping.Cubanito], new Rectangle(1300, 590, 100, 50))

            };
            fuente = content.Load<SpriteFont>("Fuentes/fuenteGruesa");
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeDecoracion);
            if (base.tortasEstacion.Count > 0)
            {
                tortaActual = tortasEstacion[0];
                tortaActual.cambiarArea(areaTorta);
                
                    foreach (Topping topping in toppingsDisponibles)
                    {
                        if (ManejoEntrada.ElementoClickeado() && topping.Area.Contains(posicionVirtual) && contadorToppingsPuestos < 3 && toppingActivo == null)
                        {
                            toppingActivo = new Topping(topping.tipoTopping, topping.textura, new Rectangle(topping.Area.X, topping.Area.Y, 100, 100));
                        }
                    }
                    if (toppingActivo != null)
                    {
                        toppingActivo.Actualizar(posicionVirtual);
                        if (DetectorDeColisiones.DetectarColision(toppingActivo.Area, tortasEstacion[0].PosicionesToppings[contadorToppingsPuestos]))
                        {
                            toppingActivo.Area = tortasEstacion[0].PosicionesToppings[contadorToppingsPuestos];
                            tortasEstacion[0].toppings.Add(toppingActivo);
                            contadorToppingsPuestos++;
                            toppingActivo = null;
                        }else if (DetectorDeColisiones.DetectarColision(toppingActivo.Area, areaDeBasura))
                        {
                            toppingActivo = null;
                        }
                    }
                foreach(BotonRelleno boton in r.IconosBotonesRelleno)
                {
                    if(boton.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado())&& !rellenoSeleccionado)
                    {
                        rellenoSeleccionado = true;
                        tortaActual.Relleno = boton.Sabor;
                    }
                }

            }
                foreach (BotonCobertura boton in r.IconosBotonesCobertura)
                {
                    boton.Actualizar(posicionVirtual);
                }
                foreach (BotonRelleno boton in r.IconosBotonesRelleno)
                {
                    boton.Actualizar(posicionVirtual);
                }
            
                botonSiguiente.Actualizar(posicionVirtual);
                base.gestorDeTickets.ActualizarTickets(posicionVirtual);
            
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            if (base.tortasEstacion.Count > 0)
            {
                string texto = "Estado de bizcochuelo: " + tortaActual.EstadoCoccion.ToString();
                Torta torta = base.tortasEstacion[0];
                Texture2D textura = r.TexturasBizcochueloSinRelleno[(torta.SaborBizcochuelo, torta.FormaBizcochuelo)];
                torta.Dibujar(spriteBatch, areaTorta, textura);
                spriteBatch.DrawString(fuente, texto, new Vector2(520, 880), Color.Black, 0f, Vector2.Zero, 1.8f, SpriteEffects.None, 1f);
                if(rellenoSeleccionado)
                {
                    tortaActual.Dibujar(spriteBatch, areaTorta, r.TexturasBizcochueloConRelleno[(tortaActual.SaborBizcochuelo, tortaActual.FormaBizcochuelo, tortaActual.Relleno)]);
                }
            }
            foreach (BotonCobertura boton in r.IconosBotonesCobertura)
            {
                boton.Dibujar(spriteBatch);
            }
            foreach (BotonRelleno boton in r.IconosBotonesRelleno)
            {
                boton.Dibujar(spriteBatch);
            }
            foreach (Topping topping in toppingsDisponibles)
            {
                topping.Dibujar(spriteBatch);
            }
            if (toppingActivo != null)
            {
                toppingActivo.Dibujar(spriteBatch);
            }
            spriteBatch.Draw(r.TexturaBasura, areaDeBasura, Color.White);
            botonSiguiente.Dibujar(spriteBatch);
            DibujarTickets(spriteBatch);
        }

    }
}
