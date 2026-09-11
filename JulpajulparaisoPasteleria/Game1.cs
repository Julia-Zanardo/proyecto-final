using JulpajulparaisoPasteleria.Content.Controladores;
using JulpajulparaisoPasteleria.Content.Estaciones;
using JulpajulparaisoPasteleria.Content.Personajes;
using JulpajulparaisoPasteleria.Content.Utilidades;
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
        private GestorDeJuego gestor;
        private AdaptadorDeResolucion adaptadorDeResolucion;
        private AdministradorDePestañas administradorDePestañas;
        public Game1()
        {
             _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void Initialize()
        {
            adaptadorDeResolucion = new AdaptadorDeResolucion(Constante.ANCHO_VIRTUAL, Constante.ALTO_VIRTUAL);
            Window.ClientSizeChanged += AlCambiarTamanoPantalla;
            adaptadorDeResolucion.Actualizar(Window.ClientBounds.Width, Window.ClientBounds.Height);
            gestor = new GestorDeJuego();
            administradorDePestañas = new AdministradorDePestañas();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            dibujo = new SpriteBatch(GraphicsDevice);
            administradorDePestañas.LoadContent(Content);

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
            estacionActual = estaciones[0];
        }

        protected override void Update(GameTime gameTime)
        {
            ManejoEntrada.Actualizar();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 posMouse = ManejoEntrada.PosicionMouse;
            Vector2 posVirtual = adaptadorDeResolucion.AjustarCoordenada(posMouse);
            estacionActual = administradorDePestañas.Actualizar(posVirtual, estaciones, estacionActual);
            estacionActual?.Update(gameTime, posVirtual);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            dibujo.Begin(transformMatrix: adaptadorDeResolucion.MatrizDeTransformacion);
            estacionActual.Draw(dibujo);
            administradorDePestañas.Dibujar(dibujo);
            dibujo.End();

            base.Draw(gameTime);
        }

        private void AlCambiarTamanoPantalla(object sender, EventArgs e)
        {
            adaptadorDeResolucion.Actualizar(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }
    }
}
