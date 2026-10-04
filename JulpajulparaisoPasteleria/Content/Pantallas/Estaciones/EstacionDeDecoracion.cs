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
        private bool rellenoSeleccionado = false;
        private bool coberturaSeleccionada = false;
        private BotonBase botonVolverAtras;
        public EstacionDeDecoracion(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes r) : base(gestorDeTickets, gestorDePedidos, r)
        {
        }
        public override void LoadContent(ContentManager content)
        {
            Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionDecoracion");
            botonSiguiente = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonSiguiente"), new Rectangle(1500, 750, 300, 100));
            toppingsDisponibles = new Topping[]
            {
                new Topping(TipoTopping.Banana, r.IconosToppings[TipoTopping.Banana], new Rectangle(480, 470, 100, 50)),
                new Topping(TipoTopping.Cereza, r.IconosToppings[TipoTopping.Cereza], new Rectangle(480, 590, 100, 50)),
                new Topping(TipoTopping.Oreo, r.IconosToppings[TipoTopping.Oreo], new Rectangle(480, 710, 100, 50)),
                new Topping(TipoTopping.Waffle, r.IconosToppings[TipoTopping.Waffle], new Rectangle(1300, 470, 100, 50)),
                new Topping(TipoTopping.Cubanito, r.IconosToppings[TipoTopping.Cubanito], new Rectangle(1300, 590, 100, 50))

            };
            fuente = content.Load<SpriteFont>("Fuentes/fuenteGruesa");
            botonVolverAtras = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonTirar"), new Rectangle(200, 750, 100,100));
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeDecoracion);
            if (base.tortasEstacion.Count > 0)
            {
                tortaActual = tortasEstacion[0];
                tortaActual.cambiarArea(areaTorta);
                foreach(BotonRelleno boton in r.IconosBotonesRelleno)
                {
                    if(boton.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado())&& !rellenoSeleccionado)
                    {
                        rellenoSeleccionado = true;
                        tortaActual.Relleno = boton.Sabor;
                    }
                }
                if(!coberturaSeleccionada && rellenoSeleccionado)
                {
                    foreach (BotonCobertura boton in r.IconosBotonesCobertura)
                    {
                        if (boton.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                        {
                            coberturaSeleccionada = true;
                            tortaActual.Cobertura = boton.Cobertura;
                        }
                    }
                }
                if (coberturaSeleccionada && rellenoSeleccionado)
                {
                    foreach (Topping topping in toppingsDisponibles)
                    {
                        if (ManejoEntrada.ElementoClickeado() && topping.Area.Contains(posicionVirtual) && contadorToppingsPuestos < 3 && toppingActivo == null)
                        {
                            toppingActivo = new Topping(topping.tipoTopping, topping.textura, new Rectangle(topping.Area.X, topping.Area.Y, 100, 100));
                        }
                    }
                }
                if (toppingActivo != null && coberturaSeleccionada)
                {
                    toppingActivo.Actualizar(posicionVirtual);
                        if (DetectorDeColisiones.DetectarColision(toppingActivo.Area, tortasEstacion[0].PosicionesToppings[contadorToppingsPuestos]))
                        {
                            toppingActivo.Area = tortasEstacion[0].PosicionesToppings[contadorToppingsPuestos];
                            tortasEstacion[0].toppings.Add(toppingActivo);
                            contadorToppingsPuestos++;
                            toppingActivo = null;
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
            if (botonVolverAtras.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
            {
                if (rellenoSeleccionado && !coberturaSeleccionada)
                {
                    rellenoSeleccionado = false;
                }
                else if (coberturaSeleccionada && contadorToppingsPuestos ==0)
                {
                    coberturaSeleccionada = false;
                }
                else if (coberturaSeleccionada && contadorToppingsPuestos > 0)
                {
                    tortaActual.toppings.RemoveAt(tortaActual.toppings.Count - 1);
                    contadorToppingsPuestos--;
                }
            }
            if(botonSiguiente.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
            {
                tortaActual.EstacionActual = EstacionActual.EstacionDeEntrega;
                tortasEstacion.Remove(tortaActual);
                rellenoSeleccionado = false;
                coberturaSeleccionada = false;
                contadorToppingsPuestos = 0;
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
                Texture2D textura = r.TexturasBizcochueloSinRelleno[(tortaActual.SaborBizcochuelo, tortaActual.FormaBizcochuelo)];
                tortaActual.Dibujar(spriteBatch, areaTorta, textura);
                spriteBatch.DrawString(fuente, texto, new Vector2(520, 880), Color.Black, 0f, Vector2.Zero, 1.8f, SpriteEffects.None, 1f);
                if(rellenoSeleccionado && !coberturaSeleccionada)
                {
                    tortaActual.Dibujar(spriteBatch, areaTorta, r.TexturasBizcochueloConRelleno[(tortaActual.SaborBizcochuelo, tortaActual.FormaBizcochuelo, tortaActual.Relleno)]);
                }
                if (coberturaSeleccionada)
                {
                    tortaActual.Dibujar(spriteBatch, areaTorta, r.TexturasBizcochueloConCobertura[(tortaActual.FormaBizcochuelo, tortaActual.Cobertura)]);
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
            if (coberturaSeleccionada && rellenoSeleccionado)
            {
                botonSiguiente.Dibujar(spriteBatch);
            }
            if (coberturaSeleccionada || rellenoSeleccionado) 
            {
                botonVolverAtras.Dibujar(spriteBatch);
            }
            DibujarTickets(spriteBatch);
        }

    }
}
