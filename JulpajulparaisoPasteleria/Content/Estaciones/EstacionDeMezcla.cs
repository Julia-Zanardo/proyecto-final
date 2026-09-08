using JulpajulparaisoPasteleria.Content;
using JulpajulparaisoPasteleria.Content.Botones;
using JulpajulparaisoPasteleria.Content.Objetos;
using JulpajulparaisoPasteleria.Enumeradores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Estaciones
{
    public class EstacionDeMezcla : Estacion
    {
        private BotonSiguiente botonSiguiente;
        private SaborBizcochuelo Sabor;
        private Texture2D texturaCarameloVainilla;
        private BotonSabor botonCarameloVainilla;
        private Texture2D texturaBotonSiguiente;
        private BotonSabor[] listaDeBotones = new BotonSabor[1];
        private Tazon tazon;
        private bool todoListo= false;
        public bool ListoParaHorno { get; private set; }
        public override void LoadContent(ContentManager content)
        {
            
            base.Fondo = content.Load<Texture2D>("imagenes/Fondos/EstacionMezcla");
            texturaCarameloVainilla = content.Load<Texture2D>("imagenes/Objetos/BotonCarameloVainilla");
           // Texture2D texturaTazon = content.Load<Texture2D>("Tazon/TazonVacio");
           // texturaBotonSiguiente = content.Load<Texture2D>("Botones/botonSiguiente");
            listaDeBotones[0] = new BotonSabor(texturaCarameloVainilla, new Rectangle(100, 100, 50, 50), SaborBizcochuelo.CarameloVainilla  );
           // tazon = new Tazon(texturaTazon);
            //tazon.LoadContent(content);
            botonSiguiente = new BotonSiguiente(texturaBotonSiguiente, new Rectangle(700, 500, 100, 50));
        }
        public override void Update(GameTime gameTime)
        {
            foreach (BotonSabor boton in listaDeBotones)
            {
                if (boton.FueClickeado(ManejoEntrada.PosicionMouse, ManejoEntrada.elementoClickeado()))
                {
                  // tazon.agregarSabor(boton.Sabor);
                    todoListo = true;
                }
                if (todoListo) 
                {
                    if(botonSiguiente.FueClickeado(ManejoEntrada.PosicionMouse, ManejoEntrada.elementoClickeado()))
                    {
                        ListoParaHorno = true;
                    }

                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destino = new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(Fondo, destino, Color.White);
            //tazon.Draw(spriteBatch);
            foreach(BotonSabor boton in listaDeBotones)
            {
                boton.Draw(spriteBatch);
            }
            if (todoListo) 
            { 
            botonSiguiente.Draw(spriteBatch);
            }

        }

    }
}
