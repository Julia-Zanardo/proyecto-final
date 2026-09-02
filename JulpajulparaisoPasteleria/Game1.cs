using JulpajulparaisoPasteleria.Content;
using JulpajulparaisoPasteleria.Enumeradores;
using JulpajulparaisoPasteleria.Content.Estaciones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        public Game1()
        {
             _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            Window.ClientSizeChanged += OnWindowSizeChanged;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            dibujo = new SpriteBatch(GraphicsDevice);

            ActualizarDimensionesPestanias();
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
            if (ManejoEntrada.elementoClickeado())
            {
                int i = 0;
                bool encontrado = false;
                while (!encontrado && i < indicePestanias.Length)
                {
                    if(indicePestanias  [i].fueClickeada(posMouse.ToPoint()))
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

            GraphicsDevice.Clear(Color.MistyRose);

            dibujo.Begin();
            estacionActual.Draw(dibujo);
            int altoBarra = 500;
            dibujo.Draw(pestanias, new Rectangle(0, GraphicsDevice.Viewport.Height - altoBarra, GraphicsDevice.Viewport.Width, altoBarra), Color.White);

            dibujo.End();

            base.Draw(gameTime);
        }
        private void ActualizarDimensionesPestanias()
        {
            int altoBarra = 500;
            int yBarra = GraphicsDevice.Viewport.Height - altoBarra;
            int anchoPestania = GraphicsDevice.Viewport.Width / 5;

            for (int i = 0; i < indicePestanias.Length; i++)
            {
                Rectangle area = new Rectangle(i * anchoPestania, yBarra, anchoPestania, altoBarra);
                if (indicePestanias[i] == null)
                {
                    indicePestanias[i] = new Pestania(area, i);
                }
                else
                {
                  
                    indicePestanias[i].ActualizarArea(area);
                }

            }
        }

        private void OnWindowSizeChanged(object sender, System.EventArgs e)
        {
            ActualizarDimensionesPestanias();
        }
    }
}
