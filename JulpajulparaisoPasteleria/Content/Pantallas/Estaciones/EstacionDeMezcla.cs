using JulpajulparaisoPasteleria.Content.Botones;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JulpajulparaisoPasteleria.Content.Pantallas.Estaciones
{
    public class EstacionDeMezcla : Estacion
    {
        public EstacionDeMezcla(GestorDeTickets gestorDeTickets) : base(gestorDeTickets)
        {
        }
        private BotonBase botonSiguiente;
        private Tazon tazon;
        private bool todoListo= false;
        private bool saborElegido = false;
        private Dictionary<SaborBizcochuelo, Texture2D> texturasBowl;
        public bool ListoParaHorno { get; private set; }
        private RepositorioImagenes r=new RepositorioImagenes();
        private BotonBase botonTirar;
        public override void LoadContent(ContentManager content)
        {
            base.Fondo = content.Load<Texture2D>("imagenes/Fondos/estacionMezcla");
            botonSiguiente = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonSiguiente"), new Rectangle(1400, 800, 300, 100));
            botonTirar = new BotonBase(content.Load<Texture2D>("imagenes/Botones/botonTirar"), new Rectangle(200, 600, 100, 100));
            r.LoadContent(content);
            tazon = new Tazon(content.Load<Texture2D>("imagenes/Bowls/bowlVacio"), r.TexturasBowl);
        }
        public override void Actualizar(GameTime gameTime, Vector2 posicionVirtual)
        {
            foreach (BotonSabor boton in r.IconosBotonesSabor)
            {
                if (boton.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                {
                    tazon.AgregarSabor(boton.Sabor);
                    saborElegido = true;
                }
            }
            if (botonTirar.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
            {
                tazon.Tirar();
            }
            if (todoListo) 
                {
                    if(botonSiguiente.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        ListoParaHorno = true;
                    }

            }
            base.gestorDeTickets.ActualizarTickets(posicionVirtual);
        }
        public override void ActualizarEnSegundoPlano(GameTime gameTime)
        {
        }
        public override void Dibujar(SpriteBatch spriteBatch)
        {
            base.Dibujar(spriteBatch);
            tazon.Dibujar(spriteBatch);
            foreach (BotonSabor boton in r.IconosBotonesSabor)
            {
                boton.Dibujar(spriteBatch);
            }
            if (todoListo) 
            { 
            botonSiguiente.Dibujar(spriteBatch);
            }
            DibujarTickets(spriteBatch);
            botonTirar.Dibujar(spriteBatch);
        }

    }
}
