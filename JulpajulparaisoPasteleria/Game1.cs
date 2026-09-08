using JulpajulparaisoPasteleria.Content;
using JulpajulparaisoPasteleria.Content.Estaciones;
using JulpajulparaisoPasteleria.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JulpajulparaisoPasteleria
{
    public class Game1 : Game
    {
        private const int ANCHO_VIRTUAL = 1920;
        private const int ALTO_VIRTUAL = 1080;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch dibujo;
        private Estacion[] estaciones;
        private Estacion estacionActual;
        private Pestania[] indicePestanias = new Pestania[5];
        private Texture2D pestanias;
        private AdaptadorDeResolucion adaptadorDeResolucion;
        public Game1()
        {
             _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            adaptadorDeResolucion = new AdaptadorDeResolucion(ANCHO_VIRTUAL, ALTO_VIRTUAL);
            Window.ClientSizeChanged += AlCambiarTamanoPantalla;
            adaptadorDeResolucion.Actualizar(Window.ClientBounds.Width, Window.ClientBounds.Height);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            dibujo = new SpriteBatch(GraphicsDevice);
            int altoBarra = 100;
            int yBarra = ALTO_VIRTUAL - altoBarra;
            int anchoPestania = ANCHO_VIRTUAL / 5;

            for (int i = 0; i < indicePestanias.Length; i++)
            {
                Rectangle areaVirtual = new Rectangle(i * anchoPestania, yBarra, anchoPestania, altoBarra);
                indicePestanias[i] = new Pestania(areaVirtual, i);
            }

            estaciones = new Estacion[] {
                new EstacionDeOrdenes(),
                new EstacionDeMezcla(),
                new EstacionDeHorneado(),
                new EstacionDeDecoracion(),
                new EstacionDeEntrega()
            };
            foreach (Estacion e in estaciones)
            {
                e.LoadContent(Content);
            }
            pestanias = Content.Load<Texture2D>("Fondos/Pestanias");

            estacionActual = estaciones[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ManejoEntrada.actualizar();
            Vector2 posMouse = ManejoEntrada.PosicionMouse;
            Vector2 posVirtual = adaptadorDeResolucion.AjustarCoordenada(posMouse);

            if (ManejoEntrada.elementoClickeado())
            {
                int i = 0;
                bool encontrado = false;
                while (!encontrado && i < indicePestanias.Length)
                {
                    if(indicePestanias  [i].fueClickeada(posVirtual.ToPoint()))
                    {
                        estacionActual = estaciones[i];
                        encontrado = true;
                    }
                    i++;
                }
            }
            estacionActual?.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            dibujo.Begin(transformMatrix: adaptadorDeResolucion.MatrizDeTransformacion);
            estacionActual.Draw(dibujo);
            int altoBarra = 100;
            int yBarra = ALTO_VIRTUAL - altoBarra;
            dibujo.Draw(pestanias, new Rectangle(0, yBarra, ANCHO_VIRTUAL, altoBarra), Color.White);

            dibujo.End();

            base.Draw(gameTime);
        }

        private void AlCambiarTamanoPantalla(object sender, EventArgs e)
        {
            adaptadorDeResolucion.Actualizar(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }
    }
}
