using JulpajulparaisoPasteleria.Content;
using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Estaciones;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace JulpajulparaisoPasteleria
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch dibujo;
        private Estacion[] estaciones;
        private Estacion estacionActual;
        private Pestania[] indicePestanias = new Pestania[5];
        private Texture2D pestanias;
        private GestorDeJuego gestor;
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
            adaptadorDeResolucion = new AdaptadorDeResolucion(Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL);
            Window.ClientSizeChanged += AlCambiarTamanoPantalla;
            adaptadorDeResolucion.Actualizar(Window.ClientBounds.Width, Window.ClientBounds.Height);
            gestor = new GestorDeJuego();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            dibujo = new SpriteBatch(GraphicsDevice);
            int yBarra = Constante.ALTO_VIRTUAL - Constante.ALTO_BARRA;
            int anchoPestania = Constante.ANCHO_VIRTUAL / 5;

            for (int i = 0; i < indicePestanias.Length; i++)
            {
                Rectangle areaVirtual = new Rectangle(i * anchoPestania, yBarra, anchoPestania, Constante.ALTO_BARRA);
                indicePestanias[i] = new Pestania(areaVirtual, i);
            }

            estaciones = new Estacion[] {
                new EstacionDeOrdenes(gestor),
                new EstacionDeMezcla(),
                new EstacionDeHorneado(),
                new EstacionDeDecoracion(),
                new EstacionDeEntrega()
            };
            foreach (Estacion e in estaciones)
            {
                e.LoadContent(Content);
            }
            pestanias = Content.Load<Texture2D>("imagenes/Fondos/Pestañas");
            Vector2 inicio = new Vector2(1920, 200);
            Vector2 objetivo = new Vector2(100, 200);

            estacionActual = estaciones[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ManejoEntrada.Actualizar();
            Vector2 posMouse = ManejoEntrada.PosicionMouse;
            Vector2 posVirtual = adaptadorDeResolucion.AjustarCoordenada(posMouse);

            if (ManejoEntrada.ElementoClickeado())
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
            estacionActual?.Update(gameTime, posVirtual);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            dibujo.Begin(transformMatrix: adaptadorDeResolucion.MatrizDeTransformacion);
            estacionActual.Draw(dibujo);
            int yBarra = Constante.ALTO_VIRTUAL - Constante.ALTO_BARRA;
            dibujo.Draw(pestanias, new Rectangle(0, yBarra, Constante.ANCHO_VIRTUAL, Constante.ALTO_BARRA), Color.White);
            dibujo.End();

            base.Draw(gameTime);
        }

        private void AlCambiarTamanoPantalla(object sender, EventArgs e)
        {
            adaptadorDeResolucion.Actualizar(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }
    }
}
