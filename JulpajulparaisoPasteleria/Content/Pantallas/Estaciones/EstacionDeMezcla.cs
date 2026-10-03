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
        public EstacionDeMezcla(GestorDeTickets gestorDeTickets, GestorDePedidos gestorDePedidos, RepositorioImagenes r) : base(gestorDeTickets, gestorDePedidos, r)
        {
        }

        private BotonBase botonSiguiente;
        private Tazon tazon;
        private bool todoListo= false;
        private bool botonSaborSeleccionado = false;
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
            base.tortasEstacion = gestorDePedidos.getTortasEstacion(EstacionActual.EstacionDeMezcla);
            if (tortasEstacion.Count > 0 )
            {
                foreach (BotonSabor boton in r.IconosBotonesSabor)
                {
                    boton.Actualizar(posicionVirtual);
                    if (boton.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        tazon.AgregarSabor(boton.Sabor);
                        tortasEstacion[0].SaborBizcochuelo = boton.Sabor;
                        botonSaborSeleccionado = true;
                    }
                }
            }
            if (botonSaborSeleccionado == true) 
            {
                foreach (BotonForma boton in r.IconosBotonesFormas)
                {
                    if(boton.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        tortasEstacion[0].FormaBizcochuelo = boton.Forma;
                        todoListo = true;
                    }
                }
            }
            if (botonTirar.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
            {
                tazon.Reiniciar();
                botonSaborSeleccionado = false;
                todoListo = false;
            }
            if (todoListo) 
                {
                    if(botonSiguiente.FueClickeado(posicionVirtual, ManejoEntrada.ElementoClickeado()))
                    {
                        base.tortasEstacion[0].EstacionActual = EstacionActual.EstacionDeHorneado;
                        base.tortasEstacion.RemoveAt(0);
                        Reiniciar();
                }

            }
            if (botonSaborSeleccionado)
            {
                foreach (BotonForma boton in r.IconosBotonesFormas)
                {
                    boton.Actualizar(posicionVirtual);
                }
            }
            botonSiguiente.Actualizar(posicionVirtual);
            botonTirar.Actualizar(posicionVirtual);
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
            if (botonSaborSeleccionado)
            {
                foreach (BotonForma boton in r.IconosBotonesFormas)
                {
                    boton.Dibujar(spriteBatch);
                }
            }
            if (todoListo) 
            { 
               botonSiguiente.Dibujar(spriteBatch);
            }
            DibujarTickets(spriteBatch);
            botonTirar.Dibujar(spriteBatch);
        }
        public void Reiniciar()
        {
            tazon.Reiniciar();
            botonSaborSeleccionado = false;
            todoListo = false;
        }
    }
}
